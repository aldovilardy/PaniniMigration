using Newtonsoft.Json.Linq;
using PaniniMigration.DataAccess;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniniMigration
{
    class PaniniMigration
    {
        public List<Sticker> GetStickers()
        {
            //short stickerId = 379; //Falcao
            List<Sticker> stickersList = new List<Sticker>();
            for (int stickerId = 1; stickerId <= 465; stickerId++)
            {
                string statsJson = stickerStatsJson(stickerId);
                Console.WriteLine($"\n\n\n{statsJson}\n\n");

                Console.WriteLine($"Sticker Id: {stickerId}");

                var stickerStats = StickerStats.FromJson(statsJson);

                string largeImageUrl = stickerStats.FirstOrDefault().LargeImageUrl;
                Console.WriteLine($"LargeImageUrl: {largeImageUrl}");
                string name = getStickerName(stickerId);
                Console.WriteLine($"Name: {name}");

                //Crea Objeto Sticker para ser almacenado
                Sticker sticker = new Sticker()
                {
                    StickerId = stickerId,
                    LargeImageUrl = largeImageUrl,
                    Name = name
                };

                if (stickerStats.FirstOrDefault().Stats.Count > 0)
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>() { };
                    foreach (var item in stickerStats.FirstOrDefault().Stats.FirstOrDefault().Pairs)
                        pairs.Add(item.ElementAt(0), item.ElementAt(1));
                    foreach (var item in pairs)
                        Console.WriteLine($"{item.Key}: {item.Value}");

                    /** every single elements from panini
                    Console.WriteLine($"Position: {pairs["Position"]}");
                    Console.WriteLine($"Dob: {pairs["Dob"]}");
                    Console.WriteLine($"Club: {pairs["Club"]}");
                    Console.WriteLine($"Height: {pairs["Height"]}");
                    Console.WriteLine($"Weight: {pairs["Weight"]}");
                    Console.WriteLine($"Debut: {pairs["Debut"]}");
                    **/

                    sticker.Position = pairs["Position"];
                    //sticker.DoB = DateTime.ParseExact($"{pairs["Dob"].Replace('-','/')} 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", new CultureInfo("en-US", true));
                    sticker.DoB = DateTime.ParseExact($"{pairs["Dob"]}", "dd-MM-yyyy", new CultureInfo("es-Es"));
                    sticker.Club = (pairs.ContainsKey("Club")) ? pairs["Club"] : sticker.Club;
                    sticker.Heigth = pairs["Height"];
                    sticker.Weigth = pairs["Weight"];
                    sticker.Debut = int.Parse(pairs["Debut"]);
                }
                string country = String.Empty;
                //country = GetCountry(stickerId, country);

                var countrySwitch = new Dictionary<Func<int, bool>, Action>
                {
                    {s => s >= 1 && s<= 8 , () => country = String.Empty},
                    {s => s >= 9 && s<= 20 , () => country ="Rusia"},
                    {s => s >= 21 && s<= 32 , () => country ="Arabia Saudí"},
                    {s => s >= 33 && s<= 44 , () => country ="Egipto"},
                    {s => s >= 45 && s<= 56 , () => country ="Uruguay"},
                    {s => s >= 57 && s<= 68 , () => country ="Portugal"},
                    {s => s >= 69 && s<= 80 , () => country ="España"},
                    {s => s >= 81 && s<= 92 , () => country ="Marruecos"},
                    {s => s >= 93 && s<= 104 , () => country ="RI de Irán"},
                    {s => s >= 105 && s<= 116 , () => country ="Francia"},
                    {s => s >= 117 && s<= 128 , () => country ="Australia"},
                    {s => s >= 129 && s<= 140 , () => country ="Perú"},
                    {s => s >= 141 && s<= 152 , () => country ="Dinamarca"},
                    {s => s >= 153 && s<= 164 , () => country ="Argentina"},
                    {s => s >= 165 && s<= 176 , () => country ="Islandia"},
                    {s => s >= 177 && s<= 188 , () => country ="Croacia"},
                    {s => s >= 189 && s<= 200 , () => country ="Nigeria"},
                    {s => s >= 201 && s<= 212 , () => country ="Brasil"},
                    {s => s >= 213 && s<= 224 , () => country ="Suiza"},
                    {s => s >= 225 && s<= 236 , () => country ="Costa Rica"},
                    {s => s >= 237 && s<= 248 , () => country ="Serbia"},
                    {s => s >= 249 && s<= 260 , () => country ="Alemania"},
                    {s => s >= 261 && s<= 272 , () => country ="México"},
                    {s => s >= 273 && s<= 284 , () => country ="Suecia"},
                    {s => s >= 285 && s<= 296 , () => country ="República de Corea"},
                    {s => s >= 297 && s<= 308 , () => country ="Bélgica"},
                    {s => s >= 309 && s<= 320 , () => country ="Panamá"},
                    {s => s >= 321 && s<= 332 , () => country ="Túnez"},
                    {s => s >= 333 && s<= 344 , () => country ="Inglaterra"},
                    {s => s >= 345 && s<= 356 , () => country ="Polonia"},
                    {s => s >= 357 && s<= 368 , () => country ="Senegal"},
                    {s => s >= 369 && s<= 380 , () => country ="Colombia"},
                    {s => s >= 381 && s<= 392 , () => country ="Japón"},
                    {s => s >= 393 && s<= 465 , () => country = String.Empty}
                };
                countrySwitch.First(sw => sw.Key(stickerId)).Value();

                Console.WriteLine($"Country: {country}");
                sticker.Country = country;
                stickersList.Add(sticker);
            }

            return stickersList;
        }
        string stickerStatsJson(int stickerId)
        {
            string param = $"%7b%22sticker_id%22%3a{stickerId}%7d";
            var client = new RestClient("https://paninistickeralbum.fifa.com/api/sticker_stats.json");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", $"json=%257b%2522sticker_id%2522%253a{stickerId}%257d&locale=es", ParameterType.RequestBody);
            request.AddCookie("_fifastwc_session", "s4~SVR2d09hMzhEckQ5eVhSdnJWMzFMU3VxV2YxZUhzSGZDR1JZUUhLeWZkQWNpb1NSWFFtb29VcEhRVHg3ZHdyRlNDbTVZYzh4VmVpd1FEQy9XTy9LZnJqamVRS2lPbWhySERDVlJkdllydDBGWlg5WWtXYUdKOWZaZUZ1ZXVxdWVvbWpZWmg0MmlCWWp4MnNMSXJpTk5hY2pPV25La2ZoMWQ4eVpuUkk2YjBSdHdlbFF0L1pSa0lnVkNmckJCWTdoMm8zL0RhUHhLcW9mQVZySVh6WHliZ0hWdHlZWTRsZkNDQkE3NzdzRml3U2JKZW5jRlBGV3k1bUVNa3RoY3pjcmxGQ2lTS2ZSMkh2Z0Z1MGxMdDliZm45QXpLc2xDNEViQ1F5YUZDVlYxeGwzMmdMUXBoQXRwdER3SXh6Z0UxYmpxMVEvb0FENzRHR0V2ZXdoczNsVGdVR0xRaDZrNGxmOS80dVFiK0RiczVJTFl5aGlGa2dMN0JrT3RSUzYyVWxtV3hrUURJbkZoTUkvWS9BdFV0TDdJMU9uaHl6dWFYem9TT2EyajNBbHNiall1b3VvYXJHcFZINjdOaXQ3NlJ0VmhpVkNNdUtxaWxKR1YvR1B1OE1IVVQrbDM4b2o3QTRTSXBTUVhSWFRMWHA3QmxtZWM0akNzbWQrdkxzbEdOcDJ1VVhSVFFHVUVWMmkza2VuczJKQXd5NEZoYzQ3VmNEUGMvR3hOWUozSmNmcWZoVldpbW5EM1JkcW45bFd2R0dKRXdROGw2ZlRDUXFVUkEvWFMyU01GZEVrL0R2aW1DVWZoK2lmNVFDTUhXSUhJREl0ZjFVQ2RWSElYRFJyREZaK21TV0VzOExyaWhaNHpjSThkVnhrOHN3bHFBTUhlSm9QdFRKc1NFMkRhUnFRQjI4aVVydnlGRE5oWEw1cCs5YzdibmIwSmY2bGViTHF4LzlRTndQMW9BK1hWRVc3bncwNVFWcFU2WlFVUWlvaVFhSDZwTjRvenhSemk2QjlaclZaYXA1RVVtQ3JOdEtndGg4ZlhXN0kzM1RlRmlpWk0wVXFwK3dwWUQyM2RyOWRxczBGSWxSS0M3Z3Z6bGtEa0p5RWVVUFBGcyt3MS9FaXhzVzRFNGdsYmxjUExUNkhtd2xBV3p3Uy9LN1lCdGhybTZNQkkwT1JzT1ZhN3VpOUMwc0I0VEVXcTFtaEM2aWV0TTA4SG03L255L3RoZDVqeGpEVjl3MlZ1UlJGSk9HNk93ejY1RnBYME5WNHZQbzJqWlI3NXFiRjUvYlNLOC95R2VwUEN1STA4US82VWNROUlwRnJoWG82LzFKTEUya2h2K0krQnB4ZkVwTjdvMGRlcmtWTkVod3IzSjNLQXJsZHVEbHNKRGg1NitrMEY2NnRpWmVUbkt6cmpmcGN1eW9mWHlydFZnc1RvQkRCc3FlVEN5b2RRVDEyS2xaMHdQSktTRDFkL1JNRWpJSHdMMEc5R2ZzM2NmalZhU3hPdjhpVktiOXdhSUJRNnFLWTFHbTJKM1RxTTlReFNOcDdCQ3ZnMGVZdGdPRWE4dzJmNFQ3YTlDNFVBUnRzUit4V1ZHdUFpaWJpRng5Slo0dGFBWHFSWjl0RFVBV3NhVkt3c2NScFRqSFducUpVOUp2eFlQcWpRaWI5NHhxR3NoS1JKUHhOZjdMV1puaFc4di81Ni92UW5OTndzeDlqN1hWaEwzY1FjMnF0OHpURHBjQ0UwN1l1NlYwbzJzUzBGZXFvbXEwQmRmU0ZIbGpQbUFpMDM3dEF4NmdrL0NwYStuMFphTVpleFhlQ2xpNklCbmNPQUt0dVVKZGgxcDlHTkZBNHBuYTkyMzd6WENMYWZieTczU3VnenpBMnMvM0J4SzFHNXJHQjFybTY5R3BtL3d6bDlKWVVzTWlzMkRPTVYyLy9rWVFsYmM4bXJoS1ZLNUJzY0NYckhHeTUwZE1JeWFmQW1qb0VYZEdHUjdFNHM4OTEvMC82Z3ZneVhXSFVtNnl1MXcyTEVYV1dySEU5Q1lnNVZHM0NPTzYzWkkzUVViSUpoVC9aM1ZUSzkzY1QwQ0hWSDk1TCs3bjViekZ4ZU5ONnJsNjdtNStsQUNQQ2hnTDVwZzFYT2VwRVdjKzhndjY5byt4SnRFRkVra2k1dTZqbDIyeGpaY0d2Q3NWZjFHdlVPdXNrMk9NMmU0R0tIVW9yL3Nta1ZsSk9qRFNyUnJGU09lWGVCR1Myd0dDbVhxS1JDUjRlQnBBaXFvWEpLVXBZZ1FmenljMCtqTGVRaXV1c3JlSHZYSjVzdmZ2NUl6TnN4eGxaRjRyYXF3R1l2UEtZN0RDcEFOMnppcEFuY3I3ZktEeGF2NkRFQ3FBZm5FUjMrN1JNa1AyZFZzdXpVNlVhWllMOUw1OHUxTHkrVEJDM0R4aEo2OVJJY25hb0pGTC95UXVLa2FsanM4c1ByUWpJOThVemk5OUFMU0M3RlhBZ2NEcHY5SUZJQlZMRkQrODduc1NDWEVTMmNGSGczQ29uNnNqMFhLMEdBODFORTVaemxPbHJQME83T0xxM2ZkZDdYRjNqUzRwTE1YMUp6dz09LS1YN09xWm8vSmhMaE1MZi9LRGxxQm1nPT0%3D--ceb24436340d5ec765b509b2f2d0118fdac476d3");

            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        string getStickerName(int stickerId)
        {
            var client = new RestClient("https://paninistickeralbum.fifa.com/mobile/share/open_pack");
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddCookie("_fifastwc_session", "s2~eDh4K2hrblF2Q29iSllnV0pnM1Rhdmp4eU1DQk5KWG5ZU3pYbm4vQnZXQ2RiNkRWekVneTJUb1VhTzZnSFlCay9uOU1RTmV2UDNtTnd2MFZlUFFMYVpRdnpWanFMZktPYXNVMzdENTFQbWdXZklnbmhLdi90QjMwWFM2c29VQncvSTF6YzBPMWlyY1RMZThTWVF4TXhBRmxoWWFSWVkwdC8rY0ptRTFTRjlEN0liS2dZcVJEb0xDb2Y4VnEzMFlZT1RqbnBaSks2N1AzelJtbUJ1cUo5WHl6TmpUc215TVVxUDJIaWpvaDZGYz0tLUhMdmI5NlpJaVFpcFFoQzhEazB3Smc9PQ%3D%3D--ea0310b9f4e3901b3650bc0ce293630fa4e8e1ef");
            request.AddParameter("undefined", $"gained={stickerId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            JObject jsonObj = JObject.Parse(response.Content);
            string description = (string)jsonObj["facebook"]["description"];

            return description.Replace("¡Acabo de recibir este cromo ", string.Empty).Replace("! #PaniniStickerAlbum", string.Empty);
        }
    }
}
