using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public partial class File
    {
        public string SecureUrl { get; set; }
    }

}
