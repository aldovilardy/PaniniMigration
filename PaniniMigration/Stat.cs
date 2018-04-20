using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniniMigration
{
    public partial class Stat
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("pairs")]
        public List<List<string>> Pairs { get; set; }
    }
}
