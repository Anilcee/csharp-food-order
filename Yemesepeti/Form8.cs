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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        public string kullaniciAdi;
        public string ad;
        public string soyad;
        public string eposta;
        private void Form8_Load(object sender, EventArgs e)
        {
            label2.Text += kullaniciAdi;
            label4.Text += ad;
            label5.Text+= soyad;
            label6.Text += eposta;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
