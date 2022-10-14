using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSplitter.Utility
{
    public class Helper
    {
        public static string[] GetFilesOver10mb()
        {
            string[] files = new string[0];

            files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/", "*.txt");
            List<string> filesList = files.ToList();

            filesList.RemoveAll(f => new FileInfo(f).Length < 10000000);

            return filesList.ToArray();
        }
    }
}