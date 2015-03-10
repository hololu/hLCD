using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using Ini;

namespace hLCD
{
    public partial class Form1 : Form
    {
        //string marqueeText = "Çok Uzun bir metin dir bu metin çün ki deneme amaçlı olarak yapılmış bir metindir.. 0 545 Ayrıca içerisinde birden çok rakam mevcuttur.";
        String Satir1, Satir2, Satir3, Satir4, Fontu;
        private int xpos = 0, ypos = 200, boyut=0,fonb=82,hiz=100,adim=5;
        public string mode = "<";
        StringFormat strf = new StringFormat();
        int sw = Screen.PrimaryScreen.WorkingArea.Width;
        //int sx = Screen.PrimaryScreen.GetType;

        public Form1()
        {
            this.Left = sw;
            strf.Alignment = StringAlignment.Center;
            strf.LineAlignment = StringAlignment.Center;

            IniFile ini = new IniFile( Application.StartupPath + "\\ayar.ini");

            Satir1 = ini.IniReadValue("bilgiler","doktor");
            Satir2 = ini.IniReadValue("bilgiler", "hemsire");
            Satir3 = ini.IniReadValue("bilgiler", "memur");
            Satir4 = ini.IniReadValue("bilgiler", "eczane");

            Fontu = ini.IniReadValue("ayarlar", "font");
            fonb = Convert.ToInt32(ini.IniReadValue("ayarlar", "boyut"));
            hiz = Convert.ToInt32(ini.IniReadValue("ayarlar", "hiz"));
            adim = Convert.ToInt32(ini.IniReadValue("ayarlar", "adim"));
            mode = ini.IniReadValue("ayarlar", "yon");
            InitializeComponent();
            tmrZaman.Enabled = false;
            tmrZaman.Interval = hiz;
            label1.Text = Satir4;
            boyut = label1.Width; //marqueeText.Length * fonb;
            xpos = this.Width;
            this.DoubleBuffered = true;
            if (Screen.AllScreens.Length <= 1)
            {
                MessageBox.Show("2. Ekran Saptanamadı Lütfen Ayarlarınız Kontrol Ediniz..!");
                
                Application.Exit();
            }

            tmrZaman.Enabled = true;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                Application.Exit();
            }

            if (e.KeyChar == 32)
            {
                if (tmrZaman.Enabled) { tmrZaman.Enabled = false; } else { tmrZaman.Enabled = true; };
            }
        }

         private void tmrZaman_Tick(object sender, EventArgs e)
         {
            //this.Refresh();

            SolidBrush renk = new SolidBrush(Color.FromArgb(227, 19, 28));
            Graphics gra = this.CreateGraphics();
            //gra.Clear(Color.Transparent);
            //gra.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gra.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            Rectangle kutu = new Rectangle(0, 150, this.Width, label1.Height+12);
            gra.DrawString("Nöbetçi Doktor : " + Satir1, new Font(Fontu, fonb, FontStyle.Bold), renk, kutu,strf);

            kutu = new Rectangle(0, 360, this.Width, label1.Height + 12);
            gra.DrawString("Nöbetçi Hemşire : " + Satir2, new Font(Fontu, fonb, FontStyle.Bold), renk, kutu, strf);

            kutu = new Rectangle(0, 560, this.Width, label1.Height + 12);
            gra.DrawString("Nöbetçi Memur : " + Satir3, new Font(Fontu, fonb, FontStyle.Bold), renk, kutu, strf);

            //gra.DrawString("Nöbetçi Eczaneler : " + Satir4, new Font(Fontu, fonb, FontStyle.Bold), Brushes.White , xpos+5, ypos + 550);

            gra.FillRectangle(Brushes.White , 0,  ypos +550,  this.Width, 140);
            //gra.DrawString("Nöbetçi Eczaneler : " + Satir4, new Font(Fontu, fonb, FontStyle.Bold), Brushes.White, xpos+adim, ypos + 550);
            gra.DrawString("Nöbetçi Eczaneler : " + Satir4, new Font(Fontu, fonb, FontStyle.Bold), renk, xpos, ypos + 550);

 
           if (mode == ">")
            {
                if (this.Width == xpos)
                {
                    //this.label1.Location = new System.Drawing.Point(0, ypos);
                    xpos = 0;
                }
                else
                {
                    //this.label1.Location = new System.Drawing.Point(xpos, ypos);
                    xpos += adim;
                }
            }

            else if (mode == "<")
            {
                if (xpos <= boyut*-1)
                {
                    //this.label1.Location = new System.Drawing.Point(this.Width, ypos);
                    this.Refresh();
                    xpos = this.Width;
                }
                else
                {
                    //this.label1.Location = new System.Drawing.Point(xpos, ypos);
                    xpos -= adim;
                }
            }
         }

         private void Form1_Load(object sender, EventArgs e)
         {

         }

        
    }
}
