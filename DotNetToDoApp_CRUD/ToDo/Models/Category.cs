using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using ToDo.Models;

namespace ToDo.Models
{
    public class Category
    {
       [Key]
       public int catID { get; set; }
       public string Name { get; set; }
       public virtual  ICollection<Item> Items { get; set; }

    }
}