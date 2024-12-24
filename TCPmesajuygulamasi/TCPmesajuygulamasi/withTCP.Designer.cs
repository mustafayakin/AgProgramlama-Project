namespace TCPmesajuygulamasi
{
    partial class withTCP
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
            this.refresh = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.userSec = new System.Windows.Forms.Button();
            this.whoBox = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.sent = new System.Windows.Forms.Button();
            this.beServer = new System.Windows.Forms.Button();
            this.stopServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(153, 32);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 30);
            this.refresh.TabIndex = 1;
            this.refresh.Text = "yenile";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(-2, 32);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(149, 513);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "username =";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "usr";
            this.label1.Visible = false;
            // 
            // userSec
            // 
            this.userSec.Location = new System.Drawing.Point(153, 217);
            this.userSec.Name = "userSec";
            this.userSec.Size = new System.Drawing.Size(75, 30);
            this.userSec.TabIndex = 1;
            this.userSec.Text = "seç ->";
            this.userSec.UseVisualStyleBackColor = true;
            this.userSec.Click += new System.EventHandler(this.userSec_Click);
            // 
            // whoBox
            // 
            this.whoBox.Location = new System.Drawing.Point(356, 36);
            this.whoBox.Name = "whoBox";
            this.whoBox.Size = new System.Drawing.Size(181, 22);
            this.whoBox.TabIndex = 4;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(356, 391);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(409, 96);
            this.txtMessage.TabIndex = 6;
            this.txtMessage.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Kime";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(353, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mesaj";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(356, 74);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(409, 270);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // sent
            // 
            this.sent.Location = new System.Drawing.Point(690, 503);
            this.sent.Name = "sent";
            this.sent.Size = new System.Drawing.Size(75, 30);
            this.sent.TabIndex = 1;
            this.sent.Text = "gonder";
            this.sent.UseVisualStyleBackColor = true;
            this.sent.Visible = false;
            this.sent.Click += new System.EventHandler(this.sent_Click);
            // 
            // beServer
            // 
            this.beServer.Location = new System.Drawing.Point(590, 28);
            this.beServer.Name = "beServer";
            this.beServer.Size = new System.Drawing.Size(75, 30);
            this.beServer.TabIndex = 1;
            this.beServer.Text = "start";
            this.beServer.UseVisualStyleBackColor = true;
            this.beServer.Click += new System.EventHandler(this.beServer_Click);
            // 
            // stopServer
            // 
            this.stopServer.Location = new System.Drawing.Point(690, 28);
            this.stopServer.Name = "stopServer";
            this.stopServer.Size = new System.Drawing.Size(75, 30);
            this.stopServer.TabIndex = 1;
            this.stopServer.Text = "stop";
            this.stopServer.UseVisualStyleBackColor = true;
            this.stopServer.Click += new System.EventHandler(this.stopServer_Click);
            // 
            // withTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 559);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.whoBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.sent);
            this.Controls.Add(this.stopServer);
            this.Controls.Add(this.beServer);
            this.Controls.Add(this.userSec);
            this.Controls.Add(this.refresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "withTCP";
            this.Text = "withTCP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.withTCP_FormClosing);
            this.Load += new System.EventHandler(this.withTCP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button userSec;
        private System.Windows.Forms.TextBox whoBox;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button sent;
        private System.Windows.Forms.Button beServer;
        private System.Windows.Forms.Button stopServer;
    }
}