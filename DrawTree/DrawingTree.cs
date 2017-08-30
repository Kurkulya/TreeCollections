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
        int dY = 0;
        int rad = 15;

        public void Draw(PictureBox pBox)
        {
            graph = pBox.CreateGraphics();
            dY = pBox.Height / (Height() + 1);
            DrawNode(root, 0, pBox.Width, 0, pBox.Width / 2, dY - 2 * rad);
        }

        private void DrawNode(Node node, int left, int right, int lvl, int pX, int pY)
        {
            if (node == null)
                return;

            int x = (left + right) / 2;
            int y = ++lvl * dY;

            graph.DrawLine(new Pen(Color.Black), x, y - rad, pX, pY + rad);        
            graph.DrawEllipse(new Pen(Color.Red, 3), x - rad, y - rad, 2 * rad, 2 * rad);
            graph.DrawString("" + node.val, new Font("Arial", 10 , FontStyle.Bold), Brushes.Blue, x - rad / 2, y - rad / 2);

            DrawNode(node.left, left, x, lvl, x, y);
            DrawNode(node.right, x, right, lvl, x, y);
        }
    }
}
