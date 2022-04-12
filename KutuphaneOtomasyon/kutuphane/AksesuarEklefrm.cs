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
    public partial class AksesuarEklefrm : Form
    {



        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        //burada yazmıyor ama connection stringde trusted_connection=true yazıyorsa user ve password istemeden giriş yapılsın demektir


        public AksesuarEklefrm()
        {
            InitializeComponent();
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ResimEklebtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            txtResim.Text = openFileDialog1.FileName;
        }

        private void btnUyeEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("AksesuarEkle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@UrunID", txtUrunNo.Text);
            komut.Parameters.AddWithValue("@UrunAdi", txtAksesuarAdi.Text);
            komut.Parameters.AddWithValue("@StokMiktari", txtMiktar.Text);
            komut.Parameters.AddWithValue("@UrunFiyati", txtFiyat.Text);
            komut.Parameters.AddWithValue("@UrunMaliyeti", txtMaliyet.Text);
            komut.Parameters.AddWithValue("@UrunResimYolu", txtResim.Text);


            //Image foto = Image.FromFile(txtResim.Text);                //ürün resmi çekiliyor

            //Bitmap yeniimg = new Bitmap(64, 64);                 //çekilen ürün resmi 64x64 boyutuna getiriliyor
            //using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
            //    g.DrawImage(foto, 0, 0, 64, 64);
            //Image foto2 = (Image)yeniimg;

           // komut.Parameters.AddWithValue("@UrunResmi", foto2);


            
            komut.ExecuteNonQuery();



            //EKLENEN ÜRÜN MALİYETLERE EKLENİYOR        //TRIGGER İLE YAPILDI


            //komut2.Parameters.AddWithValue("@UrunID", int.Parse(txtUrunNo.Text));
            //komut2.Parameters.AddWithValue("@ToplamMaliyet", int.Parse(txtMaliyet.Text)*int.Parse(txtMiktar.Text));       //TRIGGER OLUŞTURULURKEN BURADA DEĞİŞKENE ARİTMETİK İŞLEM OLARAK VERİ ATANMIŞ MI KONTROL EDİLMELİ
            //komut2.ExecuteNonQuery();




            baglanti.Close();


            MessageBox.Show("Ürün Kaydı Yapıldı");


            pictureBox1.Image = null;
            foreach (Control item in Controls)  
            {
                if (item is TextBox) 
                {
                        item.Text = "";
                }
            }




        }

        private void AksesuarEklefrm_Load(object sender, EventArgs e)
        {

        }

        private void txtUrunNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void txtMaliyet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';     //bu kod textboxa ondalık değer girmemizi sağlamaktadır
        }





        private void txtMaliyet_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
