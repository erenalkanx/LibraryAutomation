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
    public partial class KitapAramafrm : Form
    {


        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();





        public KitapAramafrm()
        {
            InitializeComponent();
            SqlCommand komut = new SqlCommand("KitapArama", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;

            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
        }






        private void textBox1_TextChanged(object sender, EventArgs e)      
        {

            if (daset.Tables["EmanetKitaplar"] != null)
                daset.Tables["EmanetKitaplar"].Clear();

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();




            SqlDataAdapter adtr = new SqlDataAdapter("select Uye.adsoyad, Kitap.kitapadi, EmanetKitaplar.* from EmanetKitaplar  INNER JOIN Uye ON EmanetKitaplar.UyeID = Uye.tc INNER JOIN Kitap ON EmanetKitaplar.KitapBarkod = Kitap.barkodno                     where kitapadi like '%" + txtKitapAdi.Text + "%'", baglanti);

            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];


            baglanti.Close();



        }

        

        private void KitapAramafrm_Load(object sender, EventArgs e)
        {

        }
    }
}
