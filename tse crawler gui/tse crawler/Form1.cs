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
            var t = x.get_table(x.get_table_url(x.search("حبندر")[0].id));
            MessageBox.Show(t.row1sellcount+"-"+t.row1sellvolume+"-"+t.row1sellcost+"|"+ t.row1buycost + "-" + t.row1buyvolume + "-" + t.row1buycount);
            MessageBox.Show(t.row2sellcount+"-"+t.row2sellvolume+"-"+t.row2sellcost+"|"+ t.row2buycost + "-" + t.row2buyvolume + "-" + t.row2buycount);
            MessageBox.Show(t.row3sellcount+"-"+t.row3sellvolume+"-"+t.row3sellcost+"|"+ t.row3buycost + "-" + t.row3buyvolume + "-" + t.row3buycount);
        }
    }
}
