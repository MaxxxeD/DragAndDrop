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
    public partial class BacklogEntry : UserControl
    {
        private double mMouseX   = 0.0;
        private double mMouseY   = 0.0;
        private bool   mMoving     = false;
        private Point  Label;
        private BacklogEntry tempEntry;

        public INotifyPositionChanged Delegate
        {
            get;
            set;
        }

        public BacklogEntry ()
        {
            InitializeComponent ();
        }

        private void BacklogEntryMouseDown (object theSender, MouseButtonEventArgs theEvent)
        {
            UserControl userControl = theEvent.Source as UserControl;
            if (userControl != null) {
                userControl.CaptureMouse();
                mMoving = true;
                tempEntry = new BacklogEntry ();
                tempEntry.Background = new SolidColorBrush (Color.FromArgb (50, 255, 0, 0));
                Point p = theEvent.GetPosition(null);
                mMouseX = p.X;
                mMouseY = p.Y;
                Delegate.ChangedPositionCanvas (tempEntry, 0, 0, this);
                Label = Mouse.GetPosition (userControl);
                userControl.BorderBrush = new SolidColorBrush (Color.FromArgb (255, 0, 0, 0));
                userControl.BorderThickness = new Thickness (5);
            }
        }

        private void BacklogEntryMouseMove (object theSender, MouseEventArgs theEvent)
        {
            if (mMoving) {
                Point p = theEvent.GetPosition(null);
                mMouseX = p.X;
                mMouseY = p.Y;
                Delegate.ChangedPositionCanvas (tempEntry, mMouseX - Label.X, mMouseY - Label.Y, this);
            }
        }

        private void BacklogEntryMouseUp (object theSender, MouseButtonEventArgs theEvent)
        {
            UserControl userControl = theEvent.Source as UserControl;
            if (userControl != null) {
                userControl.ReleaseMouseCapture();
                Delegate.DeleteElementFromCanvas (tempEntry);
                Delegate.ChangedPosition (this, mMouseX, mMouseY);
                userControl.BorderThickness = new Thickness (0);
                mMouseX = 0.0;
                mMouseY = 0.0;
                mMoving = false;
            }
        }
    }
}
