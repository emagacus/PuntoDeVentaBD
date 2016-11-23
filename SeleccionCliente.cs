using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace LoncheriaToñita
{
    public partial class SeleccionCliente : Form
    {
        string dbsettings;
        public SeleccionCliente(string dbsettings)
        {
            InitializeComponent();
            this.dbsettings = dbsettings;
            IdCliente = "1";
            


        }


        public string IdCliente { get; set; }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SelectId();
        }


        void SelectId()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                IdCliente = item.SubItems[0].Text;
            }
            else { IdCliente = "1"; }


            this.Close();

        }


        private void butonBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }



        void Buscar()
        {
            listView1.Items.Clear();
            List<string> clientes = GetClientList(textBox1.Text).ToList();
            NpgsqlConnection con = new NpgsqlConnection(dbsettings);
            con.Open();
            var command = con.CreateCommand();
            foreach (string s in clientes)
            {
                command.CommandText = "select nombre from cliente where id = " + s + "";
                string nombre = command.ExecuteScalar().ToString();
                string[] data = { s, nombre };
                ListViewItem item = new ListViewItem(data);
                listView1.Items.Add(item);
            }

            listView1.Focus();
        }


        public IEnumerable<string> GetClientList(string busqueda)
        {
            using (var connection = new NpgsqlConnection(dbsettings))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "select id from cliente where UPPER(nombre) like UPPER('%" + busqueda + "%')"; // update select command accordingly
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader.GetString(reader.GetOrdinal("id"));
                    }
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Buscar();

            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                SelectId();
            }
        }
    }
}
