using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Text;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YoutubeCh9
{
    public class DataFactory
    {
        public static IList<Video> Videos { get; private set; }  

        static DataFactory()
        {
            Videos = new ObservableCollection<Video> { };

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAYwoKk947ZiOVEx1eLxg8wMTSU3pVSxX8", //HttpClientInitializer = credential,  //
                ApplicationName = ""
            });

            //Console.WriteLine(this.GetType().ToString());
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = "UCsMica-v34Irf9KVTh6xx-g"; // Replace with your search term.
            searchListRequest.Type = "video";
            searchListRequest.MaxResults = 10;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.ExecuteAsync();

            var videoListRequest = youtubeService.Videos.List("statistics");
            
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                foreach (var search in searchListResponse.Result.Items)
                {

                    videoListRequest.Id = search.Id.VideoId;
                    var videoListResponse = videoListRequest.ExecuteAsync();
                    var vCount = (ulong)videoListResponse.Result.Items[0].Statistics.ViewCount;
                    //Console.WriteLine("{0} ({1})", search.Snippet.Title, search.Id.VideoId);
                    //playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId);
                    Videos.Add(new Video {  ID = videoListRequest.Id,
                                            Title = search.Snippet.Title,
                                            Description = search.Snippet.Description,
                                            ViewCount = vCount,
                                            Thumbnail = "https://img.youtube.com/vi/" + videoListRequest.Id + "/hqdefault.jpg"
                    });

                }
                nextPageToken = null; // NextPageToken;
            }
        }
        
    }
}
