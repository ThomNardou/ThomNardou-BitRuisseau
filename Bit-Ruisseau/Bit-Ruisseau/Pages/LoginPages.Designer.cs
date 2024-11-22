namespace Bit_Ruisseau.Pages
{
    partial class LoginPages
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
            label4 = new Label();
            label3 = new Label();
            passwordBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            connectButton = new Button();
            userBox = new TextBox();
            hostBox = new TextBox();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Magneto", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(287, 94);
            label4.Name = "label4";
            label4.Size = new Size(487, 78);
            label4.TabIndex = 15;
            label4.Text = "Bit-Ruisseau";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(486, 365);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 14;
            label3.Text = "Password";
            // 
            // passwordBox
            // 
            passwordBox.Location = new Point(360, 383);
            passwordBox.Name = "passwordBox";
            passwordBox.Size = new Size(300, 23);
            passwordBox.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(486, 314);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 12;
            label2.Text = "Username";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(499, 224);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 11;
            label1.Text = "Host";
            // 
            // connectButton
            // 
            connectButton.Location = new Point(413, 462);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(196, 58);
            connectButton.TabIndex = 10;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // userBox
            // 
            userBox.Location = new Point(360, 332);
            userBox.Name = "userBox";
            userBox.Size = new Size(300, 23);
            userBox.TabIndex = 9;
            // 
            // hostBox
            // 
            hostBox.Location = new Point(360, 242);
            hostBox.Name = "hostBox";
            hostBox.Size = new Size(300, 23);
            hostBox.TabIndex = 8;
            // 
            // LoginPages
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 603);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(passwordBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(connectButton);
            Controls.Add(userBox);
            Controls.Add(hostBox);
            Name = "LoginPages";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private Label label3;
        private TextBox passwordBox;
        private Label label2;
        private Label label1;
        private Button connectButton;
        private TextBox userBox;
        private TextBox hostBox;
    }
}