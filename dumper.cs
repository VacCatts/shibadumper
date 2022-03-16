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

        // hi another dev note:
        // its super simple and messy, you can do better than this super easily
        // im gonna accept pull requests
        // i dont really have other ideas for this
        // have fun <3

        static void Main()
        {
            // this lookin ass ascii art cool awesome u dont really gotta think abt it
            Console.Title = "shibadumper v1.1 @ petmydog.online";
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

            // menu thing
            Console.Write("\n\n");
            Console.WriteLine("[shibadumper] [1] Dump");
            Console.WriteLine("[shibadumper] [2] Sort Files");
            Console.WriteLine("[shibadumper] [3] Exit");
            Console.Write("\n> ");

            int input = Utils.getKey();

            // shoutout v8a
            switch (input)
            {
                case 1:
                    Dump();
                    break;
                case 2:
                    ManageFolders();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid input");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Main();
                    break;
            }
        }

        static void Dump() {
            // just overall setup
            Console.Clear();
            Console.WriteLine("[shibadumper] How many threads");
            int threads = int.Parse(Console.ReadLine());

            Console.WriteLine("[shibadumper] How many shibas to dump");
            int amt = int.Parse(Console.ReadLine());

            Console.WriteLine("[shibadumper] Dumping...");
            // stopwatch (for timing (well no shit (lmao)))
            stopwatch.Reset();
            stopwatch.Start();
            
            // oh yeah i made this MakeThreads function which is pretty cool ig
            // (its a mistake and we'll see it later)
            MakeThreads(threads, amt);

            // should explain itself
            while (donethreads != threads)
            {
                // update the title
                Console.Title = $"shibadumper v1.1 @ petmydog.online | Threads: {threads - donethreads} | Dumped: {count}";
                Thread.Sleep(100); // now this i was contemplating on what to put as it, im guessing 100 is fine enough
            }
            // bla bla bla stopwatch
            stopwatch.Stop();
            // t
            Console.WriteLine($"[shibadumper] Done in {stopwatch.ElapsedMilliseconds}ms or {stopwatch.Elapsed.TotalSeconds}s");
            Thread.Sleep(5000);
            // call the main function because we're so cool
            Main();
        }
        static void MakeThreads(int threads, int amt)
        {
            // now this is the makethreads function i was talking about
            // its ass
            // and its super dumb because of the amt / threads bullshit it runs like super slow at the end lmfaoo
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
                    amount++;
                    return;
                }
                
                // try downloading the file
                try {
                    File.WriteAllBytes(@"output\" + fileName, new System.Net.WebClient().DownloadData(imageUrl));
                    count++;
                    Console.WriteLine("[shibadumper] Downloaded " + fileName + " for a total of " + count);
                } catch { // and catch because im such a shitter lol, im pretty sure it actually helps w stuff
                    Console.WriteLine("[shibadumper] Failed to download " + fileName);
                }
            }
            // increment the donethreads
            donethreads++;
            Console.WriteLine("[shibadumper] Done with thread " + donethreads);
        }

        // manage folders (nvm its only sort rn)
        static void ManageFolders() {
            Console.Clear();
            // stop watch
            stopwatch.Reset();
            stopwatch.Start();

            // lets go maths and shit
            DirectoryInfo d = new DirectoryInfo(@"output\"); // folder
            Utils.Sort(d);
            stopwatch.Stop();
            Console.WriteLine($"[shibadumper] Done in {stopwatch.ElapsedMilliseconds}ms or {stopwatch.Elapsed.TotalSeconds}s");
            Thread.Sleep(5000);
            Main();
        }
    }
}
