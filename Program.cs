using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace mouse_de_copy
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            CaptureAndCopy();
        }

        static void CaptureAndCopy()
        {
            double ratio = 0.5;
            var f = new Selector();

            // ディスプレイが複数ある場合でも全画面を覆うようにウィンドウサイズを調整
            int left, top, right, bottom;
            left = Screen.AllScreens.Min((s) => s.Bounds.Left);
            top = Screen.AllScreens.Min((s) => s.Bounds.Top);
            right = Screen.AllScreens.Max((s) => s.Bounds.Right);
            bottom = Screen.AllScreens.Max((s) => s.Bounds.Bottom);
            f.Left = left;
            f.Top = top;
            f.Width = right - left;
            f.Height = bottom - top;

            f.ShowDialog();
            var end = f.end;
            var start = f.start;
            Bitmap bmp = new Bitmap(end.X - start.X, end.Y - start.Y);
            Bitmap bmp2 = new Bitmap((int)(bmp.Width * ratio), (int)(bmp.Height * ratio));
            f.Dispose();
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(f.start, new Point(0, 0), new Size(bmp.Width, bmp.Height));
            using (var g = Graphics.FromImage(bmp2))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, bmp2.Width, bmp2.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            }
            Clipboard.SetImage(bmp2);
            bmp.Dispose();
            bmp2.Dispose();
        }
    }
}
