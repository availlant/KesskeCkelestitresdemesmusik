using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExtraireNomsFichiers
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Pas assez d'arguments
            if (args.Length < 1)
            {
                return;
            }

            string directoryPath = args[0];

            // Le dossier n'existe pas / L'argument de désigne pas un dossier
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            StringBuilder chaineDesNomsDeFichier = new StringBuilder();

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

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

            Clipboard.SetText(chaineDesNomsDeFichier.ToString());
        }
    }
}
