using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SPA_Template.Models
{
    class Client
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public String Phone { get; set; }
        public DateTime Date { get; set; }
        public bool editable { get; set; }
        public int[] LR{ get; set; }
    }
}
