using System.ComponentModel;
using Bit_Ruisseau.Classes;
using Bit_Ruisseau.Utils;

namespace Bit_Ruisseau.Pages;

public partial class CatalogPage : Form
{
    

    public CatalogPage()
    {
        InitializeComponent();
        this.dataGridView1.DataSource = Utils.Utils.CatalogList;
        this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 || e.RowIndex < Utils.Utils.CatalogList.Count)
        {
            if (!Utils.Utils.DownloadStarted)
            {
                MediaData selectedMedia = Utils.Utils.CatalogList[e.RowIndex];
                MessageUtilis.AskFile(selectedMedia);
                Utils.Utils.DownloadStarted = true;
            
                Console.WriteLine("File selected : " + Utils.Utils.CatalogList[e.RowIndex].Title);
                MessageBox.Show("Téléchargement en cours, veuillez patienter.");
            }
            else
            {
                MessageBox.Show("Un téléchargement est déjà en cours, veuillez patienter.");
            }
        }
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
        Utils.MessageUtilis.AskCatalog();
        Thread.Sleep(5000);
        this.dataGridView1.DataSource = null;
        this.dataGridView1.Rows.Clear();
        this.dataGridView1.ColumnCount = 5;
        Utils.Utils.CatalogList.ForEach(media => { this.dataGridView1.Rows.Add(media.Title, media.Artist, media.Type, media.Size, media.Duration); });
        this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridView1.Refresh();
    }
}