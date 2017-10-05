using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rysowanie
{
    public partial class Form1 : Form
    {
        public Point current = new Point();
        public Point old = new Point();
        public Pen p = new Pen(Color.Black, 5);
        public Graphics g;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();

           // SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            panel1.BackColor = Color.Transparent;
            panel1.Bounds = Screen.PrimaryScreen.Bounds;


            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = Color.AliceBlue;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            this.TopMost = true;

            Application.EnableVisualStyles();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            old = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                current = e.Location;
                g.DrawLine(p, old, current);
                old = current;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            p.Color = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
