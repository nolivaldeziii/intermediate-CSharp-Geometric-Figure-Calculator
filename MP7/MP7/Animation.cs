using System;
using System.Collections.Generic;
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
using System.Threading;

namespace MP7_ValdezIII
{
    public class Animation : IDisposable
    {
        List<Thread> MyThread;
        double angleReverse = -.5;
        public Animation()
        {
            MyThread = new List<Thread>();
        }
        public void CenterWindowOnScreen(MainWindow x)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = x.Width;
            double windowHeight = x.Height;
            x.Left = (screenWidth / 2) - (windowWidth / 2);
            x.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void AnimateRotation(ref Image S)
        {

            if (((S.RenderTransform as RotateTransform).Angle) > 20)
            {
                angleReverse = -.5;
            }
            else if ((S.RenderTransform as RotateTransform).Angle < -20)
            {
                angleReverse = .5;
            }
            RotateTransform rotate = new RotateTransform((S.RenderTransform as RotateTransform).Angle + angleReverse);
            S.RenderTransform = rotate;


        }

        public void ChangeShape(Image s, int i)
        {
            Uri uriSource;
            switch (i)
            {
                
                case 0:
                    uriSource = new Uri(@"Images/circle.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 1:
                    uriSource = new Uri(@"Images/sphere.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 2:
                    uriSource = new Uri(@"Images/cylinder.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 3:
                    uriSource = new Uri(@"Images/rcc.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 4:
                    uriSource = new Uri(@"Images/square.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 5:
                    uriSource = new Uri(@"Images/rectangle.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 6:
                    uriSource = new Uri(@"Images/parallelogram.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                case 7:
                    uriSource = new Uri(@"Images/triangle.png", UriKind.Relative);
                    s.Source = new BitmapImage(uriSource);
                    break;
                
                default:
                    break;
            }
        }

        public void MainWindowsEpicExit(MainWindow M)
        {
            while (M.Opacity > 0)
            {
                Thread.Sleep(250);
                M.Opacity -= .05;
            }
        }

        public void Dispose()
        {
            foreach (Thread t in MyThread)
            {
                t.Abort();
            }
        }
    }
}
