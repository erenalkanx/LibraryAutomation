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
    public partial class KitapEklefrm : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");




        public KitapEklefrm()
        {
            InitializeComponent();



            //BU YAPI ŞUBELERİ COMBOBOXA AKTARMAKTADIR
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT *FROM Subeler";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboSubeler.Items.Add(dr["SubeAdi"].ToString().Trim());
            }
            baglanti.Close();





        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand bul = new SqlCommand("select SubeID from Subeler where SubeAdi='" + comboSubeler.Text + "'", baglanti);
            int subeid = int.Parse(bul.ExecuteScalar().ToString());




            SqlCommand komut = new SqlCommand("KitapEkle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;


            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@stoksayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.Parameters.AddWithValue("@kayittarihi", DateTime.Now.ToShortDateString());
            komut.Parameters.AddWithValue("@SubeID", subeid);



            komut.ExecuteNonQuery();        //EĞER BU SATIR OLMAZSA KOMUT ASLA ÇALIŞMAZ
            baglanti.Close();


            //BURADAN SONRAKİ KOMUT TRIGGER İLE ÇALIŞMAKTADIR




            MessageBox.Show("Kitap Kaydı Yapıldı");

            //kayıt yapıldıktan sonra textboxlar boşaltılsın
            foreach (Control item in Controls)  //sayfadaki tüm kontrollere eriş
            {
                if (item is TextBox) //kontroller eğer textbox ise
                {
                    item.Text = "";

                }

            }







        }

        private void KitapEklefrm_Load(object sender, EventArgs e)
        {

        }

        private void txtBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSayfaSayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtStokSayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
