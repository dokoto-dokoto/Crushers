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
            var result = System.Windows.Forms.MessageBox.Show("フルスクリーンで起動しますか？", "Crushers", System.Windows.Forms.MessageBoxButtons.YesNo);
            var option = new asd.EngineOption();
            option.IsFullScreen = result == System.Windows.Forms.DialogResult.Yes;

            asd.Engine.Initialize("Crushers", 800, 800, option);

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();

        }
    }
}
