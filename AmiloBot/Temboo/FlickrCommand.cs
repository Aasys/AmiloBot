using AmiloBot.util;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temboo.Library.Flickr.Photos;

namespace AmiloBot.temboo
{
    class FlickrCommand
    {
        private static readonly string FLICKR_API_KEY = "89e8f9774762dd94bda5e7e11288576f";

        SearchPhotos _searchPhotosChoreo;

        private readonly CommandService _commandService;

        public FlickrCommand(CommandService commandService)
        {
            _commandService = commandService;
            _searchPhotosChoreo = new SearchPhotos(TembooUtil.ACTIVE_SESSION);
                _searchPhotosChoreo.setAPIKey(FLICKR_API_KEY);
            RegisterFlickerCommand();
        }

        private void RegisterFlickerCommand()
        {
            _commandService.CreateCommand(".flickr")
            .Alias(new string[] { "pic"})
            .Description("Get Pictures from Flicker")
            .Parameter("arg1", ParameterType.Optional)
            .Parameter("arg2", ParameterType.Optional)
            .Do(async e =>
            {
                if (!StringUtil.IsEmpty(e.GetArg("arg1")))
                {
                    String url = SearchPhotos(e.GetArg("arg1")).Replace("\\", "");

                    Console.WriteLine(url);
                    String message = (url != null) ? " Found this - \n\t[[img src=" + url + "]]" : " Sorry, couldn't find any pics.";
                    await e.Channel.SendMessage(e.User.Mention + message);
                }
                else
                {
                    await e.Channel.SendMessage(e.User.Mention + " Usage:\n\ta.pic search_term");
                }
            });
        }

        public string SearchPhotos(string query) {
            _searchPhotosChoreo.setText(query);
            SearchPhotosResultSet result = _searchPhotosChoreo.execute();
            int matchIdIndex = result.Response.IndexOf("url_m");
            if (matchIdIndex >= 0)
            {
                int colonIndex = result.Response.IndexOf(":", matchIdIndex);
                int commaIndex = result.Response.IndexOf(",", matchIdIndex);
                string imgUrl = result.Response.Substring(colonIndex + 2, commaIndex - colonIndex - 3);
                return imgUrl;
            }

            return null;
        }


    }
}
