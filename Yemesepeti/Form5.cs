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

namespace Yemesepeti
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6BORMV2\SQLSERVER; Initial Catalog=YemesepetiDB; Integrated Security=True;");
        SqlCommand cmd;
        public ListBox listBox1=new ListBox();
        public ListBox listBox2 = new ListBox();
        
        public int restoranID;
        public float toplamFiyat;
        public Label lbl = new Label();
        private void button3_Click(object sender, EventArgs e)
        {
            //baglanti.Open();
            //string kayit = "insert into tblOrders (UserID,RestaurantID,Cart,TotalPrice) values(@UserID,@RestaurantID,@Cart,@TotalPrice)";
            //SqlCommand cmd =new SqlCommand(kayit,baglanti);
            //cmd.Parameters.AddWithValue("@UserID",userID);
            //cmd.Parameters.AddWithValue("@RestaurantID",restoranID);
            //cmd.Parameters.AddWithValue("@Cart",listBox1.Items+" ");
            //cmd.Parameters.AddWithValue("@TotalPrice",toplamFiyat);
            //cmd.ExecuteNonQuery();
            //baglanti.Close();
            Form7 form7 = new Form7();
            this.Close();
            form7.Show();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            toplamFiyat = 0;
            listBox1.Size = new Size(500, 400);
            listBox1.Location = new Point(300, 200);
            listBox1.Font = new Font("Segoe UI", 14);
            listBox2.Size = new Size(90, 400);
            listBox2.Location = new Point(810, 200);
            listBox2.Font = new Font("Segoe UI", 14);
            Controls.Add(listBox1);
            Controls.Add(listBox2);
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                toplamFiyat += Convert.ToSingle(listBox2.Items[i]);
                listBox2.Items[i] += " TL";
            }
            Label fiyatTxt=new Label();
            fiyatTxt.Size = new Size(350, 200);
            fiyatTxt.Location = new Point(295, 600);
            fiyatTxt.Text = "Toplam Tutar : " + toplamFiyat + " TL";
            fiyatTxt.Font = new Font("Segoe UI", 13);
            Controls.Add(fiyatTxt);
            
            lbl.Text = form1.userID + " a " + restoranID + " " + toplamFiyat+"  "+form1.userID.ToString();
            lbl.Location = new Point(200, 600);
            Controls.Add(lbl);
            }

        private void label3_Click(object sender, EventArgs e)
        {
            Form3 form3= new Form3();
            this.Close();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            this.Close();
            form6.Show();
        }
    }
    }

