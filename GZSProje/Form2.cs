using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports; //BT için Kütüphanemizi Ekledik
using System.Data.OleDb; //Access veritabanını kullanıcağımızı söylüyoruz.

namespace GZSProje
{
    public partial class Form2 : Form
    {
        private SerialPort benimportum;
        public string mesaj;
        public string doluluk;
        public string gelenVeri;


        OleDbConnection connectiondoluluk;
        OleDbCommand commanddoluluk;
        OleDbCommand commandtxt;
        OleDbDataReader dataReaddoluluk;
       

        


        public Form2()
        {
            InitializeComponent();

            connectiondoluluk = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kullanici.accdb");  //Hangi veritabanına bağlanıcağımızı söylüyoruz. Veritabanı Projemizin bin/debug klasöründe. Başka bi yerde tutmak istersek yolunu belirtmemiz gerekir.
            connectiondoluluk.Open();   //bağlantımızı açıyoruz.
            commanddoluluk = new OleDbCommand("insert into doluluk(tarih,saat) values (@tarih,@saat)", connectiondoluluk);  //veritabanı sorgusu yapmamız için tanımlıyoruz.
            commandtxt = new OleDbCommand("insert into textMessage(txtMsgTarih,icerik) values (@txtMessageTarih,@icerik)", connectiondoluluk);  //veritabanı sorgusu yapmamız için tanımlıyoruz.

            progressBar1.Minimum = 0;       //Progressbar ayarlaması
            progressBar1.Maximum = 100;     //Progressbar ayarlaması
            progressBar1.Step = 1;          //Progressbar ayarlaması
            progressBar1.Style = ProgressBarStyle.Marquee;      //Progressbar ayarlaması
           

           
            init();         //Port ayarları

            timer1.Interval = 100; //1 saniye     //Nekadar sürede bir çalışacağı
            timer1.Start();


        }
  

        private void init()     //Port ayarları
        {
            try     //Eğer işlem yapılırsa
            {
                benimportum = new SerialPort();
                benimportum.BaudRate = 9600;
                benimportum.PortName = "COM8";
                benimportum.Open();

            }
            catch(Exception)        //Hata çıkarsa
            {
                MessageBox.Show("Port ayarlarını kontrol et");
            }

            
            
            
        }

     
        private void timer1_Tick(object sender, EventArgs e)            // 1 sn de bir doluluğa göre progress barımızı güncelleme
        {

            gelenVeri=benimportum.ReadLine();    //Arduinodan gelen veriyi oku
            label4.Text = gelenVeri;
            progressBar1.Value=100-Int32.Parse(gelenVeri);      //gelen veri string türünde. İnteger a çevirip progressbarımızın değerine atıyoruz.

            if (Int32.Parse(gelenVeri) == 100)
            {
                commanddoluluk.Parameters.AddWithValue("@tarih", DateTime.Now.ToShortDateString());
                commanddoluluk.Parameters.AddWithValue("@saat", DateTime.Now.ToShortTimeString());
                commanddoluluk.ExecuteNonQuery();
            }


        }

        private void button1_Click(object sender, EventArgs e)   //Butona tıkladığımızda mesajın arduinoya gönderilmesi
        {

            mesaj = textBox1.Text;   //mesajı al
            benimportum.WriteLine(Text);        //arduinoya yolla
            commandtxt.Parameters.AddWithValue("@txtMessageTarih", DateTime.Now.ToShortTimeString());
            commandtxt.Parameters.AddWithValue("@icerik", mesaj);
            commandtxt.ExecuteNonQuery();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form3 f3 = new Form3(); //Tanımlama
            f3.Show(); //Form2 ye geçiş yap.

        }
    }
}
