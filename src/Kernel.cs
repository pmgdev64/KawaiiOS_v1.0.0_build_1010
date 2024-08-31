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
  public static string defaultuser = "root";
  public static string defaultpassword = "12345678";
  public static string arg1 = "-s";
  public static string arg2 = "--system";
  public static string defaultdirectory = "/system/x86-84";
  public static bool IsInstalled = false;
  public readonly string exception_text;
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
  File_explorer file_explorer;
  Dock dock;
  public static bool Pressed;

  public static List<App> apps = new List<App>();

  public static Color avgCol;
  
  protected override void BeforeRun() {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(" _  __                   _ _  ____   _____        __   ___   ___                                       ");
    Console.WriteLine("| |/ /                  (_|_)/ __ \ / ____|      /_ | / _ \ / _ \                                      ");
    Console.WriteLine("| ' / __ ___      ____ _ _ _| |  | | (___   __   _| || | | | | | |                                     ");
    Console.Writeline("|  < / _` \ \ /\ / / _` | | | |  | |\___ \  \ \ / / || | | | | | |         "+ ConsoleColor.Green +"Created By Pmg Dev @ Nexon");
    Console.WriteLine("| . \ (_| |\ V  V / (_| | | | |__| |____) |  \ V /| || |_| | |_| |         "+ ConsoleColor.Red + "Version: 1.0.0 | build: 1010");
    Console.WriteLine("|_|\_\__,_| \_/\_/ \__,_|_|_|\____/|_____/    \_/ |_(_)___(_)___/                                      ");
                                                                   
    Console.Writeline("[!] Welcome to KawaiiOS! Type [Help] To Show All The Command List.");
    Console.WriteLine("[!] you have logged in as default user");
  }

  protected override void Run() {
    CustomConsole.WriteLineInfo("Loading files...");
    Files.LoadFiles();

    CustomConsole.WriteLineInfo("Checking for autostart.bat script...");
    Console.Write($ defaultuser+"@KawaiiOS: ~$ ");
    var input = Console.ReadLine();
    string[] words = input.Split(' ');
    switch (words[0]) {
        case "cpu":
            Console.WriteLine($"Vendor: {CPU.GetCPUVendorName()}, Name: {CPU.GetCPUBrandString()}, Frequency: {CPU.GetCPUCycleSpeed()}");
            break;
        case "shutdown":
            Sys.Power.Shutdown(); // shutdown is supported
            break;
        case "kawaii": // a.k.a neofetch
            Console.WriteLine("not recognized");
        case "restart":
            Sys.Power.Reboot(); // restart too
            break;
        case "start":
            start_system();
        case "cls":
            Console.Clear();
        case "mkfike" + input:
            try {
              Console.WriteLine("[!] operation completed successfully.");
            }
            catch(Exeption ex) {
              Console.WriteLine("[!] operation failed");
            }
        case "help":
            // console methods are plugged
            Console Writeline("<----------------Help Page--------------->");
            Console.WriteLine("[For more information, visit https://kawaiiproject.neocities.org]");
            Console.WriteLine("cpu      - prints info about current cpu");
            Console.WriteLine("start    - running the system")
            Console.WriteLine("cls      - clear the terminal")
            Console.WriteLine("shutdown - shuts down current computer");
            Console.WriteLine("restart  - restarts current computer");
            Console.WriteLine("help     - shows this help menu");
            Console.Writeline("install (-s or --system) - install the system into hard disk");
            break;
        case "install":
            Console.WriteLine("cannot install because this function isn't added");
        default:
            // switch operator works great
            Console.WriteLine($"<"{words[0]}>" is not a recognized command, operable function or scripts");
            break;
    }
    if (File.Exists(CurrentDirectory + "autostart.bat"))
    {
          CustomConsole.WriteLineOK("Detected autostart script, starting autostart.bat....");

          Batch.Execute(CurrentDirectory + "autostart.bat");
    }    
    public static Font Sansfont = new PCScreenFont(16, 16, System.IO.File.ReadAllBytes(@"../Fonts/SansFont.ttf"), null);

    private void start_system() {
      CosmosVFS cosmosVFS = new CosmosVFS();
      VFSManager.RegisterVFS(cosmosVFS);

      bootBitmap = new Bitmap(@"0:\Resources\boot.bmp");

      vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
      vMWareSVGAII.SetMode(screenWidth, screenHeight);

      vMWareSVGAII.DoubleBuffer_DrawImage(bootBitmap, 640 / 4, 0);
      vMWareSVGAII.DoubleBuffer_Update();

      bitmap = new Bitmap(@"0:\Resources\background.bmp");
      programlogo = new Bitmap(@"0:\Resources\Dakirby309-Simply-Styled-Default-Programs-1.bmp");

      uint r = 66;
      uint g = 212;
      uint b = 245;
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
      file_explorer = new File_explorer(200, 100, 10, 300);
      dock = new Dock();

      apps.Add(logView);
      apps.Add(Clock);
      apps.Add(notepad);
      apps.Add(file_explorer);

      enter_desktop();
    }
    private void enter_desktop() {
      switch (MouseManager.MouseState)
        {
          case MouseState.Left:
              Pressed = true;
              break;
          case MouseState.None:
              Pressed = false;
              break;
        }

        vMWareSVGAII.DoubleBuffer_Clear((uint)Color.Black.ToArgb());
        vMWareSVGAII.DoubleBuffer_SetVRAM(bitmap.rawData, (int)vMWareSVGAII.FrameSize);
        logView.text = $"Time: {DateTime.Now}\nInstall RAM: {CPU.GetAmountOfRAM()}MB, Video RAM: {vMWareSVGAII.Video_Memory.Size}Bytes";
        foreach (App app in apps)
        app.Update();

        dock.Update();

        DrawCursor(vMWareSVGAII, MouseManager.X, MouseManager.Y);

        vMWareSVGAII.DoubleBuffer_U
      }
      public void draw_cursor(DoubleBufferedVMWareSVGAII vMWareSVGAII, uint x, uint y) {
        for (uint h = 0; h < 19; h++)
            {
              for (uint w = 0; w < 12; w++)
                {
                  if (cursor[h * 12 + w] == 1)
                    {
                      vMWareSVGAII.DoubleBuffer_SetPixel(w + x, h + y, (uint)Color.Black.ToArgb());
                    }
                  if (cursor[h * 12 + w] == 2)
                    {
                      vMWareSVGAII.DoubleBuffer_SetPixel(w + x, h + y, (uint)Color.White.ToArgb());
                    }
                }
            }
        }
      public void play_media_files(file_types, media_types) {} 
      public static void BSOD(string exception_text)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine(":(");
                Console.WriteLine("A problem that it ran in your device/pc/laptop and need to restart to fix these problems.");
                Console.WriteLine();
                Console.WriteLine("For more infomation about this issue and possiable fixes, visit: https://kawaiiproject.neocities.org/issues");
                Console.WriteLine("If you call a support person, give them this info: ");
                Console.WriteLine("Stop code: " + exception_text);
                Console.WriteLine("Collecting info: " + i + "%");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            Console.WriteLine("Please press Enter key to reboot");
            Console.ReadLine();
            Sys.Power.Reboot();
      }
    }
  }
}

