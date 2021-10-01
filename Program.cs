using System;

namespace DNS_Cacher {
    class Program {
        static void Welcome() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Welcome to DNS Cacher!\nEnter a hostname to start.\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args) {
            Welcome();

            Listener listener = new Listener();

            while (true) {
                listener.listen();

                string currCommand = listener.currCommand;

                Console.WriteLine(currCommand);
            }
        }
    }
}
