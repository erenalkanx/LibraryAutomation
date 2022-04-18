using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace kutuphane
{
    public partial class EmanetKitapVerfrm : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();

        public EmanetKitapVerfrm()
        {
            InitializeComponent();

            baglanti.Open();

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand kontrol = new SqlCommand("select stoksayisi from Kitap", baglanti);

            if (int.Parse(kontrol.ExecuteScalar().ToString()) > 0)
            {

                SqlCommand komut = new SqlCommand("EmanetKitapEkle", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                //komut.Connection.Open();

                komut.Parameters.AddWithValue("@KitapBarkod", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@KitapSayisi", txtKitapSayisi.Text);
                komut.Parameters.AddWithValue("@TeslimTarihi", Convert.ToDateTime(dateTimePicker1.Text));   //textboxdaki bilgiyi date veri türüne dönüştürür
                komut.Parameters.AddWithValue("@İadeTarihi", Convert.ToDateTime(dateTimePicker2.Text));

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kitaplar sepete eklendi", "Ekleme İşlemi");
                daset.Tables["Sepet"].Clear();
                sepetListele();

                lblKitapSayisi.Text = "";
                kitapsayisi();

                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = "";
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Bu kitabı alamazsınız. Kitap stokta yoktur!", "Uyarı");

                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = "";
                        }
                    }
                }

            }

        }

        private void EmanetKitapVerfrm_Load(object sender, EventArgs e)
        {
            sepetListele();
            kitapsayisi();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void sepetListele()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("SepetListele", baglanti);

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;

            adtr.Fill(daset, "Sepet");
            dataGridView1.DataSource = daset.Tables["Sepet"];   //datagriview in kaynağı daset'dir.
            baglanti.Close();

        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Uye where tc like'" + txtTcAra.Text + "'", baglanti);

            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();

            if (baglanti.State == ConnectionState.Closed)     
                baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(KitapSayisi) from EmanetKitaplar                  where UyeID=@UyeID", baglanti);

            komut2.Parameters.AddWithValue("@UyeID", txtTcAra.Text);
            lblKayitliKitapSayisi.Text = komut2.ExecuteScalar().ToString();
            baglanti.Close();

            if (txtTcAra.Text == "")
            {
                foreach (Control item in grpUyeBilgi.Controls)
                {

                    if (item is TextBox)    //eğer bunu yazmasak labellerı da siler
                    {
                        item.Text = "";

                    }

                }
                lblKayitliKitapSayisi.Text = "";
            }

        }

        private void kitapsayisi()      
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut = new SqlCommand("select dbo.KitapSayisi()", baglanti);
            lblKitapSayisi.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();

        }

        //BARKOD NO'YA GÖRE ARAMA
        //private void txtBarkodNo_TextChanged(object sender, EventArgs e)    //barkodno'ya göre kitap arıyor
        //{

        //    if (baglanti.State == ConnectionState.Closed)
        //        baglanti.Open();

        //    SqlCommand komut = new SqlCommand("select * from kitap where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
        //    SqlDataReader read = komut.ExecuteReader();

        //    while (read.Read())
        //    {
        //        txtKitapAdi.Text = read["kitapadi"].ToString();
        //        txtYazari.Text = read["yazari"].ToString();
        //        txtYayinevi.Text = read["yayinevi"].ToString();
        //        txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
        //    }
        //    baglanti.Close();

        //    if (txtBarkodNo.Text == "")
        //    {

        //        foreach (Control item in grpKitapBilgi.Controls)
        //        {
        //            if (item is TextBox)
        //            {
        //                if (item != txtKitapSayisi)
        //                {
        //                    item.Text = "";
        //                }

        //            }
        //        }
        //    }
        //}

        private void btnSil_Click(object sender, EventArgs e)
        {

            DialogResult dialog;
            dialog = MessageBox.Show("Kayıt silinsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("SepetSil", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@KitapBarkod", dataGridView1.CurrentRow.Cells["KitapBarkod"].Value.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Silme işlemi yapıldı", "Silme İşlemi");
                daset.Tables["Sepet"].Clear();

                sepetListele();

                lblKitapSayisi.Text = "";
                kitapsayisi();
            }

        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {

            if (lblKitapSayisi.Text != "")
            {

                if ((lblKayitliKitapSayisi.Text == "" && int.Parse(lblKitapSayisi.Text) <= 3) || (lblKayitliKitapSayisi.Text != "" && int.Parse(lblKayitliKitapSayisi.Text) + int.Parse(lblKitapSayisi.Text) <= 3))
                {

                    if (txtTcAra.Text != "" && txtAdSoyad.Text != "" && txtYas.Text != "" && txtTelefon.Text != "")
                    {
                        for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                        {
                            if (baglanti.State == ConnectionState.Closed)
                                baglanti.Open();

                            SqlTransaction islem = null;                    //KİTAP TESLİM ETME İŞLEMİ TRANSACTION İLE OLMAKTADIR
                            islem = baglanti.BeginTransaction();

                            SqlCommand komut = new SqlCommand("EmanetKitapTeslimEt", baglanti);
                            komut.CommandType = CommandType.StoredProcedure;
                            komut.Transaction = islem;

                            //komut.Connection.Open();

                            komut.Parameters.AddWithValue("@UyeID", txtTcAra.Text);
                            komut.Parameters.AddWithValue("@KitapBarkod", dataGridView1.Rows[i].Cells["KitapBarkod"].Value.ToString());
                            komut.Parameters.AddWithValue("@KitapSayisi", dataGridView1.Rows[i].Cells["KitapSayisi"].Value.ToString());
                            komut.Parameters.AddWithValue("@TeslimTarihi", Convert.ToDateTime(dataGridView1.Rows[i].Cells["TeslimTarihi"].Value.ToString()));
                            komut.Parameters.AddWithValue("@İadeTarihi", Convert.ToDateTime(dataGridView1.Rows[i].Cells["İadeTarihi"].Value.ToString()));
                            komut.ExecuteNonQuery();    //komutu çalıştır

                            //BU KISIMDAN SONRA DİĞER KOMUTLAR TRIGGER İLE ÇALIŞTIRILMAKTADIR



                            islem.Commit();


                        }

                        baglanti.Close();
                        MessageBox.Show("Kitaplar emanet edildi");
                        daset.Tables["Sepet"].Clear();
                        sepetListele();
                        txtTcAra.Text = "";

                        lblKitapSayisi.Text = "";
                        kitapsayisi();

                        lblKayitliKitapSayisi.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Önce üye ismi seçmeniz gerekir", "Uyarı");
                    }

                }
                else
                {
                    MessageBox.Show("Emanet kitap sayısı 3'den fazla olamaz!", "Uyarı");
                }

            }
            else
            {
                MessageBox.Show("Önce sepete kitap ekelenmelidir", "Uyarı");
            }

        }

        private void txtKitapAdi_TextChanged(object sender, EventArgs e)          
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("select * from kitap where kitapadi like '" + txtKitapAdi.Text + "'", baglanti);          //sql tablo ismi ve sütun isimleri büyük küçük harf duyarlı değildir. küçük harflerle komutlar yazılabilir
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtBarkodNo.Text = read["barkodno"].ToString();
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinevi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
            }
            baglanti.Close();

            if (txtKitapAdi.Text == "")
            {

                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = "";
                        }

                    }
                }
            }

        }

        private void txtTcAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
