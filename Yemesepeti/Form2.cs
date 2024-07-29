using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Yemesepeti
{
    public partial class Form2 : Form
    {
        SqlConnection baglanti= new SqlConnection(@"Data Source=DESKTOP-6BORMV2\SQLSERVER; Initial Catalog=YemesepetiDB; Integrated Security=True;");
        SqlCommand cmd;
        public Form2()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text.Length >= 5 && txtUsername.Text.Length >= 5)
            {
                if (txtPassword.Text == txtPassword2.Text)
                {
                    string userName = txtUsername.Text;
                    string password = txtPassword.Text;
                    string password2 = txtPassword2.Text;
                    string userTel = txtTel.Text;
                    string userFirstName = txtFirstName.Text;
                    string userLastName = txtLastName.Text;
                    baglanti.Open();
                    string kayit = "insert into tblCustomers(UserName,UserFirstName,UserLastName,UserPassword,UserTel) values (@UserName,@UserFirstName,@UserLastName,@UserPassword,@UserTel)";
                    cmd = new SqlCommand(kayit, baglanti);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@UserFirstName", userFirstName);
                    cmd.Parameters.AddWithValue("@UserLastName", userLastName);
                    cmd.Parameters.AddWithValue("@UserPassword", password);
                    cmd.Parameters.AddWithValue("@UserTel", userTel);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarılı");
                    baglanti.Close();

                }
                else
                {
                    MessageBox.Show("Girdiğiniz şifreler aynı değil");
                }
            }
            else
            {
                MessageBox.Show("Girdiğiniz bilgiler gereksinimleri karşılamıyor!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
            
        }
    }
}
