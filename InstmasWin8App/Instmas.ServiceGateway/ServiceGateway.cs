using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instmas.Data.Models;

namespace Instmas.ServiceGateway
{
    public class ServiceGateway
    {
        private const string ServiceUri = "http://instmas.apphb.com/api";



        public List<Picture> GetAllPictures()
        {
            var uri = ServiceUri + "/picture/get";
            return null;
        }

    }
}
