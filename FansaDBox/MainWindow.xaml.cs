using FansaDBox.data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FansaDBox
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Database Database { get; set; }

        private IEnumerable<Volume> _volumesList;
        private Author selectedAuthor;

        public MainWindow()
        {
            InitializeComponent();

            Database = new Database();
            Database.LoadData();

            author_grid.ItemsSource = Database.Authors;
            SetVolumes( Database.Volumes );

            //Database.SaveAuthors();
            //Database.SaveVolumes();
        }

        private void SetVolumes(IEnumerable<Volume> volumesList)
        {
            _volumesList = volumesList;
            volume_grid.ItemsSource = volumesList;
        }

        /// <summary>
        /// On double-click on an author, filter the volume list.
        /// If the same author is selected twice, reset the filter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Author_grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            Author newSelection = dg.CurrentItem as Author;
            if (selectedAuthor != newSelection)
            {
                selectedAuthor = newSelection;
                SetVolumes(Database.Volumes.Where(v => v.Authors.Contains(selectedAuthor)).ToList());
            }
            else
            {
                selectedAuthor = null;
                SetVolumes(Database.Volumes);
            }
        }

        /// <summary>
        /// On double-click on a volume, Open the pdf or ask the user to find it if the file is not found.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Volume_grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            Volume selectedVolume = dg.CurrentItem as Volume;
            if (File.Exists(selectedVolume.Filepath))
            {
                FileInfo fi = new FileInfo(selectedVolume.Filepath);
                System.Diagnostics.Process.Start("explorer.exe", fi.FullName);
            }
            else
            {
                // find the file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                {
                    openFileDialog.InitialDirectory = Database.Config.pdfFolder;
                    openFileDialog.Filter = "pdf file (*.pdf)|*.pdf";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == true)
                    {
                        selectedVolume.Filepath = openFileDialog.FileName;
                        volume_grid.ItemsSource = null;
                        SetVolumes(_volumesList);
                        Database.SaveVolumes();
                    }
                }
            }
        }

        #region Cell Editing Events
        private Boolean _save_authors = false;
        private Boolean _save_volumes = false;
        private void Author_grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _save_authors = true;
        }

        private void Volume_grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _save_volumes = true;
        }

        private void Author_grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (_save_authors)
            {
                _save_authors = false;
                Database.SaveAuthors();
            }
        }

        private void Volume_grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (_save_volumes)
            {
                _save_volumes = false;
                Database.SaveVolumes();
            }
        } 
        #endregion
    }
}
