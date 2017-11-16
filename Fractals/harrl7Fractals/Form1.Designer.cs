namespace harrl7Fractals
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
            this.numDepth = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSnowflake = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dragonScaler = new System.Windows.Forms.TrackBar();
            this.lblDepth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).BeginInit();
            this.panel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragonScaler)).BeginInit();
            this.SuspendLayout();
            // 
            // numDepth
            // 
            this.numDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDepth.Location = new System.Drawing.Point(4, 28);
            this.numDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDepth.Name = "numDepth";
            this.numDepth.Size = new System.Drawing.Size(113, 35);
            this.numDepth.TabIndex = 0;
            this.numDepth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(4, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Triangle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.BackColor = System.Drawing.Color.Black;
            this.panel.Controls.Add(this.panel1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1904, 1041);
            this.panel.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(4, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Tree";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(4, 167);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "Dragon";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.lblDepth);
            this.panel1.Controls.Add(this.btnSnowflake);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dragonScaler);
            this.panel1.Controls.Add(this.numDepth);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 428);
            this.panel1.TabIndex = 5;
            // 
            // btnSnowflake
            // 
            this.btnSnowflake.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSnowflake.Location = new System.Drawing.Point(4, 213);
            this.btnSnowflake.Name = "btnSnowflake";
            this.btnSnowflake.Size = new System.Drawing.Size(113, 40);
            this.btnSnowflake.TabIndex = 6;
            this.btnSnowflake.Text = "Snowflake";
            this.btnSnowflake.UseVisualStyleBackColor = true;
            this.btnSnowflake.Click += new System.EventHandler(this.btnSnowflake_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 40);
            this.label1.TabIndex = 5;
            this.label1.Text = "Dragon\r\nScale";
            // 
            // dragonScaler
            // 
            this.dragonScaler.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dragonScaler.Location = new System.Drawing.Point(4, 287);
            this.dragonScaler.Minimum = 1;
            this.dragonScaler.Name = "dragonScaler";
            this.dragonScaler.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.dragonScaler.Size = new System.Drawing.Size(45, 134);
            this.dragonScaler.TabIndex = 1;
            this.dragonScaler.Value = 2;
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepth.ForeColor = System.Drawing.Color.White;
            this.lblDepth.Location = new System.Drawing.Point(3, 0);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(74, 25);
            this.lblDepth.TabIndex = 7;
            this.lblDepth.Text = "Depth";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragonScaler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numDepth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar dragonScaler;
        private System.Windows.Forms.Button btnSnowflake;
        private System.Windows.Forms.Label lblDepth;
    }
}

