using System.Collections.Generic;

namespace Kakao_Page_Downloader
{
    public partial class Base
    {
        public DownloadData DownloadData { get; set; }
    }

    public partial class DownloadData
    {
        public Members Members { get; set; }
    }

    public partial class Members
    {
        public List<File> Files { get; set; }
        public string SAtsServerUrl { get; set; }
        public int TotalCount { get; set; }
    }

    public partial class File
    {
        public string SecureUrl { get; set; }
        public int No { get; set; }
    }

    public partial class Manga
    {
        public List<Chapter> Singles { get; set; }
        public int Total_Count { get; set; }
    }

    public partial class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Page_Count { get; set; }

        public List<File> Files { get; set; }
    }
    
}