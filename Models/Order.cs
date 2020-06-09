using System;

namespace tireFitting.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Comment { get; set; } // Описание работы
        public DateTime DateCompletion { get; set; } // Дата выполнения
        public bool IsReady { get; set; } // выполнена

        public int PeopleId { get; set; } 
        public People People { get; set; }

    }
}
