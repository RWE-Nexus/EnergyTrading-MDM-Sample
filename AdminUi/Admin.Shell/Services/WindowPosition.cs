using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Shell.Services
{
    public static class WindowPosition
    {
        public static double Left { get; set; }
        public static double Top { get; set; }
        public static double Width { get; set; }
        public static double Height { get; set; }
        public static WindowStartupLocation Location { get; set; }

        static WindowPosition()
        {
            Location = WindowStartupLocation.CenterScreen;
            Height = 620;
            Width = 850;
        }

        public static void SavePosition(double left, double top, double width, double height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Location = WindowStartupLocation.Manual;
        }

        public static void Reposition()
        {
            if (Location == WindowStartupLocation.Manual)
            {
                Application.Current.MainWindow.WindowStartupLocation = Location;
                Application.Current.MainWindow.Left = Left;
                Application.Current.MainWindow.Top = Top;
                Application.Current.MainWindow.Width = Width;
                Application.Current.MainWindow.Height = Height;
            }
        }
    }
}
