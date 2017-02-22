using AmiloBot.util;
using AmiloBot.Util;
using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AmiloBot.dota
{
    class Dota2Api
    {
        private static readonly string STEAM_API_KEY="9F2B5DB1EFCB505BF9F6711AB681677A";
        private static readonly string MATCH_HISTORY_URL = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=API_KEY&account_id=ACCOUNT_ID";

        private static readonly string DOTA_BUFF_MATCH_URL = "https://www.dotabuff.com/matches/MATCH_ID";
        private static readonly string DOTA_BUFF_PROFILE_URL = "https://www.dotabuff.com/players/STEAM_ID";

        private static readonly string DOTA_BLOG_UPDATE = @"http://www.dota2.com/news/updates/";
        
private static readonly Dictionary<string, string> DISCORD_ID_2_STEAM_ID = new Dictionary<string, string>()
        {
            {"244946768981262340", "111328970"}, //AASYS
            {"247512073389473793", "127810965"}, //Nisl
            {"241981491712622593", "110788814"}, //Avash
            {"244964486983712778", "113188578"}, //Susar
            {"268241286979387392", "348681300"}, //Sazin
            {"215511121195433994", "114830926"},  //Nischaya
            {"247466709999550464", "381577558"}
        };

        public string GetLastMatch(string discordId) {
            string steamID;
            if (DISCORD_ID_2_STEAM_ID.TryGetValue(discordId, out steamID))
            {
                RestClient restClient = new RestClient(GenerateMatchHistoryUrl(steamID));
                RestRequest restRequest = new RestRequest(Method.GET);
                IRestResponse response = restClient.Execute(restRequest);
                int matchIdIndex = response.Content.IndexOf("match_id");
                if (matchIdIndex >= 0)
                {
                    int colonIndex = response.Content.IndexOf(":", matchIdIndex);
                    int commaIndex = response.Content.IndexOf(",", matchIdIndex);
                    string matchId = response.Content.Substring(colonIndex + 1, commaIndex - colonIndex - 1);
                    return DOTA_BUFF_MATCH_URL.Replace("MATCH_ID", matchId);
                }
            }
            return null;
        }

        public string GetDotaBuffProfile(string discordId)
        {
            string steamId;
            if (DISCORD_ID_2_STEAM_ID.TryGetValue(discordId, out steamId))
            {
                return DOTA_BUFF_PROFILE_URL.Replace("STEAM_ID", steamId);
            }
            return null;
        }

        public string GetLastUpdate()
        {           
            RestClient restClient = new RestClient(DOTA_BLOG_UPDATE);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse response = restClient.Execute(restRequest);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response.Content);
            
            HtmlNode updatesSection = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"mainLoop\"]");
            HtmlNode titleNode = updatesSection.SelectSingleNode("//*[@class=\"entry-title\"]/a");
            HtmlNode dateNode = updatesSection.SelectSingleNode("//*[@class=\"entry-meta\"]");
            HtmlNode metaNode = updatesSection.SelectSingleNode("//*[@class=\"entry-content\"]");

            String s = "";
            s += DiscordFormatter.Bold(titleNode.InnerText.Trim()) + " - " +
                DiscordFormatter.Url(titleNode.GetAttributeValue("href", null)) + "\n";
            s += dateNode.InnerText.Trim() + "\n";
            foreach (var node in metaNode.ChildNodes)
            {
                String childStr = node.InnerText;
                if (! StringUtil.IsEmpty(childStr.Trim())) {
                    s += childStr.Trim() + "\n";
                }
            }
            return "" + s + "";
        }

        private string GenerateMatchHistoryUrl(string accountId)
        {
            return MATCH_HISTORY_URL.Replace("API_KEY", STEAM_API_KEY)
                                    .Replace("ACCOUNT_ID", accountId);
        }

    }
}
