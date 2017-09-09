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
    public class BsTreeDraw : BsTreeBR
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

            SolidBrush color = new SolidBrush((node.color == Color.Black) ? System.Drawing.Color.Black : System.Drawing.Color.Red);
            graph.DrawLine(new Pen(System.Drawing.Color.Black), x, y - rad, data.pX, data.pY + rad);        
            graph.FillEllipse(color, x - rad, y - rad, 2 * rad, 2 * rad);
            graph.DrawString("" + node.val, new Font("Arial", 10 , FontStyle.Bold), Brushes.White, x - rad / 2, y - rad / 2);

            data.Init(left , x , lvl, x, y);
            DrawNode(node.left, data);

            data.Init(x, right, lvl, x, y);
            DrawNode(node.right, data);
        }
    }
}
