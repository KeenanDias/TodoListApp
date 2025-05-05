using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp
{
    public class todoItems
    {
        [Key]
        public int id;
        public string description {  get; set; } = string.Empty;
    }
}
