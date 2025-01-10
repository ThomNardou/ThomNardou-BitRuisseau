using System.ComponentModel;

namespace Bit_Ruisseau.Pages;

partial class CatalogPage
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        dataGridView1 = new System.Windows.Forms.DataGridView();
        label1 = new System.Windows.Forms.Label();
        refreshButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.Location = new System.Drawing.Point(247, 12);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new System.Drawing.Size(807, 638);
        dataGridView1.TabIndex = 0;
        dataGridView1.CellClick += dataGridView1_CellContentClick;
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Magneto", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.Location = new System.Drawing.Point(21, 24);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(199, 76);
        label1.TabIndex = 1;
        label1.Text = "Musiques Disponibles";
        // 
        // refreshButton
        // 
        refreshButton.Location = new System.Drawing.Point(41, 121);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new System.Drawing.Size(162, 63);
        refreshButton.TabIndex = 2;
        refreshButton.Text = "button1";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += refreshButton_Click;
        // 
        // CatalogPage
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1066, 662);
        Controls.Add(refreshButton);
        Controls.Add(label1);
        Controls.Add(dataGridView1);
        Text = "CatalogPage";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button refreshButton;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.DataGridView dataGridView1;

    #endregion
}