using Project12_JsonWebToken.JWT;
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

namespace Project12_JsonWebToken
{
    public partial class FrmLogin: Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=GUNESBERKANT\\SQLEXPRESS;initial catalog=Db12Project20;integrated security=true");
        private void btnLogin_Click(object sender, EventArgs e)
        {
            TokenGenerator tokenGen = new TokenGenerator();

            conn.Open();
            SqlCommand command = new SqlCommand("Select * From TblUser Where Username=@username and Password=@password",conn); // kullanıcı adı ve sifreyi veritabanıyla erismek icin param atadik
            command.Parameters.AddWithValue("@username", txtUser.Text);// sonra param degerlerimize disardan gelen degeleri atadik
            command.Parameters.AddWithValue("@password",txtPassword.Text);
            SqlDataReader dataReader = command.ExecuteReader(); // girilen verinin okunmasını sağladık.
            if(dataReader.Read()) // eger dataReader okuma yapabiliyorsa (kullanıcı adı ve sifre) databas
                                  // ile eslesiyorsa
            {
                string token = tokenGen.GenerateJwtToken2(txtUser.Text);   //egerki okuma islemi basariliysa bir token olustur. paramtre olarak da usernameden gelen değer verilir.
                //MessageBox.Show(token);
                FrmEmployee frm = new FrmEmployee();
                frm.tokenGet = token; // tokeni employee formuna tasidik.
                frm.Show(); // bana employee formunu aç
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password!");
                txtPassword.Clear();
                txtUser.Clear();
                txtUser.Focus();
            }
            conn.Close();    
        }
    }
}
