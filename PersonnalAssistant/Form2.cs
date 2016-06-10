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
    public partial class Form2 : Form
    {
        SpeechSynthesizer spch = new SpeechSynthesizer();
        Choices c = new Choices();
        public Form2()
        {
            InitializeComponent();
            spch.Speak("Yaah");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void getCommands() 
        {
        }
    }
}
