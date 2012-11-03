using System.Collections.Generic;
using System.Linq;
using Instmas.Data.Models;
using MongoDB.Driver;

namespace InstmasService.Utils
{
    public class ObjectStoreStub : IObjectStore
    {
        private readonly List<Picture> _pictures;

        public ObjectStoreStub()
        {
           _pictures = new List<Picture>();
        }

        public int AddPicture(Picture picture)
        {
            _pictures.Add(picture);
            return 0;
        }

        public Picture GetPicture(string pictureId)
        {
            return _pictures.Single(p => p.Id == pictureId);
        }

        public List<Picture> GetPictures()
        {
            return _pictures;
        }

        public IEnumerable<string> GetPictureIds()
        {
            return _pictures.Select(p => p.Id);
        }

        public SafeModeResult ClearAll()
        {
            return new SafeModeResult();
        }

        public void Remove(string id)
        {
        }
    }
}