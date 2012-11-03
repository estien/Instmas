using System;
using System.Collections.Generic;
using System.Web.Http;
using Instmas.Data.Models;

namespace InstmasService.Controllers
{
    public class PictureController : ApiController
    {
        // GET api/picture
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/picture/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/picture
        public void Post([FromBody]string value)
        {
        }

        // PUT api/picture/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/picture/5
        public void Delete(int id)
        {
        }
    }
}
