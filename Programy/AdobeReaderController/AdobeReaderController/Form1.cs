using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdobeReaderController
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

        private void button1_Click(object sender, EventArgs e)
        {
            openFile.Filter = "GPD|*.pdf";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                axAcroPDF1.src = openFile.FileName;

            }
        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            axAcroPDF1.gotoNextPage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axAcroPDF1.gotoPreviousPage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            axAcroPDF1.setZoomScroll(40, 20, 70);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            axAcroPDF1.setZoomScroll(130, 130, 120);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            axAcroPDF1.setZoomScroll(100, 100, 100);
        }
    }
}
