using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FileChanger3.Dal.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
    }
}
