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

using Hendese.Controls;
using ExtendedWindowsControls;
using System.Windows.Media.Animation;

namespace Hendese
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ExtendedNotifyIcon extendedNotifyIcon;
        private Storyboard gridFadeInStoryBoard;
        private Storyboard gridFadeOutStoryBoard;
        double posX = 0.0, posY = 0.0;

        MainControl mainControl = null;

        public MainWindow()
        {
            extendedNotifyIcon = new ExtendedNotifyIcon();
            extendedNotifyIcon.MouseLeave += new ExtendedNotifyIcon.MouseLeaveHandler(extendedNotifyIcon_OnHideWindow);
            extendedNotifyIcon.MouseDown += new ExtendedNotifyIcon.MouseDownHandler(extendedNotifyIcon_OnShowWindow);
            extendedNotifyIcon.MouseMove += new ExtendedNotifyIcon.MouseMoveHandler(delegate(){ });
            extendedNotifyIcon.targetNotifyIcon.Text = "Popup Text";
            SetNotifyIcon();

            InitializeComponent();
            

            SetWindowToBottomRightOfScreen();
            this.Opacity = 0;

            mainControl = new MainControl();
            this.MainGrid.Children.Add(mainControl);

            gridFadeOutStoryBoard = (Storyboard)this.TryFindResource("gridFadeOutStoryBoard");
            gridFadeOutStoryBoard.Completed += new EventHandler(gridFadeOutStoryBoard_Completed);
            gridFadeInStoryBoard = (Storyboard)TryFindResource("gridFadeInStoryBoard");
            gridFadeInStoryBoard.Completed += new EventHandler(gridFadeInStoryBoard_Completed);

        }

        #region NotifyIcon
        /// <summary>
        /// Pulls an icon from the packed resource and applies it to the NotifyIcon control
        /// </summary>
        private void SetNotifyIcon()
        {
            System.IO.Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,/Icons/GreenOrb.ico")).Stream;
            extendedNotifyIcon.targetNotifyIcon.Icon = new System.Drawing.Icon(iconStream);
        }

        /// <summary>
        /// Does what it says on the tin - ensures the popup window appears at the bottom right of the screen, just above the task bar
        /// </summary>
        private void SetWindowToBottomRightOfScreen()
        {
            Left = SystemParameters.WorkArea.Width - Width - 10;
            Top = SystemParameters.WorkArea.Height - Height;
        }

        /// <summary>
        /// When the notification manager requests the popup to be displayed through this event, perform the below actions
        /// </summary>
        void extendedNotifyIcon_OnShowWindow()
        {
            gridFadeOutStoryBoard.Stop();

            this.Opacity = 1; // Show the window (backing)
            this.Topmost = true; // Very rarely, the window seems to get "buried" behind others, this seems to resolve the problem
            if (MainGrid.Opacity > 0 && MainGrid.Opacity < 1) // If its animating, just set it directly to visible (avoids flicker and keeps the UX slick)
            {
                MainGrid.Opacity = 1;
            }
            else if (MainGrid.Opacity == 0)
            {
                gridFadeInStoryBoard.Begin();  // If it is in a fully hidden state, begin the animation to show the window
            }
        }

        /// <summary>
        /// When the notification manager requests the popup to be hidden through this event, perform the below actions
        /// </summary>
        void extendedNotifyIcon_OnHideWindow()
        {
            gridFadeInStoryBoard.Stop();

            MainGrid.Opacity = 0;

            if (MainGrid.Opacity == 1 && this.Opacity == 1)
                gridFadeOutStoryBoard.Begin();
            else // Just hide the window and grid
            {
                MainGrid.Opacity = 0;
                this.Opacity = 0;
            }
        }

        /// <summary>
        /// If the mouse leaves the popup, start the process to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiWindowMainNotification_MouseLeave(object sender, MouseEventArgs e)
        {
            extendedNotifyIcon_OnHideWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (posX < 0 || posX > this.Width || posY < 0 || posY > this.Height)
                extendedNotifyIcon_OnHideWindow();
            else
                extendedNotifyIcon_OnShowWindow();
        }

        /// <summary>
        /// Once the grid fades out, set the backing window to "not visible"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridFadeOutStoryBoard_Completed(object sender, EventArgs e)
        {
            this.Opacity = 0;
        }

        /// <summary>
        /// Once the grid fades out, set the backing window to "visible"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridFadeInStoryBoard_Completed(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }
        #endregion

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            extendedNotifyIcon.StopMouseLeaveEventFromFiring();
            gridFadeOutStoryBoard.Stop();
            MainGrid.Opacity = 1;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.NavigationFrame.CanGoBack)
                mainControl.NavigationFrame.GoBack();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Opacity = 0;
            this.Opacity = 0;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);

            posX = p.X; // private double posX is a class member
            posY = p.Y; // private double posY is a class member
        }

        #region Button Styles

        Dictionary<Button, Brush> history = new Dictionary<Button, Brush>();
        private void btnMouseEnter(object sender, MouseEventArgs e)
        {
            Button obj = (Button)sender;
            if (!history.Keys.Contains(obj))
                history.Add(obj, obj.Background);
            obj.Background = Brushes.Gray;
            obj.MouseLeave += Obj_MouseLeave;
            obj.MouseDown += Obj_MouseDown;
        }

        private void Obj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Button obj = (Button)sender;
            obj.Background = Brushes.DarkGray;
        }

        private void Obj_MouseLeave(object sender, MouseEventArgs e)
        {
            Button obj = (Button)sender;
            obj.Background = history[obj];
        }

        #endregion
    }
}
