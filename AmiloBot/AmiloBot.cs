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
        DiscordClient discord;
        CommandService commandService;
        Flickr flickr = new Flickr();
        Dota2Api dota2Api = new Dota2Api();

        public AmiloBot()
        {
            //create client instance
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            //register prefix
            discord.UsingCommands(x =>
            {
                x.PrefixChar = 'a';
                x.AllowMentionPrefix = true;
            });

            //setup command service
            commandService = discord.GetService<CommandService>();
            registerCommands();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjgwNDU2MjgwMDY4NDU2NDQ4.C4cZ1A.HrGi1rIQYf7xFWdJhU4wIgzqZxc", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine("[" + e.Severity + "] " + e.Message);
        }

        private void registerCommands() {
            command_hi();
            command_dota();
        }

        private void command_dota()
        {
            commandService.CreateCommand(".dota").Do(async (e) =>
            {
                String url = dota2Api.getLastMatch(Convert.ToString(e.User.Id));
                String message = (url != null) ? " here is your last dota match - " + url : " Sorry, you are not setup for this yet.";
                await e.Channel.SendMessage(e.User.Mention + message);
            });
        }

        private void command_hi() {
            commandService.CreateCommand(".hi").Do(async (e) =>
            {
                Console.WriteLine("[USER_ID] " + e.User.Name + " " + e.User.Id);
                await e.Channel.SendMessage(e.User.Mention + " नमस्ते! केहि अमिलो कुरा गर्नु होला |");
            });            
        }
    }
}
