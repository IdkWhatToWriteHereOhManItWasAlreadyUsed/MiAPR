namespace Test
{
    partial class Form1
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
            pictureBox = new PictureBox();
            lblFunction = new Label();
            txtX1 = new TextBox();
            lblResult = new Label();
            txtY1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(291, 22);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(700, 700);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // lblFunction
            // 
            lblFunction.AutoSize = true;
            lblFunction.Location = new Point(57, 44);
            lblFunction.Name = "lblFunction";
            lblFunction.Size = new Size(38, 15);
            lblFunction.TabIndex = 1;
            lblFunction.Text = "label1";
            // 
            // txtX1
            // 
            txtX1.Location = new Point(48, 186);
            txtX1.Name = "txtX1";
            txtX1.Size = new Size(100, 23);
            txtX1.TabIndex = 2;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(48, 168);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(38, 15);
            lblResult.TabIndex = 3;
            lblResult.Text = "label1";
            // 
            // txtY1
            // 
            txtY1.Location = new Point(154, 186);
            txtY1.Name = "txtY1";
            txtY1.Size = new Size(100, 23);
            txtY1.TabIndex = 4;
            // 
            // button1
            // 
            button1.BackColor = Color.Chartreuse;
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(48, 227);
            button1.Name = "button1";
            button1.Size = new Size(206, 34);
            button1.TabIndex = 5;
            button1.Text = "Классифицировать";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(48, 390);
            button2.Name = "button2";
            button2.Size = new Size(206, 39);
            button2.TabIndex = 6;
            button2.Text = "Сгенерировать выборку";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1015, 742);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtY1);
            Controls.Add(lblResult);
            Controls.Add(txtX1);
            Controls.Add(lblFunction);
            Controls.Add(pictureBox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Label lblFunction;
        private TextBox txtX1;
        private Label lblResult;
        private TextBox txtY1;
        private Button button1;
        private Button button2;
    }
}