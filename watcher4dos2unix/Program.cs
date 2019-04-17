using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace watcher4dos2unix
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class Program
    {
        public static void Main(string[] args)
        {
            FileInfo fileInfo = new FileInfo(args);
            if (!fileInfo.IsAllGood())
            {
                Console.WriteLine("Usage: dos2unixWatcher <path to file>");
                return;
            }

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = fileInfo.Dir;
                watcher.Filter = fileInfo.Name;

                watcher.NotifyFilter = NotifyFilters.LastWrite;

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' (or Ctrl+C) to quit the watcher.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Dos2Unix(e.FullPath);
        }

        private static void Dos2Unix(string fullPath)
        {
            List<string> lines = new List<string>();
            using (var reader = File.OpenText(fullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            string fileNameOut = $"{fullPath}.out";
            using (StreamWriter file = new StreamWriter(fileNameOut))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}
