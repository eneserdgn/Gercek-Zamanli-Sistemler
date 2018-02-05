using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //Access veritabanını kullanıcağımızı söylüyoruz.

namespace GZSProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection connection;
        OleDbCommand commandKullanici;
        OleDbDataReader dataRead;


        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;  //Textbox1 den kullanıcı adını alıyoruz.
            string sifre = textBox2.Text;  //Textbox2 den şifreyi alıyoruz.
            connection = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kullanici.accdb");  //Hangi veritabanına bağlanıcağımızı söylüyoruz. Veritabanı Projemizin bin/debug klasöründe. Başka bi yerde tutmak istersek yolunu belirtmemiz gerekir.
            commandKullanici = new OleDbCommand();  //veritabanı sorgusu yapmamız için tanımlıyoruz.
            connection.Open();   //bağlantımızı açıyoruz.
            commandKullanici.Connection = connection;  
            commandKullanici.CommandText = "SELECT * FROM kullanici where k_ad='" + textBox1.Text + "' AND k_sifre='" + textBox2.Text + "'";  //Veritabanı sorgusu
            dataRead = commandKullanici.ExecuteReader(); //gelen veriyi okutuyoruz.
            if (dataRead.Read())  //Eğer veri geldiyse
            {
                this.Hide();  //Bu formu kapatıyoruz.
                Form2 f2 = new Form2(); //Tanımlama
                f2.Show(); //Form2 ye geçiş yap.
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifre yanlış");   //Yanlış girilmiş ise Hata Mesajı 
            }

            connection.Close();   //Bağlantımızı kapatıyoruz.

        }
    }
}
