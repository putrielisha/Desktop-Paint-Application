namespace Doodle_231962J
{
    partial class Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.picBoxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnStart.Font = new System.Drawing.Font("Arial Rounded MT Bold", 25.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnStart.Location = new System.Drawing.Point(314, 917);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(570, 105);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Launch";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnExit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 25.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnExit.Location = new System.Drawing.Point(314, 1028);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(570, 105);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picBoxLogo
            // 
            this.picBoxLogo.Image = global::Doodle_231962J.Properties.Resources.LOGO;
            this.picBoxLogo.Location = new System.Drawing.Point(84, 21);
            this.picBoxLogo.Name = "picBoxLogo";
            this.picBoxLogo.Size = new System.Drawing.Size(1127, 890);
            this.picBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxLogo.TabIndex = 0;
            this.picBoxLogo.TabStop = false;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1376, 1161);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.picBoxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxLogo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
    }
}