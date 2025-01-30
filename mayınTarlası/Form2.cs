using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace minesweeper
{
    public partial class Form2 : Form
    {
        private Oyun oyun;
        private int boyut;
        private int mayinSayisi;
        private string oyuncuAdi;

        public Form2(int boyut, int mayinSayisi, string oyuncuAdi)
        {
            InitializeComponent();
            this.boyut = boyut;
            this.mayinSayisi = mayinSayisi;
            this.oyuncuAdi = oyuncuAdi;

            oyun = new Oyun();
            this.Resize += new EventHandler(Form2_Resize);

            if (timer1 != null)
            {
                timer1.Enabled = false;
                timer1.Tick -= timer1_Tick;
            }

            this.Resize += (sender, e) =>
            {
                labelERS.Width = this.ClientSize.Width;
                labelERS.Location = new Point(0, this.ClientSize.Height - labelERS.Height);
            };

            
            oyun.BaslatOyun(boyut, mayinSayisi, oyuncuAdi, this, panel1,
                           labelTime, hamleSayısı, labelPlayerName);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            int yeniBoyut = Math.Max(this.Width / 30, 4);
            labelPlayerName.Font = new Font(labelPlayerName.Font.FontFamily, yeniBoyut, FontStyle.Bold);
            hamleSayısı.Location = new Point(this.ClientSize.Width - hamleSayısı.Width - 10, 10);

            buttonSkorboard.Location = new Point(this.ClientSize.Width / 2, yeniBoyut * 2); 
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Bu fonk olmadı, Oyun sınıfında yaptım silecem bunu unutma
        }

        private void buttonSkorboard_Click(object sender, EventArgs e)
        {
            SkorboardForm skorboardForm = new SkorboardForm(oyun.Skorboard);
            skorboardForm.ShowDialog();
        }
    }
}