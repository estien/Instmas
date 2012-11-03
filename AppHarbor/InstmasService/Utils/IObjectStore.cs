using System.Collections.Generic;
using Instmas.Data.Models;
using MongoDB.Driver;

namespace InstmasService.Utils
{
    public interface IObjectStore
    {
        int AddPicture(Picture picture);
        Picture GetPicture(string pictureId);
        List<Picture> GetPictures();
        IEnumerable<string> GetPictureIds();
        SafeModeResult ClearAll();
        void Remove(string id);
    }
}