using LoncheriaToñita.Properties;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoncheriaToñita
{
    public class PsqlConnection
    {


       public PsqlConnection()
        {
            

        }

        string DecryptString(string encryptedData)
        {

            byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                Convert.FromBase64String(encryptedData),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decryptedData);

        }

        byte[] entropy = Encoding.Unicode.GetBytes("Salt Is Not A Password");

        public NpgsqlConnection conexion()
        {


            string ConectString= Convert.ToString(Settings.Default.PropertyValues["DBHost"].PropertyValue);

            ConectString =ConectString+"; Username= "+ Convert.ToString(Properties.Settings.Default.PropertyValues["DBUser"].PropertyValue);

            ConectString =ConectString +"; Password= " + DecryptString(Convert.ToString(Properties.Settings.Default.PropertyValues["DBpassword"].PropertyValue));

            ConectString = ConectString +";Database= "+ Convert.ToString(Properties.Settings.Default.PropertyValues["DBName"].PropertyValue);
            Console.WriteLine(ConectString);
            

            
            NpgsqlConnection c = new NpgsqlConnection();
                //ConectString);
                //"Host= localhost; Username=postgres; password=juanito1; Database= punto_de_venta");
            return c;
        }
    }
}
