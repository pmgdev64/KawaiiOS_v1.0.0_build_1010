using Cosmos.Core;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using Sys = Cosmos.System;

namespace KawaiiOS;

public class Kernel : Sys.Kernel {
  public static uint screenWidth = 640;
  public static uint screenHeight = 480;
  public static DoubleBufferedVMWareSVGAII vMWareSVGAII;
  Bitmap bitmap;
  public static Bitmap programlogo;
  Bitmap bootBitmap;

  int[] cursor = new int[]
      {
          1,0,0,0,0,0,0,0,0,0,0,0,
          1,1,0,0,0,0,0,0,0,0,0,0,
          1,2,1,0,0,0,0,0,0,0,0,0,
          1,2,2,1,0,0,0,0,0,0,0,0,
          1,2,2,2,1,0,0,0,0,0,0,0,
          1,2,2,2,2,1,0,0,0,0,0,0,
          1,2,2,2,2,2,1,0,0,0,0,0,
          1,2,2,2,2,2,2,1,0,0,0,0,
          1,2,2,2,2,2,2,2,1,0,0,0,
          1,2,2,2,2,2,2,2,2,1,0,0,
          1,2,2,2,2,2,2,2,2,2,1,0,
          1,2,2,2,2,2,2,2,2,2,2,1,
          1,2,2,2,2,2,2,1,1,1,1,1,
          1,2,2,2,1,2,2,1,0,0,0,0,
          1,2,2,1,0,1,2,2,1,0,0,0,
          1,2,1,0,0,1,2,2,1,0,0,0,
          1,1,0,0,0,0,1,2,2,1,0,0,
          0,0,0,0,0,0,1,2,2,1,0,0,
          0,0,0,0,0,0,0,1,1,0,0,0
      };

  LogView logView;
  Clock Clock;
  Notepad notepad;
  Dock dock;
  public static bool Pressed;

  public static List<App> apps = new List<App>();

  public static Color avgCol;
  
  protected override void BeforeRun() {
    Console.Writeline('''
  _  __                   _ _  ____   _____        __   ___   ___  
 | |/ /                  (_|_)/ __ \ / ____|      /_ | / _ \ / _ \ 
 | ' / __ ___      ____ _ _ _| |  | | (___   __   _| || | | | | | |
 |  < / _` \ \ /\ / / _` | | | |  | |\___ \  \ \ / / || | | | | | |
 | . \ (_| |\ V  V / (_| | | | |__| |____) |  \ V /| || |_| | |_| |
 |_|\_\__,_| \_/\_/ \__,_|_|_|\____/|_____/    \_/ |_(_)___(_)___/ 
                                                                   

        ''')
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

    private void start_system() {
      CosmosVFS cosmosVFS = new CosmosVFS();
      VFSManager.RegisterVFS(cosmosVFS);

      bootBitmap = new Bitmap(@"0:\resource\boot.bmp");

      vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
      vMWareSVGAII.SetMode(screenWidth, screenHeight);

      vMWareSVGAII.DoubleBuffer_DrawImage(bootBitmap, 640 / 4, 0);
      vMWareSVGAII.DoubleBuffer_Update();

      bitmap = new Bitmap(@"0:\resource\background.bmp");
      programlogo = new Bitmap(@"0:\resource\program.bmp");

      uint r = 0;
      uint g = 0;
      uint b = 0;
      for (uint i = 0; i < bitmap.rawData.Length; i++)
          {
              Color color = Color.FromArgb(bitmap.rawData[i]);
              r += color.R;
              g += color.G;
              b += color.B;
          }
      avgCol = Color.FromArgb((int)(r / bitmap.rawData.Length), (int)(g / bitmap.rawData.Length), (int)(b / bitmap.rawData.Length));

      MouseManager.ScreenWidth = screenWidth;
      MouseManager.ScreenHeight = screenHeight;
      MouseManager.X = screenWidth / 2;
      MouseManager.Y = screenHeight / 2;

      logView = new LogView(300, 200, 10, 30);
      Clock = new Clock(200, 200, 400, 200);
      notepad = new Notepad(200, 100, 10, 300);
      dock = new Dock();

      apps.Add(logView);
      apps.Add(Clock);
      apps.Add(notepad);

      enter_desktop();
    }
    private void enter_desktop() {}
    }
  }
}


