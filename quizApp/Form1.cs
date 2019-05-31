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

            
            
        }
        private void LoadNextQuestion(int i)
        {
            label1.Text = items[i].Text;
            label2.Text = String.Format("{0}/{1}", index + 1, items.Count());

            checkBox1.Text = items[i].Answers[0].Text;
            checkBox1.Checked = false;

            checkBox2.Text = items[i].Answers[1].Text;
            checkBox2.Checked = false;

            checkBox3.Text = items[i].Answers[2].Text;
            checkBox3.Checked = false;

            checkBox4.Text = items[i].Answers[3].Text;
            checkBox4.Checked = false;

            if ( index <= items.Count())
            {
                index += 1;
            }
            
        }

        private int UpdateScore() //TODO make a loop with OfType
        {
            int currentScore = 0;
            if (checkBox1.Checked && items[index-1].Answers[0].AnswerKind == 1)
            {
                currentScore += items[index-1].Answers[0].Points; 
                if (currentScore < 0) return currentScore;
            }

            if (checkBox2.Checked && items[index-1].Answers[1].AnswerKind == 1)
            {
                currentScore += items[index-1].Answers[1].Points;
                if (currentScore < 0) return currentScore;
            }

            if (checkBox3.Checked && items[index-1].Answers[2].AnswerKind == 1)
            {
                currentScore += items[index-1].Answers[2].Points;
                if (currentScore < 0) return currentScore;
            }

            if (checkBox4.Checked && items[index-1].Answers[3].AnswerKind == 1)
            {
                currentScore += items[index-1].Answers[3].Points;
                if (currentScore < 0) return currentScore;
            }

            if (checkBox1.Checked && items[index - 1].Answers[0].AnswerKind == 0) currentScore = 0;
            if (checkBox2.Checked && items[index - 1].Answers[1].AnswerKind == 0) currentScore = 0;
            if (checkBox3.Checked && items[index - 1].Answers[2].AnswerKind == 0) currentScore = 0;
            if (checkBox4.Checked && items[index - 1].Answers[3].AnswerKind == 0) currentScore = 0;

            //Console.WriteLine(currentScore);
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
                if(wynik<0)
                {
                    MessageBox.Show(String.Format("Koniec. Udzieliłeś odpowiedzi kardynalnej.\nProsimy nie strzelać!", wynik.ToString()));
                    System.Windows.Forms.Application.Exit();
                }
                MessageBox.Show(String.Format("Koniec. Twój wynik: {0}", wynik.ToString()));
                System.Windows.Forms.Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<RootObject>>(json);
            }

            button2.Enabled = true;
            LoadNextQuestion(0);
        }
    }
}
