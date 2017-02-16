using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temboo.Library.Flickr.Photos;

namespace AmiloBot.temboo
{
    class Flickr
    {
        private static readonly string FLICKR_API_KEY = "89e8f9774762dd94bda5e7e11288576f";

        SearchPhotos searchPhotosChoreo;

        public Flickr() {
            searchPhotosChoreo = new SearchPhotos(TembooUtil.ACTIVE_SESSION);
            searchPhotosChoreo.setAPIKey(FLICKR_API_KEY);
        }

        public void searchPhotos(string query) {
            searchPhotosChoreo.setText(query);
            SearchPhotosResultSet result = searchPhotosChoreo.execute();
            foreach (var item in result.Response)
            {
                Console.WriteLine(item);

            }
        }


    }
}
