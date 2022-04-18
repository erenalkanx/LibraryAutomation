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
    public partial class Maliyetlerfrm : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();

        public Maliyetlerfrm()
        {
            InitializeComponent();
        }

        private void Maliyetlerfrm_Load(object sender, EventArgs e)
        {
            MaliyetListele();
        }

        private void MaliyetListele()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand listele = new SqlCommand("MaliyetListele", baglanti);
            listele.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = listele;

            adtr.Fill(daset, "Maliyetler");
            dataGridView1.DataSource = daset.Tables["Maliyetler"];

            SqlCommand komut = new SqlCommand("Select dbo.ToplamMaliyetHesapla()", baglanti);      //Veri tabanından bu şekilde veri çekeriz

            label2.Text = komut.ExecuteScalar().ToString() + " 00,TL";

            SqlCommand komut2 = new SqlCommand("select dbo.ToplamUrunSayisi()", baglanti);      //Veri tabanından bu şekilde veri çekeriz
            label4.Text = komut2.ExecuteScalar().ToString();

            baglanti.Close();
        }

        private void txtUyeAdiAra_TextChanged(object sender, EventArgs e)       
        {

            daset.Tables["Maliyetler"].Clear();

            baglanti.Open();

            //BURADA VIEW DE KULLANILABİLİR---Select çok uzun olduğu için
            SqlDataAdapter adtr = new SqlDataAdapter("select  Urunler.* , Maliyetler.ToplamMaliyet  from         Maliyetler inner join Urunler           ON Maliyetler.UrunID= Urunler.UrunID       where adsoyad like '%" + txtUyeAdiAra.Text + "%' ", baglanti);

            adtr.Fill(daset, "Maliyetler");
            dataGridView1.DataSource = daset.Tables["Maliyetler"];
            baglanti.Close();
        }
    }
}
