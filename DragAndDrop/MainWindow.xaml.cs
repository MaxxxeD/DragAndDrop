using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragAndDrop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow ()
        {
            InitializeComponent ();
            double x = 2;
            double y = 2;
            for (int i = 0; i < 20; ++i) {
                BacklogEntry backlog = new BacklogEntry ();
                Canvas.SetLeft (backlog, x);
                Canvas.SetTop (backlog, y);
                x += backlog.Width;
                if (x > 1590) {
                    y += backlog.Height;
                    x = 2;
                }
                myCanvas.Children.Add (backlog);
            }
        }
    }
}
