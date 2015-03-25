using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace samskip.rating_browser.Models
{
    public class RatingsAPICall
    {
        [JsonProperty(PropertyName = "data")]
        public RatingsCollection Data { get; set; }
    }

    public class RatingsCollection
    {
        [JsonProperty(PropertyName = "ratings")]
        public List<Rating> Ratings { get; set; }
    }

    public class Rating
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "fullname")]
        public string Fullname { get; set; }

        [JsonProperty(PropertyName = "jirakey")]
        public string Jirakey { get; set; }

        [JsonProperty(PropertyName = "stars")]
        public int Stars { get; set; }

        [JsonProperty(PropertyName = "updated")]
        public DateTime Updated { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
    }
}
