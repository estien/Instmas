using System.Collections.Generic;
using Instmas.Data.Models;

namespace InstmasWin8App.PictureService
{
    public class Api
    {
        private const string ServiceUri = "http://instmas.apphb.com/api";

        public static string GetAllPictures = ServiceUri + "/picture/get";

    }
}
