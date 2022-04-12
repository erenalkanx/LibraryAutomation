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
    public partial class AksesuarSatinAlfrm : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();

        public AksesuarSatinAlfrm()
        {
            InitializeComponent();
            //label10.Text = DateTime.Today.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("SepetAksesuarEkle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@UrunID", int.Parse(txtUrunNo.Text));
            komut.Parameters.AddWithValue("@SiparisAdedi", int.Parse(txtUrunMiktari.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Ürünler sepete eklendi", "Ekleme İşlemi");
            daset.Tables["SepetAksesuar"].Clear();
            sepetListele();

            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

        private void sepetListele()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            //SepetAksesuar ile Urunler tablosu UrunID'ye göre inner join edilecek

            SqlCommand komut = new SqlCommand("SepetAksesuarListele", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();      //bu komut inner join edilip datagridviewa aktarılacak
            adtr.SelectCommand = komut;

            adtr.Fill(daset, "SepetAksesuar");
            dataGridView1.DataSource = daset.Tables["SepetAksesuar"];   //datagriview in kaynağı daset'dir.
            baglanti.Close();

        }

        private void txtUrunAdi_TextChanged(object sender, EventArgs e)           
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("select * from Urunler where UrunAdi like '%" + txtUrunAdi.Text + "%'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtUrunNo.Text = read["UrunID"].ToString();
                txtUrunFiyati.Text = read["UrunFiyati"].ToString();
            }
            baglanti.Close();

            if (txtUrunAdi.Text == "")
            {

                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)           
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("select * from Uye where tc like '" + txtTcAra.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();

            if (txtTcAra.Text == "")
            {

                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {

            if (txtTcAra.Text != "" && txtAdSoyad.Text != "" && txtYas.Text != "" && txtTelefon.Text != "")
            {

                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();

                    SqlTransaction islem = null;
                    islem = baglanti.BeginTransaction();            //TRANSACTION'UN SQL DOSYASI YOKTUR. SADECE C# İÇİNDE ÇALIŞTIRILIR. (HERHANGİ BİR DOSYASI YOKTUR)

                    SqlCommand komut = new SqlCommand("AksesuarSatinAl", baglanti);         //PROCEDURE HAKKINDA ÇOK ÖNEMLİ NOT!!!!  SAKIN PROCEDURE'A 2 KEZ AYNI DEĞERİ GÖNDERME VE PROCEDUREA LAZIM OLMAYAN DEĞERLERİ GÖNDERME YOKSA ÇALIŞMAZ AMA HATA DA VERMEZ
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.Transaction = islem;

                    try
                    {

                        //komut.Connection.Open();

                        //   SqlCommand tarih = new SqlCommand("select Convert(date,getdate(),104)");

                        komut.Parameters.AddWithValue("@UyeID", txtTcAra.Text);
                        komut.Parameters.AddWithValue("@UrunID", int.Parse(dataGridView1.Rows[i].Cells["UrunID"].Value.ToString()));
                        komut.Parameters.AddWithValue("@SiparisTarihi", DateTime.Today);
                        komut.Parameters.AddWithValue("@SiparisAdedi", int.Parse(dataGridView1.Rows[i].Cells["SiparisAdedi"].Value.ToString()));

                        //eğer o müşteri satışlarda yoksa EKLEME YAPILACAK EĞER O MÜŞTERİ SATIŞLAR TABLOSUNDA VARSA UPDATE YAPILACAK

                        //SATIŞLARA EKLENECEK       toplamsatis üyeye toplam ne kadar satış yapıldığını gösteriyor
                        komut.Parameters.AddWithValue("@ToplamSatis", int.Parse(dataGridView1.Rows[i].Cells["UrunFiyati"].Value.ToString()) * int.Parse(dataGridView1.Rows[i].Cells["SiparisAdedi"].Value.ToString()));

                        //ÜYENİN TOPLAM ALIŞVERİŞİNE GÜNCELLENECEK      toplam alışveriş üyenin kaç adet ürün aldığını gösteriyor
                   //   TRIGGER İLE YAPILIYOR

                        komut.ExecuteNonQuery();

                        islem.Commit();

                        baglanti.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;       //programı durdurup hatayı gösterir
                        islem.Rollback();
                    }
                }

                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                SqlCommand komut4 = new SqlCommand("delete from SepetAksesuar", baglanti);
                komut4.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show("Sipariş teslim edildi");
                daset.Tables["SepetAksesuar"].Clear();
                sepetListele();
                txtTcAra.Text = "";
            }

            else
            {
                MessageBox.Show("Önce üye ismi seçmeniz gerekir", "Uyarı");
            }

        }

        private void AksesuarSatinAlfrm_Load(object sender, EventArgs e)
        {
            sepetListele();
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            DialogResult dialog;
            dialog = MessageBox.Show("Sepetten silinsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("SepetAksesuarSil", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@UrunID", dataGridView1.CurrentRow.Cells["UrunID"].Value.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Silme işlemi yapıldı", "Silme İşlemi");
                daset.Tables["SepetAksesuar"].Clear();

                sepetListele();

            }
        }
    }
}
