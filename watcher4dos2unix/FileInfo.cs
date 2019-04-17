using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watcher4dos2unix
{
    public class FileInfo
    {
        public string Name { get; set; }
        public string Dir { get; set; }

        public FileInfo (string name, string dir)
        {
            Name = name;
            Dir = dir;
        }

        public FileInfo (string[] args)
        {
            if (args.Length == 1)
            {
                string[] a = args[0].Split('\\');
                string fileToWatch = a[a.Length - 1], dirToWatchTheFileIn;

                if (a.Length > 0)
                {
                    dirToWatchTheFileIn = args[0].Substring(0, args[0].Length - a[a.Length - 1].Length);

                    Name = fileToWatch;
                    Dir = dirToWatchTheFileIn;

                    Console.WriteLine($"Watching {fileToWatch} located at {dirToWatchTheFileIn}");
                }
            }
        }

        public bool IsAllGood()
        {
            return Name != null && Dir != null;
        }
    }
}
