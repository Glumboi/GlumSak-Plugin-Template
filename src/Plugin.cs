using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using GlumSak_PluginTemplate.GlumSak;

namespace GlumSak_PluginTemplate
{
    public class Plugin
    {
        public static int exitCode;

        public static int PluginEntryPoint(IntPtr mainWindowhandle, object[] args) //--> Default Entrypoint of the Plugin
        {
            if (args.Length > 0 &&
                args[0] is bool &&
                (bool)args[0] == true)
            {
                AppDomain.CurrentDomain.ProcessExit
                    += OverrrideExitEvent; //--> Use custom ExitEvent if first param is bool and true
            }
            //Your custom code here, you can also use WindowsForms with this (WPF might be possible too)

            //UI access demo
            MainWindowDummy mainWindowDummy = new MainWindowDummy(mainWindowhandle);

            //Invoke to the UI Thread of GlumSak
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                //Show a notification in GlumSak
                mainWindowDummy.Notification_SnackBar.Content = "This got changed from a plugin!";
                mainWindowDummy.Notification_SnackBar.Show();

                //Update the ProgressBar
                mainWindowDummy.Progress_Border.Visibility = Visibility.Visible;
                mainWindowDummy.Download_ProgressBar.Value = 50;
                mainWindowDummy.DownloadProgress_TextBlock.Text = "Plugin Test";

                //Example to get UIControls by their name, to know the names please reference GlumSaks source Files
                var yuzuVerComboBox = mainWindowDummy.GetUiElement("YuzuVersions_DropDown") as ComboBox;
                yuzuVerComboBox.SelectedIndex = 1;
            }));

            exitCode = 0;
            return exitCode;
        }

        private static void OverrrideExitEvent(object sender, EventArgs e)
        {
            Console.WriteLine($"Plugin {Assembly.GetExecutingAssembly().FullName} exited with code: {exitCode}");
            Console.ReadKey();
        }
    }
}