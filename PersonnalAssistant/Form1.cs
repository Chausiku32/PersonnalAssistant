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

namespace PersonnalAssistant
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechSynthesizer spk = new SpeechSynthesizer();
        Choices list = new Choices();
        public Form1()
        { 
            introduction();
            letsTalk();
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void introduction(){
            spk.Speak("how may I help you?");
        }

        public void letsTalk() {
            //list of key words
            list.Add(new string[] { "hello", "hey", "update", "open youtube", "what is the time", "open gmail", "when is today" });
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
            if(command == "update")
            {
                spk.Rate = -1;
                news.news();
            }
            if (command == "hey")
            {
                say("Hello there");
            }
            if (command == "open youtube")
            {
                say("opening youtube");
                Process.Start("https://youtube.com");
            }
            if(command == "open gmail")
            {
                say("opening gmail");
                Process.Start("https://gmail.com");
            }
            if (command == "what is the time")
            {
                say("It is now:");
                say(DateTime.Now.ToString("h:mm:tt"));
            }
            if (command == "when is today")
            {
                say("Today is:");
                say(DateTime.Now.ToString("d/M/yyyy"));
            }
        }

        //her reply
        public void say(string res)
        {
            spk.Speak(res);
        }

    }
}
