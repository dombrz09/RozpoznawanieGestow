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
        private string[] NameOfFile = new string[1000];
        private int[] arrayOfDrawing3DIndex = new int[1000];
        private int numberOfImg = 0;
        private int sizeOFNameOfFile;
        private int amountOfDrawing3D = 0;
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

            this.numberOfImg++;

            Grid newGrid2 = new Grid();
            newGrid2.Name = "g" + this.numberOfImg.ToString();
            gridDraw.Children.Add(newGrid2);
            Grid.SetRow(newGrid2, 1);
            Grid.SetColumn(newGrid2, 1);
            this.gridArr.Add(newGrid2);
            this.gridArr[this.numberOfImg].Visibility = Visibility.Hidden;

            newGrid2.Children.Add(new Drawing3DUC());
            this.numberOfImg--;
        }

        public void checkDrawing3D(int nr)
        {
            bool isIn = false;
            for (int i = 0; i < this.amountOfDrawing3D; i++)
            {
                System.Diagnostics.Debug.WriteLine("XXX "+ this.arrayOfDrawing3DIndex[i]);
                if (this.arrayOfDrawing3DIndex[i] == nr)
                    isIn = true;
            }
            if (isIn == true)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(PDFPresentation))
                    {
                        (window as PDFPresentation).Drawing3D.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(PDFPresentation))
                    {
                        (window as PDFPresentation).Drawing3D.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void ClickButtonPrevImg(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("prev:");
            for (int i = 0; i < this.gridArr.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("arr " + this.gridArr[i].Name);
            }
            System.Diagnostics.Debug.WriteLine("end");


            if (this.numberOfImg > 0)
            {
                this.numberOfImg--;
                if ((this.numberOfImg % 2) != 0)
                {
                    System.Diagnostics.Debug.WriteLine("jestem");
                    checkDrawing3D(this.numberOfImg);

                    bool isIn = false;
                    for (int i = 0; i < this.amountOfDrawing3D; i++)
                    {
                        if (this.arrayOfDrawing3DIndex[i] == this.numberOfImg)
                            isIn = true;
                    }
                    if (isIn == false)
                    {
                        numberOfImg--;
                        int numberInArr = -1;
                        for (int i = 0; i < gridArr.Count; i++)
                        {
                            if (this.gridArr[i].Name != "g" + this.numberOfImg.ToString())
                            {
                                this.gridArr[i].Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                numberInArr = i;
                            }
                        }

                        this.gridArr[numberInArr].Visibility = Visibility.Visible;
                    }
                    else
                    {
                        int numberInArr = -1;
                        for (int i = 0; i < gridArr.Count; i++)
                        {
                            if (this.gridArr[i].Name != "g" + this.numberOfImg.ToString())
                            {
                                this.gridArr[i].Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                numberInArr = i;
                            }
                        }

                        this.gridArr[numberInArr].Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    checkDrawing3D(this.numberOfImg);
                    System.Diagnostics.Debug.WriteLine("wartosc num "+ this.numberOfImg);
                    int numberInArr = -1;
                    for (int i = 0; i < gridArr.Count; i++)
                    {
                        if (this.gridArr[i].Name != "g" + this.numberOfImg.ToString())
                        {
                            this.gridArr[i].Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            numberInArr = i;
                        }
                    }

                    this.gridArr[numberInArr].Visibility = Visibility.Visible;

                }
                

                
            }
        }

        private void ClickButtonNextImg(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("next:");
            for (int i = 0; i < this.gridArr.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("arr " + this.gridArr[i].Name);
            }
            System.Diagnostics.Debug.WriteLine("end");



            if ((this.numberOfImg) < ((2*listDraw.Count) - 1))
            {
                this.numberOfImg++;
                if((this.numberOfImg%2)==0)
                {

                    checkDrawing3D(this.numberOfImg);
                    bool isAdded = false;

                    for (int i = 0; i < gridArr.Count; i++)
                    {
                        if (this.gridArr[i].Name == "g" + this.numberOfImg.ToString())
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

                        for (int i = 0; i < gridArr.Count; i++)
                        {
                            if (this.gridArr[i].Name != ("g" + this.numberOfImg.ToString()))
                            {
                                this.gridArr[i].Visibility = Visibility.Hidden;
                            }
                        }

                        newGrid.Children.Add(listDraw[(this.numberOfImg / 2)]);
                        listDraw[(this.numberOfImg / 2)].setImgToInkCanvas();

                        this.numberOfImg++;

                        Grid newGrid2 = new Grid();
                        newGrid2.Name = "g" + this.numberOfImg.ToString();
                        gridDraw.Children.Add(newGrid2);
                        Grid.SetRow(newGrid2, 1);
                        Grid.SetColumn(newGrid2, 1);
                        this.gridArr.Add(newGrid2);
                        this.gridArr[this.numberOfImg].Visibility = Visibility.Hidden;

                        newGrid2.Children.Add(new Drawing3DUC());

                        this.numberOfImg--;
                    }
                    else
                    {
                        int numberInArr = 0;
                        for (int i = 0; i < gridArr.Count; i++)
                        {
                            if (this.gridArr[i].Name != "g" + this.numberOfImg.ToString())
                            {
                                this.gridArr[i].Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                numberInArr = i;
                            }
                        }

                        this.gridArr[numberInArr].Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    checkDrawing3D(this.numberOfImg);

                    bool isIn = false;

                    for (int i = 0; i < this.amountOfDrawing3D; i++)
                    {
                        if (this.arrayOfDrawing3DIndex[i] == this.numberOfImg)
                            isIn = true;
                    }
                    if (isIn == false)
                    {
                        if ((this.numberOfImg) < ((2 * listDraw.Count) - 1))
                        {

                            this.numberOfImg++;

                            bool isAdded = false;

                            for (int i = 0; i < gridArr.Count; i++)
                            {
                                if (this.gridArr[i].Name == "g" + this.numberOfImg.ToString())
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

                                for (int i = 0; i < gridArr.Count; i++)
                                {
                                    if (this.gridArr[i].Name != ("g" + this.numberOfImg.ToString()))
                                    {
                                        this.gridArr[i].Visibility = Visibility.Hidden;
                                    }
                                }

                                newGrid.Children.Add(listDraw[(this.numberOfImg / 2)]);
                                listDraw[(this.numberOfImg / 2)].setImgToInkCanvas();

                                this.numberOfImg++;

                                Grid newGrid2 = new Grid();
                                newGrid2.Name = "g" + this.numberOfImg.ToString();
                                gridDraw.Children.Add(newGrid2);
                                Grid.SetRow(newGrid2, 1);
                                Grid.SetColumn(newGrid2, 1);
                                this.gridArr.Add(newGrid2);
                                this.gridArr[this.numberOfImg].Visibility = Visibility.Hidden;

                                newGrid2.Children.Add(new Drawing3DUC());

                                this.numberOfImg--;
                            }
                            else
                            {
                                int numberInArr = 0;
                                for (int i = 0; i < gridArr.Count; i++)
                                {
                                    if (this.gridArr[i].Name != "g" + this.numberOfImg.ToString())
                                    {
                                        this.gridArr[i].Visibility = Visibility.Hidden;
                                    }
                                    else
                                    {
                                        numberInArr = i;
                                    }
                                }

                                this.gridArr[numberInArr].Visibility = Visibility.Visible;
                            }
                        }
                        if (this.numberOfImg == ((2 * listDraw.Count) - 1))
                        {
                            this.numberOfImg--;
                        }

                    }
                    else
                    {
                        int numberInArr = -1;
                        for (int i = 0; i < gridArr.Count; i++)
                        {
                            if (this.gridArr[i].Name != "g" + this.numberOfImg.ToString())
                            {
                                this.gridArr[i].Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                numberInArr = i;
                            }
                        }

                        this.gridArr[numberInArr].Visibility = Visibility.Visible;

                    }
                }

                

                
            }
        }

        private void Click_Drawing3D(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PDFPresentation))
                {
                    (window as PDFPresentation).Drawing3D.Visibility = Visibility.Hidden;
                }
            }
            this.numberOfImg++;
            bool isDrawing3D = false;
            for (int i = 0; i < this.amountOfDrawing3D; i++)
            {
                if (this.arrayOfDrawing3DIndex[i] == this.numberOfImg)
                    isDrawing3D = true;
            }
            if (isDrawing3D == false)
            {
                System.Diagnostics.Debug.WriteLine("YYY "+ this.numberOfImg);
                this.arrayOfDrawing3DIndex[this.amountOfDrawing3D] = this.numberOfImg;

                for (int i = 0; i < gridArr.Count; i++)
                {
                    if (this.gridArr[i].Name != ("g" + this.numberOfImg.ToString()))
                    {
                        this.gridArr[i].Visibility = Visibility.Hidden;
                    }
                }

                this.gridArr[this.numberOfImg].Visibility = Visibility.Visible;
                this.amountOfDrawing3D++;
            }
            else
            {
                for (int i = 0; i < gridArr.Count; i++)
                {
                    if (this.gridArr[i].Name != ("g" + this.numberOfImg.ToString()))
                    {
                        this.gridArr[i].Visibility = Visibility.Hidden;
                    }
                }
                this.gridArr[this.numberOfImg].Visibility = Visibility.Visible;
            }

        }
    }
}
