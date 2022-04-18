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
    public partial class Hakkindafrm : Form
    {
        public Hakkindafrm()
        {
            InitializeComponent();
        }

        private void btnKapatma_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        //PENCEREYİ SÜRÜKLEME KODLARI
        bool move;
        int mouse_x;
        int mouse_y;


        private void Hakkindafrm_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Hakkindafrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
        }

        private void Hakkindafrm_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
    }
}
