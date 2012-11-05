using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Instmas.Data.Models;

namespace InstmasWin8App.Services
{
    public class PictureService
    {
        private static PictureService _current;
        private readonly HttpClient _httpClient;

        public static PictureService Current
        {
            get
            {
                if (_current != null) return _current;
                return (_current = new PictureService());
            }
        }

        private PictureService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Picture>> GetPicturesForHashtagAsync(string hashTag)
        {
            var result = await _httpClient.GetStringAsync(Api.GetAllPictures+"+?id="+hashTag);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Picture>>(result);
        }

        public async Task<IEnumerable<Picture>> GetPicturesAsync()
        {
            var result = await _httpClient.GetStringAsync(Api.GetAllPictures);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Picture>>(result);
        }
    }
}
