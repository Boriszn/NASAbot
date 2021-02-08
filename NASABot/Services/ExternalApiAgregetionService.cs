using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Options;
using NASABot.Api.Nasa;
using NASABot.Configuration;
using NASABot.Model;

namespace NASABot.Services
{
    public class ExternalApiAgregetionService : IExternalApiAgregetionService
    {
        private Donki _donkiApi;
        private readonly IOptions<Luis> _luisConfig;

        public ExternalApiAgregetionService(IOptions<Luis> luisConfig)
        {
            _donkiApi = new Donki();
            _luisConfig = luisConfig;
        }

        public async Task<string> GetObservarionText(string apiKey, DateTime startDate, DateTime endDate, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var observationRecognizer = new ObservationRecognizer(_luisConfig);

            var luisObservationTypeResult = await observationRecognizer.RecognizeAsync<ObservationType>(turnContext, cancellationToken);

            string apiResult = "I don't get it. :( Could you please reformulate ?";

            switch (luisObservationTypeResult.TopIntent().intent)
            {
                case ObservationType.Intent.CoronalMassEjection:

                    apiResult = await _donkiApi.GetCoronalMassEjection(apiKey, startDate, endDate);

                    break;
                case ObservationType.Intent.GeomagneticStorm:

                    apiResult = await _donkiApi.GetGeomagneticStorm(apiKey, startDate, endDate);

                    break;
                case ObservationType.Intent.None:
                    break;
            }

            return apiResult;
        }
    }
}
