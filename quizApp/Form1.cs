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
        int index = 0;
        int wynik = 0;
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

            //foreach (var pytanie in items)
            //{
            //    Console.WriteLine(pytanie.Text);
            //    foreach (var odpowiedz in pytanie.Answers)
            //    {
            //        Console.WriteLine("Punkty: {0}, waga: {1}", odpowiedz.AnswerKind.ToString(), odpowiedz.Points);

            //    }
            //}
            //for (int i = 0; i < items.Count(); i++)
            //{
            //    Console.WriteLine(items[i].Text);
            //    for (int j = 0; j < items[i].Answers.Count(); j++)
            //    {
            //        Console.WriteLine("Punkty: {0}, waga: {1}, pytanie: {2}",
            //            items[i].Answers[j].AnswerKind.ToString(), items[i].Answers[j].Points, items[i].Answers[j].Text);

            //    }
            //}

            button2.Enabled = true;
            LoadNextQuestion(0);
            
        }
        private void LoadNextQuestion(int i)
        {
            label1.Text = items[i].Text;
            label2.Text = String.Format("{0}/{1}", index + 1, items.Count());
            checkBox1.Text = items[i].Answers[0].Text;
            checkBox2.Text = items[i].Answers[1].Text;
            checkBox3.Text = items[i].Answers[2].Text;
            checkBox4.Text = items[i].Answers[3].Text;

            if ( index <= items.Count())
            {
                index += 1;
            }
            
        }

        private int UpdateScore()
        {
            int currentScore = 0;
            if (checkBox1.Checked && items[index-1].Answers[0].AnswerKind == 1) currentScore += items[index-1].Answers[0].Points; //TODO make a loop with OfType
            if (checkBox2.Checked && items[index-1].Answers[1].AnswerKind == 1) currentScore += items[index-1].Answers[1].Points;
            if (checkBox3.Checked && items[index-1].Answers[2].AnswerKind == 1) currentScore += items[index-1].Answers[2].Points;
            if (checkBox4.Checked && items[index-1].Answers[3].AnswerKind == 1) currentScore += items[index-1].Answers[3].Points;

            Console.WriteLine(currentScore);
            return currentScore;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wynik += UpdateScore();

            if(index < items.Count())
            {
                LoadNextQuestion(index);
            }
            else
            {
                MessageBox.Show(String.Format("Koniec. Twój wynik: {0}", wynik.ToString()));
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
