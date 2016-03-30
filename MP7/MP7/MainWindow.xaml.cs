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
//using System.Windows.Shapes;
using System.Threading;
using MP2_ValdezIII;

namespace MP7_ValdezIII
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //important global vars
        TextToSpeech MyVoice;
        Animation Animate;
        bool[] RadioInfo = { true, false, false };

        //for new threads
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        //for shapes
        enum ShapeKind { Circles, Spheres, Cylinders, RightCircularCone, Squares, Rectangles, Parallelograms, Triangales };

        ShapeKind MyShapeEnum;

        //for mouse events
        public TimeSpan TimeoutToHide { get; private set; }
        public DateTime LastMouseMove { get; private set; }
        public bool IsHidden { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            MyVoice = new TextToSpeech();
            
            Animate = new Animation();
            MyShapeEnum = new ShapeKind();
            MyShapeEnum = (ShapeKind)0;
            Animate.CenterWindowOnScreen(this);

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(250000);
            dispatcherTimer.Start();

            TimeoutToHide = TimeSpan.FromSeconds(2);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);

            MyVoice.SpeakString("Welcome to Pipboy 3001");
            image_knob.RenderTransform = new RotateTransform(47.8);
            image_shapeVisualizer.RenderTransform = new RotateTransform(0);
            this.RenderTransform = new RotateTransform(0);
            this.KeyDown += new KeyEventHandler(Tab_KeyDown);
            label_system_message.Content = "";
            label_answer.Content = "";
            this.UpdateTextBox();


        }

        private void InitiateComputation()
        {
            try
            {
                Shape MyShape = null;
                ICircle Circles = null;
                ITriangle Triangels = null;
                ISquare Squares = null;
                switch (MyShapeEnum)
                {
                    case ShapeKind.Circles:
                        MyShape = new Circle(Convert.ToDouble(textBox_var1.Text));
                        Circles = new Circle(Convert.ToDouble(textBox_var1.Text));
                        break;
                    case ShapeKind.Spheres:
                        MyShape = new Sphere(Convert.ToDouble(textBox_var1.Text));
                        Circles = new Sphere(Convert.ToDouble(textBox_var1.Text));
                        break;
                    case ShapeKind.Cylinders:
                        MyShape = new Cylinder(Convert.ToDouble(textBox_var1.Text), Convert.ToDouble(textBox_var2.Text));
                        Circles = new Cylinder(Convert.ToDouble(textBox_var1.Text), Convert.ToDouble(textBox_var2.Text));
                        break;
                    case ShapeKind.RightCircularCone:
                        MyShape = new RightCircularCone(Convert.ToDouble(textBox_var1.Text), Convert.ToDouble(textBox_var2.Text));
                        Circles = new RightCircularCone(Convert.ToDouble(textBox_var1.Text), Convert.ToDouble(textBox_var2.Text));
                        break;
                    case ShapeKind.Squares:
                        MyShape = new Square(Convert.ToDouble(textBox_var1.Text));
                        Squares = new Square(Convert.ToDouble(textBox_var1.Text));
                        break;
                    case ShapeKind.Rectangles:
                        MyShape = new Rectangle(Convert.ToDouble(textBox_var1.Text), Convert.ToDouble(textBox_var2.Text));
                        Squares = new Rectangle(Convert.ToDouble(textBox_var1.Text), Convert.ToDouble(textBox_var2.Text));
                        break;
                    case ShapeKind.Parallelograms:
                        MyShape = new Parallelogram(Convert.ToDouble(textBox_var1.Text),
                            Convert.ToDouble(textBox_var2.Text), Convert.ToDouble(textBox_var3.Text));
                        Squares = new Parallelogram(Convert.ToDouble(textBox_var1.Text),
                            Convert.ToDouble(textBox_var2.Text), Convert.ToDouble(textBox_var3.Text));
                        break;
                    case ShapeKind.Triangales:
                        if (RadioInfo[1])
                        {
                            Triangels = new Triangle(Convert.ToDouble(textBox_var1.Text),
                                Convert.ToDouble(textBox_var2.Text),
                                Convert.ToDouble(textBox_var3.Text));
                        }
                        if (RadioInfo[0])
                        {
                            MyShape = new Triangle(Convert.ToDouble(textBox_var1.Text),
                                Convert.ToDouble(textBox_var2.Text));
                        }
                        break;
                    default:
                        break;

                }

                if (MyShape != null)
                {
                    if (RadioInfo[0])
                    {
                        label_answer.Content = string.Format("The Answer is: \n {0}", MyShape.ToString());

                    }
                    else if (RadioInfo[2])
                    {
                        label_answer.Content = string.Format("The Answer is: \n {0:F}", Circles.GetCircumference());
                    }
                    else if (RadioInfo[1])
                    {
                        if (Triangels != null)
                        {
                            label_answer.Content = string.Format("The Answer is: \n {0:F}", Triangels.GetPerimeter());
                        }
                        if (Squares != null)
                        {
                            label_answer.Content = string.Format("The Answer is: \n {0:F}", Squares.GetPerimeter());
                        }
                    }
                }

                MyVoice.SpeakString(label_answer.Content.ToString());
            }
            catch (Exception ex)
            {
                label_system_message.Content = ex.Message;
                MyVoice.SpeakString(ex.Message);
            }

            
        }



        











        #region Animation
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //MyVoice.test();
        }

        private void radio_i_1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RadioClicked((Image)sender, 0);
            if(e != null)
                MyVoice.SpeakString("Computing for Area");


            //computation animation
            //
            ////////////////////////
            textBox_var1.Text = "";
            textBox_var2.Text = "";
            textBox_var3.Text = "";
            textBox_var4.Text = "";
            if (RadioInfo[1] && Convert.ToInt32(MyShapeEnum) == 7)
            {
                this.TextBoxHide1();
                label_var1.Content = "Side1";
                label_var2.Content = "Side2";
                label_var3.Content = "Height";
                radio_i_circumference.IsEnabled = false;
            }
            if (RadioInfo[0] && Convert.ToInt32(MyShapeEnum) == 7)
            {
                this.TextBoxHide2();
                label_var1.Content = "Base";
                label_var2.Content = "Height";
                radio_i_circumference.IsEnabled = false;
            }
            
        }

        private void RadioClicked(Image s, int index)
        {
            for (int i = 0; i < 3; i++)
            {
                RadioInfo[i] = false;
            }
            var uriSource = new Uri(@"Images/radio_off.png", UriKind.Relative);
            var uriSource2 = new Uri(@"Images/radio_disabled.png", UriKind.Relative);
            radio_i_1.Source = new BitmapImage(uriSource);

            if (radio_i_perimeter.IsEnabled)
            {
                radio_i_perimeter.Source = new BitmapImage(uriSource);
            }
            else
            {
                radio_i_perimeter.Source = new BitmapImage(uriSource2);
            }
            if (radio_i_circumference.IsEnabled)
            {
                radio_i_circumference.Source = new BitmapImage(uriSource);
            }
            else
            {
                radio_i_circumference.Source = new BitmapImage(uriSource2);
            }
            
            RadioInfo[index] = true;

            uriSource = new Uri(@"Images/radio_on.png", UriKind.Relative);
            s.Source = new BitmapImage(uriSource);
        }

        private void radio_i_2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            textBox_var1.Text = "";
            textBox_var2.Text = "";
            textBox_var3.Text = "";
            textBox_var4.Text = "";
            RadioClicked((Image)sender, 1);
            MyVoice.SpeakString("Computing for Perimeter");
            
            //computation animation
            //
            //////////////////////
            if (RadioInfo[1] && Convert.ToInt32(MyShapeEnum) == 7)
            {
                this.TextBoxHide1();
                label_var1.Content = "Side1";
                label_var2.Content = "Side2";
                label_var3.Content = "Side3";
                radio_i_circumference.IsEnabled = false;
            }
            if (RadioInfo[0] && Convert.ToInt32(MyShapeEnum) == 7)
            {
                this.TextBoxHide2();
                label_var1.Content = "Base";
                label_var2.Content = "Height";
                radio_i_circumference.IsEnabled = false;
            }
            
        }

        private void radio_i_3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RadioClicked((Image)sender, 2);
            MyVoice.SpeakString("Computing for Circumference");
            textBox_var1.Text = "";
            textBox_var2.Text = "";
            textBox_var3.Text = "";
            textBox_var4.Text = "";
            
        }

        private void image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RotateTransform rotate = new RotateTransform((image_knob.RenderTransform as RotateTransform).Angle + 45);
            image_knob.RenderTransform = rotate;

            if (Convert.ToInt32(MyShapeEnum + 1) < 8) { MyShapeEnum += 1; } else { MyShapeEnum = 0; }
            label_shapevisualizer.Content = MyShapeEnum.ToString();

            Animate.ChangeShape(image_shapeVisualizer, Convert.ToInt32(MyShapeEnum));

            MyVoice.SpeakString(string.Format("The shape {0} is selected", label_shapevisualizer.Content.ToString()));


            //coumputaiton when clicked
            //
            ///////////////////////////

            this.UpdateTextBox();
           
        }

        private void image3_Loaded(object sender, RoutedEventArgs e)
        {
            //Animate.AnimateRotation(ref image_shapeVisualizer);
        }




        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
            Animate.AnimateRotation(ref image_shapeVisualizer);
        }

        void Tab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MyVoice.Mute();
                Animate.Dispose();
                this.Close();
                // Do something
            }
            if (e.Key == Key.Enter)
            {
                label_system_message.Content = "";
                InitiateComputation();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToDouble(textBox_var1.Text);
                MyVoice.SpeakString(string.Format("{0} is {1}",label_var1.Content.ToString(), textBox_var1.Text));
            } catch (FormatException ex)
            {
                label_system_message.Content = ex.Message;
                MyVoice.SpeakString(ex.Message);
                textBox_var1.Text = "0";
            }


        }

        private void textBox_Copy_LostFocus(object sender, RoutedEventArgs e)
        {

            try
            {
                Convert.ToDouble(textBox_var2.Text);
                MyVoice.SpeakString(string.Format("{0} is {1}", label_var2.Content.ToString(), textBox_var2.Text));
            }
            catch (FormatException ex)
            {
                label_system_message.Content = ex.Message;
                MyVoice.SpeakString(ex.Message);
                textBox_var2.Text = "0";
            }

        }

        private void textBox_Copy1_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToDouble(textBox_var3.Text);
                MyVoice.SpeakString(string.Format("{0} is {1}", label_var3.Content.ToString(), textBox_var3.Text));
            }
            catch (FormatException ex)
            {
                label_system_message.Content = ex.Message;
                MyVoice.SpeakString(ex.Message);
                textBox_var3.Text = "0";
            }
        }

        private void textBox_Copy2_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToDouble(textBox_var4.Text);
                MyVoice.SpeakString(string.Format("{0} is {1}", label_var4.Content.ToString(), textBox_var4.Text));
            }
            catch (FormatException ex)
            {
                label_system_message.Content = ex.Message;
                MyVoice.SpeakString(ex.Message);
                textBox_var4.Text = "0";
            }
        }

        private void textBox_var1_GotFocus(object sender, RoutedEventArgs e)
        {
            try { Convert.ToDouble(textBox_var1.Text); }
            catch { textBox_var1.Text = ""; }
        }

        private void textBox_var2_GotFocus(object sender, RoutedEventArgs e)
        {
            try { Convert.ToDouble(textBox_var2.Text); }
            catch { textBox_var2.Text = ""; }
        }

        private void textBox_var3_GotFocus(object sender, RoutedEventArgs e)
        {
            try { Convert.ToDouble(textBox_var3.Text); }
            catch { textBox_var3.Text = ""; }
        }

        private void textBox_var4_GotFocus(object sender, RoutedEventArgs e)
        {
            try { Convert.ToDouble(textBox_var4.Text); }
            catch { textBox_var4.Text = ""; }
        }

        private void TextBoxHide0()
        {
            textBox_var1.Visibility = Visibility.Visible;
            textBox_var2.Visibility = Visibility.Visible;
            textBox_var3.Visibility = Visibility.Visible;
            textBox_var4.Visibility = Visibility.Visible;
            label_var1.Visibility = Visibility.Visible;
            label_var2.Visibility = Visibility.Visible;
            label_var3.Visibility = Visibility.Visible;
            label_var4.Visibility = Visibility.Visible;
        }

        private void TextBoxHide1()
        {
            textBox_var1.Visibility = Visibility.Visible;
            textBox_var2.Visibility = Visibility.Visible;
            textBox_var3.Visibility = Visibility.Visible;
            textBox_var4.Visibility = Visibility.Hidden;
            label_var1.Visibility = Visibility.Visible;
            label_var2.Visibility = Visibility.Visible;
            label_var3.Visibility = Visibility.Visible;
            label_var4.Visibility = Visibility.Hidden;
        }

        private void TextBoxHide2()
        {
            textBox_var1.Visibility = Visibility.Visible;
            textBox_var2.Visibility = Visibility.Visible;
            textBox_var3.Visibility = Visibility.Hidden;
            textBox_var4.Visibility = Visibility.Hidden;
            label_var1.Visibility = Visibility.Visible;
            label_var2.Visibility = Visibility.Visible;
            label_var3.Visibility = Visibility.Hidden;
            label_var4.Visibility = Visibility.Hidden;
        }

        private void TextBoxHide3()
        {
            textBox_var1.Visibility = Visibility.Visible;
            textBox_var2.Visibility = Visibility.Hidden;
            textBox_var3.Visibility = Visibility.Hidden;
            textBox_var4.Visibility = Visibility.Hidden;
            label_var1.Visibility = Visibility.Visible;
            label_var2.Visibility = Visibility.Hidden;
            label_var3.Visibility = Visibility.Hidden;
            label_var4.Visibility = Visibility.Hidden;
        }

        private void UpdateTextBox()
        {
            label_answer.Content = "";

            textBox_var1.Text = "";
            textBox_var2.Text = "";
            textBox_var3.Text = "";
            textBox_var4.Text = "";

            radio_i_perimeter.IsEnabled = true;
            radio_i_circumference.IsEnabled = true;

            switch (Convert.ToInt32(MyShapeEnum))
            {
                case 0:
                    this.TextBoxHide3();
                    label_var1.Content = "Radius";
                    radio_i_perimeter.IsEnabled = false;
                    break;
                case 1:
                    this.TextBoxHide3();
                    label_var1.Content = "Radius";
                    radio_i_perimeter.IsEnabled = false;
                    radio_i_circumference.IsEnabled = false;
                    break;
                case 2:
                    this.TextBoxHide2();
                    label_var1.Content = "Radius";
                    label_var2.Content = "Height";
                    radio_i_perimeter.IsEnabled = false;
                    radio_i_circumference.IsEnabled = false;
                    break;
                case 3:
                    this.TextBoxHide2();
                    label_var1.Content = "Radius";
                    label_var2.Content = "Height";
                    radio_i_perimeter.IsEnabled = false;
                    radio_i_circumference.IsEnabled = false;
                    break;
                case 4:
                    this.TextBoxHide3();
                    label_var1.Content = "Side";
                    radio_i_circumference.IsEnabled = false;
                    break;
                case 5:
                    this.TextBoxHide2();
                    label_var1.Content = "Side 1";
                    label_var2.Content = "Side 2";
                    radio_i_circumference.IsEnabled = false;
                    break;
                case 6:
                    this.TextBoxHide2();
                    label_var1.Content = "Side 1";
                    label_var2.Content = "Side 2";
                    label_var3.Content = "Height";
                    radio_i_circumference.IsEnabled = false;
                    break;
                case 7:
                    radio_i_circumference.IsEnabled = false;
                    if (RadioInfo[1])
                    {
                        this.TextBoxHide1();
                        label_var1.Content = "Side 1";
                        label_var3.Content = "Side 2";
                        label_var3.Content = "Side 3";
                        radio_i_circumference.IsEnabled = false;
                    }
                    if (RadioInfo[2])
                    {
                        this.TextBoxHide2();
                        label_var1.Content = "Base";
                        label_var2.Content = "Height";
                        radio_i_circumference.IsEnabled = false;
                    }
                    break;
                default:
                    this.TextBoxHide1();
                    break;
            }
        }
        private void radio_i_perimeter_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            radio_i_1_MouseLeftButtonUp(radio_i_1, null);
            if (radio_i_perimeter.IsEnabled)
            {
                var uriSource = new Uri(@"Images/radio_off.png", UriKind.Relative);
                radio_i_perimeter.Source = new BitmapImage(uriSource);
            }
            else
            {
                var uriSource = new Uri(@"Images/radio_disabled.png", UriKind.Relative);
                radio_i_perimeter.Source = new BitmapImage(uriSource);
            }

            
        }
        private void radio_i_circumference_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            radio_i_1_MouseLeftButtonUp(radio_i_1, null);
            if (radio_i_circumference.IsEnabled)
            {
                var uriSource = new Uri(@"Images/radio_off.png", UriKind.Relative);
                radio_i_circumference.Source = new BitmapImage(uriSource);
                
            }
            else
            {
                var uriSource = new Uri(@"Images/radio_disabled.png", UriKind.Relative);
                radio_i_circumference.Source = new BitmapImage(uriSource);
            }
            
        }
        private void radio_i_1_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void textBox_var1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            LastMouseMove = DateTime.Now;
        }
        private void image1_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            InitiateComputation();
        }

        private void image1_MouseEnter(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"Images/power_on.png", UriKind.Relative);
            image_power.Source = new BitmapImage(uriSource);
        }

        private void image_power_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"Images/power_off.png", UriKind.Relative);
            image_power.Source = new BitmapImage(uriSource);
        }

        private void image_knob_MouseEnter(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"Images/knob2.png", UriKind.Relative);
            image_knob.Source = new BitmapImage(uriSource);
        }

        private void image_knob_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"Images/knob.png", UriKind.Relative);
            image_knob.Source = new BitmapImage(uriSource);
        }
        private void radio_i_1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!RadioInfo[0])
            {
                var uriSource = new Uri(@"Images/radio_off2.png", UriKind.Relative);
                radio_i_1.Source = new BitmapImage(uriSource);
            }
        }

        private void radio_i_perimeter_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!RadioInfo[1])
            {
                var uriSource = new Uri(@"Images/radio_off2.png", UriKind.Relative);
                radio_i_perimeter.Source = new BitmapImage(uriSource);
            }
        }

        private void radio_i_circumference_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!RadioInfo[2])
            {
                var uriSource = new Uri(@"Images/radio_off2.png", UriKind.Relative);
                radio_i_circumference.Source = new BitmapImage(uriSource);
            }
        }

        private void radio_i_1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RadioInfo[0])
            {
                var uriSource = new Uri(@"Images/radio_off.png", UriKind.Relative);
                radio_i_1.Source = new BitmapImage(uriSource);
            }
        }

        private void radio_i_perimeter_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RadioInfo[1])
            {
                var uriSource = new Uri(@"Images/radio_off.png", UriKind.Relative);
                radio_i_perimeter.Source = new BitmapImage(uriSource);
            }
        }

        private void radio_i_circumference_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RadioInfo[2])
            {
                var uriSource = new Uri(@"Images/radio_off.png", UriKind.Relative);
                radio_i_circumference.Source = new BitmapImage(uriSource);
            }
        }
        #endregion



    }
}
