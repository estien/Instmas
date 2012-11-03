using System;
using System.Collections.Generic;

namespace Instmas.Data.Models
{
    public class Picture
    {
        public DateTime ForDate { get; set; }
        public PictureVersion Small { get; set; }
        public PictureVersion Medium { get; set; }
        public PictureVersion Large { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<Comment> Comments { get; set; }
        public int Likes { get; set; }
        public bool IsNull { get; set; }
    }

    public class PictureVersion
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Comment
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FullName { get; set; }
    }
}
