using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mouse_de_copy
{
    public partial class Selector : Form
    {
        public Point start, end;

        public Selector()
        {
            InitializeComponent();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            start = this.PointToScreen(new Point(e.X, e.Y));
            pictureBox1.Visible = true;
            this.Capture = true;
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            end = this.PointToScreen(new Point(e.X, e.Y));
            if (start.X > end.X)
            {
                var temp = start.X;
                start.X = end.X;
                end.X = temp;
            }
            if (start.Y > end.Y)
            {
                var temp = start.Y;
                start.Y = end.Y;
                end.Y = temp;
            }
            this.Close();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Selector_MouseMove(object sender, MouseEventArgs e)
        {
            end = this.PointToScreen(new Point(e.X, e.Y));

            pictureBox1.Top = start.Y < end.Y ? start.Y : end.Y;
            pictureBox1.Left = start.X < end.X ? start.X : end.X;
            pictureBox1.Width = Math.Abs(start.X - end.X);
            pictureBox1.Height = Math.Abs(start.Y - end.Y);
        }
    }
}
