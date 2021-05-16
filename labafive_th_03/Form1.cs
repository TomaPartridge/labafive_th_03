using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace labafive_th_03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int kol = 0;

        SmtpClient client;

        //делегат для события
        public delegate void SendMessageHandler(object sender, SendMessageArgs e);
        //событие "отправка сообщения", вызывается нажатием кнопки "отправить" через метод OnNewMessage
        event SendMessageHandler Send_Message;

        //public delegate void DelegatZayavki();
        //event Send

        //аж целый метод, чтобы вызвать событие "отправка сообщения"
        private void OnNewMessage(SendMessageArgs e)
        {

            Send_Message.Invoke(this, e);
        }

        //метод, подписанный на событие "отправка сообщения" (во время загрузки формы1)
        //т. е. будет отрабатывать при возникновении (вызове) события
        private void SendMessage(object sender, SendMessageArgs e)
        {
            e.MessageDate = "what";
            Send("mamin111haker@gmail.com", textBox1.Text,textBox2.Text,richTextBox1.Text,e.MessageDate);
        }
        private void SendMes2(object sender, SendMessageArgs e)
        {
            e.MessageDate = "what";//0_00000000000000000000000000000000000000
            string polya =$"<ul type=\"square\">"+
                $"<li>ФИО: {textBox3.Text}</li>"+
                $"<li>Номер группы: {textBox6.Text}</li>"+
                $"<li>Корпус: {domainUpDown1.SelectedItem}</li>"+
                $"<li>Аудитория: {(domainUpDown1.SelectedIndex==0?405:domainUpDown1.SelectedIndex==1?50:2900)}</li>"+
                $"<li>Дата: {monthCalendar1.SelectionStart.ToString()}</li>"+
                $"<li>Количество: {textBox7.Text}</li>"+                
                $"<li>Цель: {richTextBox2.Text}</li></ul>"+
                $"\nСтоловая \"Вилка\" дарит вам {textBox7.Text} пирожков за активную жизненную позицию и боевой настрой, не забудьте их забрать\n";
            string state="одобрено";

            string bod = "<head><style>*{font-family:Lobster,Georgia,Serif;color:lightblue;background:#0D0D0D}ul{color:aqua;}h5{color:white;font-size:3pt}</style></head>" +
                $"Здравствуйте, {textBox3.Text}!<br/>" +
                $"Вы подали новую заявку <br/>" +
                $"Описание заявки:<br/>" +
                $"{polya}<br/>" +
                "<h5>набор абстрактных функций и свойств, через который программы взаимодействует с COM-компонентом. Состав этого набора объявляется независимо от компонента, и публикуется, как правило, на языке IDL.</h5>" +
                $"Состояние заявки:{state}<br/>" +
                $"Ссылка на Хабр: https://habr.com/ru/";

            //e = new SendMessageArgs();//значение даты = null
            Send("mamin111haker@gmail.com", textBox5.Text, textBox4.Text, bod, e.MessageDate);
        }

        //когда форма загружается, на событие "отправка сообщения" подписывается метод - обработчик события, т.е. метод SendMessage добавляется в делегат SendMessageHandler
        //(Load - событие; Form1_Load - метод-обработчик события "загрузка", он же - единственный подписчик)
        private void Form1_Load(object sender, EventArgs e)
        {
            Send_Message += SendMessage;
            ///Console.WriteLine(textBox5.Text);
            //string[] gik = textBox5.Text.Split('\n');
            //foreach (var item in gik)
            //{
            //    Console.WriteLine(item);
            //}

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // smtp-сервер, к которому подключаемся и его порт
            client = new SmtpClient("smtp.gmail.com", 587);//25);//("smtp.gmail.com", 587);465
            //адрес почтового ящика в выбранной почтовой системе и пароль от него
            client.Credentials = new NetworkCredential("mamin111haker@gmail.com", "ipapin00");
            //включение/отключение шифрования
            client.EnableSsl = true;//false;

            //вызов события отправки
            OnNewMessage(new SendMessageArgs());

            //Если всё прошло успесшно, выведется сообщение
            MessageBox.Show("Сообщение отправлено!");


        }

        public void Send(string from,string to,string subject,string body,string time)
        {
            //создание сообщения
            var message = new MailMessage()
            {
                From = new MailAddress(from, "ФигураА22К"),
                Subject = subject,
                Body = body + "\n♪☼♫╪√√√<br><br>\n" + time,
                IsBodyHtml = true
            };


            // перевод массива адресов
            string[] gik = to.Split('\n');
            bool sent = false;
            for(int prot=0;prot<gik.Length;prot++)
            {
                gik[prot]=gik[prot].Trim('\r');
            }
            foreach (var item in gik)
            {
                //item

                //метод проверки адреса, на который отправляем сообщение, опсан ниже
                if (!ValidateEmail(item))
                    continue;
                sent = true;
                kol++;

                //добавление адресов, кому отправляем письмо. Их может быть несколько.
                message.To.Add(new MailAddress(item));
            }
            if (!sent)
                throw new Exception("Нет верных адресов");
            
            //непосредственно отправка сообщения
            client.Send(message);
        }

        public static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // smtp-сервер, к которому подключаемся и его порт
            client = new SmtpClient("smtp.gmail.com", 587);
            //адрес почтового ящика в выбранной почтовой системе и пароль от него
            client.Credentials = new NetworkCredential("mamin111haker@gmail.com", "ipapin00");
            //включение/отключение шифрования
            client.EnableSsl = true;

            Send_Message -= SendMessage;
            Send_Message += SendMes2;

            try
            {
                OnNewMessage(new SendMessageArgs());//Invoke
                MessageBox.Show($"Сообщение отправлено! ({kol})");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Send_Message += SendMessage;
            Send_Message -= SendMes2;
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                StreamReader r = new StreamReader("pas.txt");//grehrius?dlk
                string pass = r.ReadLine();
                int g = pass.Length / 5;
                string psw="";
                for(int i=0; i<pass.Length; i+=g)
                {
                    psw+=pass[i] >> pass.Length;
                    for (int k = i + 1; k < i + g; k++)
                        psw += ~pass[i];
                }
  
                if ( psw== textBox8.Text)
                {
                    panel1.Visible = false;
                }
                else
                {
                    r.BaseStream.Position = 0;
                    label12.Text = "Неверный пароль";
                    label12.Refresh();
                    Thread.Sleep(4000);
                    label12.Text = "Введите пароль";
                }
            }
        }
    }

    public class SendMessageArgs
    {
        public SendMessageArgs()
        { }

        string messageDate;
        public string MessageDate
        {
            get { return messageDate; }
            set { messageDate = DateTime.Now.ToString(); }
        }
    }
}
