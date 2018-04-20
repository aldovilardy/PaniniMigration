using Newtonsoft.Json;
using System.Collections.Generic;

namespace PaniniMigration
{
    public partial class StickerStats
    {
        [JsonProperty("unlocked", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Unlocked { get; set; }

        [JsonProperty("unlock_prize", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnlockPrize { get; set; }

        [JsonProperty("large_image_url", NullValueHandling = NullValueHandling.Ignore)]
        public string LargeImageUrl { get; set; }

        [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
        public List<Stat> Stats { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("has_new_challenge", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasNewChallenge { get; set; }

        [JsonProperty("has_just_completed_challenge", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasJustCompletedChallenge { get; set; }

        [JsonProperty("num_available_challenges", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumAvailableChallenges { get; set; }
    }
    public partial class StickerStats
    {
        public static List<StickerStats> FromJson(string json) => JsonConvert.DeserializeObject<List<StickerStats>>(json, Converter.Settings);
    }
}
