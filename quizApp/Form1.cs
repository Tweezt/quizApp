using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace quizApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Answer
        {
            public string Text { get; set; }
            public int AnswerKind { get; set; }
            public int Points { get; set; }
        }

        public class RootObject
        {
            public string Text { get; set; }
            public List<Answer> Answers { get; set; }
        }


        public string path;
        List<RootObject> items;
        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
            }
            textBox1.Text = path;

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<RootObject>>(json);
            }

            foreach (var pytanie in items)
            {
                Console.WriteLine(pytanie.Text);
                foreach(var odpowiedz in pytanie.Answers)
                {
                    Console.WriteLine("Punkty: {0}, waga: {1}",odpowiedz.AnswerKind.ToString(),odpowiedz.Points);

                }
            }
        }
    }
}
