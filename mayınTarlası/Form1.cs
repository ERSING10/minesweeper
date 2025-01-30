namespace minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load_1(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen isminizi giriniz!",
                               "Hata",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }

            
            if (!int.TryParse(textBox3.Text, out int boyut))
            {
                MessageBox.Show("Boyut için sayýsal bir deðer giriniz!",
                               "Hata",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }

            if (boyut < 4 || boyut > 30)
            {
                MessageBox.Show("Boyut 4 ile 30 arasýnda olmalýdýr!",
                               "Hata",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }

            
            if (!int.TryParse(textBox2.Text, out int mayinSayisi))
            {
                MessageBox.Show("Mayýn sayýsý için sayýsal bir deðer giriniz!",
                               "Hata",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }

            if (mayinSayisi < 10)
            {
                MessageBox.Show("Mayýn sayýsý 10'dan az olamaz!",
                               "Hata",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }

            Form2 gameForm = new Form2(boyut, mayinSayisi, textBox1.Text);
            gameForm.ShowDialog();

            
        }





    }
}
