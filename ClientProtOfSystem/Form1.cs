using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProtOfSystem
{
    interface IForm
    {
        //Методы обработки введенного текста
        void ClearAllField();
        void WriteAllField();
    }

    public partial class Form1 : Form, IForm
    {
        //Описание полей письма
        public string Title { get; set; }
        public string Target { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }

        public Form1()
        {
            InitializeComponent();
            ClearAllField();
            labelTime.Text = "";
            labelState.Text = "";
        }

        //Обрабатываем нажатие кнопки отправки письма на сервер
        private async void buttonSend_ClickAsync(object sender, EventArgs e)
        {
            ClearAllField();
            WriteAllField();
            if (fieldsAreFull())
            {
                try
                {
                    labelState.Text = "";
                    await RequestClass.PostRequest(CreateLetter(Title, Target, Sender, Content, Date));
                    labelState.Text = "Данные отправлены!";
                }
                catch (Exception ex)
                {
                    labelState.Text = ex.Message;
                }
            }
            else
                labelState.Text = "Заполните все поля!";
        }


        //Создаем объект письма
        private Letter CreateLetter(string title, string target, string sender, string content, string date)
        {

            Letter letter = new Letter
            {
                Title = title,
                Target = target,
                Sender = sender,
                Content = content,
                Date = date
            };
            return letter;
        }


        //Методы обработки введенного текста
        #region 
        public void ClearAllField()
        {
            Title = "";
            Target = "";
            Sender = "";
            Content = "";
            Date = "";
        }

        public void WriteAllField()
        {
            Title = textBoxTitle.Text;
            Target = textBoxTarget.Text;
            Sender = textBoxSender.Text;
            Content = textBoxContent.Text;
            Date = labelTime.Text.ToString();
        }

        #endregion

        //Тики таймера обновляют время на форме
        private void timerClock_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString();
        }

        //Очистка полей формы
        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAllField();
            textBoxTitle.Clear();
            textBoxTarget.Clear();
            textBoxSender.Clear();
            textBoxContent.Clear();
        }

        private bool fieldsAreFull()
        {
            if ((textBoxTitle.Text != "")&&(textBoxTarget.Text != "")&&(textBoxSender.Text != "")&&(textBoxContent.Text != ""))
            {
                return true;
            }
            return false;
        }
    }
}
