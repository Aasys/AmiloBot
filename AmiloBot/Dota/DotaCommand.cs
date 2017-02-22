using AmiloBot.util;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiloBot.dota
{
    class DotaCommand
    {
        private readonly CommandService _commandService;
        private readonly Dota2Api _dota2Api;

        public DotaCommand(CommandService commandService) {
            _commandService = commandService;
            _dota2Api = new Dota2Api();
            RegisterDotaCommand();
        }

        private void RegisterDotaCommand()
        {
            _commandService.CreateCommand(".dota")
            .Description("Dota 2")
            .Parameter("arg1", ParameterType.Optional)
            .Parameter("arg2", ParameterType.Optional)
            .Do(async e =>
            {
                if (e.GetArg("arg1").Equals("last"))
                {
                    String queryUser = e.GetArg("arg2");
                    queryUser = StringUtil.IsEmpty(queryUser) ? Convert.ToString(e.User.Id) : StringUtil.GetNumericValue(queryUser);
                    String url = _dota2Api.GetLastMatch(queryUser);
                    Console.WriteLine(queryUser);
                    String message = (url != null) ? " here is the last dota match \n <" + url + ">" : " Sorry, couldn't find any matches.";
                    await e.Channel.SendMessage(e.User.Mention + message);
                }
                else if (e.GetArg("arg1").Equals("profile"))
                {
                    String queryUser = e.GetArg("arg2");
                    queryUser = StringUtil.IsEmpty(queryUser) ? Convert.ToString(e.User.Id) : StringUtil.GetNumericValue(queryUser);
                    String url = _dota2Api.GetDotaBuffProfile(queryUser);
                    Console.WriteLine(queryUser);
                    String message = (url != null) ? " here is the DotaBuff Profile \n <" + url + ">" : " Sorry, couldn't find profile.";
                    await e.Channel.SendMessage(e.User.Mention + message);
                }
                else if (e.GetArg("arg1").Equals("update"))
                {
                    await e.Channel.SendMessage(_dota2Api.GetLastUpdate());
                }
                else
                {
                    await e.Channel.SendMessage(e.User.Mention + " Usage:\n\ta.dota <last|profile> [@User]");
                }
            });
        }
    }
}
