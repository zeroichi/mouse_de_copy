using System;
using System.Collections.Generic;
using System.Linq;
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
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            double ratio = 1.0;
            if (args.Length>0 && !double.TryParse(args[0], out ratio))
            {
                MessageBox.Show("比率の指定が不正です");
                return;
            }
            using (var f = new SelectForm())
            {
                f.ShowDialog();
                var r = f.SelectedRect;
                if (r.Width == 0 || f.Height == 0) return;
                using (var bmp = CreateBitmapFromScreenRect(r))
                {
                    if (ratio == 1.0)
                        Clipboard.SetImage(bmp);
                    else
                    {
                        using (var scaled = ScaleBitmap(bmp, ratio))
                            Clipboard.SetImage(scaled);
                    }
                }
            }
        }

        static Bitmap CreateBitmapFromScreenRect(Rectangle rect)
        {
            var bmp = new Bitmap(rect.Width, rect.Height);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(rect.Location, Point.Empty, bmp.Size);
            return bmp;
        }

        static Bitmap ScaleBitmap(Bitmap bmp, double ratio)
        {
            var scaled = new Bitmap((int)(bmp.Width * ratio), (int)(bmp.Height * ratio));
            using (var g = Graphics.FromImage(scaled))
                g.DrawImage(bmp, new Rectangle(Point.Empty, scaled.Size), new Rectangle(Point.Empty, bmp.Size), GraphicsUnit.Pixel);
            return scaled;
        }
    }
}
