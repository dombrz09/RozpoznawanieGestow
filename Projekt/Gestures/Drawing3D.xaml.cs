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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Threading;
using System.Globalization;
using System.IO;

namespace Gestures
{
    /// <summary>
    /// Interaction logic for Drawing3D.xaml
    /// </summary>
    public partial class Drawing3D : Window
    {
        String path;
        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
        public Drawing3D()
        {
            InitializeComponent();

            BuildSolid();
        }
        private GeometryModel3D mGeometry;
        private bool mDown;
        private Point mLastPos;

        MeshGeometry3D mesh;

        private void BuildSolid()
        {
            // Define 3D mesh object
            mesh = new MeshGeometry3D();

            // Geometry creation
            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Red));
            mGeometry.Transform = new Transform3DGroup();
            group.Children.Add(mGeometry);
            AddPointAt(0, 0, 0);

            mainPoint = mesh;



            mesh = new MeshGeometry3D();

            // Geometry creation
            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Yellow));
            mGeometry.Transform = new Transform3DGroup();
            group.Children.Add(mGeometry);

        }

        MeshGeometry3D mainPoint = new MeshGeometry3D();





        private void AddPointAt(double x, double y, double z)
        {
            double delta = 0.01;
            mesh.Positions.Add(new Point3D(x - delta, y - delta, z + delta));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(x + delta, y - delta, z + delta));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(x + delta, y + delta, z + delta));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(x - delta, y + delta, z + delta));
            //mesh.Normals.Add(new Vector3D(0, 0, 1));

            mesh.Positions.Add(new Point3D(x - delta, y - delta, z - delta));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Positions.Add(new Point3D(x + delta, y - delta, z - delta));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Positions.Add(new Point3D(x + delta, y + delta, z - delta));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Positions.Add(new Point3D(x + -delta, y + delta, z + -delta));
            //mesh.Normals.Add(new Vector3D(0, 0, -1));

            // Front face
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 0);

            // Back face
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 6);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 5);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 4);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 4);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 7);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 6);

            // Right face
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 5);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 5);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 6);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 2);

            // Top face
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 6);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 6);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 7);

            // Bottom face
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 5);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 4);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 5);

            // Right face
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 4);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 7);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + 4);


        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - e.Delta / 250D);
        }

        int counter = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (GeometryModel3D gm3 in models)
            {
                gm3.Transform = null;
                gm3.Geometry = null;

            }
            for (int i = 0; i < models.Count; i++)
            {
                models[i] = null;
            }
            models.Clear();

        }


        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {


            Point pos = Mouse.GetPosition(viewport);

            double X = pos.X;
            double Y = pos.Y;
            double Z = 0;
            ShowCoursorMove(X, Y, Z);


        }


        public double lastX;
        public double lastY;
        internal void ShowCoursorMove(double X, double Y, double Z)
        {
            lastX = X;
            lastY = Y;
            if (Mouse.MiddleButton == MouseButtonState.Pressed)
            {
                mesh = new MeshGeometry3D();
                load(readfile(path));
            }

            if (Mouse.RightButton == MouseButtonState.Pressed)
            {

                Point3D newPoint = new Point3D(100, 100, 0);

                double delta = 0;
                mainPoint.Positions[0] = new Point3D(newPoint.X - delta, newPoint.Y + -delta, newPoint.Z + delta);
                mainPoint.Positions[1] = new Point3D(newPoint.X + delta, newPoint.Y + -delta, newPoint.Z + delta);
                mainPoint.Positions[2] = new Point3D(newPoint.X + delta, newPoint.Y + delta, newPoint.Z + delta);
                mainPoint.Positions[3] = new Point3D(newPoint.X + -delta, newPoint.Y + delta, newPoint.Z + delta);

                mainPoint.Positions[4] = new Point3D(newPoint.X + -delta, newPoint.Y + -delta, newPoint.Z + -delta);
                mainPoint.Positions[5] = new Point3D(newPoint.X + delta, newPoint.Y + -delta, newPoint.Z + -delta);
                mainPoint.Positions[6] = new Point3D(newPoint.X + delta, newPoint.Y + delta, newPoint.Z + -delta);
                mainPoint.Positions[7] = new Point3D(newPoint.X + -delta, newPoint.Y + delta, newPoint.Z + -delta);




                Point actualPos = new Point(X - viewport.ActualWidth / 2, viewport.ActualHeight / 2 - Y);
                double dx = actualPos.X - mLastPos.X, dy = actualPos.Y - mLastPos.Y;

                double mouseAngle = 0;
                if (dx != 0 && dy != 0)
                {
                    mouseAngle = Math.Asin(Math.Abs(dy) / Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)));
                    if (dx < 0 && dy > 0) mouseAngle += Math.PI / 2;
                    else if (dx < 0 && dy < 0) mouseAngle += Math.PI;
                    else if (dx > 0 && dy < 0) mouseAngle += Math.PI * 1.5;
                }
                else if (dx == 0 && dy != 0) mouseAngle = Math.Sign(dy) > 0 ? Math.PI / 2 : Math.PI * 1.5;
                else if (dx != 0 && dy == 0) mouseAngle = Math.Sign(dx) > 0 ? 0 : Math.PI;

                double axisAngle = mouseAngle + Math.PI / 2;

                Vector3D axis = new Vector3D(0, Math.Sin(axisAngle) * 4, 0);

                //                Vector3D axis = new Vector3D(Math.Cos(axisAngle) * 4, Math.Sin(axisAngle) * 4, 0);

                double rotation = 0.01 * Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

                foreach (GeometryModel3D gm3 in models)
                {

                    Transform3DGroup group = gm3.Transform as Transform3DGroup;
                    QuaternionRotation3D r = new QuaternionRotation3D(new Quaternion(axis, rotation * 180 / Math.PI));
                    group.Children.Add(new RotateTransform3D(r));
                }
                // light.Position = new Point3D(3*axis.X, 3 * axis.Y, 3 * axis.Z);
                mLastPos = actualPos;
            }
            else
            {
                Point actualPos = new Point((X - viewport.ActualWidth / 2) / viewport.ActualHeight, -(Y - viewport.ActualHeight / 2) / viewport.ActualHeight);


                Point3D newPoint = new Point3D(3.9 * actualPos.X, 3.9 * actualPos.Y, (Z - 500) / -300);
                if (viewport.ActualHeight < 1025)
                {
                    newPoint = new Point3D(5 * actualPos.X, 5 * actualPos.Y, (Z - 500) / -300);
                }

                double delta = 0.03;
                mainPoint.Positions[0] = new Point3D(newPoint.X - delta, newPoint.Y + -delta, newPoint.Z + delta);
                mainPoint.Positions[1] = new Point3D(newPoint.X + delta, newPoint.Y + -delta, newPoint.Z + delta);
                mainPoint.Positions[2] = new Point3D(newPoint.X + delta, newPoint.Y + delta, newPoint.Z + delta);
                mainPoint.Positions[3] = new Point3D(newPoint.X + -delta, newPoint.Y + delta, newPoint.Z + delta);

                mainPoint.Positions[4] = new Point3D(newPoint.X + -delta, newPoint.Y + -delta, newPoint.Z + -delta);
                mainPoint.Positions[5] = new Point3D(newPoint.X + delta, newPoint.Y + -delta, newPoint.Z + -delta);
                mainPoint.Positions[6] = new Point3D(newPoint.X + delta, newPoint.Y + delta, newPoint.Z + -delta);
                mainPoint.Positions[7] = new Point3D(newPoint.X + -delta, newPoint.Y + delta, newPoint.Z + -delta);

                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {




                    //Vector3D v = new Vector3D(newPoint.X - lastPoint.X, newPoint.Y - lastPoint.Y, newPoint.Z - lastPoint.Z);

                    if (!(lastPoint.X == 0 && lastPoint.Y == 0 && lastPoint.Z == 0))
                    {
                        AddLine(newPoint, lastPoint);
                    }
                    else
                    {
                        AddPointAt(newPoint.X, newPoint.Y, newPoint.Z);
                    }

                    //int lenght = (int)(v.Length * 90);
                    //for (int i=0;i<= lenght; i++)
                    //{
                    //    AddPointAt((newPoint.X * i + lastPoint.X * (lenght - i)) / lenght,( newPoint.Y * i + lastPoint.Y * (lenght - i)) / lenght, (newPoint.Z * i + lastPoint.Z * (lenght - i)) / lenght);
                    //}
                    lastPoint = newPoint;
                }
            }
        }

        public void AddFace(int f0, int f1, int f2, int f3)
        {
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f0);

            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f1);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f1);

            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f3);

            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f0);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 16 + f3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 8 + f0);
        }

        private void AddLine(Point3D newPoint, Point3D lastPoint)
        {
            AddPointAt(newPoint.X, newPoint.Y, newPoint.Z);

            //AddFace( 3,2,1,0);
            //AddFace( 7,4,5,6);
            //AddFace( 2,6,5,1);
            //AddFace(3,7,6,2);
            //AddFace( 5,4,0,1);
            //AddFace(3,0,4,7);



            AddFace(0, 1, 2, 3);
            AddFace(6, 5, 4, 7);
            AddFace(1, 5, 6, 2);
            AddFace(2, 6, 7, 3);
            AddFace(1, 0, 4, 5);
            AddFace(7, 4, 0, 3);

            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        if (i != j)
            //        {
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - i);
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - j);
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - (j + 8));

            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - j);
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - i);
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - (j + 8));

            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - (i + 8));
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - j);
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - (j + 8));

            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - j);
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - (i + 8));
            //            mesh.TriangleIndices.Add(mesh.Positions.Count - 1 - (j + 8));
            //        }
            //    }
            //}
        }


        Point3D lastPoint = new Point3D(0, 0, 0);

        List<GeometryModel3D> models = new List<GeometryModel3D>();


        DateTime prevRightClick = DateTime.Now;
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (DateTime.Now.Ticks - prevRightClick.Ticks < TimeSpan.TicksPerSecond / 3)
                {
                    if (models.Count != 0)
                    {
                        foreach (GeometryModel3D gm3 in models)
                        {
                            gm3.Transform = null;
                            gm3.Geometry = null;

                        }
                        for (int i = 0; i < models.Count; i++)
                        {
                            models[i] = null;
                        }
                        models.Clear();
                    }
                }
                prevRightClick = DateTime.Now;
            }


            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mesh = new MeshGeometry3D();

                counter = counter + 1;
                if (counter > 4)
                {
                    counter = 0;
                }

                Brush br = Brushes.White;
                if (counter == 0)
                {
                    br = Brushes.Yellow;
                }
                if (counter == 1)
                {
                    br = Brushes.Pink;
                }
                if (counter == 2)
                {
                    br = Brushes.GreenYellow;
                }
                if (counter == 3)
                {
                    br = Brushes.LightBlue;
                }
                mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(br));

                models.Add(mGeometry);
                mGeometry.Transform = new Transform3DGroup();
                group.Children.Add(mGeometry);

                // camera.Position = new Point3D(camera.Position.X, camera.Position.Y, 5);
                mGeometry.Transform = new Transform3DGroup();

            }

            lastPoint = new Point3D(0, 0, 0);
            //    if (e.LeftButton != MouseButtonState.Pressed) return;
            ///	mDown = true;
            Point pos = new Point(lastX, lastY);  //Mouse.GetPosition(viewport);
            mLastPos = new Point(pos.X - viewport.ActualWidth / 2, viewport.ActualHeight / 2 - pos.Y);
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            lastPoint = new Point3D(0, 0, 0);
            mDown = false;
        }

        private void viewport_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void viewport_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B)
            {
                if (models.Count != 0)
                {
                    foreach (GeometryModel3D gm3 in models)
                    {
                        gm3.Transform = null;
                        gm3.Geometry = null;

                    }
                    for (int i = 0; i < models.Count; i++)
                    {
                        models[i] = null;
                    }
                    models.Clear();
                }
            }
        }



        public void load(Object3D object3d)
        {

            foreach (Point3D point in object3d.GeometricVerticiesList)
                mesh.Positions.Add(point);

            int counter = 0;





            foreach (Tface tface in object3d.FaceElementList)
            {
                mesh.TriangleIndices.Add(tface.X - 1);
                mesh.TriangleIndices.Add(tface.Y - 1);
                mesh.TriangleIndices.Add(tface.Z - 1);

                /*    switch (counter)
                    {
                        case 0:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Red));
                            counter = counter + 1;
                            break;
                        case 1:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Green));
                            counter = counter + 1;
                            break;
                        case 2:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Yellow));
                            counter = counter + 1;
                            break;
                        case 3:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Violet));
                            counter = counter + 1;
                            break;
                        case 4:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Orange));
                            counter = counter + 1;
                            break;
                        case 5:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Blue));
                            counter = counter + 1;
                            break;
                        case 6:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Brown));
                            counter = counter + 1;
                            break;
                        case 7:
                            mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Red));
                            counter = 0;
                            break;
                    }*/
                mGeometry = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Red));
                models.Add(mGeometry);

                mGeometry.Transform = new Transform3DGroup();
                group.Children.Add(mGeometry);

                mGeometry.Transform = new Transform3DGroup();
            }

        }


        public Object3D readfile(String path)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");


            Object3D object3d = new Object3D();

            try
            {

                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {

                        Console.WriteLine(line);
                        if (line[0].Equals('v') & line[1].Equals('t'))
                        {
                            string[] split = line.Split(' ');
                            object3d.addTextureCoordinates(Convert.ToDouble(split[1]),
                                              Convert.ToDouble(split[2]),
                                              Convert.ToDouble(split[3]));

                            //Console.WriteLine("TextureCoordinates: " + object3d.testTextureCoordinates());
                        }
                        else if (line[0].Equals('v') & line[1].Equals('n'))
                        {
                            string[] split = line.Split(' ');
                            object3d.addVertexNormals(Convert.ToDouble(split[1]),
                                              Convert.ToDouble(split[2]),
                                              Convert.ToDouble(split[3]));

                            //Console.WriteLine("VertexNormals: " + object3d.testVertexNormals());
                        }
                        else if (line[0].Equals('v') & line[1].Equals('p'))
                        {
                            string[] split = line.Split(' ');
                            object3d.addSpaceVertices(Convert.ToDouble(split[1]),
                                              Convert.ToDouble(split[2]),
                                              Convert.ToDouble(split[3]));

                            //Console.WriteLine("SpaceVertices: " + object3d.testSpaceVertices());
                        }
                        else if (line[0].Equals('v'))
                        {
                            string[] split = line.Split(' ');
                            object3d.addGeometricVerticies(Convert.ToDouble(split[1]),
                                                           Convert.ToDouble(split[2]),
                                                           Convert.ToDouble(split[3]));

                            //Console.WriteLine("GeometricVerticies: " + object3d.testGeometricVerticies());
                        }
                        else if (line[0].Equals('f'))
                        {
                            string[] split = line.Split(' ');
                            object3d.addFaceElement(Convert.ToInt32(split[1]),
                                                    Convert.ToInt32(split[2]),
                                                    Convert.ToInt32(split[3]));
                            //Console.WriteLine("FaceElement: " + object3d.testFaceElement());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            return object3d;
        }

        public class Tface
        {
            public Tface(int X, int Y, int Z)
            {
                this.X = X;
                this.Y = Y;
                this.Z = Z;
            }
            public int X;
            public int Y;
            public int Z;
        }


        public class Object3D
        {
            double shrink = 0.15;
            public void addGeometricVerticies(double X, double Y, double Z)
            {
                GeometricVerticiesList.Add(new Point3D(X * shrink, Y * shrink, Z * shrink));
            }
            public void addTextureCoordinates(double X, double Y, double Z)
            {
                TextureCoordinatesList.Add(new Point3D(X, Y, Z));
            }
            public void addVertexNormals(double X, double Y, double Z)
            {
                VertexNormalsList.Add(new Point3D(X, Y, Z));
            }
            public void addSpaceVertices(double X, double Y, double Z)
            {
                SpaceVerticesList.Add(new Point3D(X, Y, Z));
            }
            public void addFaceElement(int X, int Y, int Z)
            {
                FaceElementList.Add(new Tface(X, Y, Z));
            }

            public List<Point3D> GeometricVerticiesList = new List<Point3D>();
            private List<Point3D> TextureCoordinatesList = new List<Point3D>();
            private List<Point3D> VertexNormalsList = new List<Point3D>();
            private List<Point3D> SpaceVerticesList = new List<Point3D>();
            public List<Tface> FaceElementList = new List<Tface>();

        }

        private void Load_3D(object sender, RoutedEventArgs e)
        {
            if (ofd.ShowDialog() == true)
            {
                path = ofd.FileName;
                mesh = new MeshGeometry3D();
                load(readfile(path));
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
