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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        public string dogrulamaKodu;
        public int userid;
        private void button1_Click(object sender, EventArgs e)
        {
            if(dogrulamaKodu == textBox1.Text)
            {
                this.Close();
                Form3 form3 = new Form3();
                form3.userid = userid;
                form3.Show();
            }
        }
    }
}
