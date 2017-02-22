using Discord.Commands;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiloBot
{
    class NepaliCalendarCommand
    {

        private static readonly string NEPAL_DATE_URL = "https://nepalicalendar.net/";

        private readonly CommandService _commandService;

        public NepaliCalendarCommand(CommandService commandService)
        {
            _commandService = commandService;
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            _commandService.CreateCommand(".date")
            .Description("Get Nepali Date")
            .Parameter("arg1", Discord.Commands.ParameterType.Optional)
            .Parameter("arg2", Discord.Commands.ParameterType.Optional)
            .Do(async e =>
            {
                RestClient restClient = new RestClient(NEPAL_DATE_URL);
                RestRequest restRequest = new RestRequest(Method.GET);
                IRestResponse response = restClient.Execute(restRequest);
                int matchIdIndex = response.Content.IndexOf("<div class=\"copyright\">");
                if (matchIdIndex >= 0)
                {
                    int colonIndex = response.Content.IndexOf("<p>", matchIdIndex);
                    int commaIndex = response.Content.IndexOf("</p>", matchIdIndex);
                    string matchId = response.Content.Substring(colonIndex + 3, commaIndex - colonIndex - 3);
                    await e.Channel.SendMessage(matchId);
                    return;
                }
                await e.Channel.SendMessage(e.User.Mention + " Usage:\n\ta.dota last [@User]");
            });
        }
    }
}
