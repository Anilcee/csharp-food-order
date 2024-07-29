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
    public partial class Form10 : Form
    {
        public int userid;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6BORMV2\SQLSERVER; Initial Catalog=YemesepetiDB; Integrated Security=True;");
        SqlCommand cmd;
        SqlDataReader reader;
        public Form10()
        {
            InitializeComponent();
            
        }
        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form10_Load(object sender, EventArgs e)
        {
            label1.Text += userid;
            cmd = new SqlCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "select count(*) from tblOrders where UserID='"+userid+"'";
            cmd.ExecuteNonQuery();
            Int32 count = (Int32)cmd.ExecuteScalar();
            Panel[] panels = new Panel[count];
            Label[] sepet =new Label[count];
            Label[] tutar=new Label[count];

        }
    }
}
