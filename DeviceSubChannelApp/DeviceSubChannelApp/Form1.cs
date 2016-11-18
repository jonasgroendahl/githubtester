using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Ibt.Ortc.Api;
using Ibt.Ortc.Api.Extensibility;
using Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace DeviceSubChannelApp
{


    public partial class Form1 : Form
    {


        #region attributes
        private OrtcClient _ortc;
        private Ortc api;
        private string Gapptoken = "Wexer";
        private string deviceId = "1";
        private string Gappkey = "jJWgRC";
        private string cmd1 = "1";
        private string cmd2 = "shutdown";
        private string cmd3 = "google";
        private string cmd4 = "notepad";
        // FINDWINDOW, find a window to send message to using WIN API
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        // SEND MESSAGE, SEND A MESSAGE USING THE WINDOW HANDLER
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private const int WM_USER = 0x0400;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_SYSCOMMAND = 0x018;
        private const int SC_CLOSE = 0x053;
        // private string Gtoken = "Wexer";
        // private string Gpkey = "JswPpFQgOSYE";
        //private int GTTL = 1600;
        //private bool GtokenisPrivate = true;
        //private bool Giscluster = false;
        #endregion

        public Form1()
        {
            // THIS IS USED TO HIDE THE APPLICATION FROM THE USER
            //this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            // RUN FUNCTIONALITY
            factory_start();
            connect();
            // WHEN CONNECT, it automaticly runs the ortc_OnCOnnected function that subscribes to channels HENCE SUBscribe error 
            // when we called it below :)
            //Thread.Sleep(5000);
            //ortc_OnConnected(null);
            // DONT LOAD GUI
            //InitializeComponent();
            //branch1 siger hej
            //branch1 siger hej igen
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // we dont get here because of InitializeComponent(); not being called
            //button1.Visible = false; // disconnect button used to debug
            Console.WriteLine("we get here");

            button4.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            label1.Visible = false;
            factory_start();
            connect();
            //subscribe();
            ortc_OnConnected(null);

        }

        private void connect()
        {
            _ortc.ClusterUrl = " http://ortc-developers.realtime.co/server/2.1";
            _ortc.Connect(Gappkey, Gapptoken);
            Console.WriteLine("Connected");
        }

        private void OnMessageCallback(object sender, string channel, string message)
        {
            switch (channel)
            {
                case "ortcClientConnected":
                    Console.WriteLine("Connected");
                    break;
                case "ortcClientDisconnected":
                    Console.WriteLine("Disconnected");
                    break;
                case "ortcClientSubscribed":
                    Console.WriteLine("Subbed");
                    break;
                case "ortcClientUnsubscribed":
                    Console.WriteLine("Disconnected");
                    break;
                default:
                    //Log(String.Format("Received at {0}: {1}", channel, message.Substring(message.Length - 5)));
                    //Log(String.Format("Received at {0}: {1}", channel, message));
                    Console.WriteLine("Msg incoming:");
                    Console.WriteLine("Channel: " + channel);
                    Console.WriteLine("Msg : " + message);
                    uint tester = WM_USER + 4642;
                    IntPtr ptr = FindWindow(null, "Form1");

                    if (message == cmd1)
                    {
                        Console.WriteLine("Executing cmd1, Restarting");
                        PostMessage(ptr, tester, 0, 1);

                    }
                    else if (message == cmd2)
                    {
                        Console.WriteLine("Executing cmd2, Shutting down");
                    }
                    else if (message == cmd3 && channel == "devices:100")
                    {
                        Console.WriteLine("Google from admin channel");
                        System.Diagnostics.Process.Start("http://www.google.dk");
                    }
                    else if (message == cmd3 && channel == "devices:1")
                    {
                        Console.WriteLine("Google");
                        System.Diagnostics.Process.Start("http://www.google.dk");
                    }
                    else if (message == cmd4)
                    {
                        System.Diagnostics.Process.Start("C:/Windows/System32/notepad.exe");
                    }
                    else
                    {
                        Console.WriteLine("Unknown command");
                    }
                    break;
            }
        }

        private void factory_start()
        {
            try
            {
                api = new Ortc();
                IOrtcFactory fact = api.LoadOrtcFactory("test");
                if (fact != null)
                {
                    _ortc = fact.CreateClient();

                    if (_ortc != null)
                    {
                        _ortc.Id = "dot_net_client";
                        // set OrtcCLient properties here if you wish
                        _ortc.OnConnected += new OnConnectedDelegate(ortc_OnConnected);
                        _ortc.OnDisconnected += new OnDisconnectedDelegate(ortc_OnDisconnected);
                        _ortc.OnReconnecting += new OnReconnectingDelegate(ortc_OnReconnecting);
                        _ortc.OnReconnected += new OnReconnectedDelegate(ortc_OnReconnected);
                        _ortc.OnSubscribed += new OnSubscribedDelegate(ortc_OnSubscribed);
                        _ortc.OnUnsubscribed += new OnUnsubscribedDelegate(ortc_OnUnsubscribed);
                        _ortc.OnException += new OnExceptionDelegate(ortc_OnException);
                        Console.WriteLine("Factory_start succesfull");
                    }
                }
                else
                {
                    Console.WriteLine("Failure for fact");
                }
            }
            catch
            {
                Console.WriteLine("Error");
            }
            if (_ortc == null)
            {
                Console.WriteLine("Null ortc"); // cw  TAB -- console write
            }
        }

        /*
         private void subscribe()
         {
             _ortc.Subscribe("devices:1", true, OnMessageCallback);
             _ortc.Subscribe("devices:admin", true, OnMessageCallback);
         }
         */
        // CALLING SUBSCRIBE FROM ORTC_OnConnected ** ENSURES WE HAVE CONNECTION BEFORE SUBSCRIBING 
       private void ortc_OnConnected(object sender)
           {
          //CREATE A FOR LOOP HERE TO SUB TO CHANNELS IN A CHANNEL LIST (String list)
          _ortc.Subscribe("devices:"+deviceId, true, OnMessageCallback);
          _ortc.Subscribe("devices:admin", true, OnMessageCallback);
          _ortc.Subscribe("devices:100", true, OnMessageCallback);
      }
        private void ortc_OnDisconnected(object sender)
        {

        }
        private void ortc_OnReconnecting(object sender)
        {

        }

        private void ortc_OnReconnected(object sender)
        {

        }

        private void ortc_OnSubscribed(object sender, string channel)
        {

        }

        private void ortc_OnUnsubscribed(object sender, string channel)
        {

        }

        private void ortc_OnException(object sender, Exception ex)
        {
            Console.WriteLine("Error: " + ex);
            if (ex.ToString().Contains("Ibt.Ortc.Api.Extensibility.OrtcSubscribedException"))
            {
                Console.WriteLine("Subscribe erorr: already subscriped");
            }
            else
            {

                MessageBox.Show(ex.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Closing application. Goodbye :) ");
        }
    


      //Disconect
        private void button1_Click(object sender, EventArgs e)
      {
          _ortc.Disconnect();

      }

      // start button
      private void button2_Click(object sender, EventArgs e)
      {
          try
          {
              api = new Ortc();
              IOrtcFactory fact = api.LoadOrtcFactory("test");
              if (fact != null)
              {
                  _ortc = fact.CreateClient();

                  if (_ortc != null)
                  {
                      _ortc.Id = "dot_net_client";
                      // set OrtcCLient properties here if you wish
                      _ortc.OnConnected += new OnConnectedDelegate(ortc_OnConnected);
                        _ortc.OnDisconnected += new OnDisconnectedDelegate(ortc_OnDisconnected);
                        _ortc.OnReconnecting += new OnReconnectingDelegate(ortc_OnReconnecting);
                        _ortc.OnReconnected += new OnReconnectedDelegate(ortc_OnReconnected);
                        _ortc.OnSubscribed += new OnSubscribedDelegate(ortc_OnSubscribed);
                        _ortc.OnUnsubscribed += new OnUnsubscribedDelegate(ortc_OnUnsubscribed);
                        _ortc.OnException += new OnExceptionDelegate(ortc_OnException);
                    }
              }
              else
              {
                  Console.WriteLine("Failure for fact");
              }
          }
          catch
          {
              Console.WriteLine("Error");
          }
          if (_ortc == null)
          {
              Console.WriteLine("Null ortc"); // cw  TAB -- console write
          }
      }

      //Connect
     private void button3_Click(object sender, EventArgs e)
      {
          _ortc.ClusterUrl = "http://ortc-developers.realtime.co/server/2.1";
          _ortc.Connect(Gappkey, Gapptoken);
          Console.WriteLine("Connected");
      }

      //Subscribe
      private void button4_Click(object sender, EventArgs e)
      {
          if (deviceId != "")
          {
              _ortc.Subscribe("devices:"+deviceId, true, OnMessageCallback);
              Console.WriteLine("this is being called");
          }
          else
          {
              Console.WriteLine("Insert device id");
          }
      }

      // set device id from GUI
      private void textBox1_TextChanged(object sender, EventArgs e)
      {
          deviceId = textBox1.Text;
          Console.WriteLine(deviceId);
      }

      // text on GUI --> "Device id"
      private void label1_Click(object sender, EventArgs e)
      {

      }
  }
}
