using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Kakao_Page_Downloader.Utils;
using Newtonsoft.Json;
using RestSharp;

namespace Kakao_Page_Downloader
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.Title = "Keaf's Kakao Page Downloader";
            ConsoleHelper.Print("Welcome to Keaf's Kakao Page Downloader");
            ConsoleHelper.Print("Please Enter Manga ID");
            var input = Console.ReadLine();
            try
            {
                ConsoleHelper.Print("Getting manga info...");

                await DownloadManga(input);
                ConsoleHelper.Print($"Manga {input} complete.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError($"\n{ex.Message}\n\n----Stack Trace----\n{ex.StackTrace}");
            }

            Console.ReadLine();
        }

        private static async Task DownloadManga(string input)
        {
            var getManga = new RestClient("https://api2-page.kakao.com/api/v5/store/singles");

            var mangaRequest = new RestRequest(Method.POST);
            mangaRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            mangaRequest.AddParameter("application/x-www-form-urlencoded",
                $"seriesid={input}&page=0&direction=asc&page_size=90&without_hidden=true",
                ParameterType.RequestBody);

            var mangaRaw = await getManga.ExecuteAsync(mangaRequest);
            var mangaObj = JsonConvert.DeserializeObject<Manga>(mangaRaw.Content);

            ConsoleHelper.Print($"Found {mangaObj.Total_Count} chapters. Starting download.");

            if (!Directory.Exists(input))
            {
                ConsoleHelper.Print($"Creating Folder {input}");
                Directory.CreateDirectory(input ?? throw new Exception("ID Was null somehow. Retry."));
            }
            else
            {
                ConsoleHelper.Print("Folder Already Exists");
                ConsoleHelper.Print("Are you sure you want to continue?");
                ConsoleHelper.Print("Press any key to continue...");
                Console.ReadLine();
            }

            foreach (var chapter in mangaObj.Singles)
            {
                var getPages = new RestClient("https://api2-page.kakao.com/api/v1/inven/get_download_data/web");

                var pagesRequest = new RestRequest(Method.POST);
                pagesRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                pagesRequest.AddParameter("application/x-www-form-urlencoded", $"productId={chapter.Id}",
                    ParameterType.RequestBody);

                var pagesRaw = await getPages.ExecuteAsync(pagesRequest);
                var pagesObj = JsonConvert.DeserializeObject<Base>(pagesRaw.Content);

                if (!Directory.Exists($"{input}/{chapter.Title}"))
                {
                    ConsoleHelper.Print($"Creating Folder {chapter.Title}");
                    Directory.CreateDirectory($"{input}/{chapter.Title}");
                }
                else
                {
                    ConsoleHelper.Print("Folder Already Exists");
                    ConsoleHelper.Print("Are you sure you want to continue?");
                    ConsoleHelper.Print("Press any key to continue...");
                    Console.ReadLine();
                }

                ConsoleHelper.Print($"Downloading {chapter.Page_Count} images...");

                await Task.WhenAll(pagesObj.DownloadData.Members.Files.Select(page => DownloadPage(input,
                    chapter.Page_Count, chapter.Title,
                    page.SecureUrl, page.No, pagesObj.DownloadData.Members.SAtsServerUrl)));
            }
        }

        private static async Task DownloadPage(string input, int pageCount, string chapterTitle, string secureUrl,
            int pageNumer, string baseServerUrl)
        {
            try
            {
                using var wc = new WebClient();
                var ext = "";
                if (secureUrl.Contains(".jpeg"))
                    ext = ".jpeg";
                if (secureUrl.Contains(".png"))
                    ext = ".png";

                await wc.DownloadFileTaskAsync(new Uri($"{baseServerUrl}{secureUrl}"), $"{input}/{chapterTitle}/{pageNumer}{ext}");

                ConsoleHelper.Print($"{pageNumer}/{pageCount} pages complete.");
            } 
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
    }
}