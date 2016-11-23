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
    public partial class BaseForm : Form
    {

        NpgsqlConnection dbconexion;
        string Dbsettings;


        public BaseForm(string Dbsettings)
        {
            InitializeComponent();
            this.Dbsettings = Dbsettings;
            dbconexion = new NpgsqlConnection(Dbsettings);
            WindowState = FormWindowState.Maximized;
        }


        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void DisposeForms(Form form)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm != form)
                {
                    frm.Close();
                }
            }
            return;
        }

        private void ventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
                VentaForm vform = new VentaForm(dbconexion,Dbsettings);
                DisposeForms(vform);
                vform.MdiParent = this;
                vform.Show();
                vform.Location = new Point(0, 0);
            if (WindowState == FormWindowState.Maximized)
            {
                vform.Size = new Size(Width - 4, this.Height - 4);
            }else
            {
                WindowState = FormWindowState.Maximized;
                vform.Size = new Size(Width - 4, Height - 4);
            }
        }

        private void baseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DBsettings oform = new DBsettings();
            DisposeForms(oform);
            oform.MdiParent = this;
            oform.Show();
            oform.Location = new Point(0, 0);
            if (WindowState == FormWindowState.Maximized)
            {
                oform.Size = new Size(Width - 4, this.Height - 4);
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                oform.Size = new Size(Width - 4, Height - 4);
            }

        }

        private void cOMPRASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
