using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ToDo.Models;

namespace ToDo.Models
{
    public class tododB : DbContext
    {

    public tododB()
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    }
}