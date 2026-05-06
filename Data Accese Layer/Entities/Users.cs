using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Entities
{
    public class Users
    {
        public int Id { get; set; } 
        public string TC {  get; set; }
        public string Password {  get; set; }
        public virtual Patient Patient { get; set; }
    }
}
