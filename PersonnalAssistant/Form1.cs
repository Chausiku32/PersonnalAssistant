using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Xml;

namespace PersonnalAssistant
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Boolean wake = true;
        SpeechSynthesizer spk = new SpeechSynthesizer();
        Choices list = new Choices();

        public Form1()
        {
            letsTalk();

            InitializeComponent();
        }

        System.Data.SqlClient.SqlConnection con;

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS; AttachDbFilename =Database1.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            con.Open();
            MessageBox.Show("Connection opened");
            con.Close();
            MessageBox.Show("Connection closed");
             * */

            say("Hello" + textBox5.Text);

            label4.Text = @"DateTime.Now";
        }

        //her reply
        public void say(string res)
        {
            spk.Speak(res);
            textBox2.AppendText(res + "\n");
        }

        public static void killProc(String s)
        {
            System.Diagnostics.Process[] procs = null;

            try 
            {
                procs = Process.GetProcessesByName(s);
                Process prog = procs[0];

                if(!prog.HasExited)
                {
                    prog.Kill();
                }

            }
            finally
            {
                if(procs != null)
                {
                    foreach(Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }

        public void restart()
        {
            //restart program
            Process.Start(@"C:\Users\Mariana\Desktop\PersonnalAssistant\PersonnalAssistant\bin\Debug\PersonnalAssistant.exe");
            Environment.Exit(0);
        }


        public void letsTalk()
        {
            //list of key words
            list.Add(new string[] { "hello", "hey", "how are you", "update", "open youtube", "what is the time", "open gmail", "when is today", "sleep", "restart", "open wordpad", "close wordpad", "what is the weather", "what is the temperature", "jartello", "i have a problem" });
            Grammar gr = new Grammar(new GrammarBuilder(list));

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch { return; }

        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text;
            NewsUpdate news = new NewsUpdate();

            if (command == "jartello") 
            {
                say("How may I help you");
                wake = true;
                label3.Text = "State: Awake";
            }

            if (command == "sleep") 
            { 
                wake = false;
                label3.Text = "State: Sleep Mode";
            }
           
            if (wake == true)
            {

                if (command == "restart")
                {
                    restart();
                }

                if (command == "update")
                {
                    news.news();
                }

                if (command == "hey")
                {
                    say("Hello");
                }

                if (command == "how are you")
                {
                    say("I am fine. Thanks for asking; and how are you");
                }

                if(command == "good")
                {
                    say("Thats great. How may I be of service");
                }

                if (command == "bad")
                {
                    say("I have a joke that might help.");
                    say("Why did the chicken cross the road");
                    say("");
                }

                if (command == "i am not feeling so well")
                {
                    say("I am really sorry");
                }

                if (command == "open youtube")
                {
                    say("opening youtube");
                    Process.Start("https://youtube.com");
                }

                if (command == "open gmail")
                {
                    say("opening gmail");
                    Process.Start("https://gmail.com");
                }

                if (command == "what is the time")
                {
                    say(DateTime.Now.ToString("h:mm tt"));
                }

                if (command == "when is today")
                {
                    say(DateTime.Now.ToString("M/d/yyyy"));
                    
                }

                if (command == "open wordpad")
                {
                    Process.Start(@"C:\Program Files\Windows NT\Accessories\wordpad.exe");
                }

                if (command == "close wordpad")
                {
                    killProc("wordpad.exe");
                }

                /*
                 * 
                //pastebin.com/kcSBrEFq
                //PasteBin Voice Bot Weather
                if(command == "what is the weather")
                {
                    say(GetWeather("cond"));
                }
                if (command == "what is the temperature")
                {
                    say(GetWeather("temp"));
                }
                 * 
                 
                if (command == "i have a problem")
                {
                    //this.Hide();
                    //Form2.;
                }
                 * 
                 */
            }
            textBox1.AppendText(command + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
