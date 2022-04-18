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
    public partial class EmanetKitapİadefrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();


        public EmanetKitapİadefrm()
        {
            InitializeComponent();
        }

        private void EmanetKitapİadefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
        }







        private void EmanetListele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("EmanetListele", baglanti);
            
            SqlDataAdapter adtr = new SqlDataAdapter();     //SQLDATAADAPTER İLE VERİLERİ ÇEKERİZ
            adtr.SelectCommand = komut;

            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
            baglanti.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)         
        {


            daset.Tables["EmanetKitaplar"].Clear();

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select Uye.adsoyad as 'Ad Soyad', Kitap.kitapadi as 'Kitap Adı', EmanetKitaplar.* from EmanetKitaplar  inner join Uye  ON EmanetKitaplar.UyeID=Uye.tc    inner join Kitap    ON  EmanetKitaplar.KitapBarkod=Kitap.barkodno             where UyeID like '%" + txtTcAra.Text +"%'", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");

            baglanti.Close();
            if (txtTcAra.Text=="")
            {
                daset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }


        }

        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)            
        {
            daset.Tables["EmanetKitaplar"].Clear();



            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select Uye.adsoyad as 'Ad Soyad', Kitap.kitapadi as 'Kitap Adı', EmanetKitaplar.* from EmanetKitaplar  inner join Uye  ON EmanetKitaplar.UyeID=Uye.tc    inner join Kitap    ON  EmanetKitaplar.KitapBarkod=Kitap.barkodno                    where KitapBarkod like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");

            baglanti.Close();
            if (txtBarkodNoAra.Text == "")
            {
                daset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("TeslimAl", baglanti);      //SQLCOMMAND İLE VERİTABANINA VERİ AKTARIRIZ
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@UyeID", dataGridView1.CurrentRow.Cells["UyeID"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapBarkod", dataGridView1.CurrentRow.Cells["KitapBarkod"].Value.ToString());
            komut.ExecuteNonQuery();



            //SqlCommand komut2 = new SqlCommand("update Kitap set stoksayisi=stoksayisi+ '"+ dataGridView1.CurrentRow.Cells["kitapsayisi"].Value.ToString() + "' where barkodno=@barkodno", baglanti);
            ////update Kitap set stoksayisi=stoksayisi+ @stoksayisi     where barkodno=@barkodno
            //komut2.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["KitapBarkod"].Value.ToString());
            //komut2.ExecuteNonQuery();




            baglanti.Close();
            MessageBox.Show("Kitaplar İade Edildi");

            daset.Tables["EmanetKitaplar"].Clear();
            EmanetListele();
        }

        private void txtTcAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBarkodNoAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
