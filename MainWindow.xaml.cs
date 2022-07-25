using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace mBMWDiagn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32")]
        public extern static IntPtr LoadLibrary(string libraryName);
        public delegate void FilterTraceArrived(object sender, FilterTraceArrivedEventArgs e);
        public uint PrimaryDeviceID;
        //--------------------------------------USB-Class-Varaibles--------------
        Native native = new Native();
        int TotalLines = 0;

        private bool ISWorkerBusy = false;

        public static MainWindow main;
        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        private void Window_Initialized(object sender, EventArgs e)//On Load Event
        {
            native.FilterTraceArrived += new EventHandler<FilterTraceArrivedEventArgs>(RecievedFilterTraces);
            
        }

        SerialPort mySerialPort;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RichTextLog.AppendText(mySerialPort.ReadExisting());
        }

        private void USBLister_Timer_Tick(object sender, EventArgs e)
        {
            if (ISWorkerBusy == false)
            {
                
                ISWorkerBusy = true;
            }
            
        }

        public void UpdateUSBDeviceID(uint DevID)
        {
            System.Windows.Media.SolidColorBrush mySolidColorBrush = new System.Windows.Media.SolidColorBrush();
            //continue
            if (Convert.ToInt32(DevID) == -1)
            {
                mySolidColorBrush.Color = System.Windows.Media.Color.FromRgb(218, 63, 63);
                WarnLabelX.Content = "WARNING! No USB Trace ID Is Set";
                ConnectBTN.IsEnabled = false;
            }
            else
            {
                WarnLabelX.Content = "USB Trace Set To  ID: " + DevID.ToString();
                //ColorBox
                mySolidColorBrush.Color = System.Windows.Media.Color.FromRgb(160, 225, 95);
                ConnectBTN.IsEnabled=true;
            }
            PrimaryDeviceID = DevID;
            WarnBox.Background = mySolidColorBrush;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ConnectBTN.Content.ToString() == "Disconnect")
            {
                if (ConnType.Text == "USB") native.StopTraceReader(); //CLOSE USB CON
                else mySerialPort.Close();
                
                ConnectBTN.Content = "Connect";
            }
            else {
                if (ConnType.Text == "USB")
                {
                    RichTextLog.AppendText("Starting TraceReader..." + Environment.NewLine);
                    native.StartTraceReader();
                    ConnectBTN.Content = "Disconnect";
                }
                else if (ConnType.Text == "COM")
                {
                    RichTextLog.AppendText("Started Read" + Environment.NewLine);
                    mySerialPort = new SerialPort(ComPorts.Text, 9600, Parity.None, 8, StopBits.One);//Com Port To Listen To
                    if (mySerialPort == null) MessageBox.Show("Invalid Serial Port!");
                    if (mySerialPort.IsOpen == true)
                    {
                        MessageBox.Show("Port Allready Open!");
                        mySerialPort.Close();
                    }
                    mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    ConnectBTN.Content = "Disconnect";
                    //mySerialPort.RtsEnable = true;
                    //mySerialPort.DtrEnable = true;
                   
                        mySerialPort.Open();

                    System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Tick += dispatcherTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
                    dispatcherTimer.Start();

                    mySerialPort.WriteLine("FF FF");
                    MessageBox.Show("End of");
                }
                else if (ConnType.Text == "TCP")
                {
                    try
                    {
                        IPAddress ipAddress = IPAddress.Parse(IPText.Text);


                        TcpListener server = new TcpListener(ipAddress, Convert.ToInt32(PortText.Text));

                        Byte[] bytes = new Byte[256];
                        String data = null;


                        server.Start();
                        while (true)
                        {
                            TcpClient client = server.AcceptTcpClient();
                            RichTextLog.AppendText("Connected" + Environment.NewLine);

                            // Get a stream object for reading
                            NetworkStream stream = client.GetStream();
                            int i;
                            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                // Translate data bytes to a ASCII string.
                                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                                RichTextLog.AppendText("Received: " + data);
                            }
                        }
                    }
                    catch (Exception Err)
                    {
                        MessageBox.Show(Err.Message);
                    }
                }
            }
        }

        
        private void RecievedFilterTraces(object sender, FilterTraceArrivedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new FilterTraceArrived(RecievedFilterTraces), new Object[] { sender, e });
            }
            else
            {
                foreach (FilterTrace filterTrace in e.Traces)
                {
                    AddFilterTrace(filterTrace);
                }
            }

        }
        
        private void AddFilterTrace(FilterTrace filterTrace)
        {
            string timestamp = DateTime.UtcNow.ToString("HH:mm:ss.ff", System.Globalization.CultureInfo.InvariantCulture);
            RichTextLog.AppendText("\r\n [" + timestamp +"] "+ filterTrace.BufToHex());
            RichTextLog.ScrollToEnd();
            TotalLines++;
            LinesLabel.Text = "Total Lines: " + TotalLines;
            //prevTrace = filterTrace;
        }

        private async void USBListernerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            

            //byte[] readBuffer = new byte[reader.ReadBufferSize];
            byte[] readBuffer = new byte[64];
            while (true)
            {
                // Create and submit transfer
               
                // run all background tasks here
                await Task.Delay(800);
            }
            
        }

        private void USBListernerWorker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            ISWorkerBusy = false;
            //update ui once worker complete his work
        }

        private void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            MessageBox.Show("read");
            System.Diagnostics.Debug.WriteLine("TST: " + mySerialPort.ReadExisting());

        }

        

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            PortText.Visibility = Visibility.Hidden;
            IPText.Visibility = Visibility.Hidden;
            ComPortsText.Visibility = Visibility.Hidden;
            ComPorts.Items.Clear();
            if (ConnType.Text == "USB")
            {
                WarnLabelX.Content = "Waiting for USB Trace ID...";
                //ColorBox
                System.Windows.Media.SolidColorBrush mySolidColorBrush = new System.Windows.Media.SolidColorBrush();
                mySolidColorBrush.Color = System.Windows.Media.Color.FromRgb(218, 212, 63);
                WarnBox.Background = mySolidColorBrush;
                // Find and open the usb device.
                //UsbRegDeviceList allDevices = UsbDevice.KernelType;



            }
            else if (ConnType.Text == "COM") {

                ComPorts.Visibility=Visibility.Visible;
                ComPortsText.Visibility = Visibility.Visible;
                ConnType.Width = 117;
                //load the ComPorts
                string[] ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    foreach (string port in ports) ComPorts.Items.Add(port);
                }else ComPorts.Items.Add("No Com Ports Found!");
            }
            else if (ConnType.Text == "Ethernet")
            {
                ConnType.Width = 200;
            }
            else if(ConnType.Text == "TCP") {
                PortText.Visibility = Visibility.Visible;
                IPText.Visibility = Visibility.Visible;
                ConnectBTN.IsEnabled = true;
            }
        }

        private void ComPorts_DropDownClosed(object sender, EventArgs e)
        {
            if (ComPorts.Text.ToLower().Contains("no " + ConnType.Text.ToLower())== false)
            {
                ConnectBTN.IsEnabled = true;
            }else MessageBox.Show("Please Plug In The (" + ConnType.Text + ") OBD Reader!");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OBDDiagram win2 = new OBDDiagram();
            win2.Show();
        }

        private void OpenFilterSetup(object sender, RoutedEventArgs e)
        {
            DriverSetup Form = new DriverSetup();
            Form.Show();
        }

        private void ClearLogVIewer(object sender, RoutedEventArgs e)
        {
            RichTextLog.Document.Blocks.Clear();
            TotalLines = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(ConnectBTN.Content.ToString() == "Disconnect")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Listender is not Disconnected! \r\n\r\n Do you wish to quit and Disconnect Automatically?", "Warning",System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Button_Click(this, null);
                    e.Cancel = false;
                }
            }
        }
    }
}
