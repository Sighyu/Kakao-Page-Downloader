using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakao_Page_Downloader.Utils
{
    public class ConsoleHelper
    {
        private static string nameSection = "Keafu~";

        public static void Print(string s, ConsoleColor? color = null)
		{
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(GetTimestamp());
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(nameSection);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("] ");
			if (color != null)
				Console.ForegroundColor = color.Value;
			else
				Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine(s);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		public static void PrintError(string s)
		{
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write(GetTimestamp());
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(nameSection);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Error");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(s);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		private static string GetTimestamp()
		{
			return DateTime.Now.ToString("HH:mm:ss");
		}
	}
}
