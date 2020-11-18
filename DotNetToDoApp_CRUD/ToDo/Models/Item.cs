using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class Item 
    {

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }


        [ForeignKey("Category")]
        public int catID { get; set; }
        public virtual Category Category { get; set; }


        public bool Completed { get; set; }
    }
}