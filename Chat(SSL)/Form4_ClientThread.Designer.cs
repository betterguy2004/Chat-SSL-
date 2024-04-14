namespace Lab03
{
    partial class Form4_ClientThread
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
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.yournameTxtBox = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.btnInit = new System.Windows.Forms.Button();
            this.btn_picture = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(33, 605);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Message";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.richTextBox1.Location = new System.Drawing.Point(37, 640);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(651, 92);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "Xin chao";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(744, 640);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 93);
            this.button1.TabIndex = 9;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP Remote host";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox1.Location = new System.Drawing.Point(179, 26);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(259, 30);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(470, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Port";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox2.Location = new System.Drawing.Point(534, 26);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(235, 30);
            this.textBox2.TabIndex = 11;
            this.textBox2.Text = "8080";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(33, 529);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Your name";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // yournameTxtBox
            // 
            this.yournameTxtBox.Location = new System.Drawing.Point(37, 575);
            this.yournameTxtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.yournameTxtBox.Name = "yournameTxtBox";
            this.yournameTxtBox.Size = new System.Drawing.Size(203, 26);
            this.yournameTxtBox.TabIndex = 14;
            this.yournameTxtBox.Text = "Hieu";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.richTextBox2.Location = new System.Drawing.Point(37, 91);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(797, 406);
            this.richTextBox2.TabIndex = 12;
            this.richTextBox2.Text = "";
            this.richTextBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox2_MouseClick);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(799, 28);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(84, 29);
            this.btnInit.TabIndex = 15;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btn_picture
            // 
            this.btn_picture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_picture.Location = new System.Drawing.Point(955, 629);
            this.btn_picture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_picture.Name = "btn_picture";
            this.btn_picture.Size = new System.Drawing.Size(119, 103);
            this.btn_picture.TabIndex = 16;
            this.btn_picture.Text = "Send picture";
            this.btn_picture.UseVisualStyleBackColor = true;
            this.btn_picture.Click += new System.EventHandler(this.btn_picture_Click);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(851, 206);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(128, 291);
            this.pictureBoxImage.TabIndex = 17;
            this.pictureBoxImage.TabStop = false;
            // 
            // Form4_ClientThread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 748);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.btn_picture);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.yournameTxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form4_ClientThread";
            this.Text = "Form4_ClientThread";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form4_ClientThread_FormClosed);
            this.Load += new System.EventHandler(this.Form4_ClientThread_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yournameTxtBox;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btn_picture;
        private System.Windows.Forms.PictureBox pictureBoxImage;
    }
}