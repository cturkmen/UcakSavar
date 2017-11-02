using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UcakSavar
{
    public class UcakOyunu
    {
        private Timer tmrUretici, tmrMermi, tmrKontrol, tmrUcak;
        public bool OyunDurduMu { get; set; } = false;
        public int Skor { get; set; } = 0;
        public Ucaksavar Ucaksavar { get; set; }
        public List<Ucak> Ucaklar { get; set; } = new List<Ucak>();
        private ContainerControl tasiyici;

        public UcakOyunu(ContainerControl tasiyici)
        {
            this.tasiyici = tasiyici;
            this.Ucaksavar = new Ucaksavar(tasiyici);
            tmrMermi = new Timer();
            tmrMermi.Interval = 30;
            tmrMermi.Tick += TmrMermi_Tick;
            tmrMermi.Start();

            tmrUretici = new Timer();
            tmrUretici.Interval = 1200;
            tmrUretici.Tick += TmrUretici_Tick;
            tmrUretici.Start();

            tmrUcak = new Timer();
            tmrUcak.Interval = 120;
            tmrUcak.Tick += TmrUcak_Tick;
            tmrUcak.Start();

            tmrKontrol = new Timer();
            tmrKontrol.Interval = 5;
            tmrKontrol.Tick += TmrKontrol_Tick;
            tmrKontrol.Start();

        }

        private void TmrKontrol_Tick(object sender, EventArgs e)
        {
            foreach (var ucak in Ucaklar)
            {
                Rectangle ur = new Rectangle();
                Rectangle mr = new Rectangle();

                bool vurduMu = false;
                if (ucak.ResimKutusu.Location.Y + ucak.ResimKutusu.Height > tasiyici.Height - 50)
                {
                    OyunDurduMu = true;
                    tmrKontrol.Stop();
                    tmrMermi.Stop();
                    tmrUcak.Stop();
                    tmrUretici.Stop();
                }



                foreach (var roket in this.Ucaksavar.Roketler)
                {
                    ur.X = ucak.ResimKutusu.Left;
                    ur.Y = ucak.ResimKutusu.Top;
                    ur.Height = ucak.ResimKutusu.Height;
                    ur.Width = ucak.ResimKutusu.Width;

                    mr.X = roket.ResimKutusu.Left;
                    mr.Y = roket.ResimKutusu.Top;
                    mr.Height = roket.ResimKutusu.Height;
                    mr.Width = roket.ResimKutusu.Width;

                    if (ur.IntersectsWith(mr))
                    {
                        tasiyici.Controls.Remove(ucak.ResimKutusu);
                        tasiyici.Controls.Remove(roket.ResimKutusu);
                        Ucaklar.Remove(ucak);
                        Ucaksavar.Roketler.Remove(roket);
                        Skor++;
                        vurduMu = true;

                        SoundPlayer sp = new SoundPlayer(Properties.Resources.bomb_small);
                        sp.Play();

                        if (Skor % 10 == 0 && Skor > 1 && tmrUretici.Interval > 2)
                            tmrUretici.Interval = -1;
                        break;
                    }

                }
                if (vurduMu) break;
            }

            foreach (var item in this.Ucaksavar.Roketler)
            {
                if (item.ResimKutusu.Location.Y<0)
                {
                    this.Ucaksavar.Roketler.Remove(item);
                    tasiyici.Controls.Remove(item.ResimKutusu);
                    break;
                }
            }
        }

        private void TmrUcak_Tick(object sender, EventArgs e)
        {
            foreach (var item in Ucaklar)
            {
                item.HAreketEt(Yonler.Asagi);
            }

        }

        Random rnd = new Random();
        private void TmrUretici_Tick(object sender, EventArgs e)//uçak üretilio
        {
            Point point = new Point()
            {
                X = rnd.Next(60, tasiyici.Width - 120),
                Y = 0
            };
            Ucak ucak = new Ucak(point);
            Ucaklar.Add(ucak);
            tasiyici.Controls.Add(ucak.ResimKutusu);


        }

        private void TmrMermi_Tick(object sender, EventArgs e)
        {
            foreach (var item in this.Ucaksavar.Roketler)
            {
                item.HAreketEt(Yonler.Yukari);
            }

        }
    }
}
