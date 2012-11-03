using System;
using System.Linq;
using Instmas.Data.Models;
using InstmasService.Models;
using RestSharp;

namespace InstmasService.Utils
{
    public class PicturePicker
    {
        private readonly RestClient _client;

        public PicturePicker()
        {
            _client = new RestClient("https://api.instagram.com");
        }

        public Picture PickPicture(InstagramResponse response)
        {
            if (response == null || !response.data.Any()) return null;

            double highestScore = 0;
            InstagramPicture winner = null;
            foreach (var instagramPicture in response.data)
            {
                try
                {
                    var userId = instagramPicture.user.id;
                    var request = new RestRequest("/v1/users/" + userId);
                    request.AddParameter("client_id", Settings.ClientId);
                    var userResponse = _client.Execute<InstagramUserResponse>(request);
                    var userData = userResponse.Data;
                    var score = GetScore(instagramPicture, userData.data);
                    if (!(score > highestScore)) continue;
                    highestScore = score;
                    winner = instagramPicture;
                }
                catch
                {
                }
            }

            return ToPicture(winner);
        }

        private Picture ToPicture(InstagramPicture winner)
        {
            if (winner == null) return null;

            return new Picture
                       {
                           Comments = winner.comments.data.Select(c => new Comment
                                                                           {
                                                                               FullName = c.from.full_name,
                                                                               UserName = c.from.username,
                                                                               ProfilePictureUrl =
                                                                                   c.from.profile_picture,
                                                                               Text = c.text
                                                                           }).ToList(),
                           UserName = winner.user.username,
                           FullName = winner.user.full_name,
                           PublishedDate = winner.created_time,
                           Large = new PictureVersion
                                       {
                                           Url = winner.images.standard_resolution.url,
                                           Height = winner.images.standard_resolution.height,
                                           Width = winner.images.standard_resolution.width
                                       },
                           Medium = new PictureVersion
                                        {
                                            Url = winner.images.low_resolution.url,
                                            Height = winner.images.low_resolution.height,
                                            Width = winner.images.low_resolution.width
                                        },
                           Small = new PictureVersion
                                       {
                                           Url = winner.images.thumbnail.url,
                                           Height = winner.images.thumbnail.height,
                                           Width = winner.images.thumbnail.width
                                       },
                           Likes = winner.likes.count,
                           ProfilePictureUrl = winner.user.profile_picture
                       };
        }

        private static double GetScore(InstagramPicture i, InstagramUser u)
        {
            var followedBy = u.counts.followed_by == 0 ? 1 : (double) u.counts.followed_by;
            return (double)i.likes.count / followedBy;
        }
    }
}