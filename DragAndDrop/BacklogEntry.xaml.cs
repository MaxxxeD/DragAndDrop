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
        private Point  mBasePoint = new Point (0.0, 0.0);
        private double mDeltaX   = 0.0;
        private double mDeltaY   = 0.0;
        private bool   mMoving     = false;
        private Point  mPositionInLabel;

        public double XPosition
        {
            get { 
                return mBasePoint.X + mDeltaX;
            }
        }

        public double YPosition
        {
            get { 
                return mBasePoint.Y + mDeltaY;
            }
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
                mPositionInLabel = theEvent.GetPosition(userControl);
            }
        }

        private void BacklogEntryMouseMove (object theSender, MouseEventArgs theEvent)
        {
            if (mMoving) {
                Point p = theEvent.GetPosition(null);
                mDeltaX = p.X - mBasePoint.X - mPositionInLabel.X;
                mDeltaY = p.Y - mBasePoint.Y - mPositionInLabel.Y;
                Canvas.SetLeft(this, XPosition);
                Canvas.SetTop(this, YPosition);
            }
        }

        private void BacklogEntryMouseUp (object theSender, MouseButtonEventArgs theEvent)
        {
            UserControl userControl = theEvent.Source as UserControl;
            if (userControl != null) {
                userControl.ReleaseMouseCapture();
                mBasePoint.X += mDeltaX;
                mBasePoint.Y += mDeltaY;
                mDeltaX = 0.0;
                mDeltaY = 0.0;
                mMoving = false;
            }
        }
    }
}
