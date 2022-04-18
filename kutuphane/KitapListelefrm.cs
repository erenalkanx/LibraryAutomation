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
    public partial class KitapListelefrm : Form
    {



        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();




        public KitapListelefrm()
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





        private void kitaplistele()
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("KitapListele",baglanti);
            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;

            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            baglanti.Close();
        }

        private void KitapListelefrm_Load(object sender, EventArgs e)
        {
            kitaplistele();
            label15.Text = dataGridView1.RowCount.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {



            DialogResult dialog;

            dialog = MessageBox.Show("Bu kitabı silmek istiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);


            if (dialog == DialogResult.Yes)
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("KitapSil", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi gerçekleşti");


                daset.Tables["kitap"].Clear();    //data alanı siliniyor
                kitaplistele();   //data alanı tekrar yazdırılıyor

                foreach (Control item in Controls)   //bu işlemden sonra tüm textboxları temizleme kodu
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }




        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();



            SqlCommand bul = new SqlCommand("select SubeID from Subeler where SubeAdi='" + comboSubeler.Text+"'", baglanti);
            int subeid = int.Parse(bul.ExecuteScalar().ToString());





            SqlCommand komut = new SqlCommand("KitapGuncelle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@barkodno",txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@stoksayisi", txtStokSayisi.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.Parameters.AddWithValue("@SubeID", subeid);

            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Güncelleme işlemi gerçekleşti");

            daset.Tables["kitap"].Clear();    //data alanı siliniyor
            kitaplistele();   //data alanı tekrar yazdırılıyor

            foreach (Control item in Controls)   //bu işlemden sonra tüm textboxları temizleme kodu
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }


        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)      
        {
            daset.Tables["kitap"].Clear();   

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select Kitap.*, Subeler.SubeAdi  from kitap      inner join Subeler ON Kitap.SubeID=Subeler.SubeID                     where barkodno like '%" + txtBarkodAra.Text + "%' ", baglanti);
            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            baglanti.Close();
        }


        









        
        private void txtBarkodNo_TextChanged(object sender, EventArgs e)        //yeni veri girince eğer öyle bir veri daha önceden varsa onun bilgileri textboxlara yerleşsin
        {
            






            

            baglanti.Open();
            

            SqlCommand komut = new SqlCommand("select * from  kitap  INNER JOIN Subeler ON Kitap.SubeID=Subeler.SubeID                where barkodno like '" + txtBarkodNo.Text + "' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();

           

            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinevi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
                comboTuru.Text = read["turu"].ToString();
                txtStokSayisi.Text = read["stoksayisi"].ToString();
                txtRafNo.Text = read["rafno"].ToString();
                txtAciklama.Text = read["aciklama"].ToString();

                comboSubeler.Text = read["SubeAdi"].ToString();





            }
            

            baglanti.Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtKitapAdiAra_TextChanged(object sender, EventArgs e)   
        {
            daset.Tables["kitap"].Clear();

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select Kitap.*, Subeler.SubeAdi from kitap              inner join Subeler ON Kitap.SubeID=Subeler.SubeID                          where kitapadi like '%" + txtKitapAdiAra.Text + "%' ", baglanti);
            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            baglanti.Close();
        }

        private void txtSubeAra_TextChanged(object sender, EventArgs e)          
        {
            daset.Tables["kitap"].Clear();

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select Kitap.*, Subeler.SubeAdi from kitap             inner join Subeler ON Kitap.SubeID=Subeler.SubeID             where SubeAdi like '%" + txtSubeAra.Text + "%' ", baglanti);
            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            baglanti.Close();
        }

        private void txtBarkodAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
