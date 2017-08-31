using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeCollections;


namespace DrawTree
{
    public class BsTreeDraw : BsTree
    {
        Graphics graph = null;
        int dY;
        int rad;

        public void Draw(PictureBox pBox)
        {
            graph = pBox.CreateGraphics();

            rad = 15;
            dY = pBox.Height / (Height() + 1);

            XData data = new XData(0,pBox.Width,0,pBox.Width / 2, dY - 2 * rad);

            DrawNode(root, data);
        }

        private void DrawNode(Node node, XData data)
        {
            if (node == null)
                return;

            int left = data.left;
            int right = data.right;
            int lvl = data.lvl;

            int x = (left + right) / 2;
            int y = ++lvl * dY;

            graph.DrawLine(new Pen(Color.Black), x, y - rad, data.pX, data.pY + rad);        
            graph.DrawEllipse(new Pen(Color.Red, 3), x - rad, y - rad, 2 * rad, 2 * rad);
            graph.DrawString("" + node.val, new Font("Arial", 10 , FontStyle.Bold), Brushes.Blue, x - rad / 2, y - rad / 2);

            data.Init(left , x , lvl, x, y);
            DrawNode(node.left, data);

            data.Init(x, right, lvl, x, y);
            DrawNode(node.right, data);
        }
    }
}
