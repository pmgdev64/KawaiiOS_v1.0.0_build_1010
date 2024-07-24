using System;
using Sys = Cosmos.System;

public class Kernel : Sys.Kernel {
  protected override void BeforeRun() {
    Console.Writeline("Welcome to KawaiiOS! Type Help To Show All The Command List.");
  }

  protected override void Run() {}
}


