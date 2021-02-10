using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Options;
using NASABot.Api.Nasa;
using NASABot.Configuration;
using NASABot.Model;
using NASABot.Services;

namespace NASABot.Bots
{
    public class NasaBot : ActivityHandler
    {
        private Donki _donkiApi;
        private IOptions<NasaApi> _config;
        private readonly IOptions<Luis> _luisConfig;
        private readonly IExternalApiAgregetionService _externalApiAgregetionService;
        private readonly BotState _userState;
        private readonly BotState _conversationState;

        public NasaBot(
            IOptions<NasaApi> config,
            IOptions<Luis> luisConfig, 
            ConversationState conversationState, 
            UserState userState,
            IExternalApiAgregetionService externalApiAgregetionService)
        {
            _donkiApi = new Donki();
            _config = config;
            _luisConfig = luisConfig;
            _conversationState = conversationState;
            _userState = userState;
            _externalApiAgregetionService = externalApiAgregetionService;
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "---- Welcome to the NASA Bot ----";
            
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText), cancellationToken);
                    await turnContext.SendActivityAsync("Please enter something to start", null, null, cancellationToken);
                }
            }
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            // Conversation Flow Accessors state setup
            var conversationFlowAccessors = _conversationState.CreateProperty<ConversationFlow>(nameof(ConversationFlow));
            ConversationFlow flow = await conversationFlowAccessors.GetAsync(turnContext, () => new ConversationFlow(), cancellationToken);

            // ObservationParams state setup
            var observationParamsAccessor = _userState.CreateProperty<ObservationParams>(nameof(ObservationParams));
            ObservationParams observationParams = await observationParamsAccessor.GetAsync(turnContext, () => new ObservationParams(), cancellationToken);

            // Start question Process
            await StartQuestioningProcess(flow, observationParams, turnContext, cancellationToken);

            // Save data changes
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        private async Task StartQuestioningProcess(ConversationFlow conversationFlow, ObservationParams observationParams, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string input = turnContext.Activity.Text?.Trim();
            string message = string.Empty;

            switch (conversationFlow.Question)
            {
                case Question.None:
                    // Move to the next question
                    conversationFlow.Question = Question.ObservationStartDateQuestion;

                    await turnContext.SendActivityAsync("Enter observation Start-Date", null, null, cancellationToken);

                    break;
                case Question.ObservationStartDateQuestion:

                    observationParams.StartDate = DateTime.Parse(input);

                    await turnContext.SendActivityAsync("And please observation End-Date", null, null, cancellationToken);
                    
                    // Move to the next question
                    conversationFlow.Question = Question.ObservationEndDateQuestion;

                    break;
                case Question.ObservationEndDateQuestion:
                    
                    observationParams.EndDate = DateTime.Parse(input);
                    
                    await turnContext.SendActivityAsync("Observation type", null, null, cancellationToken);
                    conversationFlow.Question = Question.ObservationTypeQuestion;

                    break;
                case Question.ObservationTypeQuestion:
                    
                    observationParams.ObservationType = input;

                    string observationDataText = await _externalApiAgregetionService.GetObservarionText(_config.Value.ApiKey, observationParams.StartDate, observationParams.EndDate, turnContext, cancellationToken);

                    await turnContext.SendActivityAsync(MessageFactory.Text(observationDataText, observationDataText), cancellationToken);

                    await turnContext.SendActivityAsync("Observation type", null, null, cancellationToken);

                    // Move to the first question
                    conversationFlow.Question = Question.ObservationTypeQuestion;
                    break;
            }
        }
    }
}
