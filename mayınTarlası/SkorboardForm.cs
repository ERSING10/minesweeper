using minesweeper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace minesweeper
{
    public partial class SkorboardForm : Form
    {
        private Skorboard skorboard;

        public SkorboardForm(Skorboard skorboard)
        {
            InitializeComponent();
            this.skorboard = skorboard;
            ListeyiGuncelle();
        }

        
        private void ListeyiGuncelle()
        {
            listBox1.Items.Clear();  

            
            List<Oyuncu> oyuncular = skorboard.GetEnIyiOyuncular();
            foreach (var oyuncu in oyuncular)
            {
                listBox1.Items.Add(oyuncu.ToString());  
            }
        }

        
        private void buttonKapat_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        
        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            ListeyiGuncelle();
        }
    }
}

