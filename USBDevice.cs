using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mBMWDiagn
{
    public class USBDevice
    {
        Native native = new Native();
        DeviceManagement devManage = new DeviceManagement();
        private bool ISrunningAsAdmin = WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
        public bool CheckDriverInstallation()
        {
            bool installDriver = false;
            System.Diagnostics.FileVersionInfo busdogDriverVersion;
            // show driver incompatablities
            if (DriverManagement.IsDriverInstalled(out busdogDriverVersion))
            {
                string thatVersion = string.Format("{0}.{1}",
                    busdogDriverVersion.FileMajorPart,
                    busdogDriverVersion.FileMinorPart);
                Version assVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string thisVersion = string.Format("{0}.{1}",
                    assVersion.Major,
                    assVersion.Minor);
                if (thatVersion != thisVersion)
                {
                    if (System.Windows.MessageBox.Show(
                            string.Format("MVBus Filter Driver version ({0}) does not match the GUI version ({1}). Would you like to install MVBus Filter Driver version {1} now?", thatVersion, thisVersion),
                            "Driver Version Mismatch",
                            System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                        installDriver = true;
                }
                else return true;
            }
            else
            {
                if (System.Windows.MessageBox.Show("MVBus Filter Driver is not installed. Do you want to install it now?", "Driver Not Installed",
                        System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                    installDriver = true;
            }
            // install driver if one of the complicated if descision trees above set 'installDriver'
            if (ISrunningAsAdmin == false && installDriver==true)  if (System.Windows.MessageBox.Show("Not running in UAC Privileges!, Driver Install Might Not Work" + Environment.NewLine + "Do You Wish To Continue ?",
                        "Not Running As Admin", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                installDriver = true; else installDriver=false;
            if (installDriver)
            {
                InstallDriver();
                return true;
                
            }else return false;
        }
        public void InstallDriver()
        {
            bool needRestart;
            string failureReason;
            if (DriverManagement.InstallDriver(out needRestart, out failureReason))
            {
                if (needRestart)
                    System.Windows.MessageBox.Show("BusDog Filter Driver installed! Restart required to complete.",
                        "Driver Installed");
                else
                    System.Windows.MessageBox.Show("BusDog Filter Driver installed!",
                        "Driver Installed");
            }
            else
                System.Windows.MessageBox.Show(string.Format("BusDog Filter Driver installation failed ({0})", failureReason),
                    "Driver Installation Failed");
        }

        public void UninstallDriver()
        {
            bool needRestart;
            string failureReason;
            if (DriverManagement.UninstallDriver(out needRestart, out failureReason))
            {
                if (needRestart)
                    System.Windows.MessageBox.Show("BusDog Filter Driver uninstalled! Restart required to complete.",
                        "Driver Installed");
                else
                    System.Windows.MessageBox.Show("BusDog Filter Driver uninstalled!",
                        "Driver Uninstalled");
            }
            else
                System.Windows.MessageBox.Show(string.Format("BusDog Filter Driver uninstallation failed ({0})", failureReason),
                    "Driver Uninstallation Failed");
        }
        public void EnumFilterDevices( ListView ListViewControl,Label LabelCount)
        {
            ListViewControl.Items.Clear();

            List<DeviceId> deviceIds;
            native.GetDeviceList(out deviceIds);

            for (int i = 0; i < deviceIds.Count; i++)
            {
                DeviceId devId = deviceIds[i];
                devManage.FindDeviceProps(devId.PhysicalDeviceObjectName, out devId.HardwareId, out devId.Description, out devId.InstanceId);


                ListViewControl.Items.Add(new MyItem { DevID=devId.DevId, Description = devId.Description, ISenabled = devId.Enabled});
                

            }
            LabelCount.Content = "Total USB Devices: " + deviceIds.Count;

        }
        
        public class MyItem
    {
        public uint DevID { get; set; }
        public string Description { get; set; }

        public bool ISenabled { get; set; }
    }
        

    }
}
