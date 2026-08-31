using System.Net.WebSockets;
using System.Threading;

namespace weather_station
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
            this.simple_text1 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.simple_text2 = new System.Windows.Forms.Label();
            this.join_btn = new System.Windows.Forms.Button();
            this.leave_btn = new System.Windows.Forms.Button();
            this.temp_title_lbl = new System.Windows.Forms.Label();
            this.humid_title_lbl = new System.Windows.Forms.Label();
            this.press_title_lbl = new System.Windows.Forms.Label();
            this.temp_mes_lbl = new System.Windows.Forms.Label();
            this.humid_mes_lbl = new System.Windows.Forms.Label();
            this.press_mes_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // simple_text1
            // 
            this.simple_text1.AutoSize = true;
            this.simple_text1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simple_text1.Location = new System.Drawing.Point(283, 21);
            this.simple_text1.Name = "simple_text1";
            this.simple_text1.Size = new System.Drawing.Size(368, 55);
            this.simple_text1.TabIndex = 0;
            this.simple_text1.Text = "Weather Station";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.BackColor = System.Drawing.SystemColors.Desktop;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(512, 97);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(104, 20);
            this.status.TabIndex = 1;
            this.status.Text = "disconnected";
            // 
            // simple_text2
            // 
            this.simple_text2.AutoSize = true;
            this.simple_text2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simple_text2.Location = new System.Drawing.Point(397, 97);
            this.simple_text2.Name = "simple_text2";
            this.simple_text2.Size = new System.Drawing.Size(104, 20);
            this.simple_text2.TabIndex = 2;
            this.simple_text2.Text = "server status:";
            // 
            // join_btn
            // 
            this.join_btn.BackColor = System.Drawing.Color.Lime;
            this.join_btn.Location = new System.Drawing.Point(370, 135);
            this.join_btn.Name = "join_btn";
            this.join_btn.Size = new System.Drawing.Size(75, 23);
            this.join_btn.TabIndex = 3;
            this.join_btn.Text = "Join";
            this.join_btn.UseVisualStyleBackColor = false;
            this.join_btn.Click += new System.EventHandler(this.join_btn_Click);
            // 
            // leave_btn
            // 
            this.leave_btn.BackColor = System.Drawing.Color.Silver;
            this.leave_btn.Location = new System.Drawing.Point(370, 164);
            this.leave_btn.Name = "leave_btn";
            this.leave_btn.Size = new System.Drawing.Size(75, 23);
            this.leave_btn.TabIndex = 4;
            this.leave_btn.Text = "Leave";
            this.leave_btn.UseVisualStyleBackColor = false;
            this.leave_btn.Click += new System.EventHandler(this.leave_btn_Click);
            // 
            // temp_title_lbl
            // 
            this.temp_title_lbl.AutoSize = true;
            this.temp_title_lbl.Location = new System.Drawing.Point(504, 135);
            this.temp_title_lbl.Name = "temp_title_lbl";
            this.temp_title_lbl.Size = new System.Drawing.Size(78, 13);
            this.temp_title_lbl.TabIndex = 5;
            this.temp_title_lbl.Text = "temperature [F]";
            // 
            // humid_title_lbl
            // 
            this.humid_title_lbl.AutoSize = true;
            this.humid_title_lbl.Location = new System.Drawing.Point(504, 148);
            this.humid_title_lbl.Name = "humid_title_lbl";
            this.humid_title_lbl.Size = new System.Drawing.Size(80, 13);
            this.humid_title_lbl.TabIndex = 6;
            this.humid_title_lbl.Text = "humidity       [%]";
            // 
            // press_title_lbl
            // 
            this.press_title_lbl.AutoSize = true;
            this.press_title_lbl.Location = new System.Drawing.Point(504, 161);
            this.press_title_lbl.Name = "press_title_lbl";
            this.press_title_lbl.Size = new System.Drawing.Size(84, 13);
            this.press_title_lbl.TabIndex = 7;
            this.press_title_lbl.Text = "pressure      [Pa]";
            // 
            // temp_mes_lbl
            // 
            this.temp_mes_lbl.AutoSize = true;
            this.temp_mes_lbl.Location = new System.Drawing.Point(589, 135);
            this.temp_mes_lbl.Name = "temp_mes_lbl";
            this.temp_mes_lbl.Size = new System.Drawing.Size(62, 13);
            this.temp_mes_lbl.TabIndex = 8;
            this.temp_mes_lbl.Text = "placeholder";
            // 
            // humid_mes_lbl
            // 
            this.humid_mes_lbl.AutoSize = true;
            this.humid_mes_lbl.Location = new System.Drawing.Point(589, 148);
            this.humid_mes_lbl.Name = "humid_mes_lbl";
            this.humid_mes_lbl.Size = new System.Drawing.Size(62, 13);
            this.humid_mes_lbl.TabIndex = 9;
            this.humid_mes_lbl.Text = "placeholder";
            // 
            // press_mes_lbl
            // 
            this.press_mes_lbl.AutoSize = true;
            this.press_mes_lbl.Location = new System.Drawing.Point(589, 164);
            this.press_mes_lbl.Name = "press_mes_lbl";
            this.press_mes_lbl.Size = new System.Drawing.Size(62, 13);
            this.press_mes_lbl.TabIndex = 10;
            this.press_mes_lbl.Text = "placeholder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 492);
            this.Controls.Add(this.press_mes_lbl);
            this.Controls.Add(this.humid_mes_lbl);
            this.Controls.Add(this.temp_mes_lbl);
            this.Controls.Add(this.press_title_lbl);
            this.Controls.Add(this.humid_title_lbl);
            this.Controls.Add(this.temp_title_lbl);
            this.Controls.Add(this.leave_btn);
            this.Controls.Add(this.join_btn);
            this.Controls.Add(this.simple_text2);
            this.Controls.Add(this.status);
            this.Controls.Add(this.simple_text1);
            this.Name = "Form1";
            this.Text = "weather station main window";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label simple_text1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label simple_text2;
        private System.Windows.Forms.Button join_btn;
        private System.Windows.Forms.Button leave_btn;
        private System.Windows.Forms.Label temp_title_lbl;
        private System.Windows.Forms.Label humid_title_lbl;
        private System.Windows.Forms.Label press_title_lbl;
        private System.Windows.Forms.Label temp_mes_lbl;
        private System.Windows.Forms.Label humid_mes_lbl;
        private System.Windows.Forms.Label press_mes_lbl;
    }
}

