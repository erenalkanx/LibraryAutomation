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
    public partial class BugunKitapGetirenlerfrm : Form
    {




        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();






        public BugunKitapGetirenlerfrm()
        {
            InitializeComponent();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select dbo.BugunKitapGetirmesiGerekenKisiSayisi()", baglanti);

            label2.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();
        }

        private void BugunKitapGetirenlerfrm_Load(object sender, EventArgs e)     
        {


            baglanti.Open();
            SqlCommand komut = new SqlCommand("BugunKitapGetirmesiGerekenler", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;

            //Convert(date,getdate(),104)        kısmı tarihi DD.MM.YYYY  formatına çevirir             iki tarih aynı formatta olmayınca karşılaştırma yapmıyor


            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];


         

            baglanti.Close();

        }
    }
}
