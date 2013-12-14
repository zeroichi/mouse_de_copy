using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mouse_de_copy
{
    public partial class SelectForm : Form
    {
        private Point start, end; // 選択開始・終了マウス座標
        private bool selecting = false;
        public Rectangle SelectedRect;

        public SelectForm()
        {
            InitializeComponent();
            adjust_size();
        }

        // 画面全体を覆うようにウィンドウサイズを調整する
        private void adjust_size()
        {
            this.Left = Screen.AllScreens.Min((s) => s.Bounds.Left);
            this.Top = Screen.AllScreens.Min((s) => s.Bounds.Top);
            this.Width = Screen.AllScreens.Max((s) => s.Bounds.Right) - this.Left;
            this.Height = Screen.AllScreens.Max((s) => s.Bounds.Bottom) - this.Top;
        }

        private void SelectForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!selecting && e.Button.HasFlag(MouseButtons.Left))
            {
                selecting = true;
                start = PointToScreen(new Point(e.X, e.Y));
            }
            else if (selecting && e.Button.HasFlag(MouseButtons.Right))
            {
                // 選択をキャンセル
                selecting = false;
            }
        }

        private void SelectForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (selecting && e.Button.HasFlag(MouseButtons.Left))
            {
                selecting = false;
                end = PointToScreen(new Point(e.X, e.Y));
                SelectedRect = Rectangle.Union(new Rectangle(start, Size.Empty), new Rectangle(end, Size.Empty));
                Close();
            }
        }

        Rectangle last_rect = Rectangle.Empty;
        private void SelectForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (selecting)
            {
                var r = Rectangle.Union(new Rectangle(start, Size.Empty), new Rectangle(e.Location, Size.Empty));
                using (var g = CreateGraphics())
                {
                    using (var b = new SolidBrush(BackColor))
                        g.FillRectangle(b, last_rect);
                    g.FillRectangle(Brushes.LightPink, r);
                    last_rect = r;
                }
            }
        }
    }
}
