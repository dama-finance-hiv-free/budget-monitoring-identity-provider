using Newtonsoft.Json;

namespace IdentityProvider.Models
{
    public class LocationModel
    {
        public string City { get; set; }
        public string Region { get; set; }
        [JsonProperty(PropertyName = "region_code")]
        public string RegionCode { get; set; }
        public string Country { get; set; }
        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { get; set; }
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }
    }
}
