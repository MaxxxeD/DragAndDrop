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
    public partial class MainWindow : Window , INotifyPositionChanged
    {
        public MainWindow ()
        {
            InitializeComponent ();
            int x = 0;
            int y = 0;
            for (int i = 0; i < 5; ++i) {
                BacklogEntry backlog = new BacklogEntry ();
                myGrid.Children.Add (backlog);
                backlog.Delegate = this;
                Grid.SetRow (backlog, y);
                Grid.SetColumn (backlog, x);
                x ++;
                if (x > 5) {
                    y ++;
                    x = 0;
                }
            }
        }

        public void ChangedPosition (UserControl theElement, double theX, double theY)
        {
            double rows    = 4;
            double columns = 5;

            double columnWidth = Width / columns;
            double rowHeight   = Height / rows;

            int calculatedColumn = 0;
            int calculatedRow = 0;
            for (int i = 0; i < columns; ++i) {
                double currentItemX = columnWidth * i;
                double nextItemX = columnWidth * (i + 1);
                if (theX > currentItemX && theX < nextItemX) {
                    calculatedColumn = i;
                    break;
                }
            }
            for (int i = 0; i < rows; ++i) {
                if (theY > rowHeight * i && theY < rowHeight * (i + 1)) {
                    calculatedRow = i;
                    break;
                }
            }
            Grid.SetColumn (theElement, calculatedColumn);
            Grid.SetRow    (theElement, calculatedRow);
        }

        public void ChangedPositionCanvas (UserControl theElement, double theX, double theY, UserControl theParentElement)
        {
            if (! myCanvas.Children.Contains (theElement)) {
                myCanvas.Children.Add (theElement);
                int row = Grid.GetRow (theParentElement);
                int column = Grid.GetColumn (theParentElement);

                double columnWidth = Width / 5;
                double rowHeight   = Height / 4;

                Canvas.SetLeft (theElement, column * columnWidth);
                Canvas.SetTop (theElement, row * rowHeight);
            } else {
                double rows    = 4;
                double columns = 5;

                double columnWidth = Width / columns;
                double rowHeight   = Height / rows;

                double calculatedX = 0;
                double calculatedY = 0;
                for (int i = 0; i < columns; ++i) {
                    double currentItemX = columnWidth * i;
                    double nextItemX = columnWidth * (i + 1);
                    if (theX > currentItemX && theX < nextItemX) {
                        calculatedX = currentItemX;
                        break;
                    }
                }
                for (int i = 0; i < rows; ++i) {
                    if (theY > rowHeight * i && theY < rowHeight * (i + 1)) {
                        calculatedY = i * rowHeight;
                        break;
                    }
                }
                Canvas.SetLeft (theElement, theX);
                Canvas.SetTop (theElement, theY);
            }
        }

        public void DeleteElementFromCanvas (UserControl theElement)
        {
            myCanvas.Children.Remove (theElement);
        }
    }
}
