namespace weather_station_project_net8
{
    partial class weather_station_client
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
            title_lbl = new Label();
            join_btn = new Button();
            leave_btn = new Button();
            connect_status_lbl = new Label();
            temp_title_lbl = new Label();
            humid_title_lbl = new Label();
            temp_val_lbl = new Label();
            press_title_lbl = new Label();
            press_val_lbl = new Label();
            humid_val_lbl = new Label();
            SuspendLayout();
            // 
            // title_lbl
            // 
            title_lbl.AutoSize = true;
            title_lbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            title_lbl.Location = new Point(232, 9);
            title_lbl.Name = "title_lbl";
            title_lbl.Size = new Size(282, 37);
            title_lbl.TabIndex = 0;
            title_lbl.Text = "Weather Station Client";
            // 
            // join_btn
            // 
            join_btn.Location = new Point(232, 66);
            join_btn.Name = "join_btn";
            join_btn.Size = new Size(75, 23);
            join_btn.TabIndex = 1;
            join_btn.Text = "Join";
            join_btn.UseVisualStyleBackColor = true;
            // 
            // leave_btn
            // 
            leave_btn.Location = new Point(232, 95);
            leave_btn.Name = "leave_btn";
            leave_btn.Size = new Size(75, 23);
            leave_btn.TabIndex = 2;
            leave_btn.Text = "Leave";
            leave_btn.UseVisualStyleBackColor = true;
            // 
            // connect_status_lbl
            // 
            connect_status_lbl.AutoSize = true;
            connect_status_lbl.BackColor = Color.FromArgb(255, 128, 128);
            connect_status_lbl.Location = new Point(313, 70);
            connect_status_lbl.Name = "connect_status_lbl";
            connect_status_lbl.Size = new Size(78, 15);
            connect_status_lbl.TabIndex = 3;
            connect_status_lbl.Text = "disconnected";
            // 
            // temp_title_lbl
            // 
            temp_title_lbl.AutoSize = true;
            temp_title_lbl.Location = new Point(413, 70);
            temp_title_lbl.Name = "temp_title_lbl";
            temp_title_lbl.Size = new Size(54, 15);
            temp_title_lbl.TabIndex = 4;
            temp_title_lbl.Text = "Temp [F]";
            // 
            // humid_title_lbl
            // 
            humid_title_lbl.AutoSize = true;
            humid_title_lbl.Location = new Point(413, 95);
            humid_title_lbl.Name = "humid_title_lbl";
            humid_title_lbl.Size = new Size(78, 15);
            humid_title_lbl.TabIndex = 5;
            humid_title_lbl.Text = "Humidity [%]";
            // 
            // temp_val_lbl
            // 
            temp_val_lbl.AutoSize = true;
            temp_val_lbl.Location = new Point(490, 70);
            temp_val_lbl.Name = "temp_val_lbl";
            temp_val_lbl.Size = new Size(22, 15);
            temp_val_lbl.TabIndex = 6;
            temp_val_lbl.Text = "---";
            // 
            // press_title_lbl
            // 
            press_title_lbl.AutoSize = true;
            press_title_lbl.Location = new Point(413, 121);
            press_title_lbl.Name = "press_title_lbl";
            press_title_lbl.Size = new Size(75, 15);
            press_title_lbl.TabIndex = 7;
            press_title_lbl.Text = "Pressure [Pa]";
            // 
            // press_val_lbl
            // 
            press_val_lbl.AutoSize = true;
            press_val_lbl.Location = new Point(490, 121);
            press_val_lbl.Name = "press_val_lbl";
            press_val_lbl.Size = new Size(22, 15);
            press_val_lbl.TabIndex = 8;
            press_val_lbl.Text = "---";
            // 
            // humid_val_lbl
            // 
            humid_val_lbl.AutoSize = true;
            humid_val_lbl.Location = new Point(490, 95);
            humid_val_lbl.Name = "humid_val_lbl";
            humid_val_lbl.Size = new Size(22, 15);
            humid_val_lbl.TabIndex = 9;
            humid_val_lbl.Text = "---";
            // 
            // weather_station_client
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(humid_val_lbl);
            Controls.Add(press_val_lbl);
            Controls.Add(press_title_lbl);
            Controls.Add(temp_val_lbl);
            Controls.Add(humid_title_lbl);
            Controls.Add(temp_title_lbl);
            Controls.Add(connect_status_lbl);
            Controls.Add(leave_btn);
            Controls.Add(join_btn);
            Controls.Add(title_lbl);
            Name = "weather_station_client";
            Text = "Weather Station";
            Load += weather_station_client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title_lbl;
        private Button join_btn;
        private Button leave_btn;
        private Label connect_status_lbl;
        private Label temp_title_lbl;
        private Label humid_title_lbl;
        private Label temp_val_lbl;
        private Label press_title_lbl;
        private Label press_val_lbl;
        private Label humid_val_lbl;
    }
}
