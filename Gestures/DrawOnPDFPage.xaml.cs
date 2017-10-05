using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestures
{
    public partial class DrawOnPDFPage : UserControl
    {
        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;
        private int id;
        private String path;
        private String nameOfFile;

        public DrawOnPDFPage(int id, String path, String nameOfFile)
        {
            this.id = id;
            this.path = path;
            this.nameOfFile = nameOfFile;

            InitializeComponent();
            scrollPdfPage.ScrollChanged += OnScrollViewerScrollChanged;
            scrollPdfPage.MouseRightButtonUp += OnMouseRightButtonUp;
            scrollPdfPage.PreviewMouseRightButtonUp += OnMouseRightButtonUp;
            scrollPdfPage.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollPdfPage.PreviewMouseRightButtonDown += OnMouseRightButtonDown;
            scrollPdfPage.MouseMove += OnMouseMove;

            slider.ValueChanged += OnSliderValueChanged;
        }

        public void setImgToInkCanvas()
        {
           
            BitmapImage Bitmap = new BitmapImage(new Uri(this.path + this.nameOfFile, UriKind.Absolute));
            Image[] Img = new Image[1];
            Img[0] = new Image();
            Img[0].Source = Bitmap;
            DrawingCanvas.Width = Bitmap.Width;
            DrawingCanvas.Height = Bitmap.Height;
            Img[0].Width = DrawingCanvas.Width;
            Img[0].Height = DrawingCanvas.Height;
            scrollPdfPage.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 71;
            DrawingCanvas.Children.Add(Img[0]);        
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollPdfPage);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollPdfPage.ScrollToHorizontalOffset(scrollPdfPage.HorizontalOffset - dX);
                scrollPdfPage.ScrollToVerticalOffset(scrollPdfPage.VerticalOffset - dY);
            }
        }

        public void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollPdfPage);
            if (mousePos.X <= scrollPdfPage.ViewportWidth && mousePos.Y <
                scrollPdfPage.ViewportHeight)
            {
                scrollPdfPage.Cursor = Cursors.SizeAll;
                lastDragPoint = mousePos;
                Mouse.Capture(scrollPdfPage);
            }
        }

        public void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value += 0.2;
            }
            if (e.Delta < 0)
            {
                slider.Value -= 0.2;
            }
            e.Handled = true;
        }

        public void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollPdfPage.Cursor = Cursors.Arrow;
            scrollPdfPage.ReleaseMouseCapture();
            lastDragPoint = null;
        }

        public void OnSliderValueChanged(object sender,
             RoutedPropertyChangedEventArgs<double> e)
        {
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;

            var centerOfViewport = new Point(scrollPdfPage.ViewportWidth / 2,
                                             scrollPdfPage.ViewportHeight / 2);
            lastCenterPositionOnTarget = scrollPdfPage.TranslatePoint(centerOfViewport, grid);
        }

        public void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollPdfPage.ViewportWidth / 2,
                                                         scrollPdfPage.ViewportHeight / 2);
                        Point centerOfTargetNow =
                              scrollPdfPage.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / grid.Width;
                    double multiplicatorY = e.ExtentHeight / grid.Height;

                    double newOffsetX = scrollPdfPage.HorizontalOffset -
                                        dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollPdfPage.VerticalOffset -
                                        dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }
                    scrollPdfPage.ScrollToHorizontalOffset(newOffsetX);
                    scrollPdfPage.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        public void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            this.DrawingCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        public void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DrawingCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        public void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            this.DrawingCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        public void RememberButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("MyPicture.bin", FileMode.Create))
            {
                this.DrawingCanvas.Strokes.Save(fs);
            }
        }

        public void savePdfPage()
        {
            Transform transform = this.DrawingCanvas.LayoutTransform;
            
            this.DrawingCanvas.LayoutTransform = null;

            Size size = new Size(this.DrawingCanvas.Width, this.DrawingCanvas.Height);
           
            this.DrawingCanvas.Measure(size);
            this.DrawingCanvas.Arrange(new Rect(size));

            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(this.DrawingCanvas);

            if (Directory.Exists(this.path + "\\edit_img\\") == false)
            {
                DirectoryInfo dir = System.IO.Directory.CreateDirectory(this.path + "\\edit_img\\");
            }

            using (FileStream outStream = new FileStream(this.path + "\\edit_img\\" + System.IO.Path.GetFileNameWithoutExtension(this.nameOfFile) + ".png", FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(outStream);
            }

            this.DrawingCanvas.LayoutTransform = transform;
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            savePdfPage();
        }

        public void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("MyPicture.bin", FileMode.Open, FileAccess.Read))
            {
                StrokeCollection sc = new StrokeCollection(fs);
                this.DrawingCanvas.Strokes = sc;
            }
        }

        public void Drawing3D_Click(object sender, RoutedEventArgs e)
        {
            Drawing3D d3d = new Drawing3D();
            d3d.ShowDialog();
        }

        public void ColorMain_Click(object sender, RoutedEventArgs e)
        {
            if (ColorsList.Visibility == Visibility.Hidden)
                ColorsList.Visibility = Visibility.Visible;
            else
                ColorsList.Visibility = Visibility.Hidden;
        }

        public void ColorChange_Click(object sender, RoutedEventArgs e)
        {
            strokeAttr.Color = (Color)ColorConverter.ConvertFromString(((sender as Button).Background).ToString());
            ColorsList.Visibility = Visibility.Hidden;
            this.DrawingCanvas.EditingMode = InkCanvasEditingMode.Ink;

            Border border = new Border();
            border.Background = ColorMain.Background;

            Border border2 = new Border();
            border2.Background = (sender as Button).Background;

            ColorMain.ClearValue(Border.BackgroundProperty);
            ColorMain.Background = border2.Background;

            (sender as Button).ClearValue(Border.BackgroundProperty);
            (sender as Button).Background = border.Background;
        }

        public void BigerDraw_Click(object sender, RoutedEventArgs e)
        {
            if (strokeAttr.Width < 25) { 
                strokeAttr.Width = strokeAttr.Width + 2;
                strokeAttr.Height = strokeAttr.Height + 2;
            }
        }

        public void SmalerDraw_Click(object sender, RoutedEventArgs e)
        {
            if (strokeAttr.Width >= 5)
            {
                strokeAttr.Width = strokeAttr.Width - 2;
                strokeAttr.Height = strokeAttr.Height - 2;
            }
        }

        private void FullSizePage_Click(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleX = 1.355;
            scaleTransform.ScaleY = 1.355;
            slider.Value = 1.355;
        }
    }
}
