
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab6
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
            ChartArea chartArea3 = new ChartArea();
            Legend legend3 = new Legend();
            Series series3 = new Series();
            pictureBox1 = new PictureBox();
            dataGridView1 = new DataGridView();
            initButton = new Button();
            ClassifyButton = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            chart1 = new Chart();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(220, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 286);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(500, 500);
            dataGridView1.TabIndex = 1;
            dataGridView1.TabStop = false;
            // 
            // initButton
            // 
            initButton.Location = new Point(12, 124);
            initButton.Name = "initButton";
            initButton.Size = new Size(182, 50);
            initButton.TabIndex = 2;
            initButton.Text = "Инициализировать";
            initButton.UseVisualStyleBackColor = true;
            initButton.Click += initButton_Click;
            // 
            // ClassifyButton
            // 
            ClassifyButton.Location = new Point(12, 180);
            ClassifyButton.Name = "ClassifyButton";
            ClassifyButton.Size = new Size(182, 50);
            ClassifyButton.TabIndex = 3;
            ClassifyButton.Text = "Классифицировать";
            ClassifyButton.UseVisualStyleBackColor = true;
            ClassifyButton.Click += ClassifyButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(32, 35);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 77);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 5;
            label1.Text = "Число обьектов";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chart1.Legends.Add(legend3);
            chart1.Location = new Point(534, 12);
            chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chart1.Series.Add(series3);
            chart1.Size = new Size(1143, 774);
            chart1.TabIndex = 6;
            chart1.Text = "chart2";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(220, 229);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(85, 19);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "Максимум";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(220, 254);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(81, 19);
            radioButton2.TabIndex = 8;
            radioButton2.Text = "Минимум";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1557, 812);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(chart1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(ClassifyButton);
            Controls.Add(initButton);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button initButton;
        private Button ClassifyButton;
        private TextBox textBox1;
        private Label label1;
        private PictureBox pictureBox1;
        private DataGridView dataGridView1;
        private Chart chart1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
    }
}
