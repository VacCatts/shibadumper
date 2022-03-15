using System;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace shibadumper
{
    class Dumper
    {
        static int count = 0;
        static int donethreads = 0;
        static Stopwatch stopwatch = new Stopwatch();
        static void Main()
        {
            Console.Title = "shibadumper v1.0 @ petmydog.online";
            Console.Clear();
            Console.WriteLine(" ▄▄▄▄▄▄▄ ▄▄   ▄▄ ▄▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄  ▄▄   ▄▄ ▄▄   ▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄  ");
            Console.WriteLine("█       █  █ █  █   █  ▄    █       █      ██  █ █  █  █▄█  █       █       █   ▄  █  ");
            Console.WriteLine("█  ▄▄▄▄▄█  █▄█  █   █ █▄█   █   ▄   █  ▄    █  █ █  █       █    ▄  █    ▄▄▄█  █ █ █  ");
            Console.WriteLine("█ █▄▄▄▄▄█       █   █       █  █▄█  █ █ █   █  █▄█  █       █   █▄█ █   █▄▄▄█   █▄▄█▄ ");
            Console.WriteLine("█▄▄▄▄▄  █   ▄   █   █  ▄   ██       █ █▄█   █       █       █    ▄▄▄█    ▄▄▄█    ▄▄  █");
            Console.WriteLine(" ▄▄▄▄▄█ █  █ █  █   █ █▄█   █   ▄   █       █       █ ██▄██ █   █   █   █▄▄▄█   █  █ █");
            Console.WriteLine("█▄▄▄▄▄▄▄█▄▄█ █▄▄█▄▄▄█▄▄▄▄▄▄▄█▄▄█ █▄▄█▄▄▄▄▄▄██▄▄▄▄▄▄▄█▄█   █▄█▄▄▄█   █▄▄▄▄▄▄▄█▄▄▄█  █▄█");
            Console.WriteLine("made by vaccat <3");
            Console.WriteLine("powered by shibe.online");
            Thread.Sleep(1000);

            // menu
            Console.Write("\n\n");
            Console.WriteLine("[shibadumper] [1] Dump");
            Console.WriteLine("[shibadumper] [2] Sort Files");
            Console.WriteLine("[shibadumper] [3] Exit");
            Console.Write("\n> ");
            var userinput = Console.ReadKey();
            var input = 1;
            if (char.IsDigit(userinput.KeyChar))
            {
                input = int.Parse(userinput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                input = 1;
            }

            if (input == 1) {
                Dump();
            } else if (input == 2) {
                ManageFolders();
            } else if (input == 3) {
                Environment.Exit(0);
            } else {
                Console.WriteLine("\nInvalid input");
                Thread.Sleep(1000);
                Console.Clear();
                Main();
            }
        }

        static void Dump() {
            Console.Clear();
            Console.WriteLine("[shibadumper] How many threads");
            int threads = int.Parse(Console.ReadLine());

            Console.WriteLine("[shibadumper] How many shibas to dump");
            int amt = int.Parse(Console.ReadLine());

            Console.WriteLine("[shibadumper] Dumping...");
            stopwatch.Reset();
            stopwatch.Start();
            MakeThreads(threads, amt);
            while (donethreads != threads)
            {
                Console.Title = $"shibadumper v1.0 @ petmydog.online | Threads: {threads - donethreads} | Dumped: {count}";
                Thread.Sleep(500);
            }
            stopwatch.Stop();
            Console.WriteLine($"[shibadumper] Done in {stopwatch.ElapsedMilliseconds}ms or {stopwatch.Elapsed.TotalSeconds}s");
            Thread.Sleep(5000);
            Main();
        }
        static void MakeThreads(int threads, int amt)
        {
            for (int i = 0; i < threads; i++)
            {
                Thread t = new Thread(() => Dump2(amt / threads));
                t.Start();
            }
        }
        static void Dump2(int amount) {
            // if folder "output" doesnt exist, create it
            if (!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }
            for (int i = 0; i < amount; i++)
            {
                // download shiba from url and save it in the "output" folder
                string url = "https://shibe.online/api/shibes?count=1&urls=true&httpsUrls=true";
                string downloadurl = new System.Net.WebClient().DownloadString(url);
                string imageUrl = downloadurl.Replace("[", "").Replace("]", "").Replace("\"", "");
                string fileName = imageUrl.Split('/')[imageUrl.Split('/').Length - 1];

                // if file already exists, skip it
                if (File.Exists("output/" + fileName))
                {
                    i--;
                    continue;
                }
                
                try {
                    File.WriteAllBytes(@"output\" + fileName, new System.Net.WebClient().DownloadData(imageUrl));
                    count++;
                    Console.WriteLine("[shibadumper] Downloaded " + fileName + " for a total of " + count);
                } catch {
                    Console.WriteLine("[shibadumper] Failed to download " + fileName);
                }
            }
            donethreads++;
            Console.WriteLine("[shibadumper] Done with thread " + donethreads);
        }

        // manage folders
        static void ManageFolders() {
            Console.Clear();
            stopwatch.Reset();
            stopwatch.Start();
            count = 0;
            DirectoryInfo d = new DirectoryInfo(@"output\");
            Sort(d);
            stopwatch.Stop();
            Console.WriteLine($"[shibadumper] Done in {stopwatch.ElapsedMilliseconds}ms or {stopwatch.Elapsed.TotalSeconds}s");
            Thread.Sleep(5000);
            Main();
        }

        // sorting function
        static void Sort(DirectoryInfo d) {
            foreach (var file in d.GetFiles("*.jpg"))
            {
                Directory.Move(file.FullName, @"output\" + count + ".jpg");
                count++;
            }
        }
    }
}
