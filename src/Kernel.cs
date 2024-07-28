using System;
using Cosmos.Core;
using Sys = Cosmos.System;
using Cosmos.Graphic.System;

namespace KawaiiOS;

public class Kernel : Sys.Kernel {
  protected override void BeforeRun() {
    Console.Writeline("
  _  __                   _ _  ____   _____        __   ___   ___  
 | |/ /                  (_|_)/ __ \ / ____|      /_ | / _ \ / _ \ 
 | ' / __ ___      ____ _ _ _| |  | | (___   __   _| || | | | | | |
 |  < / _` \ \ /\ / / _` | | | |  | |\___ \  \ \ / / || | | | | | |
 | . \ (_| |\ V  V / (_| | | | |__| |____) |  \ V /| || |_| | |_| |
 |_|\_\__,_| \_/\_/ \__,_|_|_|\____/|_____/    \_/ |_(_)___(_)___/ 
                                                                   

        ")
    Console.Writeline("Welcome to KawaiiOS! Type Help To Show All The Command List.");
  }

  protected override void Run() {
        
    Console.Write($"Root@KawaiiOS: ~$ ");
    var input = Console.ReadLine();
    string arg1 = "-s";
    string arg2 = "--system";
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
            start_system();
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

    private void start_system() {}
  }
}


