using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tse_crawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gettseinfo x = new gettseinfo();

            MessageBox.Show(x.downloadString("http://www.tsetmc.com/tsev2/data/search.aspx?skey=%D8%B4%D8%B3%D8%AA%D8%A7"));
        }
    }
}
