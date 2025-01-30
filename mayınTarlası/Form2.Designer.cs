namespace minesweeper
{
    partial class Form2
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            labelERS = new Label();
            buttonSkorboard = new Button();
            hamleSayısı = new Label();
            labelPlayerName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelTime = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonSkorboard);
            panel1.Controls.Add(hamleSayısı);
            panel1.Controls.Add(labelPlayerName);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(labelTime);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(747, 88);
            panel1.TabIndex = 0;
            // 
            // labelERS
            // 
            labelERS.AutoSize = true;
            labelERS.Location = new Point(236, 853);
            labelERS.Name = "labelERS";
            labelERS.Size = new Size(211, 20);
            labelERS.TabIndex = 1;
            labelERS.Text = "ERSİN GÜNERİGÖK 220229012";
            labelERS.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonSkorboard
            // 
            buttonSkorboard.Location = new Point(641, 55);
            buttonSkorboard.Name = "buttonSkorboard";
            buttonSkorboard.Size = new Size(94, 29);
            buttonSkorboard.TabIndex = 2;
            buttonSkorboard.Text = "SkorBoard";
            buttonSkorboard.UseVisualStyleBackColor = true;
            buttonSkorboard.Click += buttonSkorboard_Click;
            // 
            // hamleSayısı
            // 
            hamleSayısı.AutoSize = true;
            hamleSayısı.Location = new Point(687, 21);
            hamleSayısı.Name = "hamleSayısı";
            hamleSayısı.Size = new Size(17, 20);
            hamleSayısı.TabIndex = 1;
            hamleSayısı.Text = "0";
            // 
            // labelPlayerName
            // 
            labelPlayerName.AutoSize = true;
            labelPlayerName.Font = new Font("Imprint MT Shadow", 12F, FontStyle.Underline, GraphicsUnit.Point, 0);
            labelPlayerName.Location = new Point(12, 8);
            labelPlayerName.Name = "labelPlayerName";
            labelPlayerName.Size = new Size(58, 24);
            labelPlayerName.TabIndex = 3;
            labelPlayerName.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(3, 131);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(573, 426);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Font = new Font("Old English Text MT", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTime.Location = new Point(12, 55);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(21, 24);
            labelTime.TabIndex = 1;
            labelTime.Text = "0";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(747, 882);
            Controls.Add(labelERS);
            Controls.Add(panel1);
            Name = "Form2";
            Text = "Form2";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private Label labelTime;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelPlayerName;
        private Label hamleSayısı;
        private Label labelERS;
        private Button buttonSkorboard;
    }
}