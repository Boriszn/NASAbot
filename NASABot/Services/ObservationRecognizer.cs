using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NASABot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NASABot.Services 
{
    public class ObservationRecognizer : IRecognizer
    {
        private readonly LuisRecognizer _recognizer;

        public ObservationRecognizer(IOptions<Luis> configuration)
        {
            var luisIsConfigured = !string.IsNullOrEmpty(configuration.Value.LuisAppId) 
                && !string.IsNullOrEmpty(configuration.Value.LuisAPIKey) 
                && !string.IsNullOrEmpty(configuration.Value.LuisAPIHostName);
            if (luisIsConfigured)
            {
                var luisApplication = new LuisApplication(
                    configuration.Value.LuisAppId,
                    configuration.Value.LuisAPIKey,
                    "https://" + configuration.Value.LuisAPIHostName);
                // Set the recognizer options depending on which endpoint version you want to use.
                // More details can be found in https://docs.microsoft.com/en-gb/azure/cognitive-services/luis/luis-migration-api-v3
                var recognizerOptions = new LuisRecognizerOptionsV3(luisApplication)
                {
                    PredictionOptions = new Microsoft.Bot.Builder.AI.LuisV3.LuisPredictionOptions
                    {
                        IncludeInstanceData = true,
                    }
                };

                _recognizer = new LuisRecognizer(recognizerOptions);
            }
        }

        // Returns true if luis is configured in the appsettings.json and initialized.
        public virtual bool IsConfigured => _recognizer != null;

        public virtual async Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken)
            => await _recognizer.RecognizeAsync(turnContext, cancellationToken);

        public virtual async Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken)
            where T : IRecognizerConvert, new()
            => await _recognizer.RecognizeAsync<T>(turnContext, cancellationToken);
    }
}
