using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW_Project.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
    }
}
