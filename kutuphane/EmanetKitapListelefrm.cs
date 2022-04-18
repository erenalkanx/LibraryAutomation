using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane
{
    public partial class EmanetKitapListelefrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();



        public EmanetKitapListelefrm()         
        {
            InitializeComponent();
            EmanetListele();


            comboBox1.SelectedIndex = 0;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select dbo.EmanetKitapAlanKisiSayisi() ", baglanti);      //distinct UyeID idyerek farklı UyeID'lerinin sayısını çektik
            label2.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();

        }

        private void EmanetListele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("EmanetListele", baglanti);

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;

            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
            baglanti.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["EmanetKitaplar"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                EmanetListele();

            }
            else if(comboBox1.SelectedIndex==1)        
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select Uye.adsoyad as 'Ad Soyad', Kitap.kitapadi as 'Kitap Adı', EmanetKitaplar.* from EmanetKitaplar inner join Uye          ON EmanetKitaplar.UyeID = Uye.tc        inner join Kitap     ON EmanetKitaplar.KitapBarkod=Kitap.barkodno            where getdate() >iadetarihi", baglanti);
                adtr.Fill(daset, "EmanetKitaplar");
                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select Uye.adsoyad as 'Ad Soyad', Kitap.kitapadi as 'Kitap Adı', EmanetKitaplar.* from EmanetKitaplar inner join Uye          ON EmanetKitaplar.UyeID = Uye.tc        inner join Kitap     ON EmanetKitaplar.KitapBarkod=Kitap.barkodno             where getdate() <=iadetarihi", baglanti);
                adtr.Fill(daset, "EmanetKitaplar");
                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }


        }

        private void EmanetKitapListelefrm_Load(object sender, EventArgs e)
        {

        }
    }
}
