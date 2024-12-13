namespace Bit_Ruisseau.Pages
{
    partial class LobbyPage
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
            sendButton = new System.Windows.Forms.Button();
            fileDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)fileDataGridView).BeginInit();
            SuspendLayout();
            // 
            // sendButton
            // 
            sendButton.Location = new System.Drawing.Point(81, 51);
            sendButton.Name = "sendButton";
            sendButton.Size = new System.Drawing.Size(75, 23);
            sendButton.TabIndex = 1;
            sendButton.Text = "button1";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // fileDataGridView
            // 
            fileDataGridView.Location = new System.Drawing.Point(247, 12);
            fileDataGridView.Name = "fileDataGridView";
            fileDataGridView.Size = new System.Drawing.Size(807, 638);
            fileDataGridView.TabIndex = 2;
            // 
            // LobbyPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1066, 662);
            Controls.Add(fileDataGridView);
            Controls.Add(sendButton);
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)fileDataGridView).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView fileDataGridView;

        #endregion

        private System.Windows.Forms.Button sendButton;
    }
}