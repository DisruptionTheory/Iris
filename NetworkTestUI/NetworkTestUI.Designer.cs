namespace NetworkTestUI
{
    partial class NetworkTestUI
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLocalMouseClick = new System.Windows.Forms.Label();
            this.lblLocalEvents = new System.Windows.Forms.Label();
            this.lblLocalMouseMove = new System.Windows.Forms.Label();
            this.btnSendStuff = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(496, 263);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblLocalMouseClick);
            this.panel1.Controls.Add(this.btnSendStuff);
            this.panel1.Controls.Add(this.lblLocalEvents);
            this.panel1.Controls.Add(this.lblLocalMouseMove);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 260);
            this.panel1.TabIndex = 0;
            // 
            // lblLocalMouseClick
            // 
            this.lblLocalMouseClick.AutoSize = true;
            this.lblLocalMouseClick.Location = new System.Drawing.Point(3, 58);
            this.lblLocalMouseClick.Name = "lblLocalMouseClick";
            this.lblLocalMouseClick.Size = new System.Drawing.Size(88, 13);
            this.lblLocalMouseClick.TabIndex = 3;
            this.lblLocalMouseClick.Text = "local mouse click";
            // 
            // lblLocalEvents
            // 
            this.lblLocalEvents.AutoSize = true;
            this.lblLocalEvents.Location = new System.Drawing.Point(3, 7);
            this.lblLocalEvents.Name = "lblLocalEvents";
            this.lblLocalEvents.Size = new System.Drawing.Size(68, 13);
            this.lblLocalEvents.TabIndex = 2;
            this.lblLocalEvents.Text = "Local events";
            // 
            // lblLocalMouseMove
            // 
            this.lblLocalMouseMove.AutoSize = true;
            this.lblLocalMouseMove.Location = new System.Drawing.Point(3, 33);
            this.lblLocalMouseMove.Name = "lblLocalMouseMove";
            this.lblLocalMouseMove.Size = new System.Drawing.Size(92, 13);
            this.lblLocalMouseMove.TabIndex = 2;
            this.lblLocalMouseMove.Text = "local mouse move";
            // 
            // btnSendStuff
            // 
            this.btnSendStuff.Location = new System.Drawing.Point(53, 223);
            this.btnSendStuff.Name = "btnSendStuff";
            this.btnSendStuff.Size = new System.Drawing.Size(99, 23);
            this.btnSendStuff.TabIndex = 1;
            this.btnSendStuff.Text = "Send a packet";
            this.btnSendStuff.UseVisualStyleBackColor = true;
            this.btnSendStuff.Click += new System.EventHandler(this.btnSendStuff_Click);
            // 
            // NetworkTestUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 262);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "NetworkTestUI";
            this.Text = "Iris";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLocalEvents;
        private System.Windows.Forms.Label lblLocalMouseMove;
        private System.Windows.Forms.Label lblLocalMouseClick;
        private System.Windows.Forms.Button btnSendStuff;
    }
}

