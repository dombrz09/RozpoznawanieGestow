using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PPT = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;

namespace PowerPointController2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        OpenFileDialog openFile = new OpenFileDialog();
        Presentations ppPresens;
        Presentation objPres;
        Microsoft.Office.Interop.PowerPoint.Application ppApp;
       
        

        private void button1_Click(object sender, EventArgs e)
        {
            

                openFile.Filter = "GPD|*.pptx";
                if (openFile.ShowDialog() == DialogResult.OK)
                {

                    String path = openFile.FileName;
                    try
                    {
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
                        


                        ppApp.Visible = MsoTriState.msoTrue;
                        ppPresens = ppApp.Presentations;
                        objPres = ppPresens.Open(path, MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoTrue);
                        Microsoft.Office.Interop.PowerPoint.SlideShowWindows objSSWs; Microsoft.Office.Interop.PowerPoint.SlideShowSettings objSSS;

                        objSSS = objPres.SlideShowSettings;
                        objSSS.Run();



                        objSSWs = ppApp.SlideShowWindows;

                        
                            //while (objSSWs.Count >= 1)
                               // System.Threading.Thread.Sleep(100);
                        ppApp.Quit();


                    }).Start();

                }
                    catch (Exception m)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(m.Message);
                    }


                }

            }
           

        private void button2_Click(object sender, EventArgs e)
        {
         
            objPres.SlideShowWindow.View.Next();
            
        }

     
        private void button3_Click(object sender, EventArgs e)
        {
            objPres.SlideShowWindow.View.Previous();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }
    }
}
