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
            fileDataGridView = new System.Windows.Forms.DataGridView();
            yourFiles = new System.Windows.Forms.Button();
            changeviewCatalog_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)fileDataGridView).BeginInit();
            SuspendLayout();
            // 
            // fileDataGridView
            // 
            fileDataGridView.Location = new System.Drawing.Point(247, 12);
            fileDataGridView.Name = "fileDataGridView";
            fileDataGridView.Size = new System.Drawing.Size(807, 638);
            fileDataGridView.TabIndex = 2;
            // 
            // yourFiles
            // 
            yourFiles.Location = new System.Drawing.Point(56, 65);
            yourFiles.Name = "yourFiles";
            yourFiles.Size = new System.Drawing.Size(148, 55);
            yourFiles.TabIndex = 3;
            yourFiles.Text = "Vos fichiers";
            yourFiles.UseVisualStyleBackColor = true;
            yourFiles.Click += yourFiles_Click;
            // 
            // changeviewCatalog_button
            // 
            changeviewCatalog_button.Location = new System.Drawing.Point(56, 157);
            changeviewCatalog_button.Name = "changeviewCatalog_button";
            changeviewCatalog_button.Size = new System.Drawing.Size(148, 55);
            changeviewCatalog_button.TabIndex = 4;
            changeviewCatalog_button.Text = "Catalogue";
            changeviewCatalog_button.UseVisualStyleBackColor = true;
            changeviewCatalog_button.Click += changeviewCatalog_button_Click;
            // 
            // LobbyPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1066, 662);
            Controls.Add(changeviewCatalog_button);
            Controls.Add(yourFiles);
            Controls.Add(fileDataGridView);
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)fileDataGridView).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button yourFiles;
        private System.Windows.Forms.Button changeviewCatalog_button;

        private System.Windows.Forms.DataGridView fileDataGridView;

        #endregion
    }
}