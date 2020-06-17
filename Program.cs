using Kakao_Page_Downloader.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kakao_Page_Downloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Keaf's Kakao Page Downloader";
            ConsoleHelper.Print("Welcome to Keaf's Kakao Page Downloader");
            ConsoleHelper.Print("Please Enter ProductID");
            string input = Console.ReadLine();
            try
            {
                ConsoleHelper.Print("Sending request to API...");
                WebClient wc = new WebClient();
                
                string title = wc.DownloadString("https://page.kakao.com/viewer?productId=" + input).Split(new string[] { "og:title\" content=\"" }, StringSplitOptions.None)[1].Trim().Split('"')[0].Trim();
                ConsoleHelper.Print("Requesting api for " + title + " Image Data...");
                wc.Dispose();
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.106 Safari/537.36");
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                Base fu = JsonConvert.DeserializeObject<Base>(wc.UploadString("https://api2-page.kakao.com/api/v1/inven/get_download_data/web", "productId=" + input));
                ConsoleHelper.Print("Success!", ConsoleColor.Cyan);
                if (!Directory.Exists(title))
                {
                    ConsoleHelper.Print("Creating Folder " + title);
                    Directory.CreateDirectory(title);
                }
                else
                {
                    ConsoleHelper.Print("Folder Already Exists");
                    ConsoleHelper.Print("Are you sure you want to continue?");
                    ConsoleHelper.Print("Press any key to oontinue...");
                    Console.ReadLine();
                }
                ConsoleHelper.Print("Downloading images...");
                int pagenum = 1;
                foreach (var file in fu.DownloadData.Members.Files)
                {
                    
                    string ext = "";
                    if (file.SecureUrl.Contains(".jpeg"))
                        ext = ".jpeg";
                    if (file.SecureUrl.Contains(".png"))
                        ext = ".png";
                    ConsoleHelper.Print("Saving File to \\" + title + "\\Page " + pagenum + ext, ConsoleColor.Yellow);
                    wc.DownloadFile(fu.DownloadData.Members.SAtsServerUrl + file.SecureUrl, title + "\\Page " + pagenum + ext);
                    pagenum++;
                }
                ConsoleHelper.Print("Job Done!", ConsoleColor.Cyan);

            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError("\n"+ex.Message + "\n\n----StackTrace---\n"+ex.StackTrace);
            }
            Console.ReadLine();
        }
    }
}
