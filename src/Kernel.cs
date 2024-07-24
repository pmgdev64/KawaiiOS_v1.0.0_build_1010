using System;
using Sys = Cosmos.System;

namespace terminal;

public class Kernel : Sys.Kernel {
  protected override void BeforeRun() {
    Console.Writeline("Welcome to KawaiiOS! Type Help To Show All The Command List.");
  }

  protected override void Run() {
        Console.Write($"{_Prompt}> ");
        var input = Console.ReadLine();
        string[] words = input.Split(' ');
        switch (words[0]) {
            case "cpu":
                Console.WriteLine($"Vendor: {CPU.GetCPUVendorName()}, Name: {CPU.GetCPUBrandString()}, Frequency: {CPU.GetCPUCycleSpeed()}");
                break;
            case "shutdown":
                Sys.Power.Shutdown(); // shutdown is supported
                break;
            case "restart":
                Sys.Power.Reboot(); // restart too
                break;
            case "help":
                // console methods are plugged
                Console Writeline("<Help Page>");
                Console.WriteLine("cpu      - prints info about current cpu");
                Console.WriteLine("shutdown - shuts down current computer");
                Console.WriteLine("restart  - restarts current computer");
                Console.WriteLine("help     - shows this help menu");
                Console.Writeline("install  - install the system into hard disk");
                break;
            default:
                // switch operator works great
                Console.WriteLine($"\"{words[0]}\" is not a command");
                break;
      }
    public static Font OpenSans = new PCScreenFont(16, 16, System.IO.File.ReadAllBytes(@"../Fonts/OpenSans.ttf"), null);
  }
}


