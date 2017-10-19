using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace Gestures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String pathDir;
        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertPDFToJPG(String inputPDFFile, String outputImagesPath)
        {
            string ghostScriptPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\..\")) + @"ghostscript\gs\gs9.10\bin\gswin32.exe";
            String ars = "-dNOPAUSE -sDEVICE=jpeg -r300 -o" + outputImagesPath + "%d.jpg -sPAPERSIZE=a4 " + "\"" + inputPDFFile + "\"";
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = ghostScriptPath;
            proc.StartInfo.Arguments = ars;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }

        private void ButtonLoadPDF_Click(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "GPD|*.pdf";
            if (ofd.ShowDialog()==true)
            {
                this.pathDir = System.IO.Path.GetDirectoryName(ofd.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension((ofd.SafeFileName).Replace(" ", string.Empty)) + "_img\\";
                if (System.IO.Directory.Exists(this.pathDir) == false)
                {
                    System.IO.DirectoryInfo dir = System.IO.Directory.CreateDirectory(this.pathDir);
                }
                
                ConvertPDFToJPG(ofd.FileName, this.pathDir);
                
                PDFPresentation pdfp = new PDFPresentation(pathDir);
                pdfp.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void ButtonAdobeReader_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Directory.GetCurrentDirectory()+"../../../../../Programy/AdobeReaderController/AdobeReaderController/bin/Debug/AdobeReaderController.exe");
        }

        private void ButtonPowerPoint_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Directory.GetCurrentDirectory() + "../../../../../Programy/PowerPointController2/PowerPointController2/bin/Debug/PowerPointController2.exe");
        }
    }
}
