using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphane
{
    public partial class Aksesuarlarfrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();


        int guncellendimi = 0;


        public Aksesuarlarfrm()
        {
            InitializeComponent();

            pictureBox1.ImageLocation = txtUrunResmi.Text;
            
        }

        private void Aksesuarlarfrm_Load(object sender, EventArgs e)
        {

            

            AksesuarListele();
            label9.Text = dataGridView1.RowCount.ToString();

        }



        private void AksesuarListele()
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("AksesuarListele", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;



            adtr.Fill(daset, "Urunler");
            dataGridView1.DataSource = daset.Tables["Urunler"];




            //datagridviewın satır ve sütun boyutu 64x64 yapılıyor
            dataGridView1.RowHeadersWidth = 64;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Height = 64;


            //string path = System.Windows.Forms.Application.StartupPath + "\\img\\";

            if (guncellendimi == 0) { 
            dataGridView1.Columns["UrunResimYolu"].Visible = false;
            DataGridViewImageColumn col = new DataGridViewImageColumn();
            col.Name = "UrunResmi";
            dataGridView1.Columns.Add(col);
            }


            

            for (int i = 0; i < dataGridView1.Rows.Count; i++)      //ÜRÜN RESİMLERİ YERLEŞTİRİLİYOR
            {


                if (dataGridView1.Rows[i].Cells["UrunResimYolu"].Value.ToString() != ""     &&      File.Exists(dataGridView1.Rows[i].Cells["UrunResimYolu"].Value.ToString())==true)   //File.Exists ile dosyanın olup olmadığını kontrol ederiz.
                {
                    Image foto = Image.FromFile(dataGridView1.Rows[i].Cells["UrunResimYolu"].Value.ToString()); //ürün resmi çekiliyor



                    Bitmap yeniimg = new Bitmap(64, 64);                                                          //çekilen ürün resmi 64x64 boyutuna getiriliyor
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(foto, 0, 0, 64, 64);

                    dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = yeniimg;                //ürün resmi gerekli sütuna atanıyor
                }
                else {      //EĞER ÜRÜNÜN RESMİ YOKSA
                    Image foto = Image.FromFile(@"C:\Users\EREN\documents\visual studio 2019\Projects\KutuphaneOtomasyon\KutuphaneOtomasyon\img\1.png");
                    Bitmap yeniimg = new Bitmap(64, 64);                                                          //çekilen ürün resmi 64x64 boyutuna getiriliyor
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(foto, 0, 0, 64, 64);

                    dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = yeniimg;
                }




            }



            baglanti.Close();
        }




        private void txtUrunNoAra_TextChanged(object sender, EventArgs e)      
        {
            

            daset.Tables["Urunler"].Clear();

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Urunler where UrunID like '%" + txtUrunNoAra.Text + "%' ", baglanti);
            adtr.Fill(daset, "Urunler");
            dataGridView1.DataSource = daset.Tables["Urunler"];
            baglanti.Close();



            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Height = 64;




            //BU SATIR ARAMA YAPILIRKEN RESMİN SİLİNMEMESİNİ SAĞLIYOR
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (File.Exists(dataGridView1.Rows[i].Cells["UrunResimYolu"].Value.ToString()) == true)
                {
                    Image foto = Image.FromFile(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 2].Value.ToString());                //ürün resmi çekiliyor

                    Bitmap yeniimg = new Bitmap(64, 64);                                                        //çekilen ürün resmi 64x64 boyutuna getiriliyor
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(foto, 0, 0, 64, 64);

                    dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = yeniimg;                //ürün resmi gerekli sütuna atanıyor


                }
                else
                {
                    Image foto = Image.FromFile(@"C:\Users\EREN\documents\visual studio 2015\Projects\kutuphane\kutuphane\img\1.png");
                    Bitmap yeniimg = new Bitmap(64, 64);                                                          //çekilen ürün resmi 64x64 boyutuna getiriliyor
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(foto, 0, 0, 64, 64);

                    dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = yeniimg;

                }
            }





        }


        private void txtUrunAdiAra_TextChanged(object sender, EventArgs e)      
        {
            

            daset.Tables["Urunler"].Clear();

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Urunler where UrunAdi like '%" + txtUrunAdiAra.Text + "%' ", baglanti);
            adtr.Fill(daset, "Urunler");
            dataGridView1.DataSource = daset.Tables["Urunler"];
            baglanti.Close();





            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Height = 64;


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (File.Exists(dataGridView1.Rows[i].Cells["UrunResimYolu"].Value.ToString()) == true)
                {

                    Image foto = Image.FromFile(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 2].Value.ToString());                //ürün resmi çekiliyor

                Bitmap yeniimg = new Bitmap(64, 64);                                                        //çekilen ürün resmi 64x64 boyutuna getiriliyor
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                    g.DrawImage(foto, 0, 0, 64, 64);

                dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = yeniimg;                //ürün resmi gerekli sütuna atanıyor
            }
                else
                {
                    Image foto = Image.FromFile(@"C:\Users\EREN\documents\visual studio 2015\Projects\kutuphane\kutuphane\img\1.png");
                    Bitmap yeniimg = new Bitmap(64, 64);                                                          //çekilen ürün resmi 64x64 boyutuna getiriliyor
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(foto, 0, 0, 64, 64);

                    dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value = yeniimg;
                }



            }




        }

        private void txtUrunNo_TextChanged(object sender, EventArgs e)      
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();


            SqlCommand komut = new SqlCommand("select * from Urunler where UrunID like '" + txtUrunNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();




            while (read.Read())
            {
                txtUrunAdi.Text = read["UrunAdi"].ToString();
                txtUrunMiktari.Text = read["StokMiktari"].ToString();
                txtUrunFiyati.Text = read["UrunFiyati"].ToString();
                txtUrunMaliyeti.Text = read["UrunMaliyeti"].ToString();
                txtUrunResmi.Text = read["UrunResimYolu"].ToString();
            }
            baglanti.Close();


            pictureBox1.ImageLocation = txtUrunResmi.Text;


            if (txtUrunNo.Text == "")
            {
                pictureBox1.Image = null;

                
                foreach (Control item in Controls)
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
            guncellendimi++;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("AksesuarGuncelle", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            komut.Parameters.AddWithValue("@UrunID", txtUrunNo.Text);
            komut.Parameters.AddWithValue("@UrunAdi", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@StokMiktari", txtUrunMiktari.Text);
            komut.Parameters.AddWithValue("@UrunFiyati", txtUrunFiyati.Text);
            komut.Parameters.AddWithValue("@UrunMaliyeti", txtUrunMaliyeti.Text);
            komut.Parameters.AddWithValue("@UrunResimYolu", txtUrunResmi.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Güncelleme işlemi gerçekleşti");

            daset.Tables["Urunler"].Clear();    //data alanı siliniyor
            AksesuarListele();   //data alanı tekrar yazdırılıyor

            foreach (Control item in Controls)   //bu işlemden sonra tüm textboxları temizleme kodu
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            guncellendimi++;
            DialogResult dialog;

            dialog = MessageBox.Show("Bu ürünü silmek istiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);


            if (dialog == DialogResult.Yes)
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("AksesuarSil", baglanti);
                komut.CommandType = CommandType.StoredProcedure;

                komut.Parameters.AddWithValue("@UrunID", dataGridView1.CurrentRow.Cells["UrunID"].Value.ToString());

                komut.ExecuteNonQuery();


                //MALİYETLERDEN DE SİLİNİYOR --- TRIGGER İLE




                baglanti.Close();
                MessageBox.Show("Silme işlemi gerçekleşti");




                daset.Tables["Urunler"].Clear();    //data alanı siliniyor
                AksesuarListele();   //data alanı tekrar yazdırılıyor

                foreach (Control item in Controls)   //bu işlemden sonra tüm textboxları temizleme kodu
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }





        }

        //TEXTBOXLARA KISITLAMALAR GETİRİLİYOR
        private void txtUrunNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtUrunMiktari_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtUrunFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void txtUrunMaliyeti_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void txtUrunNoAra_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
