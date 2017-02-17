using AmiloBot.dota;
using AmiloBot.temboo;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiloBot
{
    class AmiloBot
    {
        DiscordClient _discord;
        CommandService _commandService;

        public AmiloBot()
        {
            //create client instance
            _discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            //register prefix
            _discord.UsingCommands(x =>
            {
                x.PrefixChar = 'a';
                x.AllowMentionPrefix = true;
            });

            //setup command service
            _commandService = _discord.GetService<CommandService>();
            RegisterCommands();

            _discord.ExecuteAndWait(async () =>
            {
                await _discord.Connect("MjgwNDU2MjgwMDY4NDU2NDQ4.C4cZ1A.HrGi1rIQYf7xFWdJhU4wIgzqZxc", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine("[" + e.Severity + "] " + e.Message);
        }

        private void RegisterCommands() {

            FlickrCommand flickr = new FlickrCommand(_commandService);

            DotaCommand dotaCommand = new DotaCommand(_commandService);

            _commandService.CreateCommand(".hi").Do(async (e) =>
            {
                Console.WriteLine("[USER_ID] " + e.User.Name + " " + e.User.Id);
                await e.Channel.SendMessage(e.User.Mention + " नमस्ते! केहि अमिलो कुरा गर्नु होला |");
            });
        }
    }
}
