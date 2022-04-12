namespace kutuphane
{
    partial class AksesuarEklefrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AksesuarEklefrm));
            this.btnİptal = new System.Windows.Forms.Button();
            this.btnUyeEkle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.txtAksesuarAdi = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResimEklebtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtMaliyet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMiktar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUrunNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResim = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnİptal
            // 
            this.btnİptal.BackColor = System.Drawing.Color.White;
            this.btnİptal.Location = new System.Drawing.Point(182, 456);
            this.btnİptal.Name = "btnİptal";
            this.btnİptal.Size = new System.Drawing.Size(75, 23);
            this.btnİptal.TabIndex = 21;
            this.btnİptal.Text = "İptal";
            this.btnİptal.UseVisualStyleBackColor = false;
            this.btnİptal.Click += new System.EventHandler(this.btnİptal_Click);
            // 
            // btnUyeEkle
            // 
            this.btnUyeEkle.BackColor = System.Drawing.Color.White;
            this.btnUyeEkle.Location = new System.Drawing.Point(92, 456);
            this.btnUyeEkle.Name = "btnUyeEkle";
            this.btnUyeEkle.Size = new System.Drawing.Size(75, 23);
            this.btnUyeEkle.TabIndex = 20;
            this.btnUyeEkle.Text = "Ekle";
            this.btnUyeEkle.UseVisualStyleBackColor = false;
            this.btnUyeEkle.Click += new System.EventHandler(this.btnUyeEkle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(100, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Resmi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(105, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Fiyatı";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(88, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ürün Adı";
            // 
            // txtFiyat
            // 
            this.txtFiyat.Location = new System.Drawing.Point(150, 188);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(100, 22);
            this.txtFiyat.TabIndex = 10;
            this.txtFiyat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiyat_KeyPress);
            // 
            // txtAksesuarAdi
            // 
            this.txtAksesuarAdi.Location = new System.Drawing.Point(150, 113);
            this.txtAksesuarAdi.Name = "txtAksesuarAdi";
            this.txtAksesuarAdi.Size = new System.Drawing.Size(100, 22);
            this.txtAksesuarAdi.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(103, 299);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 134);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // ResimEklebtn
            // 
            this.ResimEklebtn.BackColor = System.Drawing.Color.White;
            this.ResimEklebtn.Location = new System.Drawing.Point(256, 257);
            this.ResimEklebtn.Name = "ResimEklebtn";
            this.ResimEklebtn.Size = new System.Drawing.Size(27, 23);
            this.ResimEklebtn.TabIndex = 23;
            this.ResimEklebtn.Text = "...";
            this.ResimEklebtn.UseVisualStyleBackColor = false;
            this.ResimEklebtn.Click += new System.EventHandler(this.ResimEklebtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtMaliyet
            // 
            this.txtMaliyet.Location = new System.Drawing.Point(150, 222);
            this.txtMaliyet.Name = "txtMaliyet";
            this.txtMaliyet.Size = new System.Drawing.Size(100, 22);
            this.txtMaliyet.TabIndex = 10;
            this.txtMaliyet.TextChanged += new System.EventHandler(this.txtMaliyet_TextChanged);
            this.txtMaliyet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaliyet_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(94, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Maliyeti";
            // 
            // txtMiktar
            // 
            this.txtMiktar.Location = new System.Drawing.Point(150, 150);
            this.txtMiktar.Name = "txtMiktar";
            this.txtMiktar.Size = new System.Drawing.Size(100, 22);
            this.txtMiktar.TabIndex = 4;
            this.txtMiktar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMiktar_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(72, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Stok Miktarı";
            // 
            // txtUrunNo
            // 
            this.txtUrunNo.Location = new System.Drawing.Point(150, 75);
            this.txtUrunNo.Name = "txtUrunNo";
            this.txtUrunNo.Size = new System.Drawing.Size(100, 22);
            this.txtUrunNo.TabIndex = 4;
            this.txtUrunNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUrunNo_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(88, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ürün No";
            // 
            // txtResim
            // 
            this.txtResim.Location = new System.Drawing.Point(150, 259);
            this.txtResim.Name = "txtResim";
            this.txtResim.Size = new System.Drawing.Size(100, 22);
            this.txtResim.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Crimson;
            this.label15.Location = new System.Drawing.Point(27, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 30);
            this.label15.TabIndex = 27;
            this.label15.Text = "Ürün Ekle";
            // 
            // AksesuarEklefrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::kutuphane.Properties.Resources.background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(355, 515);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.ResimEklebtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnİptal);
            this.Controls.Add(this.btnUyeEkle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResim);
            this.Controls.Add(this.txtMaliyet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMiktar);
            this.Controls.Add(this.txtUrunNo);
            this.Controls.Add(this.txtFiyat);
            this.Controls.Add(this.txtAksesuarAdi);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AksesuarEklefrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aksesuar Ekle ";
            this.Load += new System.EventHandler(this.AksesuarEklefrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnİptal;
        private System.Windows.Forms.Button btnUyeEkle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.TextBox txtAksesuarAdi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ResimEklebtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtMaliyet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMiktar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUrunNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResim;
        private System.Windows.Forms.Label label15;
    }
}