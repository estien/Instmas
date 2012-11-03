using System;
using System.Collections.Generic;
using System.Web.Http;
using Instmas.Data.Models;
using InstmasService.Models;
using InstmasService.Utils;
using RestSharp;

namespace InstmasService.Controllers
{
    public class PictureController : ApiController
    {
        private readonly string _hashTag;
        private readonly RestClient _client;
        private readonly PicturePicker _picturePicker;

        public PictureController()
        {
            _client = new RestClient("https://api.instagram.com");
            _hashTag = Settings.HashTag;
            _picturePicker = new PicturePicker();
        }

        // GET api/picture
        public IEnumerable<Picture> Get()
        {
            var startDate = Settings.StartDate;
            var daysSinceStartDate = (DateTime.Today - startDate).Days;
            var upper = daysSinceStartDate > 24 ? 24 : daysSinceStartDate;
            var pics = new List<Picture>();
            for (int i = 0; i < upper; i++)
            {
                pics.Add(GetPictureForDay(i));
            }
            return pics;
        }

        private Picture GetPictureForDay(int i)
        {
            var request = new RestRequest(
                string.Format("/v1/tags/{0}/media/recent",
                string.Format(_hashTag, i+1)));
            request.AddParameter("client_id", Settings.ClientId);
            var result = _client.Execute<InstagramResponse>(request);
            var pic = _picturePicker.PickPicture(result.Data) ?? new Picture{IsNull = true};
            pic.ForDate = Settings.StartDate.AddDays(i);
            return pic;
        }
    }
}
