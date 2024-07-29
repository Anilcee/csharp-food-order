using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Yemesepeti
{
    public partial class Form3 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6BORMV2\SQLSERVER; Initial Catalog=YemesepetiDB; Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader reader;
        public Form3()
        {
            InitializeComponent();
        }
        public int userid;
        public List<Panel> paneller = new();
        public List<string> restoranIsımleri = new();
        public void Form3_Load(object sender, EventArgs e)
        {

            int pointX = 12, pointY = 200, sizeX = 554, sizeY = 182, a = 0, ID = 0;
            int pointXNew = 12, pointYNew = 220;
            cmd = new SqlCommand();
            baglanti.Open(); 
            cmd.Connection = baglanti;
            cmd.CommandText = "select count(*) from tblRestaurants";  
            cmd.ExecuteNonQuery(); 
            Int32 count = (Int32)cmd.ExecuteScalar(); 
            baglanti.Close();
            Panel[] panels = new Panel[count];       
            PictureBox[] imgs = new PictureBox[count];
            Label[] header = new Label[count];
            Label[] minOrderAmount = new Label[count];
            for (int i = 0; i < count; i++)
            {
                if (i == 6)
                {
                    ID = 1001;
                }
                string sorgu = "select RestaurantName from tblRestaurants where RestaurantID=" + (i + ID + 1); //gönderilen id'ye göre restoran isimlerini geri döndürüyor
                string sorgu2 = "select RestaurantImage from tblRestaurants where RestaurantID=" + (i + ID + 1); //gönderilen id'ye göre restoranın resmini getirir.
                cmd = new SqlCommand(sorgu, baglanti);
                baglanti.Open();
                string sonuc = (string)cmd.ExecuteScalar();
                baglanti.Close();
                panels[i] = new Panel();
                imgs[i] = new PictureBox();
                header[i] = new Label();
                imgs[i].Size = new Size(217, 176);
                imgs[i].Location = new Point(3, 3);
                imgs[i].SizeMode = PictureBoxSizeMode.StretchImage;
                baglanti.Open();
                cmd=new SqlCommand(sorgu2, baglanti);
                if (cmd.ExecuteScalar() is not DBNull)
                {
                    byte[] image = (byte[])cmd.ExecuteScalar();
                    imgs[i].Image = byteToImage(image);
                }
                
                imgs[i].Name = sonuc;
                panels[i].Name = sonuc;
                header[i].Name = sonuc;
                panels[i].Size = new Size(554, 182);
                panels[i].Location = new Point(pointXNew, pointYNew);
                header[i].Font = new Font("Segoe UI", 18);
                header[i].Text = sonuc;
                header[i].Location = new Point(246, 16);
                header[i].Size = new Size(245, 120);
                this.Controls.Add(panels[i]);
                panels[i].Controls.Add(header[i]);
                panels[i].BackColor = Color.White;
                panels[i].Controls.Add(imgs[i]);
                panels[i].Click += panelOnClick;
                imgs[i].Click += panelOnClick;
                header[i].Click += panelOnClick;
                paneller.Add(panels[i]);
                restoranIsımleri.Add(sonuc);


                if (a % 2 == 0)
                    pointXNew += sizeX + 40;
                else
                {
                    pointXNew = pointX;
                    pointYNew += pointY + 10;
                }
                a++;
                baglanti.Close();
            }
            ID++;
        }
        public void panelOnClick(object sender, EventArgs e)
        {
            string restoranAdi = "";
            if (sender is Panel)
            {
                Panel panel = sender as Panel;
                restoranAdi = panel.Name;
            }
            else if (sender is PictureBox)
            {
                PictureBox pictureBox = sender as PictureBox;
                restoranAdi = pictureBox.Name;
            }
            else if(sender is Label)
            {
                Label label = sender as Label;
                restoranAdi=label.Text;
            }
            Form4 form4 = new Form4();
            form4.restoran = restoranAdi;
            this.Close();
            form4.Show();

        }
        public Image byteToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int pointX = 12, pointY = 200, sizeX = 554, sizeY = 182, a = 0, pointXNew = 12, pointYNew = 220; ;
            string sorgu = "select RestaurantName from tblRestaurants where RestaurantName like '%" + textBox1.Text + "%'";
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = sorgu;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader["RestaurantName"].ToString());
            }
            for (int i = 0; i < paneller.Count; i++)
            {
                paneller[i].Hide();
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < paneller.Count; j++)
                {
                    if (list[i] == restoranIsımleri[j] && list[i] != null)
                    {
                        paneller[j].Location = new Point(pointXNew, pointYNew);
                        if (a % 2 == 0)
                            pointXNew += sizeX + 40;
                        else
                        {
                            pointXNew = pointX;
                            pointYNew += pointY + 10;
                        }
                        a++;
                        paneller[j].Show();
                    }
                    else
                    {

                    }
                }

            }

            baglanti.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Restoran ara")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Restoran ara";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.userid = userid;
            this.Close();
            form6.Show();
        }
    }

    }


