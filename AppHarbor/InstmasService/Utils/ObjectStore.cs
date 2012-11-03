using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using Instmas.Data.Models;

namespace InstmasService.Utils
{
    public class ObjectStore : IObjectStore
    {
        private const string PicturesKey = "pictures";
        private readonly MongoDatabase _database;

        public ObjectStore()
        {
            var connectionString = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var server = MongoServer.Create(connectionString);
            _database = server.GetDatabase("appharbor_2f0c74e0-d157-4d3c-9cfd-2a39c43c1803");

            var exists = _database.CollectionExists(PicturesKey);
            if (!exists)
            {
                _database.CreateCollection(PicturesKey);
            }
        }

        public int AddPicture(Picture picture)
        {
            var coll = _database.GetCollection<Picture>(PicturesKey);
            coll.Save(picture);
            return (int)coll.Count();
        }

        public Picture GetPicture(string pictureId)
        {
            var coll = _database.GetCollection<Picture>(PicturesKey);
            var query = new QueryDocument("Id", pictureId);
            var picture = coll.FindOne(query);

            return picture;
        }

        public List<Picture> GetPictures()
        {
            var coll = _database.GetCollection<Picture>(PicturesKey);
            var pictures = coll.FindAll();

            return pictures.ToList();
        }

        public IEnumerable<string> GetPictureIds()
        {
            return GetPictures().Select(p => p.Id);
        }

        public SafeModeResult ClearAll()
        {
            var coll = _database.GetCollection<Picture>(PicturesKey);
            var picture = coll.RemoveAll();
            return picture;
        }

        public void Remove(string id)
        {
            var coll = _database.GetCollection<Picture>(PicturesKey);
            var picture = new QueryDocument("Id", id);
            coll.Remove(picture);
        }
    }
}