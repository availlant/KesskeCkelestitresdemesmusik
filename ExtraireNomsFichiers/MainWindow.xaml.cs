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

namespace ExtraireNomsFichiers
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string saveFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();

            if (Directory.Exists(dialog.SelectedPath))
            {
                SelectedPathLabel.Content = dialog.SelectedPath;
            }
            else
            {
                SelectedPathLabel.Content = "Le dossier sélectionné n'existe pas.";
            }
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Fichiers texte (*.txt)|*.txt|Fichiers Word 2007 et +(.docx)|*.docx";
            dialog.ShowDialog();

            if (File.Exists(dialog.FileName))
            {
                SelectedFileNameLabel.Content = dialog.SafeFileName;
                saveFilePath = dialog.FileName;
            }
            else
            {
                SelectedFileNameLabel.Content = "Le fichier sélectionné n'existe pas.";
                saveFilePath = String.Empty;
            }
        }

        private void ExtractMusicFilesNamesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(SelectedPathLabel.Content.ToString()))            
            {
                ResultLabel.Content = "Le dossier où sont contenues les musiques n'existe pas.";
                ResultLabel.Foreground = Brushes.Red;
                return;
            }

            if (!File.Exists(saveFilePath))
            {
                ResultLabel.Content = "Le fichier où il faut écrire les titres des musiques n'existe pas.";
                ResultLabel.Foreground = Brushes.Red;
                return;
            }

            StringBuilder chaineDesNomsDeFichier = new StringBuilder();

            DirectoryInfo directoryInfo = new DirectoryInfo(SelectedPathLabel.Content.ToString());

            foreach (var file in directoryInfo.GetFiles())
            {
                if ((file.Attributes & FileAttributes.Hidden) == 0)
                {
                    string fileName = file.Name;
                    int lastPointPosition = file.Name.LastIndexOf('.');
                    string fileNameWithoutExtension = fileName.Remove(lastPointPosition);

                    chaineDesNomsDeFichier.AppendLine(fileNameWithoutExtension + " (" + file.Extension + ")");
                }
            }

            using (StreamWriter fileStream = File.AppendText(saveFilePath))
            {
                fileStream.WriteLine(chaineDesNomsDeFichier);
            }

            ResultLabel.Foreground = Brushes.Green;
            ResultLabel.Content = "Ayé ! A fini !";
        }
    }
}
