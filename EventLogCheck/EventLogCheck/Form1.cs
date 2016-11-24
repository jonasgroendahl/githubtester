using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO; // Files
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventLogCheck
{
    public partial class Form1 : Form
    {
        string whatErr = "All";
        // Run VS in admin mode to access the logs
        public Form1()
        {
            //InitializeComponent();
            this.ShowInTaskbar = false;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            string[] args = Environment.GetCommandLineArgs();
            Console.WriteLine(args[1]);
            if (args[1] != "")
            {
                whatErr = args[1];
            }
            printErrors();
            dostuff();
            Environment.Exit(1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            whatErr = textBox1.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void printErrors()
        {

            //string path = Environment.CurrentDirectory + @"\systemlog.txt";
            string path2 = Environment.CurrentDirectory + @"\systemlog.html";
           // Console.WriteLine(path);
           // StreamWriter file = new StreamWriter(path, false);
            StreamWriter file2 = new StreamWriter(path2, false);
            file2.Flush();
            // EventLog[] log = EventLog.GetEventLogs();
            // Looks in the system log file
            EventLog myE = new EventLog("System", ".");
            //  foreach (EventLog aLog in log)
            //{

            //Console.WriteLine("Log name: "+aLog.Log);
            //Console.WriteLine("Number of event log entries: "+aLog.Entries.Count.ToString());
            // }
            EventLogEntryCollection col = myE.Entries;
            //file2.WriteLine("<html><head><meta http-equiv=\"Content - Type\" content=\"text / html; charset = UTF - 8\"/></head><body bgcolor =#000000  leftmargin=2 topmargin=2 bottommargin=2 rightmargin=2>");
            for (int i = 0; i < col.Count; i++)
            {
                EventLogEntry entry = col[i];
                if (whatErr == "All")
                {
                    // OPTIONS "Error", "Critical", "Warning", "Information"
                    // https://msdn.microsoft.com/en-us/library/system.diagnostics.eventlogentry(v=vs.90).aspx
                    if (entry.EntryType.ToString().Equals("Error"))
                    {
                        // Console.WriteLine("Source: "+entry.Source+", id: "+entry.EventID+" date: "+entry.TimeGenerated);
                        //file.WriteLine("Source: " + entry.Source + ", id: " + entry.EventID + " date: " + entry.TimeGenerated); // + ", message: " + entry.Message);
                        file2.WriteLine("<table bgcolor =#000000 border=0 cellpadding=0 cellspacing=8 width=100%><tr><td width=130 align=left valign=middle><font color=#FFFFFF style=\"font-family: arial; font-size: 12px;\" >" + entry.TimeGenerated + "</font></td><td align=left valign=middle width=5 style=\"background-color: #C6DC00;\"></td><td align=left valign=middle><font color=#C6DC00 style=\"font-family: arial; font-size: 10px; font-weight: bold;\">" + "Error: " + entry.EventID + "<br></font><font color=#C4C4C4 style=\"font-family: arial; font-size: 12px; font-weight: normal;\"><p>Source: " + entry.Source + "</br>Message: " + entry.Message + "</p></font></td></tr></table><table border=0 cellpadding=0 cellspacing=0 height=1 width=100% bgcolor=#262626><tr><td></td></tr></table>");
                    }
                    if (entry.EntryType.ToString().Equals("Critical"))
                    {
                        file2.WriteLine("<table bgcolor =#000000 border=0 cellpadding=0 cellspacing=8 width=100%><tr><td width=130 align=left valign=middle><font color=#FFFFFF style=\"font-family: arial; font-size: 12px;\" >" + entry.TimeGenerated + "</font></td><td align=left valign=middle width=5 style=\"background-color: #DC1600;\"></td><td align=left valign=middle><font color=#DC1600 style=\"font-family: arial; font-size: 10px; font-weight: bold;\">" + "Critical: " + entry.EventID + "<br></font><font color=#C4C4C4 style=\"font-family: arial; font-size: 12px; font-weight: normal;\">Source: " + entry.Source + "</br>Message: " + entry.Message + "</p></font></td></tr></table><table border=0 cellpadding=0 cellspacing=0 height=1 width=100% bgcolor=#262626><tr><td></td></tr></table>");
                    }
                }
                else if (whatErr == "Error")
                {
                    if (entry.EntryType.ToString().Equals("Error"))
                    {
                        // Console.WriteLine("Source: "+entry.Source+", id: "+entry.EventID+" date: "+entry.TimeGenerated);
                        //file.WriteLine("Source: " + entry.Source + ", id: " + entry.EventID + " date: " + entry.TimeGenerated); // + ", message: " + entry.Message);
                        file2.WriteLine("<table bgcolor =#000000 border=0 cellpadding=0 cellspacing=8 width=100%><tr><td width=130 align=left valign=middle><font color=#FFFFFF style=\"font-family: arial; font-size: 12px;\" >" + entry.TimeGenerated + "</font></td><td align=left valign=middle width=5 style=\"background-color: #C6DC00;\"></td><td align=left valign=middle><font color=#C6DC00 style=\"font-family: arial; font-size: 10px; font-weight: bold;\">" + "Error: " + entry.EventID + "<br></font><font color=#C4C4C4 style=\"font-family: arial; font-size: 12px; font-weight: normal;\"><p>Source: " + entry.Source + "</br>Message: " + entry.Message + "</p></font></td></tr></table><table border=0 cellpadding=0 cellspacing=0 height=1 width=100% bgcolor=#262626><tr><td></td></tr></table>");
                    }
                }
                else
                {
                    if (entry.EntryType.ToString().Equals("Critical"))
                    {
                        file2.WriteLine("<table bgcolor =#000000 border=0 cellpadding=0 cellspacing=8 width=100%><tr><td width=130 align=left valign=middle><font color=#FFFFFF style=\"font-family: arial; font-size: 12px;\" >" + entry.TimeGenerated + "</font></td><td align=left valign=middle width=5 style=\"background-color: #DC1600;\"></td><td align=left valign=middle><font color=#DC1600 style=\"font-family: arial; font-size: 10px; font-weight: bold;\">" + "Critical: " + entry.EventID + "<br></font><font color=#C4C4C4 style=\"font-family: arial; font-size: 12px; font-weight: normal;\"><p>Source: " + entry.Source + "</br>Message: " + entry.Message + "</p></font></td></tr></table><table border=0 cellpadding=0 cellspacing=0 height=1 width=100% bgcolor=#262626><tr><td></td></tr></table>");
                    }
                }
            }
            //file2.WriteLine("</ body ></ html >");
            file2.Close(); // http://stackoverflow.com/questions/12735897/streamwriter-is-cutting-off-my-last-couple-of-lines-sometimes-in-the-middle-of-a
           }

        private void dostuff()
        {

            string pathR = Environment.CurrentDirectory + @"\systemlog.html";
            string pathW = Environment.CurrentDirectory + @"\systemlog2.html";
            //var fs = File.OpenRead(pathR);
            int fileLength = File.ReadAllLines(pathR).Length;
            //fs.Position = fileLength;
            StreamReader sread = new StreamReader(pathR);
            string[] strArr = new string[fileLength];
            int j = fileLength-1;
            Console.WriteLine(fileLength);
            for (int i = 0; i < fileLength; i ++)
            {
                string line = sread.ReadLine();
                strArr[j] = line;
                j = j -1;
                Console.WriteLine(j);
            }
            sread.Close();
            StreamWriter swrite = new StreamWriter(pathW);
            swrite.Flush();
            swrite.WriteLine("<html><head><meta http-equiv=\"Content-Type\"content=\"text/html;charset=UTF-8\"/></head><body bgcolor =#000000  leftmargin=2 topmargin=2 bottommargin=2 rightmargin=2>");
            for (int i = 0; i < fileLength; i ++)
            {
                swrite.WriteLine(strArr[i]);
            }
            swrite.WriteLine("</ body ></ html >");
            swrite.Close();
            File.Delete(pathR);
            File.Move(pathW, pathR);
            //File.Replace(pathW, pathR,Environment.CurrentDirectory);
           }

        }
}
