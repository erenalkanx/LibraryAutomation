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
    public partial class UyeListelemefrm : Form
    {
        public UyeListelemefrm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();

        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("TcUyeAra", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@tc", txtTc.Text);       //tc alanını procedure'e gönderiyoruz. onun ihtiyacı olacak

            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                comboCinsiyet.Text = read["cinsiyet"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
                txtAdres.Text = read["adres"].ToString();
                txtEmail.Text = read["email"].ToString();
                txtOkunanSayi.Text = read["okunankitapsayisi"].ToString();
            }
            baglanti.Close();

        }

        DataSet daset = new DataSet();  //verileri tutacağımız data alanı
       // DataTable tablo = new DataTable();        //buna gerek kalmadı


        private void txtAraTc_TextChanged(object sender, EventArgs e)     
        {
            daset.Tables["uye"].Clear();   //tablo her seferinde sıfırlanıp tekrardan yazdırılıyor

            baglanti.Open();

            //SqlCommand komut = new SqlCommand("UyeTCAra", baglanti);  
            //komut.CommandType = CommandType.StoredProcedure;
            //komut.Parameters.AddWithValue("@tc", txtAraTc.Text);


            SqlDataAdapter adtr = new SqlDataAdapter("select * from uye where tc like '%" + txtAraTc.Text + "%' ", baglanti);
            //SqlDataAdapter adtr = new SqlDataAdapter();
          //  adtr.SelectCommand = komut;

            adtr.Fill(daset, "uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            DialogResult dialog;

            dialog = MessageBox.Show("bu kaydı silmek istiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialog == DialogResult.Yes)
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("UyeSil", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());

                komut.ExecuteNonQuery();




                


               



                baglanti.Close();
                MessageBox.Show("silme işlemi gerçekleşti");

                daset.Tables["uye"].Clear();   //data alanı siliniyor
                uyelistele();   //data alanı tekrar yazdırılıyor

                foreach (Control item in Controls)   //bu işlemden sonra tüm textboxları temizleme kodu
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }

            }

        }


        private void uyelistele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UyeListele", baglanti);        

            SqlDataAdapter adtr = new SqlDataAdapter();
             adtr.SelectCommand = komut;

            //adtr.Fill(tablo);
            //dataGridView1.DataSource = tablo;
            adtr.Fill(daset, "Uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
        }

        private void UyeListelemefrm_Load(object sender, EventArgs e)
        {
            uyelistele();   //form açıldığında listeleme işlemi gerçekleşsin
            label13.Text = dataGridView1.RowCount.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UyeGuncelle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@yas", txtYas.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboCinsiyet.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@okunankitapsayisi", int.Parse(txtOkunanSayi.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Güncelleme işlemi gerçekleşti");

            daset.Tables["uye"].Clear();   //data alanı siliniyor
            uyelistele();   //data alanı tekrar yazdırılıyor

            foreach (Control item in Controls)   //bu işlemden sonra tüm textboxları temizleme kodu
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

        private void txtAraAdSoyad_TextChanged(object sender, EventArgs e)    
        {
            daset.Tables["uye"].Clear();   //tablo her seferinde sıfırlanıp tekrardan yazdırılıyor

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Uye where adsoyad like '%" + txtAraAdSoyad.Text + "%' ", baglanti);
            adtr.Fill(daset, "Uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
        }

        private void txtOkunanSayi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);     //hataların önüne geçmek amacıyla textboxlara bu kısıtlamalar veriliyor
        }

        private void txtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtYas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAraTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnTamEkran_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }
    }
}
