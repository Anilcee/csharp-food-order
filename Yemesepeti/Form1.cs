using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Mail;
using System.Net;

namespace Yemesepeti
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6BORMV2\SQLSERVER; Initial Catalog=YemesepetiDB; Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader reader;
        
        Form8 form8 = new Form8();
        public int userID;
        public Form1()
        {
            InitializeComponent();
        }
        Form4 form4 = new Form4();
        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '*'; 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string userName=txtUser.Text;
            string password=txtPass.Text;
            string userMail="";
            string ad="";
            string soyad="";
            string userIdSorgu = "select UserID from tblCustomers where UserName='"+userName+"'";
            baglanti.Open();
            cmd = new SqlCommand(userIdSorgu, baglanti);
            userID = (int)cmd.ExecuteScalar();
            baglanti.Close();
            cmd =new SqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "Select * from tblCustomers where UserName='" + userName + "' and UserPassword='" + password + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                userMail = reader["UserMail"].ToString();
                int userID = (int)reader["UserID"];
                Random random = new Random();
                int dogrulamaKodu = random.Next(100000, 999999);
                Form9 form9=new Form9();
                form9.dogrulamaKodu = dogrulamaKodu.ToString();
                Form3 form3 = new Form3();
                form3.userid = userID;
                form9.userid = userID;

                if (reader["UserMail"] is not DBNull)
                {
                    SmtpClient sc = new SmtpClient();
                    sc.Port = 587;
                    sc.Host = "smtp-mail.outlook.com";
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential("anillcengiz@outlook.com", "aa1234567890");
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("anillcengiz@outlook.com", "Yemesepeti");
                    mail.To.Add(userMail);
                    mail.Subject = "Yemesepeti Do�rulama Kodu";
                    mail.IsBodyHtml = true;
                    mail.Body = "Yemesepeti uygulamas�na giri� yapmak i�in do�rulama kodunuz : " + dogrulamaKodu;
                   sc.Send(mail);
                    this.Hide();
                    form9.Show();
                }
                else
                {
                    this.Hide();
                    form3.Show();
                }
                
            }
            
            else
            {
                MessageBox.Show("Kullan�c� ad� veya �ifre hatal�");
            }
            
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}