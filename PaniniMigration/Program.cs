using Newtonsoft.Json.Linq;
using PaniniMigration.DataAccess;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace PaniniMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Funciona NO BORRAR!!!
            ////short stickerId = 379; //Falcao
            //List<Sticker> stickersList = new List<Sticker>();
            //for (int stickerId = 1; stickerId <= 465; stickerId++)
            //{
            //    string param = $"%7b%22sticker_id%22%3a{stickerId}%7d";
            //    var client = new RestClient("https://paninistickeralbum.fifa.com/api/sticker_stats.json");
            //    var request = new RestRequest(Method.POST);
            //    #region unnecessaryHeaders
            //    //request.AddHeader("Postman-Token", "4c68e26c-e0b8-44ec-9f09-63d40feb47b3");
            //    //request.AddHeader("Cache-Control", "no-cache");
            //    //request.AddHeader("Connection", "keep-alive");
            //    //request.AddHeader("Cookie", "o_ic_persist=; o_ec_persist=; ai_user=X+9Hy|2018-04-04T17:21:01.877Z; AssetAction=; dslv_fifa_s=First%20Visit; s_fid=0A32678EDB2E6683-23FCF7ED80A48E6D; s_cc=true; s_vi=[CS]v1|2D6284C4051D3D0E-40000170A0007142[CE]; s_sq=%5B%5BB%5D%5D; fifastwc_access_token=Rshl8lFbB6RVAOXVqMrvYA; dslv_fifa=1522862506470; ai_session=CLPDN|1522862462983|1522862627068.3; _fifastwc_session=s5~YWVEc0xnc001d3hyY0Z2bU9DYlluSGlwcmduWTZNTTVDTlpNZjJPSkl4K1lQQkt2UHVHdnkrZVhvRUlCZDN0Q0grZ2lNVDNFNDgwYzBPU3krV1QveDFyTCtzajVHdXBsN3NtTFVWSDNWd2MwZElBREFnYlF0VzU4VkRmYTYwdURFejkyZkI2aE45WjllUWpJVFVLZitSK3dPVW5LVHZFdDhXREkzamlkTTRjZ0IwQk9UVkthZS9maTFWQVpVSGhOV3paSFJKMGo5dXlxTldxU2M2aTJhWjlPTi9rdi90K0gwQithNmpWQVpXRkdySEZ2T1lxM28xdHMyam13QUR0YTBHalB2UGhWKzZ3OTVsOU1hVC9FOXRyMG93QmFFeDZzdG1Bdmk2WWs3OFNqeUFEWXBFYkVxdEw4bUJDWU94cGVYa2tBcjRvQ3phK05HbXRDRjZ0VEN4N0R3bmU0YlNKYnlwcEUvTFVIVnVzaVhraFZoYVpGdEwxNDdUMlVsei9rRzViVU9aL1hBSkhqUjkvTzY3YW9rUnVtUkg0QkVlNHJZN2hGYThKQXI5SG5sNjcwdTl3M3RPdkFtdXBHc0ZYM0FHVGZobVNLUk1JRWFFOWQrMWQxWG5GaFhmOHRNV2h2VEhDckljMzVvVzdIK2N6MWFIYURHVEFFNUgzakpxa1JMMFlZN2RYTm95Zi9ZbVFmSmhOWlFLUHNyY0gvdUFURHpoU0x4UVR5ZndoSk9pdklOZTBrMm5iOGsyMFM2UitERjAyQ3lQK05Jd2tLUnJRRTBJVXlhRTN4YXFuOEtFV0xtcUxZZ3ovUjU4WTY2RWx1azVkNEEycmoxblQrZVppK3NEWUdNK3ZqVU9ZOURNQkRzc1dMRnRXd3F3NHE1eGJJdy8vNXU4ZmdXSzBTQm9lWWFwNGpjazU5NW95d1ZDL0ZGUVAzZkpHbU9FYTFJMnVlbTFaWjBaQWQ3b1Q2THNqUmdFQzJFbmt6d3l0bkp2bUFlc2NHeVlCZHNuS0kyb2kxUXBJMmliaHJQNUY4b3FubXhTemJwQWZwRWdnY2Z0K0V4TTJmdGdKMlpQelZoclc0ejQzVTZUS290SXIya0xIVTRoa242K05hMlZyVkJwajJGYTRZd2dYSjRhZFV3VHUwdTRXNmpnWVJzeDBpNGNhSWYybXVLOWVjU1ovUXJpbnRPQ2RONDJPTWgvaU5BbXEvWkVFSG85Q2RvYWg1cGplSjlKR1pKVUdEQVl5dTFuTzgxbE1zaXM3U1NhckhYNkJqdy94UDVwVnJjc3F2QnZmTzBTZFBnVnVLK3g4cnBWMDBaYnYvTW04WGJmTWc4dWdMbmlvN1FvNEhzL3h6aVgwUFpkV3laWWJQa0dFSjFDaUF5SklqUUwwTllDWUttR2cya3ZJM2pSckxnNW9GSUVXMkppVEJPY240SUtXS3JET3FaalhFcXdGL0tOUGc0UDRsVE9UZy9GWDdScmVmMmJYUXlQUXlGaWMxb2ZXQlo3cVVoYkxyNXgvL01ISFlWL2pMS2luZ3ozdXpTRnppVWx0ckNJb05NeG1CY2lPSVZ3S25KVlJQTldKdXBxQ3RzMUxVYlZJVW1EQXJkcnhMcnN2eEVKNngxU3NMM2lYMFdNVDNjeGV2aWhmWUdJY3B5ZG15VHB2bjM5em8zbUNTNFJUd2R3cHFDMmdiT1RJSk10TmlMZHBKSjY3Nm1McmZ1SS9UMGtQTm9RR2R0aVFudjZvazQxRTNBdEhlUmhKRWJjbFNqMTBSYlhZdVVuL0prTzZUZHVyUEVOM0dKdk9XT3hDb2FPK2N1eDFudERCRG05RWx2RGRkYXllQmcwMnBkMnEzdFNHMjVzdlczUHB4eHBXazdDMFZ6RmRmOXZMYVdWaWc3ekxHckp6TUlPdWdOcy9KdXRNOVIreWhpWDJ0SjAvT2QvR3NMa3lxUlUvck5zSWNSd2ZTSjgvbm9icitRY3BJMUFiM3ZvUkQzMW5HdGtPWTl0eldMV3UzZXIzS0xPY2JWZkFBRTZ4cGJ1VHhocVdMZGZ4TVFQbnJYNWhTWWxlQ2R6eG1IVzlEdGRTN3pZQlN4VDBIZll4Z1F1Y0tWM21WYWlNOWF1dGlIaXNjRFFhVDhJL01PbjB2RFNIaW5HOEV3UjJMR015c1l3NXBYYncwclVuRlVRV2Q2Z3BFOFlsVzU3dVptZXBKTHBqTmRCOUs3U3UvTk1BeVViUGJad1FIS1ArS0IzRXJKNGRYZVhYUWZQZ2ZEbWk4blJBM3FMYkdoRGE3eUR5S3pxTmxWei91bTU1THc3S2FObVczRExlVVQ0ZTNlZitWUWFvZHE1MWhhZktoWFdIbnA2aWRFRjdCSnRtVTdrcmFCNENMK3FVS3FUbnhsV0cwNVZ1cUtLOTJiYm02YUgrYlNHT05LNUpsc29VdkdVQmJsMlZuVGQxdCt2NVRvZmY0Y2RPTHg5aU9KOHp1R1oybEJWamorMzdNdXFiYjFLUjRLd3Y0d2RBc21lb0o0OWE0TVRSbDBRbUZQWjMrVk1qbmxDd21zRVNzLS10YjBMQ3lsNnhxM1VUUVZrcStuY0p3PT0%3D--4fbc0cacdbcb46ea20b6412c8d8cc8c9e589c0c9");
            //    //request.AddHeader("Referer", "https://paninistickeralbum.fifa.com/game/flash?start_view=frontapp");
            //    //request.AddHeader("x-ms-request-id", "UBqDr");
            //    //request.AddHeader("Accept", "*/*");
            //    //request.AddHeader("x-ms-request-root-id", "wpQZu");
            //    //request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");
            //    //request.AddHeader("Accept-Language", "es-ES,es;q=0.9");
            //    //request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            //    //request.AddHeader("Origin", "https://paninistickeralbum.fifa.com");
            //    //request.AddHeader("X-User-Agent", "Unity/2.0.1 (Windows 10) Unity/2017.3.1p1 webgl_lores");
            //    #endregion
            //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //    request.AddParameter("undefined", $"json=%257b%2522sticker_id%2522%253a{stickerId}%257d&locale=es", ParameterType.RequestBody);
            //    request.AddCookie("_fifastwc_session", "s4~SVR2d09hMzhEckQ5eVhSdnJWMzFMU3VxV2YxZUhzSGZDR1JZUUhLeWZkQWNpb1NSWFFtb29VcEhRVHg3ZHdyRlNDbTVZYzh4VmVpd1FEQy9XTy9LZnJqamVRS2lPbWhySERDVlJkdllydDBGWlg5WWtXYUdKOWZaZUZ1ZXVxdWVvbWpZWmg0MmlCWWp4MnNMSXJpTk5hY2pPV25La2ZoMWQ4eVpuUkk2YjBSdHdlbFF0L1pSa0lnVkNmckJCWTdoMm8zL0RhUHhLcW9mQVZySVh6WHliZ0hWdHlZWTRsZkNDQkE3NzdzRml3U2JKZW5jRlBGV3k1bUVNa3RoY3pjcmxGQ2lTS2ZSMkh2Z0Z1MGxMdDliZm45QXpLc2xDNEViQ1F5YUZDVlYxeGwzMmdMUXBoQXRwdER3SXh6Z0UxYmpxMVEvb0FENzRHR0V2ZXdoczNsVGdVR0xRaDZrNGxmOS80dVFiK0RiczVJTFl5aGlGa2dMN0JrT3RSUzYyVWxtV3hrUURJbkZoTUkvWS9BdFV0TDdJMU9uaHl6dWFYem9TT2EyajNBbHNiall1b3VvYXJHcFZINjdOaXQ3NlJ0VmhpVkNNdUtxaWxKR1YvR1B1OE1IVVQrbDM4b2o3QTRTSXBTUVhSWFRMWHA3QmxtZWM0akNzbWQrdkxzbEdOcDJ1VVhSVFFHVUVWMmkza2VuczJKQXd5NEZoYzQ3VmNEUGMvR3hOWUozSmNmcWZoVldpbW5EM1JkcW45bFd2R0dKRXdROGw2ZlRDUXFVUkEvWFMyU01GZEVrL0R2aW1DVWZoK2lmNVFDTUhXSUhJREl0ZjFVQ2RWSElYRFJyREZaK21TV0VzOExyaWhaNHpjSThkVnhrOHN3bHFBTUhlSm9QdFRKc1NFMkRhUnFRQjI4aVVydnlGRE5oWEw1cCs5YzdibmIwSmY2bGViTHF4LzlRTndQMW9BK1hWRVc3bncwNVFWcFU2WlFVUWlvaVFhSDZwTjRvenhSemk2QjlaclZaYXA1RVVtQ3JOdEtndGg4ZlhXN0kzM1RlRmlpWk0wVXFwK3dwWUQyM2RyOWRxczBGSWxSS0M3Z3Z6bGtEa0p5RWVVUFBGcyt3MS9FaXhzVzRFNGdsYmxjUExUNkhtd2xBV3p3Uy9LN1lCdGhybTZNQkkwT1JzT1ZhN3VpOUMwc0I0VEVXcTFtaEM2aWV0TTA4SG03L255L3RoZDVqeGpEVjl3MlZ1UlJGSk9HNk93ejY1RnBYME5WNHZQbzJqWlI3NXFiRjUvYlNLOC95R2VwUEN1STA4US82VWNROUlwRnJoWG82LzFKTEUya2h2K0krQnB4ZkVwTjdvMGRlcmtWTkVod3IzSjNLQXJsZHVEbHNKRGg1NitrMEY2NnRpWmVUbkt6cmpmcGN1eW9mWHlydFZnc1RvQkRCc3FlVEN5b2RRVDEyS2xaMHdQSktTRDFkL1JNRWpJSHdMMEc5R2ZzM2NmalZhU3hPdjhpVktiOXdhSUJRNnFLWTFHbTJKM1RxTTlReFNOcDdCQ3ZnMGVZdGdPRWE4dzJmNFQ3YTlDNFVBUnRzUit4V1ZHdUFpaWJpRng5Slo0dGFBWHFSWjl0RFVBV3NhVkt3c2NScFRqSFducUpVOUp2eFlQcWpRaWI5NHhxR3NoS1JKUHhOZjdMV1puaFc4di81Ni92UW5OTndzeDlqN1hWaEwzY1FjMnF0OHpURHBjQ0UwN1l1NlYwbzJzUzBGZXFvbXEwQmRmU0ZIbGpQbUFpMDM3dEF4NmdrL0NwYStuMFphTVpleFhlQ2xpNklCbmNPQUt0dVVKZGgxcDlHTkZBNHBuYTkyMzd6WENMYWZieTczU3VnenpBMnMvM0J4SzFHNXJHQjFybTY5R3BtL3d6bDlKWVVzTWlzMkRPTVYyLy9rWVFsYmM4bXJoS1ZLNUJzY0NYckhHeTUwZE1JeWFmQW1qb0VYZEdHUjdFNHM4OTEvMC82Z3ZneVhXSFVtNnl1MXcyTEVYV1dySEU5Q1lnNVZHM0NPTzYzWkkzUVViSUpoVC9aM1ZUSzkzY1QwQ0hWSDk1TCs3bjViekZ4ZU5ONnJsNjdtNStsQUNQQ2hnTDVwZzFYT2VwRVdjKzhndjY5byt4SnRFRkVra2k1dTZqbDIyeGpaY0d2Q3NWZjFHdlVPdXNrMk9NMmU0R0tIVW9yL3Nta1ZsSk9qRFNyUnJGU09lWGVCR1Myd0dDbVhxS1JDUjRlQnBBaXFvWEpLVXBZZ1FmenljMCtqTGVRaXV1c3JlSHZYSjVzdmZ2NUl6TnN4eGxaRjRyYXF3R1l2UEtZN0RDcEFOMnppcEFuY3I3ZktEeGF2NkRFQ3FBZm5FUjMrN1JNa1AyZFZzdXpVNlVhWllMOUw1OHUxTHkrVEJDM0R4aEo2OVJJY25hb0pGTC95UXVLa2FsanM4c1ByUWpJOThVemk5OUFMU0M3RlhBZ2NEcHY5SUZJQlZMRkQrODduc1NDWEVTMmNGSGczQ29uNnNqMFhLMEdBODFORTVaemxPbHJQME83T0xxM2ZkZDdYRjNqUzRwTE1YMUp6dz09LS1YN09xWm8vSmhMaE1MZi9LRGxxQm1nPT0%3D--ceb24436340d5ec765b509b2f2d0118fdac476d3");

            //    IRestResponse response = client.Execute(request);

            //    Console.WriteLine($"\n\n\n{response.Content}\n\n");

            //    Console.WriteLine($"Sticker Id: {stickerId}");

            //    var stickerStats = StickerStats.FromJson(response.Content);

            //    string largeImageUrl = stickerStats.FirstOrDefault().LargeImageUrl;
            //    Console.WriteLine($"LargeImageUrl: {largeImageUrl}");

            //    string name;

            //    //Crea Objeto Sticker para ser almacenado
            //    Sticker sticker = new Sticker() {
            //        StickerId = stickerId,
            //        LargeImageUrl = largeImageUrl
            //    };

            //    if (stickerStats.FirstOrDefault().Stats.Count > 0)
            //    {
            //        Dictionary<string, string> pairs = new Dictionary<string, string>() { };
            //        foreach (var item in stickerStats.FirstOrDefault().Stats.FirstOrDefault().Pairs)
            //            pairs.Add(item.ElementAt(0), item.ElementAt(1));
            //        foreach (var item in pairs)
            //            Console.WriteLine($"{item.Key}: {item.Value}");

            //        /** every single elements from panini
            //        Console.WriteLine($"Position: {pairs["Position"]}");
            //        Console.WriteLine($"Dob: {pairs["Dob"]}");
            //        Console.WriteLine($"Club: {pairs["Club"]}");
            //        Console.WriteLine($"Height: {pairs["Height"]}");
            //        Console.WriteLine($"Weight: {pairs["Weight"]}");
            //        Console.WriteLine($"Debut: {pairs["Debut"]}");
            //        **/

            //        sticker.Position = pairs["Position"];
            //        //sticker.DoB = DateTime.ParseExact($"{pairs["Dob"].Replace('-','/')} 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", new CultureInfo("en-US", true));
            //        sticker.DoB = DateTime.ParseExact($"{pairs["Dob"]}", "dd-MM-yyyy", new CultureInfo("es-Es"));
            //        sticker.Club = (pairs.ContainsKey("Club")) ? pairs["Club"] : sticker.Club;
            //        sticker.Heigth = pairs["Height"];
            //        sticker.Weigth = pairs["Weight"];
            //        sticker.Debut = int.Parse(pairs["Debut"]);
            //    }
            //    string country = String.Empty;
            //    //country = GetCountry(stickerId, country);

            //    var countrySwitch = new Dictionary<Func<int, bool>, Action>
            //    {
            //        {s => s >= 1 && s<= 8 , () => country = String.Empty},
            //        {s => s >= 9 && s<= 20 , () => country ="Rusia"},
            //        {s => s >= 21 && s<= 32 , () => country ="Arabia Saudí"},
            //        {s => s >= 33 && s<= 44 , () => country ="Egipto"},
            //        {s => s >= 45 && s<= 56 , () => country ="Uruguay"},
            //        {s => s >= 57 && s<= 68 , () => country ="Portugal"},
            //        {s => s >= 69 && s<= 80 , () => country ="España"},
            //        {s => s >= 81 && s<= 92 , () => country ="Marruecos"},
            //        {s => s >= 93 && s<= 104 , () => country ="RI de Irán"},
            //        {s => s >= 105 && s<= 116 , () => country ="Francia"},
            //        {s => s >= 117 && s<= 128 , () => country ="Australia"},
            //        {s => s >= 129 && s<= 140 , () => country ="Perú"},
            //        {s => s >= 141 && s<= 152 , () => country ="Dinamarca"},
            //        {s => s >= 153 && s<= 164 , () => country ="Argentina"},
            //        {s => s >= 165 && s<= 176 , () => country ="Islandia"},
            //        {s => s >= 177 && s<= 188 , () => country ="Croacia"},
            //        {s => s >= 189 && s<= 200 , () => country ="Nigeria"},
            //        {s => s >= 201 && s<= 212 , () => country ="Brasil"},
            //        {s => s >= 213 && s<= 224 , () => country ="Suiza"},
            //        {s => s >= 225 && s<= 236 , () => country ="Costa Rica"},
            //        {s => s >= 237 && s<= 248 , () => country ="Serbia"},
            //        {s => s >= 249 && s<= 260 , () => country ="Alemania"},
            //        {s => s >= 261 && s<= 272 , () => country ="México"},
            //        {s => s >= 273 && s<= 284 , () => country ="Suecia"},
            //        {s => s >= 285 && s<= 296 , () => country ="República de Corea"},
            //        {s => s >= 297 && s<= 308 , () => country ="Bélgica"},
            //        {s => s >= 309 && s<= 320 , () => country ="Panamá"},
            //        {s => s >= 321 && s<= 332 , () => country ="Túnez"},
            //        {s => s >= 333 && s<= 344 , () => country ="Inglaterra"},
            //        {s => s >= 345 && s<= 356 , () => country ="Polonia"},
            //        {s => s >= 357 && s<= 368 , () => country ="Senegal"},
            //        {s => s >= 369 && s<= 380 , () => country ="Colombia"},
            //        {s => s >= 381 && s<= 392 , () => country ="Japón"},
            //        {s => s >= 393 && s<= 465 , () => country = String.Empty}
            //    };
            //    countrySwitch.First(sw => sw.Key(stickerId)).Value();

            //    Console.WriteLine($"Country: {country}");
            //    sticker.Country = country;
            //    stickersList.Add(sticker);
            //}
            ////agregar a la base de datos
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PaniniDB>());
            //var db = new PaniniDB();
            //db.Stickers.AddRange(stickersList);
            //db.SaveChanges();
            //Console.WriteLine("Termino la carga a la base de datos local. Busque el *.mdf en la carpeta del proyecto.");
            //Console.ReadKey();
            #endregion
            List<Sticker> stickersList = new PaniniMigration().GetStickers();

            //agregar a la base de datos
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PaniniDB>());
            var db = new PaniniDB();
            db.Stickers.AddRange(stickersList);
            db.SaveChanges();
            Console.WriteLine("Termino la carga a la base de datos local. Busque el *.mdf en su carpera de usuario o la Base de datos creada en SQL Server Express.");
            Console.ReadKey();

        }

        
    }
}
