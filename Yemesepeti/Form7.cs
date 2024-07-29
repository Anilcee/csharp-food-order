using Microsoft.VisualBasic.ApplicationServices;
using QRCoder;
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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public string fatura = "Sipariş Numaranız : 12 \n Sipariş Veren : admin \n Sepet : ";
        private void Form7_Load(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(fatura, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            pictureBox1.Image = qrCodeImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Create(@"C:\Users\anilc\OneDrive\Belgeler\siparisFaturasi2.txt");
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(fatura);
            //Bu bilgisayar\Honor 8X\Internal storage\Download
            //sw.Close();
            //File.Copy(C: \Users\anilc\OneDrive\Belgeler\siparisFaturasi2.txt,)
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
