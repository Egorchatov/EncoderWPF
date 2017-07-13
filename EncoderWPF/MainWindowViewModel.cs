using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Phidget22;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EncoderWPF
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Events
        #endregion

        #region Fields and Properties

        public CommandLineOpen CommandLineOpen { get; set; }

        private int _countsPerRevolution { get; set; }

        private int _dataInterval { get; set; }

        private Phidget22.Encoder _encoder { get; set; }

        /// <summary>
        /// Phidget Info GroupBox Fields and Properties
        /// </summary>

        private string _version;
        public string Version
        {
            get { return _version; }
            set { Set(() => Version, ref _version, value); }
        }

        private string _channel;
        public string Channel
        {
            get { return _channel; }
            set { Set(() => Channel, ref _channel, value); }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set { Set(() => DeviceName, ref _deviceName, value); }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { Set(() => SerialNumber, ref _serialNumber, value); }
        }

        private string _serialNumberName;
        public string SerialNumberName
        {
            get { return _serialNumberName; }
            set { Set(() => SerialNumberName, ref _serialNumberName, value); }
        }

        private string _hubPort;
        public string HubPort
        {
            get { return _hubPort; }
            set { Set(() => HubPort, ref _hubPort, value); }
        }

        private Visibility _hubPortVisible;
        public Visibility HubPortVisible
        {
            get { return _hubPortVisible; }
            set { Set(() => HubPortVisible, ref _hubPortVisible, value); }
        }

        private Visibility _imageVisible;
        public Visibility ImageVisible
        {
            get { return _imageVisible; }
            set { Set(() => ImageVisible, ref _imageVisible, value); }
        }

        private Visibility _remoteVisible;
        public Visibility RemoteVisible
        {
            get { return _remoteVisible; }
            set { Set(() => RemoteVisible, ref _remoteVisible, value); }
        }

        private BitmapSource _imageSource;
        public BitmapSource ImageSource
        {
            get { return _imageSource; }
            set { Set(() => ImageSource, ref _imageSource, value); }
        }

        /// <summary>
        /// Settings GroupBox Fields and Properties
        /// </summary>

        private string _dataIntervalMinText;
        public string DataIntervalMinText
        {
            get { return _dataIntervalMinText; }
            set { Set(() => DataIntervalMinText, ref _dataIntervalMinText, value); }
        }
        
        private string _dataIntervalUnitText;
        public string DataIntervalUnitText
        {
            get { return _dataIntervalUnitText; }
            set { Set(() => DataIntervalUnitText, ref _dataIntervalUnitText, value); }
        }

        private string _dataIntervalMaxText;
        public string DataIntervalMaxText
        {
            get { return _dataIntervalMaxText; }
            set { Set(() => DataIntervalMaxText, ref _dataIntervalMaxText, value); }
        }

        private string _changeTriggerMinText;
        public string ChangeTriggerMinText
        {
            get { return _changeTriggerMinText; }
            set { Set(() => ChangeTriggerMinText, ref _changeTriggerMinText, value); }
        }

        private string _changeTriggerUnitText;
        public string ChangeTriggerUnitText
        {
            get { return _changeTriggerUnitText; }
            set { Set(() => ChangeTriggerUnitText, ref _changeTriggerUnitText, value); }
        }

        private string _changeTriggerMaxText;
        public string ChangeTriggerMaxText
        {
            get { return _changeTriggerMaxText; }
            set { Set(() => ChangeTriggerMaxText, ref _changeTriggerMaxText, value); }
        }

        private string _countsPerRevolutionText;
        public string CountsPerRevolutionText
        {
            get { return _countsPerRevolutionText; }
            set { Set(() => CountsPerRevolutionText, ref _countsPerRevolutionText, value); }
        }

        private double _dataIntervalMinimum;
        public double DataIntervalMinimum
        {
            get { return _dataIntervalMinimum; }
            set { Set(() => DataIntervalMinimum, ref _dataIntervalMinimum, value); }
        }

        private double _dataIntervalValue;
        public double DataIntervalValue
        {
            get { return _dataIntervalValue; }
            set
            {
                Set(() => DataIntervalValue, ref _dataIntervalValue, value);
                try
                {
                    _encoder.DataInterval = (int)DataIntervalValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error setting change trigger: " + ex.Message);
                }
            }
        }

        private double _dataIntervalMaximum;
        public double DataIntervalMaximum
        {
            get { return _dataIntervalMaximum; }
            set { Set(() => DataIntervalMaximum, ref _dataIntervalMaximum, value); }
        }

        private double _changeTirggerMinimum;
        public double ChangeTriggerMinimum
        {
            get { return _changeTirggerMinimum; }
            set { Set(() => ChangeTriggerMinimum, ref _changeTirggerMinimum, value); }
        }

        private double _changeTriggerValue;
        public double ChangeTriggerValue
        {
            get { return _changeTriggerValue; }
            set
            {
                Set(() => ChangeTriggerValue, ref _changeTriggerValue, value);

                try
                {
                    _encoder.PositionChangeTrigger = (int)ChangeTriggerValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error setting change trigger: " + ex.Message);
                }
            }
        }

        private double _changeTriggerMaximum;
        public double ChangeTriggerMaximum
        {
            get { return _changeTriggerMaximum; }
            set { Set(() => ChangeTriggerMaximum, ref _changeTriggerMaximum, value); }
        }

        private bool _groupBoxSettingsEnable;
        public bool GroupBoxSettingsEnable
        {
            get { return _groupBoxSettingsEnable; }
            set { Set(() => GroupBoxSettingsEnable, ref _groupBoxSettingsEnable, value); }
        }

        private bool _dataIntervalEnable;
        public bool DataIntervalEnable
        {
            get { return _dataIntervalEnable; }
            set { Set(() => DataIntervalEnable, ref _dataIntervalEnable, value); }
        }

        private bool _changeTriggerEnable;
        public bool ChangeTriggerEnable
        {
            get { return _changeTriggerEnable; }
            set { Set(() => ChangeTriggerEnable, ref _changeTriggerEnable, value); }
        }

        private bool _deviceEnable;
        public bool DeviceEnable
        {
            get { return _deviceEnable; }
            set { Set(() => DeviceEnable, ref _deviceEnable, value); }
        }

        private bool _isEncoderChecked;
        public bool IsEncoderChecked
        {
            get { return _isEncoderChecked; }
            set
            {
                Set(() => IsEncoderChecked, ref _isEncoderChecked, value);

                if (_encoder != null)
                {
                    try
                    {
                        _encoder.Enabled = IsEncoderChecked;
                    }
                    catch (PhidgetException ex)
                    {
                        MessageBox.Show("Error setting enabled: " + ex.Message);
                    }
                }
            }
        }

        private bool _ioModeEnable;
        public bool IoModeEnable
        {
            get { return _ioModeEnable; }
            set { Set(() => IoModeEnable, ref _ioModeEnable, value); }
        }

        private ObservableCollection<EncoderIOMode> _ioModes;
        public ObservableCollection<EncoderIOMode> IOModes
        {
            get { return _ioModes; }
            set { Set(() => IOModes, ref _ioModes, value); }
        }

        private EncoderIOMode _selectedIOMode;
        public EncoderIOMode SelectedIOMode
        {
            get { return _selectedIOMode; }
            set
            {
                Set(() => SelectedIOMode, ref _selectedIOMode, value);
                try
                {
                    _encoder.IOMode = SelectedIOMode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error setting IO Mode: " + ex.Message);
                }
            }
        }


        /// <summary>
        /// Data GroupBox Fields and Properties
        /// </summary>

        private string _positionChangeText;
        public string PositionChangeText
        {
            get { return _positionChangeText; }
            set { Set(() => PositionChangeText, ref _positionChangeText, value); }
        }

        private string _timeChangeText;
        public string TimeChangeText
        {
            get { return _timeChangeText; }
            set { Set(() => TimeChangeText, ref _timeChangeText, value); }
        }

        private string _positionText;
        public string PositionText
        {
            get { return _positionText; }
            set { Set(() => PositionText, ref _positionText, value); }
        }

        private string _indexPositionText;
        public string IndexPositionText
        {
            get { return _indexPositionText; }
            set { Set(() => IndexPositionText, ref _indexPositionText, value); }
        }

        private string _velocityText;
        public string VelocityText
        {
            get { return _velocityText; }
            set { Set(() => VelocityText, ref _velocityText, value); }
        }

        private bool _dataEnable;
        public bool DataEnable
        {
            get { return _dataEnable; }
            set { Set(() => DataEnable, ref _dataEnable, value); }
        }

        #endregion

        #region Relay Commands

        private RelayCommand _onWindowsLoadedCommand;
        public RelayCommand OnWindowsLoadedCommand
        {
            get
            {
                return _onWindowsLoadedCommand ?? (_onWindowsLoadedCommand = new RelayCommand(() =>
                {
                    Phidget.InvokeEventCallbacks = true;
                    _encoder = new Phidget22.Encoder();
                    _encoder.Attach += OnEncoderAttach;
                    _encoder.Detach += OnEncoderDetach;
                    _encoder.Error += OnEncoderError;
                    _encoder.PositionChange += OnEncoderPositionChange;
                    CommandLineOpen.OpenCommandLine(_encoder);
                }));
            }
        }

        private RelayCommand _setCountsPerRevolutionCommand;
        public RelayCommand SetCountsPerRevolutionCommand
        {
            get
            {
                return _setCountsPerRevolutionCommand ?? (_setCountsPerRevolutionCommand = new RelayCommand(() =>
                {
                    int _cpr = -1;
                    if (!string.IsNullOrEmpty(CountsPerRevolutionText.Trim()) && CountsPerRevolutionText != "0")
                    {
                        if(Int32.TryParse(CountsPerRevolutionText, out _cpr))
                        {
                            _countsPerRevolution = _cpr;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Counts per Revolution value.");
                            _countsPerRevolution = -1;
                            VelocityText = "N/A"; CountsPerRevolutionText = null;
                        }
                    }
                    else
                    {
                        _countsPerRevolution = -1;
                        VelocityText = "N/A";
                        CountsPerRevolutionText = null;
                    }
                }));
            }
        }

        private RelayCommand _resetEncoderCommand;
        public RelayCommand ResetEncoderCommand
        {
            get
            {
                return _resetEncoderCommand ?? (_resetEncoderCommand = new RelayCommand(() =>
                {
                    try
                    {
                        _encoder.Position = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error setting position: " + ex.Message);
                    }
                }));
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel(StartupEventArgs parameters)
        {
            _countsPerRevolution = -1;
            _dataInterval = 0;
            CommandLineOpen = new CommandLineOpen();
            CommandLineOpen.CommandLine = parameters.Args;
            SerialNumberName = "Serial Number:";
            HubPortVisible = Visibility.Collapsed;
            ImageVisible = Visibility.Collapsed;
            RemoteVisible = Visibility.Collapsed;
            DeviceEnable = false;
            GroupBoxSettingsEnable = false;
            DataEnable = false;
            IsEncoderChecked = false;
            IOModes = new ObservableCollection<EncoderIOMode>();
            DeviceName = "Nothing attached";
        }

        #endregion

        #region Private Methods

        private void OnEncoderAttach(object sender, Phidget22.Events.AttachEventArgs e)
        {
            Phidget phidget = sender as Phidget;
            Version = phidget.DeviceVersion.ToString();
            Channel = phidget.Channel.ToString();

            if (phidget.IsHubPortDevice)
            {
                DeviceName = phidget.Hub.DeviceSKU + " - " + phidget.Hub.DeviceName;
            }
            else
            {
                DeviceName = phidget.DeviceSKU + " - " + phidget.DeviceName;
            }

            SerialNumber = phidget.DeviceSerialNumber.ToString();

            if (phidget.DeviceClass == DeviceClass.VINT)
            {
                SerialNumberName = "Hub Serial Number:";
                HubPortVisible = Visibility.Visible;
                HubPort = phidget.HubPort.ToString();
            }
            else
            {
                SerialNumberName = "Serial Number:";
                HubPortVisible = Visibility.Visible;
            }

            try
            {
                String StockKeepingUnit;

                if (phidget.IsHubPortDevice)
                {
                    StockKeepingUnit = phidget.Hub.DeviceSKU.Split(new char[] { '/' })[0];
                }
                else
                {
                    StockKeepingUnit = phidget.DeviceSKU.Split(new char[] { '/' })[0];
                }

                if (phidget.DeviceSKU.Contains("1018")) StockKeepingUnit = "1018";

                Assembly _assembly = Assembly.GetExecutingAssembly();
                String imageName = _assembly.GetManifestResourceNames().Where(img => img.EndsWith(StockKeepingUnit + ".png")).FirstOrDefault();

                if (imageName != null)
                {
                    ImageSource = BitmapFrame.Create(_assembly.GetManifestResourceStream(imageName));
                    ImageVisible = Visibility.Visible;
                }

                if (phidget.IsRemote)
                {
                    RemoteVisible = Visibility.Visible;
                }
                else
                {
                    RemoteVisible = Visibility.Collapsed;
                }
            }
            catch { }

            //Control par = this.Parent;
            //while (!(par is Form))
            //    par = par.Parent;
            //((Form)par).Text = phidget.ChannelName;

            Phidget22.Encoder attachedDevice = (Phidget22.Encoder)sender;

            if(_encoder.DeviceClass == DeviceClass.VINT)
            {
                try
                {
                    DataIntervalUnitText = "ms";
                    DataIntervalMinText = attachedDevice.MinDataInterval.ToString();
                    DataIntervalMinimum = attachedDevice.MinDataInterval;
                    DataIntervalMaxText = attachedDevice.MaxDataInterval.ToString();
                    DataIntervalMaximum = attachedDevice.MaxDataInterval;
                    DataIntervalValue = attachedDevice.DataInterval;

                    if (attachedDevice.MaxDataInterval == attachedDevice.MinDataInterval) DataIntervalEnable = false;

                    ChangeTriggerUnitText = "ticks";
                    ChangeTriggerMinText = attachedDevice.MinPositionChangeTrigger.ToString();
                    ChangeTriggerMinimum = attachedDevice.MinPositionChangeTrigger;
                    ChangeTriggerMaximum = attachedDevice.MaxPositionChangeTrigger;
                    ChangeTriggerMaxText = attachedDevice.MaxPositionChangeTrigger.ToString();
                    ChangeTriggerValue = attachedDevice.PositionChangeTrigger;
                    //ChangeTriggerMinText = attachedDevice.PositionChangeTrigger.ToString("F0");
                }
                catch(PhidgetException ex)
                {
                    MessageBox.Show("Error setting data interval or change trigger: ", ex.Message);
                }
            }
            else
            {
                DataIntervalEnable = false;
                ChangeTriggerEnable = false;
                IoModeEnable = false;
            }

            _dataInterval = attachedDevice.MinDataInterval;
            PositionChangeText = "N/A";
            TimeChangeText = "N/A";
            PositionText = "N/A";
            IndexPositionText = "N/A";

            switch(((Phidget)sender).DeviceID)
            {
                case DeviceID.PN_1047:
                    DeviceEnable = true;
                    IsEncoderChecked = true;
                    IoModeEnable = true;
                    break;

                case DeviceID.PN_HIN1101:
                    DeviceEnable = true;
                    IsEncoderChecked = true;
                    IoModeEnable = true;
                    break;

                case DeviceID.PN_ENC1000:
                    foreach(EncoderIOMode ioMode in Enum.GetValues(typeof(EncoderIOMode)))
                    {
                        IOModes.Add(ioMode);
                    }
                    SelectedIOMode = IOModes.FirstOrDefault();
                    break;

                case DeviceID.PN_DCC1000:
                    foreach (EncoderIOMode ioMode in Enum.GetValues(typeof(EncoderIOMode)))
                    {
                        IOModes.Add(ioMode);
                    }
                    SelectedIOMode = IOModes.FirstOrDefault();
                    break;
            }

            GroupBoxSettingsEnable = true;
            DataEnable = true;
        }

        private void OnEncoderDetach(object sender, Phidget22.Events.DetachEventArgs e)
        {
            DeviceName = "Nothing attached";
            Channel = null;
            Version = null;
            SerialNumber = null;
            HubPort = null;
            SerialNumberName = "Serial Number:";
            ImageVisible = Visibility.Collapsed;
            HubPortVisible = Visibility.Collapsed;
            RemoteVisible = Visibility.Collapsed;
            GroupBoxSettingsEnable = false;
            DataEnable = false;
            DeviceEnable = false;
            CountsPerRevolutionText = null;
            _countsPerRevolution = -1;
        }

        private void OnEncoderError(object sender, Phidget22.Events.ErrorEventArgs e)
        {
            MessageBox.Show(e.Description, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnEncoderPositionChange(object sender, Phidget22.Events.EncoderPositionChangeEventArgs e)
        {
            try
            {
                PositionChangeText = e.PositionChange.ToString() + "ticks";
                TimeChangeText = e.TimeChange.ToString("F3") + "ms";

                if (e.IndexTriggered)
                {
                    IndexPositionText = _encoder.IndexPosition.ToString() + "ticks";
                }

                PositionText = _encoder.Position.ToString() + "ticks";

                if (_countsPerRevolution != -1)
                {
                    double timeChangeMinutes = e.TimeChange / 60000.0;
                    VelocityText = (((double)e.PositionChange / _countsPerRevolution) / timeChangeMinutes).ToString("F0");
                }
            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Error reading position: ", ex.Message);
            }
        }

        #endregion

        #region Public Methods
        #endregion
    }
}
