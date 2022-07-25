using System;

using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Security.Principal;

namespace mBMWDiagn
{
    
    public partial class DriverSetup : Window
    {
        private bool ISrunningAsAdmin = WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);

        private USBDevice USBDeviceFunc = new USBDevice();
        DeviceManagement devManage = new DeviceManagement();
        Native native = new Native();
        IntPtr devNotificationsHandle;

        public DriverSetup()
        {
            InitializeComponent();
        }
       
        private void RefreshDevices_Click(object sender, RoutedEventArgs e)
        {
            USBDeviceFunc.EnumFilterDevices(USBListView, USBCountLabel);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            bool IsDriverInstall =USBDeviceFunc.CheckDriverInstallation();
            System.Windows.Media.SolidColorBrush mySolidColorBrush = new System.Windows.Media.SolidColorBrush();
            if (IsDriverInstall == true)
            {
                WarningBoxLabel.Content = "Driver Installed";
                mySolidColorBrush.Color = System.Windows.Media.Color.FromRgb(160, 225,95);
            }
            else
            {
                WarningBoxLabel.Content = "Driver Not Installed!";
                mySolidColorBrush.Color = System.Windows.Media.Color.FromRgb(225, 95, 95);
            }
            WarningBox.Background = mySolidColorBrush;

            WindowInteropHelper windowHwnd = new WindowInteropHelper(this);
            if (windowHwnd != null)
            {
                devManage.RegisterForDeviceNotifications(windowHwnd.Handle, ref devNotificationsHandle);
                USBDeviceFunc.EnumFilterDevices(USBListView, USBCountLabel);
                
            }
            

        }

       

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ISrunningAsAdmin == false) System.Windows.MessageBox.Show("The app is not running in UAC privilagues!" + Environment.NewLine + "Drive Install Might Not Work!!",
                        "Not Running As Admin", System.Windows.MessageBoxButton.OK);
            USBDeviceFunc.InstallDriver();
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //USBTreeView.Height = TreeViewSizer.Height;
        }

        private void SetPrimaryBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
            {
                MainWindow.main.UpdateUSBDeviceID((uint)GetPropValue(USBListView.SelectedItems[0], "DevID"));
            }));
            for (int i = 0; i < USBListView.Items.Count; i++)//lopp all and set all to not Filtered
            {
                uint DevIdLopp = (uint)GetPropValue(USBListView.Items[0], "DevID");
                if (DevIdLopp == (uint)GetPropValue(USBListView.SelectedItems[0], "DevID"))
                        native.SetDeviceEnabled(DevIdLopp, true);
                else native.SetDeviceEnabled(DevIdLopp, false);
            }
            RefreshDevices_Click(this, null);
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        

        private void UninStallBTN_Click(object sender, RoutedEventArgs e)
        {

            USBDeviceFunc.UninstallDriver();
        }

        private void SetTraceBTNBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SetTraceBTN.Content.ToString() == "Set Drive As Trace") native.SetDeviceEnabled((uint)GetPropValue(USBListView.SelectedItems[0], "DevID"), true);
            else native.SetDeviceEnabled((uint)GetPropValue(USBListView.SelectedItems[0], "DevID"), false);

            RefreshDevices_Click(this, null);
        }

        private void USBListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (USBListView.SelectedIndex == -1)
            {
                SetTraceBTN.IsEnabled = false;
                SetPrimaryBTNs.IsEnabled = false;
                return;
            } else {
                SetTraceBTN.IsEnabled = true;
                SetPrimaryBTNs.IsEnabled = true;
            }
            if (GetPropValue(USBListView.SelectedItems[0], "ISenabled").ToString() == "False") SetTraceBTN.Content = "Set Drive As Trace";
            else SetTraceBTN.Content = "Remove Drive As Trace";
        }


    }
}
