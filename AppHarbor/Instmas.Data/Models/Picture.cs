using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Instmas.Data.Models
{
    [DataContract]
    public class Picture
    {
        [DataMember]
        public DateTime ForDate { get; set; }
        [DataMember]
        public PictureVersion Small { get; set; }
        [DataMember]
        public PictureVersion Medium { get; set; }
        [DataMember]
        public PictureVersion Large { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string ProfilePictureUrl { get; set; }
        [DataMember]
        public DateTime PublishedDate { get; set; }
        [DataMember]
        public List<Comment> Comments { get; set; }
        [DataMember]
        public int Likes { get; set; }
        [DataMember]
        public bool IsNull { get; set; }
        [DataMember]
        public string Id { get; set; }
    }

    [DataContract]
    public class PictureVersion
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
    }

    [DataContract]
    public class Comment
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ProfilePictureUrl { get; set; }
        [DataMember]
        public string FullName { get; set; }
    }
}
