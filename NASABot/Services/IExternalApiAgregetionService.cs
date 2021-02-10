using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NASABot.Services
{
    public interface IExternalApiAgregetionService
    {
        Task<string> GetObservarionText(string apiKey, DateTime startDate, DateTime endDate, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken);
    }
}