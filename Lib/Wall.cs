using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Wall : Mob
    {
        public float[] ZBuffer;
        public int side;
        public Wall()
        {
            ZBuffer = new float[320];
        }
    }
}
