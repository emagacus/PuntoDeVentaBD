using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoncheriaToñita
{
    public partial class DBsettings : Form
    {
        public DBsettings()
        {
            InitializeComponent();


            textBox1.Text = Convert.ToString(Properties.Settings.Default.PropertyValues["DBHost"].PropertyValue);
            textBox2.Text = Convert.ToString(Properties.Settings.Default.PropertyValues["DBUser"].PropertyValue);
            textBox3.Text = DecryptString(Convert.ToString(Properties.Settings.Default.PropertyValues["DBpassword"].PropertyValue));
            textBox4.Text = Convert.ToString(Properties.Settings.Default.PropertyValues["DBName"].PropertyValue);

        }

        byte[] entropy = Encoding.Unicode.GetBytes("Salt Is Not A Password");

        public string EncryptString(string input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                Encoding.Unicode.GetBytes(input),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        string DecryptString(string encryptedData)
        {

            byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decryptedData);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {




            if (checkBox1.Checked == true)
            {
                foreach (Control c in Controls)
                {
                    if (c.GetType() != checkBox1.GetType())
                    {
                        c.Enabled = true;
                    }
                }

            }
            else
            {
                /*
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false
                */

            foreach(Control c in Controls)
                {
                    if(c.GetType() != checkBox1.GetType())
                    {
                        c.Enabled = false;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            /*
            Properties.Settings.Default.DBHost = textBox1.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.DBUser = textBox2.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.DBpassword = EncryptString(textBox3.Text);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.DBUser = textBox4.Text;
            Properties.Settings.Default.Save();
           // Properties.Settings.Default.Reload();
            Properties.Settings.Default.Upgrade();
    */
            Properties.Settings.Default.PropertyValues["DBHost"].PropertyValue = textBox1.Text;
            Properties.Settings.Default.PropertyValues["DBUser"].PropertyValue= textBox2.Text;
            Properties.Settings.Default.PropertyValues["DBpassword"].PropertyValue = EncryptString(textBox3.Text);
            Properties.Settings.Default.PropertyValues["DBName"].PropertyValue = textBox4.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("GUARDADO!", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }
    }
}
