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
            var t=x.get_history();
            foreach(history y in t)
            {
                MessageBox.Show(y.date + " - " + y.final + " - " + y.min + " - " + y.max + " - " + y.count + " - " + y.volume + " - " + y.cost);
            }
        }
    }
}
