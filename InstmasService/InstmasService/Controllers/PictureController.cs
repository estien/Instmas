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
        private readonly ObjectStore _objectStore;

        public PictureController()
        {
            _objectStore = new ObjectStore();
        }

        // GET api/picture
        public IEnumerable<Picture> Get()
        {
            return _objectStore.GetPictures();
        }
    }
}
