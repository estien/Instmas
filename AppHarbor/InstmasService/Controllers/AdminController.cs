using System;
using System.Collections.Generic;
using System.Web.Http;
using InstmasService.Models;
using InstmasService.Utils;
using RestSharp;
using Instmas.Data.Models;

namespace InstmasService.Controllers
{
    public class AdminController : ApiController
    {
        private readonly string _hashTag;
        private readonly RestClient _client;
        private readonly PicturePicker _picturePicker;
        private readonly ObjectStore _objectStore;

        public AdminController()
        {
            _client = new RestClient("https://api.instagram.com");
            _hashTag = Settings.HashTag;
            _picturePicker = new PicturePicker();
            _objectStore = new ObjectStore();
        }

        [HttpGet]
        public string ClearWinners()
        {
            var result = _objectStore.ClearAll();
            return result.ToString();
        }

        [HttpGet]
        public string Clear(string id)
        {
            _objectStore.Remove(id);
            return "Success!";
        }

        [HttpGet]
        public List<Picture> PickAWinner()
        {
            var winners = new List<Picture>();
                var startDate = Settings.StartDate;
            var daysSinceStartDate = (DateTime.Today - startDate).Days;
            var lower = _objectStore.GetPictures().Count;
            var upper = daysSinceStartDate > 24 ? 24 : daysSinceStartDate;
            for (int i = lower; i < upper; i++)
            {
                var winner = GetPictureForDay(i);
                winners.Add(winner);
                _objectStore.AddPicture(winner);
            }
            return winners;
        }

        [HttpGet]
        public List<Picture> PickAWinner2()
        {
            return PickAWinner();
        }

        private Picture GetPictureForDay(int id)
        {
            var request = new RestRequest(
                string.Format("/v1/tags/{0}/media/recent",
                string.Format(_hashTag, id + 1)));
            request.AddParameter("client_id", Settings.ClientId);
            var result = _client.Execute<InstagramResponse>(request);
            var pic = _picturePicker.PickPicture(result.Data, _objectStore.GetPictureIds()) ?? new Picture { IsNull = true };
            pic.ForDate = Settings.StartDate.AddDays(id);
            return pic;
        }
    }
}
