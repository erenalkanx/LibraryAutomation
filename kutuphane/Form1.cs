using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace kutuphane
{
    public partial class Form1 : Form
    {










        public Form1()
        {
            InitializeComponent();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            UyeEklefrm uyeekle = new UyeEklefrm();
            uyeekle.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UyeListelemefrm uyeliste = new UyeListelemefrm();
            uyeliste.Show(); 
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            KitapEklefrm kitapekle = new KitapEklefrm();
            kitapekle.Show();
        }

        private void btnKitapListele_Click(object sender, EventArgs e)
        {
            KitapListelefrm kitaplistele = new KitapListelefrm();
            kitaplistele.Show();
        }

        private void btnEmanetİade_Click(object sender, EventArgs e)
        {
            EmanetKitapİadefrm iade = new EmanetKitapİadefrm();
            iade.Show();
        }

        private void btnEmanetVer_Click(object sender, EventArgs e)
        {
            EmanetKitapVerfrm emanetkitapver = new EmanetKitapVerfrm();
            emanetkitapver.Show();  //Show yerin Show kullanılırsa pencere açılırken arkadaki pencere de kontrol edilebilir
        }

        private void btnEmanetListele_Click(object sender, EventArgs e)
        {
            EmanetKitapListelefrm listele = new EmanetKitapListelefrm();
            listele.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None; //pencere boyutunun değiştirilmesini önler---burdan FixedSingle yerine None yapılırsa FormBorder tamamen kalkar

            ControlBox = false;

        }

        private void btnSıralama_Click(object sender, EventArgs e)
        {
            Sıralamafrm sırala = new Sıralamafrm();
            sırala.Show();
        }

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            Grafikfrm grafik = new Grafikfrm();
            grafik.Show();
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BugunKitapGetirenlerfrm bugun = new BugunKitapGetirenlerfrm();
            bugun.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KitapAramafrm kitaparama = new KitapAramafrm();
            kitaparama.Show();
        }

        private void AksesuarEklebtn_Click(object sender, EventArgs e)
        {
            AksesuarEklefrm aksesuarekle = new AksesuarEklefrm();
            aksesuarekle.Show();
        }

        private void Aksesuarlarbtn_Click(object sender, EventArgs e)
        {
            Aksesuarlarfrm aksesuarlar = new Aksesuarlarfrm();
            aksesuarlar.Show();
        }

        private void AksesuarAlbtn_Click(object sender, EventArgs e)
        {
            AksesuarSatinAlfrm aksesuarsatinal = new AksesuarSatinAlfrm();
            aksesuarsatinal.Show();
        }

        private void Siparislerbtn_Click(object sender, EventArgs e)
        {
            Siparislerfrm siparisler = new Siparislerfrm();
            siparisler.Show();
        }

        private void Subelerbtn_Click(object sender, EventArgs e)
        {
            Subelerfrm subeler = new Subelerfrm();
            subeler.Show();
        }

        private void Satislarbtn_Click(object sender, EventArgs e)
        {
            Satislarfrm satislar = new Satislarfrm();
            satislar.Show();
        }

        private void Maliyetlerbtn_Click(object sender, EventArgs e)
        {
            Maliyetlerfrm maliyetler = new Maliyetlerfrm();
            maliyetler.Show();
        }

        private void Carilerbtn_Click(object sender, EventArgs e)
        {
            Carilerfrm cariler = new Carilerfrm();
            cariler.Show();
        }



        bool move;
        int mouse_x;
        int mouse_y;


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(move)
            this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
        }

        private void btnHakkinda_Click(object sender, EventArgs e)
        {
            Hakkindafrm hakkinda = new Hakkindafrm();
            hakkinda.Show();
        }

        private void btnAsagiAlma_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }



    







}
