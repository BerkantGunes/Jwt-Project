using Project12_JsonWebToken.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project12_JsonWebToken
{
    public partial class FrmTokenGenerator: Form
    {
        public FrmTokenGenerator()
        {
            InitializeComponent();
        }

        private void btnGenerateJWT_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string email = txtEmail.Text;

            TokenGenerator tokenGen = new TokenGenerator();
            string token = tokenGen.GenerateJwtToken(username,email,name,surname);
            rtbToken.Text = token;
        }
    }
}
