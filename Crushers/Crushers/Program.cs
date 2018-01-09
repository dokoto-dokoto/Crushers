using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = System.Windows.Forms.MessageBox.Show("フルスクリーンで起動しますか？", Config.Window.Title, System.Windows.Forms.MessageBoxButtons.YesNo);
            var option = new asd.EngineOption
            {
                IsFullScreen = result == System.Windows.Forms.DialogResult.Yes
            };

            asd.Engine.Initialize(Config.Window.Title, Config.Window.Width, Config.Window.Height, option);

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }

            asd.Engine.Terminate();
        }
    }
}
