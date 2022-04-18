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
    public partial class Subelerfrm : Form
    {



        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();



        public Subelerfrm()
        {
            InitializeComponent();
        }

        private void Subelerfrm_Load(object sender, EventArgs e)
        {
            SubeListele();
        }

        private void SubeListele()
        {


            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SubeListele", baglanti);
            komut2.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut2;

            adtr.Fill(daset, "Subeler");
            dataGridView1.DataSource = daset.Tables["Subeler"];



            SqlCommand komut = new SqlCommand("select dbo.SubeSayisi()", baglanti);      //Veri tabanından bu şekilde veri çekeriz    
            label2.Text = komut.ExecuteScalar().ToString();


            baglanti.Close();




        }

        private void txtSubeAdiAra_TextChanged(object sender, EventArgs e)       
        {

            daset.Tables["Subeler"].Clear();

            baglanti.Open();

            //BURADA VIEW DE KULLANILABİLİR---Select çok uzun olduğu için
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Subeler         where SubeAdi like '%" + txtSubeAdiAra.Text + "%' ", baglanti);



            adtr.Fill(daset, "Subeler");
            dataGridView1.DataSource = daset.Tables["Subeler"];
            baglanti.Close();

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubeEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            SqlCommand komut = new SqlCommand("SubeEkle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            //komut.Connection.Open();

            komut.Parameters.AddWithValue("@SubeID", txtSubeNo.Text);
            komut.Parameters.AddWithValue("@SubeAdi", txtSubeAdi.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Şube sisteme eklendi", "Ekleme İşlemi");
            daset.Tables["Subeler"].Clear();
            SubeListele();


            






            foreach (Control item in grpSubeEkleme.Controls)
            {
                if (item is TextBox)
                {
                        item.Text = "";
                }
            }





        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            DialogResult dialog;
            dialog = MessageBox.Show("Şube silinsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {



                baglanti.Open();
                SqlCommand komut = new SqlCommand("SubeSil", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@SubeID", dataGridView1.CurrentRow.Cells["SubeID"].Value.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Silme işlemi yapıldı", "Silme İşlemi");
                daset.Tables["Subeler"].Clear();

                SubeListele();
                
            }
        }

        private void txtSubeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SubeGuncelle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@SubeAdi", txtSubeAdi.Text);
            komut.Parameters.AddWithValue("@SubeID", int.Parse(txtSubeNo.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Güncelleme işlemi yapıldı", "Silme İşlemi");
            daset.Tables["Subeler"].Clear();

            SubeListele();
        }
    }
}
