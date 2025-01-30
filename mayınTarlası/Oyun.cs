using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace minesweeper
{
    public class Oyun
    {
       
        private int boyut;
        private int mayinSayisi;
        private string oyuncuAdi;
        private int gecenSure;
        private const int buttonSize = 25;
        private Button[,] gridButtons;
        private List<Button> mineButtons;
        private int mevcutMayinIndex;
        private System.Windows.Forms.Timer mineTimer;
        private int openedCells;
        private int correctFlags;
        private int placedFlags;
        private int hamleSayaci;
        private Form gameForm;
        private Panel gamePanel;
        private Label timeLabel;
        private Label moveLabel;
        private Label playerLabel;
        private System.Windows.Forms.Timer gameTimer;

        public Skorboard Skorboard { get; private set; }

        public Oyun()
        {
            mineButtons = new List<Button>();
            mineTimer = new System.Windows.Forms.Timer();
            gecenSure = 0;
            mevcutMayinIndex = 0;
            openedCells = 0;
            correctFlags = 0;
            placedFlags = 0;
            hamleSayaci = 0;
            Skorboard = new Skorboard();
        }

        public void BaslatOyun(int boyut, int mayinSayisi, string oyuncuAdi, Form form, Panel panel,
                              Label timeLabel, Label moveLabel, Label playerLabel)
        {
            this.boyut = boyut;
            this.mayinSayisi = mayinSayisi;
            this.oyuncuAdi = oyuncuAdi;
            this.gameForm = form;
            this.gamePanel = panel;
            this.timeLabel = timeLabel;
            this.moveLabel = moveLabel;
            this.playerLabel = playerLabel;

            playerLabel.Text = $" {oyuncuAdi}";

            CreateGrid();
            FormBoyutAyarla();
            MayinYerlestir();

            
             gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += Timer_Tick;
            gameTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gecenSure++;
            timeLabel.Text = gecenSure.ToString();
        }

        private void CreateGrid()
        {
            gridButtons = new Button[boyut, boyut];
            int startX = gamePanel.Left;
            int startY = gamePanel.Bottom + 10;

            
            foreach (Control control in gameForm.Controls.OfType<Button>().ToList())
            {
                gameForm.Controls.Remove(control);
            }

            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    int row = i;
                    int col = j;
                    Button btn = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(startX + (j * buttonSize), startY + (i * buttonSize)),
                        BackColor = Color.WhiteSmoke
                    };

                    btn.Click += (sender, e) => HucreClick(btn, row, col);
                    btn.MouseDown += (sender, e) => HucreSagClick(btn, e);

                    gridButtons[i, j] = btn;
                    gameForm.Controls.Add(btn);
                }
            }
        }

        private void HucreClick(Button btn, int row, int col)
        {
            if (btn.BackColor != Color.WhiteSmoke)
                return;

            HamleSayisiniGuncelle();

            if (btn.Tag != null && btn.Tag.ToString() == "mine")
            {
                btn.Text = "💣";
                btn.BackColor = Color.Red;
                SetButtonsEnabled(false);
                MayinlariSiraylaAc();
                gameTimer.Stop();
            }
            else
            {
                int komsuMayin = CevreMayinSayisi(row, col);
                btn.Text = komsuMayin > 0 ? komsuMayin.ToString() : "";
                BloklariAc(row, col);
            }
        }

        private void HucreSagClick(Button btn, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            if (btn.BackColor == Color.Yellow && btn.Text == "🚩")
            {
                if (btn.Tag != null && btn.Tag.ToString() == "mine")
                    correctFlags--;
                btn.Text = "";
                btn.BackColor = Color.WhiteSmoke;
                placedFlags--;
            }
            else if (btn.BackColor == Color.WhiteSmoke && btn.Text == "")
            {
                btn.Text = "🚩";
                btn.BackColor = Color.Yellow;
                placedFlags++;
                if (btn.Tag != null && btn.Tag.ToString() == "mine")
                    correctFlags++;
            }

            KazanmaKontrol();
            HamleSayisiniGuncelle();
        }

        private void FormBoyutAyarla()
        {
            int gridSize = boyut * buttonSize;

            
            int minWidth = Math.Max(gridSize + 5, 300); 
            int minHeight = Math.Max(gridSize + gamePanel.Height + 70, 400); 
            
            gameForm.ClientSize = new Size(minWidth, minHeight);

            
            int startX = (minWidth - gridSize) / 2;
            int startY = gamePanel.Bottom + ((minHeight - gamePanel.Height - 70 - gridSize) / 2);

            
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    gridButtons[i, j].Location = new Point(startX + (j * buttonSize), startY + (i * buttonSize));
                }
            }
        }

        private void MayinYerlestir()
        {
            Random random = new Random();
            int placeMines = 0;
            while (placeMines < mayinSayisi)
            {
                int row = random.Next(0, boyut);
                int col = random.Next(0, boyut);

                if (gridButtons[row, col].Tag == null)
                {
                    gridButtons[row, col].Tag = "mine";
                    placeMines++;
                }
            }
        }

        private void MayinlariSiraylaAc()
        {
            mineButtons.Clear();
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    if (gridButtons[i, j].Tag != null && gridButtons[i, j].Tag.ToString() == "mine")
                    {
                        mineButtons.Add(gridButtons[i, j]);
                    }
                }
            }

            mineTimer.Interval = 200;
            mineTimer.Tick += SiradakiMayin;
            mineTimer.Start();
        }

        private void SiradakiMayin(object sender, EventArgs e)
        {
            if (mevcutMayinIndex < mineButtons.Count)
            {
                Button btn = mineButtons[mevcutMayinIndex];
                btn.Text = "💣";
                btn.BackColor = Color.Red;
                mevcutMayinIndex++;
            }
            else
            {
                mineTimer.Stop();
                mevcutMayinIndex = 0;
                KaybetmeKontrol();
            }
        }

        private void SetButtonsEnabled(bool enabled)
        {
            foreach (var button in gridButtons)
            {
                button.Enabled = enabled;
            }
        }
        //önemli
        private int CevreMayinSayisi(int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < boyut && j >= 0 && j < boyut &&
                        gridButtons[i, j].Tag != null && gridButtons[i, j].Tag.ToString() == "mine")
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        //önemlix2
        private void BloklariAc(int x, int y)
        {
            if (x < 0 || y < 0 || x >= boyut || y >= boyut)
                return;

            Button btn = gridButtons[x, y];
            if (btn.BackColor == Color.Yellow || btn.Tag?.ToString() == "mine")
                return;

            int sekizliCevre = CevreMayinSayisi(x, y);
            if (sekizliCevre == 0)
            {
                btn.BackColor = Color.Yellow;
                openedCells++;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            BloklariAc(x + i, y + j);
                        }
                    }
                }
            }
            else
            {
                btn.Text = sekizliCevre.ToString();
                btn.BackColor = Color.Yellow;
                openedCells++;
            }

            KazanmaKontrol();
        }

        private void KazanmaKontrol()
        {
            int totalCells = boyut * boyut;
            int nonMineCells = totalCells - mayinSayisi;
            if (correctFlags == mayinSayisi && openedCells == nonMineCells)
            {
                gameTimer.Stop();
                MessageBox.Show("Tebrikler, kazandınız!");

                Skorboard skorboard = new Skorboard(); 
                skorboard.YeniOyuncuEkle(oyuncuAdi, correctFlags, gecenSure);


               
                SkorboardForm skorboardForm = new SkorboardForm(skorboard);
                skorboardForm.ShowDialog();

                SetButtonsEnabled(false);
            }
        }

        private void KaybetmeKontrol()
        {
            bool anyMinePressed = false;
            foreach (var button in gridButtons)
            {
                if (button.Tag != null && button.Tag.ToString() == "mine" && button.BackColor == Color.Red)
                {
                    anyMinePressed = true;
                    break;
                }
            }

            if (anyMinePressed)
            {
                MessageBox.Show("Kaybettiniz!");
                OyunuSifirla();
                YeniOyunBaslat();
            }
        }

        private void HamleSayisiniGuncelle()
        {
            hamleSayaci++;
            moveLabel.Text = $"{hamleSayaci}";
        }

        private void OyunuSifirla()
        {
            foreach (var button in gridButtons)
            {
                gameForm.Controls.Remove(button);
            }
            Array.Clear(gridButtons, 0, gridButtons.Length);

            openedCells = 0;
            correctFlags = 0;
            placedFlags = 0;
            hamleSayaci = 0;
            moveLabel.Text = "0";
            timeLabel.Text = "0";
            gecenSure = 0;
            gameTimer.Stop();
            gameTimer.Start();
        }

        private void YeniOyunBaslat()
        {
            CreateGrid();
            FormBoyutAyarla();
            MayinYerlestir();
        }
    }
}