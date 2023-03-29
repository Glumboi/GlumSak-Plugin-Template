using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Wpf.Ui.Controls;

namespace GlumSak_PluginTemplate.GlumSak
{
    public class MainWindowDummy
    {
        private IntPtr _windowHandle;

        public IntPtr WindowHandle
        {
            get => _windowHandle;
            set
            {
                if (_windowHandle != value)
                {
                    _windowHandle = value;
                }
            }
        }

        public UIElement WpfElement => HwndSource.FromHwnd(WindowHandle).RootVisual as UIElement;

        public Snackbar Notification_SnackBar => (Snackbar)GetUiElement("Notification_SnackBar");

        public ProgressBar Download_ProgressBar => (ProgressBar)GetUiElement("Download_ProgressBar");

        public TextBlock DownloadProgress_TextBlock => (TextBlock)GetUiElement("DownloadProgress_TextBlock");

        public Border Progress_Border => (Border)GetUiElement("Progress_Border");

        public MainWindowDummy(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
        }

        public UIElement GetUiElement(string name)
        {
            var frmWrk = (FrameworkElement)WpfElement;

            return frmWrk.FindName(name) as UIElement;
        }
    }
}