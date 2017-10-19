using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestures
{
    /// <summary>
    /// Interaction logic for PDFPresentation.xaml
    /// </summary>
    public partial class PDFPresentation : Window
    {
        private string path;
        private string[] NameOfFile = new string[5000];
        private int[] arrayOfDrawing3DIndex = new int[5000];
        private int numberOfImg = 0;
        private int sizeOFNameOfFile = 0;
        List<Grid> gridArr = new List<Grid>();
        List<DrawOnPDFPage> listDraw = new List<DrawOnPDFPage>();


        public PDFPresentation(String path)
        {
            InitializeComponent();
            this.path = path;
            getImages();
        }

        public void getImages()
        {
            DirectoryInfo dir = new DirectoryInfo(this.path);
            FileInfo[] Files = dir.GetFiles("*.jpg");

            int i = 0;
            foreach (FileInfo file in Files)
            {
                this.NameOfFile[i] = file.Name;
                listDraw.Add(new DrawOnPDFPage(i, this.path, this.NameOfFile[i]));
                i++;
            }
            this.sizeOFNameOfFile = i;

            Grid newGrid = new Grid();
            newGrid.Name = "g" + this.numberOfImg.ToString();
            gridDraw.Children.Add(newGrid);
            Grid.SetRow(newGrid, 1);
            Grid.SetColumn(newGrid, 1);
            gridArr.Add(newGrid);

            newGrid.Children.Add(listDraw[0]);
            listDraw[0].setImgToInkCanvas();
        }

        private void ClickButtonPrevImg(object sender, RoutedEventArgs e)
        {
            if (this.numberOfImg > 0)
            {
                this.numberOfImg--;
                this.gridArr[this.numberOfImg].Visibility = Visibility.Visible;
                this.gridArr[this.numberOfImg+1].Visibility = Visibility.Hidden;
            }
        }

        private void ClickButtonNextImg(object sender, RoutedEventArgs e)
        {
            if (this.numberOfImg < (listDraw.Count - 1))
            {
                this.numberOfImg++;
                bool isAdded = false;

                if((this.gridArr.Count) > this.numberOfImg)
                {
                    if (this.gridArr[this.numberOfImg].Name == "g" + this.numberOfImg.ToString())
                    {
                        isAdded = true;
                    }
                }

                if (isAdded == false)
                {
                    Grid newGrid = new Grid();
                    newGrid.Name = "g" + this.numberOfImg.ToString();
                    gridDraw.Children.Add(newGrid);
                    Grid.SetRow(newGrid, 1);
                    Grid.SetColumn(newGrid, 1);
                    this.gridArr.Add(newGrid);

                    this.gridArr[this.numberOfImg-1].Visibility = Visibility.Hidden;

                    newGrid.Children.Add(listDraw[this.numberOfImg]);
                    listDraw[this.numberOfImg].setImgToInkCanvas();
                }
                else
                {
                    this.gridArr[this.numberOfImg].Visibility = Visibility.Visible;
                    this.gridArr[this.numberOfImg - 1].Visibility = Visibility.Hidden;
                }
            }
        }

        private void Click_Drawing3D(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
