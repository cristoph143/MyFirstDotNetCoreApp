using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstDotNetCoreApp.Models
{
    public class PersonGridModel
    {
        public string GridTitle { get; set; } = string.Empty;
        public List<Person> Persons { get; set; } = new List<Person>();
    }
}