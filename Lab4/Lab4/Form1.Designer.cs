namespace Lab4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ClassesAmountTextBox = new TextBox();
            OgjectsAmountTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            label4 = new Label();
            button2 = new Button();
            FeaturesCountTextBox = new TextBox();
            label6 = new Label();
            dataGridView2 = new DataGridView();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // ClassesAmountTextBox
            // 
            ClassesAmountTextBox.Location = new Point(12, 51);
            ClassesAmountTextBox.Name = "ClassesAmountTextBox";
            ClassesAmountTextBox.Size = new Size(118, 23);
            ClassesAmountTextBox.TabIndex = 0;
            // 
            // OgjectsAmountTextBox
            // 
            OgjectsAmountTextBox.Location = new Point(161, 51);
            OgjectsAmountTextBox.Name = "OgjectsAmountTextBox";
            OgjectsAmountTextBox.Size = new Size(187, 23);
            OgjectsAmountTextBox.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 33);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 3;
            label1.Text = "Число классов";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(144, 33);
            label2.Name = "label2";
            label2.Size = new Size(217, 15);
            label2.TabIndex = 4;
            label2.Text = "Число обьектов обучающей выборки";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 108);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(454, 674);
            dataGridView1.TabIndex = 5;
            dataGridView1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 90);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 6;
            label3.Text = "Функции";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.MenuHighlight;
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(598, 33);
            button1.Name = "button1";
            button1.Size = new Size(188, 44);
            button1.TabIndex = 7;
            button1.Text = "Найти решающие функции";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(482, 259);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(304, 23);
            textBox1.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(482, 241);
            label4.Name = "label4";
            label4.Size = new Size(194, 15);
            label4.TabIndex = 10;
            label4.Text = "Признаки обьекта(через запятую)";
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(571, 305);
            button2.Name = "button2";
            button2.Size = new Size(142, 43);
            button2.TabIndex = 12;
            button2.Text = "Классифицировать";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // FeaturesCountTextBox
            // 
            FeaturesCountTextBox.Location = new Point(396, 51);
            FeaturesCountTextBox.Name = "FeaturesCountTextBox";
            FeaturesCountTextBox.Size = new Size(176, 23);
            FeaturesCountTextBox.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(396, 33);
            label6.Name = "label6";
            label6.Size = new Size(103, 15);
            label6.TabIndex = 14;
            label6.Text = "Число признаков";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(820, 108);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(536, 662);
            dataGridView2.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(820, 90);
            label7.Name = "label7";
            label7.Size = new Size(56, 15);
            label7.TabIndex = 16;
            label7.Text = "Выборка";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1393, 818);
            Controls.Add(label7);
            Controls.Add(dataGridView2);
            Controls.Add(label6);
            Controls.Add(FeaturesCountTextBox);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(OgjectsAmountTextBox);
            Controls.Add(ClassesAmountTextBox);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ClassesAmountTextBox;
        private TextBox OgjectsAmountTextBox;
        private Label label1;
        private Label label2;
        private DataGridView dataGridView1;
        private Label label3;
        private Button button1;
        private TextBox textBox1;
        private Label label4;
        private Button button2;
        private TextBox FeaturesCountTextBox;
        private Label label6;
        private DataGridView dataGridView2;
        private Label label7;
    }
}
