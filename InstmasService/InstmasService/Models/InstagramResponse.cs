using System;
using System.Collections.Generic;

namespace InstmasService.Models
{
    public class InstagramResponse
    {
        public List<InstagramPicture> data { get; set; }
    }

    public class InstagramPicture
    {
        public InstagramComment comments { get; set; }
        public InstragramLikes likes { get; set; }
        public InstagramImages images { get; set; }
        public InsagramCaption caption { get; set; }
        public InstagramUser user { get; set; }
        public DateTime created_time { get; set; }
        public string id { get; set; }
    }

    public class InsagramCaption
    {
        public string text { get; set; }
    }

    public class InstagramImages
    {
        public InstagramImage low_resolution { get; set; }
        public InstagramImage thumbnail { get; set; }
        public InstagramImage standard_resolution { get; set; }
    }

    public class InstagramImage
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class InstragramLikes
    {
        public int count { get; set; }
        public List<InstagramUser> data { get; set; }
    }

    public class InstagramUserResponse
    {
        public InstagramUser data { get; set; }
    }

    public class InstagramUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string profile_picture { get; set; }
        public string full_name { get; set; }
        public InstagramCounts counts { get; set; }
    }

    public class InstagramCounts
    {
        public int followed_by { get; set; }
        public int follows { get; set; }
    }

    public class InstagramComment
    {
        public List<CommentData> data { get; set; }
    }

    public class CommentData
    {
        public DateTime created_time { get; set; }
        public string text { get; set; }
        public InstagramUser from { get; set; }
    }
}