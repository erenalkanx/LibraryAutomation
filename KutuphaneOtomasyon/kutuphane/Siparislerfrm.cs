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
    public partial class Siparislerfrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();


        



        public Siparislerfrm()
        {
            InitializeComponent();
            
        }

        private void Siparislerfrm_Load(object sender, EventArgs e)
        {
            SiparisListele();
        }

        private void SiparisListele()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut2 = new SqlCommand("SiparisListele", baglanti);
            komut2.CommandType = CommandType.StoredProcedure;

             SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut2;


            adtr.Fill(daset, "Siparisler");
            dataGridView1.DataSource = daset.Tables["Siparisler"];





            label2.Text = dataGridView1.RowCount.ToString();


            baglanti.Close();


        }









        private void txtUyeAdiAra_TextChanged(object sender, EventArgs e)      
        {

            daset.Tables["Siparisler"].Clear();

            baglanti.Open();
                            
            //BURADA VIEW DE KULLANILABİLİR---Select çok uzun olduğu için
            SqlDataAdapter adtr = new SqlDataAdapter("select Siparisler.* ,Uye.adsoyad, Urunler.UrunAdi,Urunler.StokMiktari,Urunler.UrunFiyati     from Siparisler           inner join Uye ON Siparisler.UyeID=Uye.tc           inner join Urunler ON Siparisler.UrunID = Urunler.UrunID            where adsoyad like '%" + txtUyeAdiAra.Text + "%' ", baglanti);



            adtr.Fill(daset, "Siparisler");
            dataGridView1.DataSource = daset.Tables["Siparisler"];
            baglanti.Close();


            


        }





    }
}
