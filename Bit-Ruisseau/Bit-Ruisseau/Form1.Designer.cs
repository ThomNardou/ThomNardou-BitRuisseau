namespace Bit_Ruisseau
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
            hostBox = new TextBox();
            userBox = new TextBox();
            connectButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            passwordBox = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // hostBox
            // 
            hostBox.Location = new Point(423, 193);
            hostBox.Name = "hostBox";
            hostBox.Size = new Size(300, 23);
            hostBox.TabIndex = 0;
            // 
            // userBox
            // 
            userBox.Location = new Point(423, 283);
            userBox.Name = "userBox";
            userBox.Size = new Size(300, 23);
            userBox.TabIndex = 1;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(476, 413);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(196, 58);
            connectButton.TabIndex = 2;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(562, 175);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 3;
            label1.Text = "Host";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(549, 265);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(549, 316);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 6;
            label3.Text = "Password";
            // 
            // passwordBox
            // 
            passwordBox.Location = new Point(423, 334);
            passwordBox.Name = "passwordBox";
            passwordBox.Size = new Size(300, 23);
            passwordBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Magneto", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(350, 45);
            label4.Name = "label4";
            label4.Size = new Size(487, 78);
            label4.TabIndex = 7;
            label4.Text = "Bit-Ruisseau";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1133, 642);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(passwordBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(connectButton);
            Controls.Add(userBox);
            Controls.Add(hostBox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox hostBox;
        private TextBox userBox;
        private Button connectButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox passwordBox;
        private Label label4;
    }
}
