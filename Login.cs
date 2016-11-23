using LoncheriaToñita.Properties;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoncheriaToñita
{
    public partial class Login : Form
    {


        public Login()
        {
           
            InitializeComponent();

            //



            ///

            textBox2.Select();
            Size size = new Size(160, 160);
            pictureBox1.Image = new Bitmap(Resources.icon_user, size);
            // PsqlConnection con = new PsqlConnection();
            // con.conexion();
            //  NpgsqlConnection conect = con.conexion();

            
        }

         byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");

        public string EncryptString(string input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                Encoding.Unicode.GetBytes(input),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }


        public string DecryptString(string encryptedData)
        {
            
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(decryptedData);
           
        }

    

     string DBsettings()
        {


            string ConectString = "";
                
            ConectString=ConectString+ "host= " +    Convert.ToString(Properties.Settings.Default.PropertyValues["DBHost"].PropertyValue);

            ConectString = ConectString + "; Username= " + Convert.ToString(Properties.Settings.Default.PropertyValues["DBUser"].PropertyValue);

            ConectString = ConectString + "; Password= " + DecryptString(Convert.ToString(Properties.Settings.Default.PropertyValues["DBpassword"].PropertyValue));

            ConectString = ConectString + ";Database= " + Convert.ToString(Properties.Settings.Default.PropertyValues["DBName"].PropertyValue);
            Console.WriteLine(ConectString);

            return ConectString;
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {


                if (textBox2.Text == Settings.Default.User && textBox1.Text == DecryptString(Settings.Default.Password))
                {

                    BaseForm basefrm = new BaseForm(DBsettings());
                    basefrm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Contraseña o usuario incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }
    }
}
