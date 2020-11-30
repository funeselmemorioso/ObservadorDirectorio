using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace ObservadorDeCambiosEnDirectorio
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Run()
        {
            String p = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); 

            String path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\source\repos\ObservadorDeCambiosEnDirectorio\ObservadorDeCambiosEnDirectorio\unaCarpetaDePruebas";
            //String path = @"C:\Users\eveltri\Desktop\BancoMacro\CarpetaPruebaObservador";
            //string[] args = Environment.GetCommandLineArgs();

            //// If a directory is not specified, exit program.
            //if (args.Length != 2)
            //{
            //    // Display the proper way to call the program.
            //    Console.WriteLine("Usage: Watcher.exe (directory)");
            //    return;
            //}

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                // watcher.Path = args[1];
                watcher.Path = path;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                // watcher.NotifyFilter = //NotifyFilters.LastAccess |
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                //| NotifyFilters.FileName
                //| NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e) =>
            // Specify what is done when a file is changed, created, or deleted.            
            HacerAlgo(source, e);

        private static void OnRenamed(object source, RenamedEventArgs e) =>
            // Specify what is done when a file is renamed.
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");


        private static void HacerAlgo(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
            Console.WriteLine("Aca se ahce algo...");
        }

    }

}

