using System;
using Cosmos.Core;
using Sys = Cosmos.System;
using Cosmos.Graphic.System;

namespace KawaiiOS;

public class Kernel : Sys.Kernel {
  protected override void BeforeRun() {
    Console.Writeline("Welcome to KawaiiOS! Type Help To Show All The Command List.");
  }

  protected override void Run() {
        Console.Write($"Root@KawaiiOS: ~$ ");
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
            case "start":
                
            case "help":
                // console methods are plugged
                Console Writeline("<----------------Help Page--------------->");
                Console.WriteLine("cpu      - prints info about current cpu");
                Console.WriteLine("start    - running the system")
                Console.WriteLine("shutdown - shuts down current computer");
                Console.WriteLine("restart  - restarts current computer");
                Console.WriteLine("help     - shows this help menu");
                Console.Writeline("install (-s or --system) - install the system into hard disk");
                break;
            case "install":
                Console.WriteLine("cannot install because this function isn't added");
            default:
                // switch operator works great
                Console.WriteLine($"<"{words[0]}>" Command Not Found!");
                break;
      }
    public static Font Sansfont = new PCScreenFont(16, 16, System.IO.File.ReadAllBytes(@"../Fonts/SansFont.ttf"), null);
  }
}


