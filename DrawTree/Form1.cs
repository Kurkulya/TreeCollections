using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeCollections;

namespace DrawTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            BsTreeDraw tree = new BsTreeDraw();
            tree.Init(new int[] { 3, 7, 4, 9, 1, 12, 2, -5, 5 });
            tree.Draw(pBox);
        }
    }
}
