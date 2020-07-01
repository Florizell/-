using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Work_Cache_Esm
{
    public partial class Form1 : Form
    {
        Control controller;
        int AdresI, AdresJ, AdresK;
        int CountPages,
            CountLines,
            CountElements;
        int Value;
        int[] str;

        private void Button_Change_Click(object sender, EventArgs e)
        {
            try
            {
                AdresI = Convert.ToInt32(textBox_PageSearch.Text);
                AdresJ = Convert.ToInt32(textBox_StringSearch.Text);
                AdresK = Convert.ToInt32(textBox_ItemSearch.Text);
                Value = Convert.ToInt32(textBox_ItemChange.Text);
            }
            catch
            {
                MessageBox.Show("В поиске допущена ошибка!");
                return;
            }

            Time.Start();
            str = controller.SearchLine(AdresI, AdresJ);
            Time.Stop();

            if (controller.IsCache)
            {
                label_WhereFrom.Text = "Элемент загружен из Кэша";
                label_WhereFrom.ForeColor = Color.White;
            }
            else
            {
                label_WhereFrom.Text = "Элемент загружен из Оп";
                label_WhereFrom.ForeColor = Color.White;
            }

            str[AdresK] = Value;
            controller.SetLineOnCache(str, CountElements, AdresJ);
            label_ItemFrom.Text = Value.ToString();
            label_StringFrom.Text = " ";
            for (int i = 0; i < CountElements; i++)
            {
                label_StringFrom.Text += str[i].ToString() + " ";
            }

            WriteToTextBox_Cache();
            WriteToTextBox_OP();
            label_TimeFrom.Text = Time.Elapsed.ToString();
            Time.Reset();
        }

        Stopwatch Time = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            CountPages = 10;
            CountLines = 10;
            CountElements = 4;
            controller = new Control(CountPages, CountLines, CountElements, "MainMemory");
            label_WhereFrom.Text = "Элементы не загружены";
            label_WhereFrom.ForeColor = Color.White;
          
            WriteToTextBox_OP();
            WriteToTextBox_Cache();
            textBox_OP.ReadOnly = true;
            textBox_Cache.ReadOnly = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label_WhereFrom_Click(object sender, EventArgs e)
        {

        }

        private void Label_Adress_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label_CACHE_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label_Page_Click(object sender, EventArgs e)
        {
                    }

        private void Button_Search_Click_1(object sender, EventArgs e)
        {
            try
            {
                AdresI = Convert.ToInt32(textBox_PageSearch.Text);
                AdresJ = Convert.ToInt32(textBox_StringSearch.Text);
                AdresK = Convert.ToInt32(textBox_ItemSearch.Text);
            }
            catch
            {
                MessageBox.Show("В поиске допущена ошибка!");
                return;
            }

            Time.Start();
            str = controller.SearchLine(AdresI, AdresJ);
            Time.Stop();

            if (controller.IsCache)
            {
                label_WhereFrom.Text = "Элемент загружен из Кэша";
                label_WhereFrom.ForeColor = Color.White;
            }
            else
            {
                label_WhereFrom.Text = "Элемент загружен из Оп";
                label_WhereFrom.ForeColor = Color.White;
            }

            Value = str[AdresK];
            label_ItemFrom.Text = Value.ToString();
            label_StringFrom.Text = " ";
            for (int i = 0; i < CountElements; i++)
            {
                label_StringFrom.Text += str[i].ToString() + " ";
            }

            WriteToTextBox_Cache();
            WriteToTextBox_OP();
            label_TimeFrom.Text = Time.Elapsed.ToString();
            Time.Reset();

        }

        private void Button_Create_Click(object sender, EventArgs e)
        {
            WriteToTextBox_OP();
        }
      
        private void WriteToTextBox_OP()
        {
            textBox_OP.Text = "";
            for (int i = 0; i < CountPages; i++)
            {
                for (int j = 0; j < CountLines; j++)
                {
                    for (int k = 0; k < CountElements; k++)
                    {
                        textBox_OP.Text += (controller[i, j, k].ToString() + " ");
                    }
                    textBox_OP.Text += Environment.NewLine;
                }
                textBox_OP.Text += Environment.NewLine;
            }
            textBox_OP.Text += Environment.NewLine;
        }
        private void WriteToTextBox_Cache()
        {
            textBox_Cache.Text = "";
            for (int i = 0; i < CountLines; i++)
            {
                textBox_Cache.Text += controller[i] + "    ";
                for (int j = 0; j < CountElements; j++)
                {
                    textBox_Cache.Text += controller[i, j].ToString() + " ";
                }
                textBox_Cache.Text += Environment.NewLine;
            }
            textBox_Cache.Text += Environment.NewLine;
        }

    }
}
