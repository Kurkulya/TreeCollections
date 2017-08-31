using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTree
{
    public class XData
    {
        public int pX;
        public int pY;
        public int lvl;
        public int left;
        public int right;
        
        public XData(int left, int right, int lvl, int pX, int pY)
        {
            Init(left, right, lvl, pX, pY);
        }

        public void Init(int left, int right, int lvl, int pX, int pY)
        {
            this.left = left;
            this.right = right;
            this.lvl = lvl;
            this.pX = pX;
            this.pY = pY;
        }
    }
}
