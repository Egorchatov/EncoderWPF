using Phidget22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EncoderWPF
{
    class CommandLineOpen
    {
        #region Events
        #endregion

        #region Fields and Properties

        public string[] CommandLineOverride = null;

        public string[] CommandLine
        {
            get { return CommandLineOverride ?? Environment.GetCommandLineArgs(); }
            set { CommandLineOverride = value; }
        }

        #endregion

        #region Relay Commands
        #endregion

        #region Constructors
        #endregion

        #region Private Methods

        private void ShowInvalidCommandLineMessage(string appName)
        {
            StringBuilder stringbuilder = new StringBuilder();
            stringbuilder.AppendLine("Invalid Command line arguments." + Environment.NewLine);
            stringbuilder.AppendLine("Usage: " + appName + "[Flags...]");
            stringbuilder.AppendLine("Flags: ");
            stringbuilder.AppendLine("\t-n serialNumber: Serial Number, omit for any serial");
            stringbuilder.AppendLine("\t-l logFile: Enable phidget21 logging to logFile");
            stringbuilder.AppendLine("\t-v Port: Select the Port that the device is connected to. 0 by default");
            stringbuilder.AppendLine("\t-c deviceChannel: Select the specific channel of the device you want. 0 by default");
            stringbuilder.AppendLine("\t-h hubPort?: the device is connected to a hub port");
            stringbuilder.AppendLine("\t-s serverID\tServer Name, omit for any server");
            stringbuilder.AppendLine("\t-i ipAdress:port\tIp Address and Port. Port is optional, default to 5000");
            stringbuilder.AppendLine("\t-p password\tPassword, omit for no password" + Environment.NewLine);
            stringbuilder.AppendLine("Examples: ");
            stringbuilder.AppendLine(appName + " -n 50098 -h");
            stringbuilder.AppendLine(appName + " -n 1234567 -v 1 -c 0");
            stringbuilder.AppendLine(appName + " -r");
            stringbuilder.AppendLine(appName + " -s myphidgetdevice");
            stringbuilder.AppendLine(appName + " -n 45670 -i 127.0.0.1:5001 -p password");
            MessageBox.Show(stringbuilder.ToString(), "Argument Error", MessageBoxButton.OK, MessageBoxImage.Error);

            Application.Current.Shutdown();
        }

        #endregion

        #region Public Methods

        public void OpenCommandLine(Phidget phidget)
        {
            string[] args = CommandLine;
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            int channel = 0;
            int serialNumber = Phidget.AnySerialNumber;
            string label = Phidget.AnyLabel;
            int hubPort = Phidget.AnyHubPort;
            bool isHubPort = false;
            string networkServerName = null;
            string password = null;
            string logFile = null;

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].EndsWith(appName)) continue;
                    if(args[i].StartsWith("-"))
                    {
                        string flag = args[i].Remove(0, 1);
                        switch(flag)
                        {
                            case "n":
                                isHubPort = true;
                                break;

                            case "l":
                                if(args.Length <= i + 1)
                                {
                                    ShowInvalidCommandLineMessage(appName);
                                    return;
                                }
                                logFile = args[++i];
                                break;
                                
                            case"v":
                                if(args.Length <= i + 1)
                                {
                                    ShowInvalidCommandLineMessage(appName);
                                    return;
                                }
                                hubPort = int.Parse(args[++i]);
                                break;

                            case"c":
                                if (args.Length <= i + 1)
                                {
                                    ShowInvalidCommandLineMessage(appName);
                                    return;
                                }
                                channel = int.Parse(args[++i]);
                                break;

                            case"h":
                                isHubPort = true;
                                break;

                            case "s":
                                phidget.IsRemote = true;
                                if (i == args.Length - 1) break;
                                if (args.Length <= i + 1)
                                {
                                    ShowInvalidCommandLineMessage(appName);
                                    return;
                                }
                                if (args[++i] != null && !args[i].StartsWith("-"))
                                {
                                    networkServerName = args[i];
                                }
                                break;

                            case "p":
                                if (args.Length <= i + 1)
                                {
                                    ShowInvalidCommandLineMessage(appName);
                                    return;
                                }
                                password = args[++i];
                                break;

                            default:
                                ShowInvalidCommandLineMessage(appName);
                                break;
                        }
                    }
                    else
                    {
                        ShowInvalidCommandLineMessage(appName);
                    }
                }

                if(logFile != null)
                {
                    Phidget22.Log.Enable(LogLevel.Info, logFile);
                }
                phidget.Channel = channel;
                phidget.DeviceSerialNumber = serialNumber;
                phidget.DeviceLabel = label;
                phidget.HubPort = hubPort;
                phidget.IsHubPortDevice = isHubPort;
                phidget.ServerName = networkServerName;

                if(phidget.IsRemote)
                {
                    Net.EnableServerDiscovery(ServerType.Device);
                    if(password != null && networkServerName != null)
                    {
                        Net.SetServerPassword(networkServerName, password);
                    }
                }
                else
                {
                    phidget.IsLocal = true;
                }
                phidget.Open();
            }
            catch (PhidgetException ex)
            {
                if (ex.ErrorCode == ErrorCode.Busy)
                {
                    if (ex.ErrorCode == ErrorCode.Busy)
                    {
                        MessageBox.Show("This hub port is in use - hub ports can only be opened in one mode at a time.", "Port in use", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (ex.ErrorCode == ErrorCode.Duplicate)
                    {
                        MessageBox.Show("This channel is already open - channels can only be opened once.", "Already open", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Phidget exception: " + ex.Message, "Phidget Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        #endregion
    }
}
