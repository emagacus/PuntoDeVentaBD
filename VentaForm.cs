using Npgsql;
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
    public partial class VentaForm : Form
    {
        NpgsqlConnection dbconexion;
        string DBsettings;

        public VentaForm(NpgsqlConnection dbconexione, string DBsettings)
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            SeleccionCliente sc = new SeleccionCliente(DBsettings);
            sc.ShowDialog();
            textBoxCliente.Text = sc.IdCliente;
            this.DBsettings = DBsettings;
            dbconexion = new NpgsqlConnection(DBsettings);
            var command = dbconexion.CreateCommand();
            dbconexion.Open();
            command.CommandText = "insert into venta(id_cliente,total,fecha,hora) values (" + textBoxCliente.Text + ",0,current_date,current_time);";
            command.ExecuteNonQuery();
            command.CommandText = "select max(folio) from venta;";
            textBoxFolio.Text = Convert.ToString(command.ExecuteScalar());
            numericUpDown1.Value = 1;
            List<string> productos = GetproductList().ToList();
            foreach(string s in productos) { comboBox1.Items.Add(s); }
 
        }



        
        public IEnumerable<string> GetproductList()
        {
            using (var connection = new NpgsqlConnection(DBsettings))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "select descripcion from producto"; // update select command accordingly
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader.GetString(reader.GetOrdinal("descripcion"));
                    }
                }
            }
        }










        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = new NpgsqlConnection(DBsettings);
            var command = con.CreateCommand();
            con.Open();
            command.CommandText = "Select id from producto where descripcion='" + comboBox1.Text + "'";
            string id = command.ExecuteScalar().ToString();
            command.CommandText = "Select costo from producto where descripcion='" + comboBox1.Text + "'";
            string costo = command.ExecuteScalar().ToString();
            string cantidad = numericUpDown1.Value.ToString();
            string total =Convert.ToString(decimal.Parse(costo) * numericUpDown1.Value);
            string[] itemData = { id, comboBox1.Text, costo,cantidad,total};
            ListViewItem item =new ListViewItem(itemData);
            listView1.Items.Add(item);

            decimal Rtotal = 0;

            foreach(ListViewItem a in listView1.Items)
            {
                Rtotal = Rtotal + Convert.ToDecimal(a.SubItems[4].Text);
            }

            textBoxTotal.Text = Convert.ToString(Rtotal);

            if (!button2.Enabled) { button2.Enabled = true; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = new NpgsqlConnection(DBsettings);
            con.Open();
            var command = con.CreateCommand();

            foreach(ListViewItem i in listView1.Items)
            {
                command.CommandText = "insert into detalle_venta (folioventa,idprod,cantidad,precio) values (" + textBoxFolio.Text + "," + i.SubItems[0].Text + "," + i.SubItems[3].Text + "," + i.SubItems[2].Text + ");";
                command.ExecuteNonQuery();
            }

            this.Close();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            var con = new NpgsqlConnection(DBsettings);
            con.Open();
            var command = con.CreateCommand();
            command.CommandText = "delete from venta where folio = " + textBoxFolio.Text + ";";
            command.ExecuteNonQuery();
            Close();
        }
    }
}
