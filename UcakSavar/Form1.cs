using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UcakSavar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UcakOyunu oyun;

        private void Form1_Load(object sender, EventArgs e)
        {
            



        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                oyun?.Ucaksavar.HAreketEt(Yonler.Saga);
            else if (e.KeyCode == Keys.Left)
                oyun?.Ucaksavar.HAreketEt(Yonler.Sola);
            else if (e.KeyCode == Keys.Space)
                oyun?.Ucaksavar.AtesEt();
            else if (e.KeyCode == Keys.Enter)
            {
                if (oyun == null)
                {
                    this.Controls.Clear();
                    oyun = new UcakOyunu(this);//this->ContainerControl}}
                    timer1.Start();

                }

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = $"Skor: {oyun.Skor}";
            if (oyun.OyunDurduMu)
            {
                timer1.Stop();
                DialogResult cevap = MessageBox.Show("Oyun bitti yeniden başlamakistermisin?","Kaybettin",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    this.Controls.Clear();
                    oyun = new UcakOyunu(this);
                    timer1.Start();
                }
                else
                    Application.Exit();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"{e.Location}";
            oyun?.Ucaksavar.HAreketEt(e.Location);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            oyun?.Ucaksavar.AtesEt();
        }
    }
}
