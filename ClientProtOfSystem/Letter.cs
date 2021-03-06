﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProtOfSystem
{
    class Letter
    {
        //Уникальный идентификатор письма
        public int Id { get; set; }
        //Название письма
        public string Title { get; set; }       
        //Адресат письма
        public string Target { get; set; }
        //Отправитель
        public string Sender { get; set; }
        //Содержание
        public string Content { get; set; }
        //Дата отправки
        public string Date { get; set; }
    }
}
