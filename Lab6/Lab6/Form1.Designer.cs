
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
            pictureBox1 = new PictureBox();
            dataGridView1 = new DataGridView();
            initButton = new Button();
            ClassifyButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(1045, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 400);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(539, 11);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(500, 500);
            dataGridView1.TabIndex = 1;
            dataGridView1.TabStop = false;
            // 
            // initButton
            // 
            initButton.Location = new Point(159, 113);
            initButton.Name = "initButton";
            initButton.Size = new Size(182, 50);
            initButton.TabIndex = 2;
            initButton.Text = "Инициализировать";
            initButton.UseVisualStyleBackColor = true;
            initButton.Click += initButton_Click;
            // 
            // ClassifyButton
            // 
            ClassifyButton.Location = new Point(159, 231);
            ClassifyButton.Name = "ClassifyButton";
            ClassifyButton.Size = new Size(182, 50);
            ClassifyButton.TabIndex = 3;
            ClassifyButton.Text = "Классифицировать";
            ClassifyButton.UseVisualStyleBackColor = true;
            ClassifyButton.Click += ClassifyButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1557, 523);
            Controls.Add(ClassifyButton);
            Controls.Add(initButton);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private static PictureBox pictureBox1;
        private static DataGridView dataGridView1;
        private Button initButton;
        private Button ClassifyButton;
    }
}
