using System;

namespace DNS_Cacher {
    public class Listener {
        public string[] exitCommands = new string[] {"exit", ".exit", "byebye", "quit", ".quit", "exit()"};
        public string currCommand;

        public void listen() {
            Console.Write("> ");

            currCommand = Console.ReadLine();

            foreach (string cmd in exitCommands) {
                if (currCommand == cmd) {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nHave a nice day! :D");
                    System.Environment.Exit(0);
                }
            }
        }
    }
}