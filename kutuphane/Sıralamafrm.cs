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
    public partial class Sıralamafrm : Form
    {




        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();






        public Sıralamafrm()
        {
            InitializeComponent();
        }

        private void Sıralamafrm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Siralama", baglanti);
            komut.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adtr = new SqlDataAdapter();
            adtr.SelectCommand = komut;


            adtr.Fill(daset, "Uye");
            dataGridView1.DataSource = daset.Tables["Uye"];
            baglanti.Close();


            label3.Text = "";
            label4.Text = "";

            label3.Text = daset.Tables["Uye"].Rows[0]["adsoyad"].ToString()+":";
            label3.Text += daset.Tables["Uye"].Rows[0]["okunankitapsayisi"].ToString();

            label4.Text = daset.Tables["Uye"].Rows[dataGridView1.Rows.Count-2]["adsoyad"].ToString()+":";
            label4.Text += daset.Tables["Uye"].Rows[dataGridView1.Rows.Count - 2]["okunankitapsayisi"].ToString();


        }
    }
}
