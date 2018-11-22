using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace ClientProtOfSystem
{
   class RequestClass
    {
        public static string State { get; set; }

        //Адрес сервера, по которому выполняется запрос
        private const string path = "http://localhost:52676/Home/CreateLetter";        

        public static async Task PostRequest(Letter letter)
        {            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Method = "POST"; // для отправки используется метод Post
            //Формируем строку, куда передаем все данные:
            //Тема письма
            string data = "Title=" + letter.Title;
            //Адресат
            data += "&Target=" + letter.Target;
            //Отправитель
            data += "&Sender=" + letter.Sender;
            //Содержание письма
            data += "&Content=" + letter.Content;
            //Время отправки
            data += "&Date=" + letter.Date;
            //Преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            //устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";           
            //Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;
            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            //Возвращаем ответ на запрос          
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();            
            
            using (Stream stream = response.GetResponseStream())
            {                
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd() + "\n" + response.StatusCode.ToString());                    
                }                
            }            
            response.Close();            
        }
    }
}
