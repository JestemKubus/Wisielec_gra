using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wisielec_aplikacja
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }
        string slowo = "";
        List<Label> labels = new List<Label>();
        int ilosc = 0;
        enum CzesciCiala
        {
            Glowa,
            Prawe_oko,
            Lewe_oko,
            Usta,
            Cialo,
            Lewa_reka,
            Prawa_reka,
            Lewa_noga,
            Prawa_noga
        }

        void RysowaniePodstawy()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.FromArgb(24, 30, 110), 10);
            g.DrawLine(p, new Point(229,390), new Point(229, 5));
            g.DrawLine(p, new Point(234, 5), new Point(130, 5));
            g.DrawLine(p, new Point(125, 0), new Point(125, 75));
        }

        void RysowanieCzesciCiala(CzesciCiala czesciCiala)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 4);
            if (czesciCiala == CzesciCiala.Glowa)
            {
                 g.DrawEllipse(p, 95, 73, 60, 60);
            }
            else if (czesciCiala == CzesciCiala.Lewe_oko)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 113, 85, 8, 8);
            }
            else if (czesciCiala == CzesciCiala.Prawe_oko)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 128, 85, 8, 8);
            }
            else if (czesciCiala == CzesciCiala.Usta)
            {
                g.DrawArc(p, 113, 90, 20, 20, 50, 90);
            }
            else if (czesciCiala == CzesciCiala.Cialo)
            {
                g.DrawLine(p, new Point(125, 135 ) , new Point(125, 220 )); 
            }
            else if (czesciCiala == CzesciCiala.Lewa_reka)
            {
                g.DrawLine(p, new Point(125, 160), new Point(62, 120));
            }
            else if (czesciCiala == CzesciCiala.Prawa_reka)
            {
                g.DrawLine(p, new Point(125, 160), new Point(185, 120));
            }

            else if (czesciCiala == CzesciCiala.Prawa_noga)
            {
                g.DrawLine(p, new Point(125, 220), new Point(155, 260));
            }
            else if (czesciCiala == CzesciCiala.Lewa_noga)
            {
                g.DrawLine(p, new Point(125, 220), new Point(95, 260));
            }
        }
        void TworzenieLabel()
        {
               slowo =  LosowanieSlowa();
            char[] chars = slowo.ToCharArray();
            int odleglosc = 515 / chars.Length - 1;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * odleglosc) + 10, 80);
                labels[i].Text = "_";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }
                label1.Text = "Długość wyrazu:" + " " + (chars.Length - 1).ToString();

        }
        string LosowanieSlowa()
         {
            string[] wyrazy = { "Transcendencja", "Szczebrzeszyn", "Wyimaginowany", "Imponderabilia", "Lekkoatletyka", "Metamorfoza", "Prima Aprilis", "Antykoncepcja", "Antyterrorysta", "Konstantynopolitański", "Gżegżółka", "Rzeżączka", "Malkontent", "Emulgacja", "Emulacja", "Onomatopeja", "Oksymoron", "Tranzystor", "Florystyka", "Ekstrapolacja", "Frywolne zakonnice", "Antyterrorystyczne karaluchy", "Aleksandryjskie królestwo", "Wyimaginowany konsultant", "Rubieżne rusałki", "Europarlamentarzysta ortodoksyjny", "Gawiedź onieśmielona", "Chędogi apartamentowiec", "Podatek katastralny", "Ewidencja podatkowa" };
            Random rand = new Random();
            return wyrazy[rand.Next(0,  wyrazy.Length -1 )];


        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            RysowaniePodstawy();
            TworzenieLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char litera = textBox1.Text.ToLower().ToCharArray()[0];

            if (!char.IsLetter(litera))
            {
                MessageBox.Show("Musisz wpisać literę!", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                return;
            }
            if (slowo.Contains(litera))
            {
                char[] litery = slowo.ToCharArray();
                for (int i = 0; i < litery.Length; i++)
                {
                    if (litery[i] == litera)
                    {
                        labels[i].Text = litera.ToString();
                    }
                    textBox1.Text = "";
                }
                foreach (Label l in labels)
                {
                    if (l.Text == "_") return;
                    MessageBox.Show("Wygałeś!","Brawo!");
                    ResetowanieGry();
                }
            }
            else
            {
                MessageBox.Show("Litera nie jest poprawna");
                label2.Text += "  " + litera.ToString() + ", ";
                RysowanieCzesciCiala((CzesciCiala)ilosc);
                    ilosc++; 
                if(ilosc == 9 )
                {
                    MessageBox.Show("Przegrałeś! Słowo do odgadnięcia: " + slowo);
                    ResetowanieGry();
                }
            }
        }

        void ResetowanieGry()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            RysowaniePodstawy();
            LosowanieSlowa();
            TworzenieLabel();
            label2.Text = "";
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == slowo)
            {
                MessageBox.Show("Wygrałeś!","Brawo!");
                ResetowanieGry();
            }
            else
            {
                MessageBox.Show("Wpisane slowo jest złe!");
                RysowanieCzesciCiala((CzesciCiala)ilosc);
                ilosc++;                    
                label2.Text = "";

                if (ilosc == 9)
                {
                    MessageBox.Show("Przegrałeś! Słowo do odgadnięcia: " + slowo);
                    ResetowanieGry();
                }
            }
        }
    }
}
