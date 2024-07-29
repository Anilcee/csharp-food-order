using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yemesepeti
{
    public partial class Form4 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6BORMV2\SQLSERVER; Initial Catalog=YemesepetiDB; Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader reader;
        public Form4()
        {
            InitializeComponent();

        }
        public string restoran;
        public int userID;
        int z=0;
        public List<Panel> paneller=new();
        public List<string> yemekIsimleri = new List<string>();
        public List<float> yemekFiyatlari = new List<float>();
        Form5 form5 = new Form5();
        public void Form4_Load(object sender, EventArgs e)
        {
            int sizeX = 541, sizeY = 186, pointXNew = 12, pointYNew = 523, pointX = 15, pointY = 180, a = 0,c=0;
            label1.Text = restoran;
            string foodCategoryID="";

            cmd = new SqlCommand();
            baglanti.Open();
            string restoranResmi = "select RestaurantImage from tblRestaurants where RestaurantName='" + restoran + "'";
            cmd = new SqlCommand(restoranResmi, baglanti);
            if (cmd.ExecuteScalar() is not DBNull)
            {
                byte[] image = (byte[])cmd.ExecuteScalar();
                pictureBox1.Image = byteToImage(image);
            }
            baglanti.Close();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "select count(FoodName) from tblFoods where FoodCategoryID=(select RestaurantCategoryID from tblRestaurants where RestaurantName= '" + restoran + "')";
            cmd.ExecuteNonQuery(); //Sorgu sql'e gönderildi
            Int32 count = (Int32)cmd.ExecuteScalar(); //gelen sonuç count değişkenine aktarıldı
            baglanti.Close();
            string[] foodName = new string[count];
            string[] foodDesc = new string[count];
            Image[] foodImage = new Image[count];
            float[] foodPrice = new float[count];
            Panel[] panels = new Panel[count];           //Yemek sayısı kadar paneller oluşturuldu
            PictureBox[] imgs = new PictureBox[count];
            Label[] foodNames = new Label[count];
            Label[] foodDescs= new Label[count];
            Label[] foodPrices= new Label[count];
            Button[] sepetButon = new Button[count]; 
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "select * from tblFoods where FoodCategoryID = (select RestaurantCategoryID from tblRestaurants where RestaurantName= '" + restoran + "')";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foodName[c] = reader["FoodName"].ToString();
                foodDesc[c] = reader["FoodDesc"].ToString();
                 foodCategoryID = reader["FoodCategoryID"].ToString();
                byte[] image = (byte[])reader["FoodImage"];
                foodImage[c] = byteToImage(image);
                foodPrice[c] = Convert.ToSingle(reader["FoodPrice"]);
                c++;
            }
            reader.Close();
            baglanti.Close();
            string sorgu = "select CategoryName from tblCategory where CategoryID= " + foodCategoryID;
            baglanti.Open();
            cmd = new SqlCommand(sorgu, baglanti);
            string foodCategory=(string) cmd.ExecuteScalar();
            label4.Text += " " + foodCategory;
            for (int i = 0; i < count; i++)
            {
                foodNames[i] = new Label();
                foodDescs[i] = new Label();
                foodPrices[i] = new Label();
                panels[i] = new Panel();
                imgs[i] = new PictureBox();
                sepetButon[i] = new Button();
                panels[i].Size = new Size(541, 166);
                panels[i].Location = new Point(pointXNew, pointYNew);
                imgs[i].Size = new Size(158, 159);
                imgs[i].Location = new Point(5, 5);
                imgs[i].SizeMode = PictureBoxSizeMode.StretchImage;
                imgs[i].Image = foodImage[i];
                foodNames[i].Size = new Size(288, 32);
                foodNames[i].Font = new Font("Segoe UI", 14);
                foodNames[i].ForeColor = Color.Black;
                foodNames[i].Location = new Point(177, 12);
                foodNames[i].Text = foodName[i];
                foodDescs[i].Size = new Size(312, 60);
                foodDescs[i].Font = new Font("Segoe UI", 9);
                foodDescs[i].ForeColor = Color.DarkSlateGray;
                foodDescs[i].Location = new Point(177, 61);
                foodDescs[i].Text=foodDesc[i];
                foodPrices[i].Size = new Size(90, 25);
                foodPrices[i].Font = new Font("Segoe UI", 11,FontStyle.Bold);
                foodPrices[i].ForeColor = Color.Black;
                foodPrices[i].Location = new Point(177, 134);
                foodPrices[i].Text = foodPrice[i].ToString()+" TL";
                sepetButon[i].Size = new Size(135, 35);
                sepetButon[i].Font = new Font("Segoe UI", 10);
                sepetButon[i].ForeColor = Color.White;
                sepetButon[i].BackColor = Color.FromArgb(208, 34, 83);
                sepetButon[i].Location = new Point(430, 128);
                sepetButon[i].Text = "Sepete Ekle";
                sepetButon[i].TextAlign=ContentAlignment.MiddleLeft;
                sepetButon[i].Name = foodName[i].ToString();
                sepetButon[i].FlatStyle = FlatStyle.Flat;
                

                if (a % 2 == 0)
                    pointXNew += sizeX + 40;
                else
                {
                    pointXNew = pointX;
                    pointYNew += pointY + 30;
                }
                a++;
                Controls.Add(panels[i]);
                panels[i].BackColor = Color.White;
                panels[i].Controls.Add(imgs[i]);
                panels[i].Controls.Add(foodNames[i]);
                panels[i].Controls.Add(foodDescs[i]);
                panels[i].Controls.Add(foodPrices[i]);
                panels[i].Controls.Add(sepetButon[i]);
                baglanti.Close();
                sepetButon[i].Click += sepetEkle;
                paneller.Add(panels[i]);
                yemekIsimleri.Add(foodName[i]);
                yemekFiyatlari.Add(foodPrice[i]);

            }
            label3.Click += Label3_Click;
            string restoranIdSorgu = "select RestaurantID from tblRestaurants where RestaurantName='" + restoran + "'";
            baglanti.Open();
            cmd = new SqlCommand(restoranIdSorgu, baglanti);
            int restoranID = (int)cmd.ExecuteScalar();
            form5.restoranID = restoranID;

        }
        int i = 0;

        private async void sepetEkle(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            for (int i = 0; i < yemekIsimleri.Count; i++)
            {
                if (btn.Name == yemekIsimleri[i])
                {
                    form5.listBox1.Items.Add(yemekIsimleri[i]);
                    form5.listBox2.Items.Add(yemekFiyatlari[i]);
                }
            }

            btn.Text = "Ürün eklendi";
            Task.Delay(300).Wait();
            btn.Text = "Sepete Ekle";

        }
        private void Label3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 form3 = new Form3();
            form3.Show();
        }

        public Image byteToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            form5.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int sizeX = 541, sizeY = 186, pointXNew = 12, pointYNew = 523, pointX = 15, pointY = 180, a = 0;
            string sorgu = "select FoodName from tblFoods where FoodCategoryID = (select RestaurantCategoryID from tblRestaurants where RestaurantName = '" + restoran + "') and FoodName like '%"+textBox1.Text+"%'";
            baglanti.Open();
            cmd.Connection= baglanti;
            cmd.CommandText = sorgu;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader["FoodName"].ToString());
            }
            for (int i = 0; i < paneller.Count; i++)
            {
                paneller[i].Hide();
            }
            for (int i = 0; i < list.Count; i++)
            {
                for(int j=0; j < paneller.Count; j++)
                {
                    if (list[i] == yemekIsimleri[j] && list[i]!=null)
                    {
                        paneller[j].Location = new Point(pointXNew,pointYNew);
                        if (a % 2 == 0)
                            pointXNew += sizeX + 40;
                        else
                        {
                            pointXNew = pointX;
                            pointYNew += pointY + 30;
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

        private void textbox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text=="Yiyecek, içecek ara")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Yiyecek, içecek ara";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.userid = userID;
            this.Close();
            form6.Show();
        }
    }
}
