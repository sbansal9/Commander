using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Models
{
    public class Command
    {
        //public Command() { }
        //public Command(int id, string howTo, string line, string platform)
        //{
        //    Id = id;
        //    HowTo = howTo;
        //    Line = line;
        //    Platform = platform;
        //}

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
