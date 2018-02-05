using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;  // Access bağlantısı kurabilmek için.

namespace GZSProje
{
    public partial class Form3 : Form
    {

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;


        public Form3()
        {
            InitializeComponent();
        }


        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kullanici.accdb");
            da = new OleDbDataAdapter("SElect *from doluluk", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "doluluk");
            dataGridView1.DataSource = ds.Tables["doluluk"];
            con.Close();
        }

        void griddoldur2()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kullanici.accdb");
            da = new OleDbDataAdapter("SElect * from textMessage", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "textMessage");
            dataGridView1.DataSource = ds.Tables["textMessage"];
            con.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

    }
}
