// this application doesn't run on old version
using System.IO;
using System.Drawing;
using kawaiios.system.CosmosDrawString;
using Cosmos.System.FileSystem;
using Cosmos.System.FilesSystem.VFS;

namespace KawaiiOS.Application.File_Explorer 
{
  class File_explorer:App {
      int textEachLine;
      public string text = string.Empty;

      public Notepad(uint width, uint height, uint x = 0, uint y = 0) : base(width, height, x, y)
        {
            //ASC16 = 16*8
            textEachLine = (int)width / 8;
            name = "File Explorer";
        }

      public override void _Update() 
        {
            app.update()
        }
      public void read_data() {}
  }
}
