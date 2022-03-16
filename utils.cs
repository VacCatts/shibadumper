using System;
using System.IO;

namespace shibadumper {
    class Utils {
        public static int getKey() {
            var userinput = Console.ReadKey();
            var input = 1;
            // i could just not do this but whatever
            if (char.IsDigit(userinput.KeyChar))
            {
                input = int.Parse(userinput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                input = 1;
            }
            return input;
        }

        // sorting function
        public static void Sort(DirectoryInfo d) {
            int count = 0;
            foreach (var file in d.GetFiles("*.jpg"))
            {
                Directory.Move(file.FullName, @"output\" + count + ".jpg"); // i got brain cancer from reading this :skull:
                count++;
            }
        }
    }
}