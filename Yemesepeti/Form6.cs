using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yemesepeti
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        public int userid;
        private void panel2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form8 form8 = new Form8();
            form8.Show();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form10 form10 = new Form10();
            form10.userid= userid;
            form10.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Close();
            form3.Show();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
