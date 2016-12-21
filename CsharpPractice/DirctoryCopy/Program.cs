using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirctoryCopy
{
    class Program
    {

        /// <summary>
        /// Filesystem
        /// </summary>
        public class FileSystem
        {
            // Copy directory structure recursively
            public static void copyDirectory(string Src, string Dst)
            {
                String[] Files;

                if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                    Dst += Path.DirectorySeparatorChar;
                if (!Directory.Exists(Dst))
                    Directory.CreateDirectory(Dst);
                Files = Directory.GetFileSystemEntries(Src);
                foreach (string Element in Files)
                {
                    // Sub directories
                    if (Directory.Exists(Element))
                        copyDirectory(Element, Dst + Path.GetFileName(Element));
                    // Files in directory
                    else
                        File.Copy(Element, Dst + Path.GetFileName(Element), true);
                    Console.WriteLine(Dst + Path.GetFileName(Element));

                }
            }

            public static void Delete3rdPartyLibFiles(string Src, string Dst = "Assets")
            {
                string[] Files;

                if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                    Dst += Path.DirectorySeparatorChar;

                Files = Directory.GetFileSystemEntries(Src);
                foreach (string Element in Files)
                {
                    // Sub directories
                    if (Directory.Exists(Element))
                        Delete3rdPartyLibFiles(Element, Dst + Path.GetFileName(Element));
                    // Files in directory
                    else
                    {
                        File.Delete(Dst + Path.GetFileName(Element));
                        Console.WriteLine(Dst + Path.GetFileName(Element));
                    }
                }
            }


        }

        static void Main(string[] args)
        {
            foreach(string file in Directory.GetFiles("."))
            {
                string path = Path.GetFullPath(file);
            }
            int a = 1 << 8;
            string[] arguments = Environment.GetCommandLineArgs();
            Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments));

            DateTime date1 = DateTime.Now;
            string date = string.Format("{0:yyyy-MM-dd_HH-mm}", date1);  // "8 08 008 2008"   year
            Console.WriteLine(date);

            // After a successful copy, you can then call
            // Directory.Delete(@"c:\MySrcDirectory") to mimic a Directory.Move behaviour
            try
            {
                //FileSystem.copyDirectory(@"C:\MY_Dev\vr_engine\source\3rdparty\a", @"c:\MyDstDirectory");
                FileSystem.Delete3rdPartyLibFiles(@"C:\MY_Dev\vr_engine\source\3rdparty\a", @"c:\MyDstDirectory");

            }
            catch (Exception Ex)
            {
                Console.Error.WriteLine(Ex.Message);
            }
        }
    }
}
