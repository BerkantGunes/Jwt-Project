using Project12_JsonWebToken.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project12_JsonWebToken
{
    public partial class FrmEmployee: Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        public string tokenGet; // bunun deger ataması login butonundan gelecek.
        SqlConnection conn = new SqlConnection("Server=GUNESBERKANT\\SQLEXPRESS;initial catalog=Db12Project20;integrated security=true");
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            TokenValidator tokenValid = new TokenValidator();

            rtbToken.Text = tokenGet; // tokenımızı bu forma tasidk

            var principal = tokenValid.ValidateJwtToken(tokenGet);// tokenGetten gelen parametreye göre doğrulama gerçekleştirdik.

            if(principal!=null) // bu forma tasinan tokenın degeri null dan farklıysa
            {
                string username = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                // ? demek null olsa bile calistir demek
                MessageBox.Show("Welcome: " + username);

                conn.Open();
                string query = "Select * From TblEmployee";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DGVEmployee.DataSource = dt;
                conn.Close();
            }
            else
            {
                MessageBox.Show("Invalid Token!");
            }
            
        }
    }
}
