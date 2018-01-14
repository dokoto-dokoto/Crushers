using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crushers
{
    public class SystemData
    {
        private Random random;
        public int StageNumber { get; set; }
        public int[] StageRow { get; set; }
        public void RandomInit()
        {
            StageRow = new int[16];
            for (int i = 0; i < StageRow.Length; i++)
            {
                StageRow[i] = random.Next(0, 4);
            }
        }
    }
}
