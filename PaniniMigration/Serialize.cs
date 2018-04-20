using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniniMigration
{
    public static class Serialize
    {
        public static string ToJson(this List<StickerStats> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
