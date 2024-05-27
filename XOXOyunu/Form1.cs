using System;
using System.Windows.Forms;
using System.Drawing;

namespace XOXOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int kalanSure = 10;
        bool xMi = true;
        Random rnd = new Random();

        private string[] moralBozucuSozler = new string[]
        {
            "Biraz daha çalýþmalýsýn.",
            "Böyle kazanamazsýn.",
            "Dikkatli olmalýsýn.",
            "Bu hamleyle kazanamazsýn.",
            "Daha iyi yapabilirsin.",
            "Bu pek iyi bir hamle deðil.",
            "Kaybetmek üzere gibisin.",
            "Biraz daha düþün.",
            "Bu strateji iþe yaramaz.",
            "Böyle devam edersen zor."
        };

        int sozlerinSuresi = 3;

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name != "btnBaslat") // baþlat butonu kapalý olmasýn
                {
                    button.Enabled = false;
                    button.Click += ButtonClick;
                }
            }
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            OyunuResetle();
            timer1.Start();
            UpdateCursor();
            ButonlarýAç();
            lblSirayiGoster.Text = "${{ secrets.CONNECTION_STRING }}";
        }

        private void OyunuResetle()
        {
            xMi = true;
            kalanSure = 10;
            sozlerinSuresi = 3;
            lblKalanSure.Text = kalanSure.ToString();
            lblSirayiGoster.Text = "Sýra X'te";
            lblSozler.Text = "";
            timer1.Stop();
            UpdateCursor();

            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name != "btnBaslat")
                {
                    button.Text = "";
                }
            }
        }

        private void ButonlarýAç()
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name != "btnBaslat")
                {
                    button.Enabled = true;
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "")
            {
                button.Text = xMi ? "X" : "O";
                xMi = !xMi; // sýrayý deðiþtir
                kalanSure = 10;
                lblKalanSure.Text = kalanSure.ToString();
                UpdateCursor();
                KazanýKontrolEt();
                SirayiGoster();
            }
        }

        private void SirayiGoster()
        {
            lblSirayiGoster.Text = xMi ? "Sýra X'te" : "Sýra O'da";
        }

        private void MoralBozucuSozleriGetir()
        {
            lblSozler.Text = moralBozucuSozler[rnd.Next(moralBozucuSozler.Length)];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (kalanSure > 0)
            {
                kalanSure--;
                lblKalanSure.Text = kalanSure.ToString();
            }
            else
            {
                xMi = !xMi;
                kalanSure = 10;
                lblKalanSure.Text = kalanSure.ToString();
                UpdateCursor();
                SirayiGoster();
            }

            // saniyede bir moral bozucu sözler çok hýzlý oldu
            if (sozlerinSuresi > 0)
            {
                sozlerinSuresi--;
            }
            else
            {
                MoralBozucuSozleriGetir();
                sozlerinSuresi = 3;
            }
        }

        private void UpdateCursor() // CURSOR G�NCELLEMES�
        {
            this.Cursor = xMi ? Cursors.Cross : Cursors.WaitCursor;
        }
        // yorum
        private void KazanýKontrolEt()
        {
            bool kazananXO = false;

            if ((button1.Text == button2.Text) && (button2.Text == button3.Text) && button1.Text != "")
                kazananXO = true;
            else if ((button4.Text == button5.Text) && (button5.Text == button6.Text) && button4.Text != "")
                kazananXO = true;
            else if ((button7.Text == button8.Text) && (button8.Text == button9.Text) && button7.Text != "")
                kazananXO = true;
            else if ((button1.Text == button4.Text) && (button4.Text == button7.Text) && button1.Text != "")
                kazananXO = true;
            else if ((button2.Text == button5.Text) && (button5.Text == button8.Text) && button2.Text != "")
                kazananXO = true;
            else if ((button3.Text == button6.Text) && (button6.Text == button9.Text) && button3.Text != "")
                kazananXO = true;
            else if ((button1.Text == button5.Text) && (button5.Text == button9.Text) && button1.Text != "")
                kazananXO = true;
            else if ((button3.Text == button5.Text) && (button5.Text == button7.Text) && button3.Text != "")
                kazananXO = true;

            if (kazananXO)
            {
                timer1.Stop();
                ButonlarýKapat();
                string kazanan = xMi ? "O" : "X";
                MessageBox.Show($"KAZANAN {kazanan} OLDU!");
                lblSozler.Text = "Yeniden oynamak için BAÞLA tuþuna basýnýz.";
            }
            else if (ButonlarDoluMu()) // hepsinin tek tek != ile if içinde kontrolü çok uzun oldu metota aldým.
            {
                timer1.Stop();
                MessageBox.Show("BERABERE!");
                lblSozler.Text = "Yeniden oynamak için BAÞLA tuþuna basýnýz.";
            }
        }

        private bool ButonlarDoluMu()
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name != "btnBaslat" && button.Text == "")
                {
                    return false;
                }
            }
            return true;
        }

        private void ButonlarýKapat()
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name != "btnBaslat")
                {
                    button.Enabled = false;
                }
            }
        }
    }
}
