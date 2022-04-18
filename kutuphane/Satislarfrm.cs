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
    public partial class Satislarfrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();





        public Satislarfrm()
        {
            InitializeComponent();
        }

        private void Satislarfrm_Load(object sender, EventArgs e)
        {
            SatisListele();
        }

        private void SatisListele()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand listele = new SqlCommand("SatisListele", baglanti);
            listele.CommandType = CommandType.StoredProcedure;
                
            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = listele;

            adtr.Fill(daset, "Satislar");
            dataGridView1.DataSource = daset.Tables["Satislar"];

            



            SqlCommand komut = new SqlCommand("select dbo.ToplamSatisTutari()", baglanti);      //Veri tabanından bu şekilde veri çekeriz
            txtToplamSatis.Text = komut.ExecuteScalar().ToString() + ",00 TL";
            int satis = int.Parse(komut.ExecuteScalar().ToString());

            SqlCommand komut2 = new SqlCommand("select dbo.SatisSayisi()", baglanti);    
            txtToplamSiparis.Text = komut2.ExecuteScalar().ToString();


            SqlCommand komut4 = new SqlCommand("select (UrunFiyati-UrunMaliyeti)*SiparisAdedi from Urunler INNER JOIN Siparisler ON Urunler.UrunID=Siparisler.UrunID", baglanti);        //fonksiyonu hata veriyor
            txtSatilanUrunler.Text= komut4.ExecuteScalar().ToString() + ",00 TL";


            SqlCommand komut3 = new SqlCommand("select dbo.ToplamMaliyetHesapla()", baglanti);

            int maliyet = int.Parse(komut3.ExecuteScalar().ToString());

            if (satis-maliyet > 0) { 
                txtGenelDurum.Text = komut3.ExecuteScalar().ToString() + " TL Kar";
            }
            else { 
                txtGenelDurum.Text = komut3.ExecuteScalar().ToString() + " TL Zarar";
            }

            baglanti.Close();
        }

        private void txtUyeAdiAra_TextChanged(object sender, EventArgs e)     
        {

            daset.Tables["Satislar"].Clear();

            baglanti.Open();

            //BURADA VIEW DE KULLANILABİLİR---Select çok uzun olduğu için
            SqlDataAdapter adtr = new SqlDataAdapter("select Uye.* ,Satislar.ToplamSatis from Satislar inner join Uye ON Satislar.UyeID=Uye.tc         where adsoyad like '%" + txtUyeAdiAra.Text + "%' ", baglanti);



            adtr.Fill(daset, "Satislar");
            dataGridView1.DataSource = daset.Tables["Satislar"];
            baglanti.Close();


        }

        private void txtUrunID_TextChanged(object sender, EventArgs e)            
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("select SiparisAdedi from Siparisler where UrunID=@UrunID", baglanti);
            komut.Parameters.AddWithValue("@UrunID", int.Parse(txtUrunID.Text));

            txtAdetSatildi.Text = komut.ExecuteScalar().ToString();




            SqlCommand komut2 = new SqlCommand("select sum(Siparisler.SiparisAdedi * Urunler.UrunFiyati)              from                Siparisler inner join Urunler            ON       Siparisler.UrunID = Urunler.UrunID            where Siparisler.UrunID=@UrunID", baglanti);
            komut2.Parameters.AddWithValue("@UrunID", int.Parse(txtUrunID.Text));

            txtSatisYapildi.Text = komut2.ExecuteScalar().ToString();



            SqlCommand komut3 = new SqlCommand("select sum(Siparisler.SiparisAdedi * Urunler.UrunMaliyeti)              from                Siparisler inner join Urunler ON Siparisler.UrunID=Urunler.UrunID            where Siparisler.UrunID=@UrunID", baglanti);
            komut3.Parameters.AddWithValue("@UrunID", int.Parse(txtUrunID.Text));

            txtKarEdildi.Text = (int.Parse(komut2.ExecuteScalar().ToString()) - int.Parse(komut3.ExecuteScalar().ToString())).ToString();





            baglanti.Close();
        }

        private void txtUrunID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
