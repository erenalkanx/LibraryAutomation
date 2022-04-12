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
    public partial class Carilerfrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();




        public Carilerfrm()
        {
            InitializeComponent();
        }

        private void Carilerfrm_Load(object sender, EventArgs e)
        {
            CariListele();
        }

        private void CariListele()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand listele = new SqlCommand("CariListele", baglanti);
            listele.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = listele;

            adtr.Fill(daset, "Uye");
            dataGridView1.DataSource = daset.Tables["Uye"];



            baglanti.Close();
        }

        private void txtUyeAdiAra_TextChanged(object sender, EventArgs e)          
        {
            if (daset.Tables["Siparisler"] != null)
                daset.Tables["Siparisler"].Clear();

            baglanti.Open();

            //BURADA VIEW DE KULLANILABİLİR---Select çok uzun olduğu için
            SqlDataAdapter adtr = new SqlDataAdapter("select Siparisler.* ,Uye.adsoyad, Urunler.UrunAdi,Urunler.StokMiktari,Urunler.UrunFiyati     from Siparisler           inner join Uye ON Siparisler.UyeID=Uye.tc           inner join Urunler ON Siparisler.UrunID = Urunler.UrunID            where adsoyad like '%" + txtUyeAdiAra.Text + "%' ", baglanti);



            adtr.Fill(daset, "Siparisler");
            dataGridView1.DataSource = daset.Tables["Siparisler"];


            SqlCommand komut = new SqlCommand("select dbo.SiparisSay()", baglanti);      //Veri tabanından bu şekilde veri çekeriz
            komut.Parameters.AddWithValue("@adsoyad", txtUyeAdiAra.Text);
            label2.Text = dataGridView1.RowCount.ToString();


            baglanti.Close();


        

            txtToplamSiparis.Text = dataGridView1.RowCount.ToString();




        }

        private void btnAra_Click(object sender, EventArgs e)
        {

        }

        //private void btnAra_Click(object sender, EventArgs e)         //BUTONLA ARAMA YAPILMAK İSTENİRSE BU KISIM KULLANILABİLİR
        //{
        //    if (daset.Tables["Siparisler"] != null)
        //        daset.Tables["Siparisler"].Clear();

        //    baglanti.Open();

        //    //BURADA VIEW DE KULLANILABİLİR---Select çok uzun olduğu için
        //    SqlDataAdapter adtr = new SqlDataAdapter("select Siparisler.* ,Uye.adsoyad, Urunler.UrunAdi,Urunler.StokMiktari,Urunler.UrunFiyati     from Siparisler           inner join Uye ON Siparisler.UyeID=Uye.tc           inner join Urunler ON Siparisler.UrunID = Urunler.UrunID            where adsoyad like '%" + txtUyeAdiAra.Text + "%' ", baglanti);



        //    adtr.Fill(daset, "Siparisler");
        //    dataGridView1.DataSource = daset.Tables["Siparisler"];


        //    SqlCommand komut = new SqlCommand("select count(*) from Siparisler", baglanti);      //Veri tabanından bu şekilde veri çekeriz
        //    txtToplamSiparis.Text = dataGridView1.RowCount.ToString();


        //    baglanti.Close();


        //    if (txtUyeAdiAra == null)
        //        CariListele();

        //}
    }
}
