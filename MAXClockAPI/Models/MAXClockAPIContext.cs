using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MAXClockAPI.Models
{
    public class MAXClockAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MAXClockAPIContext() : base("name=MAXClockAPIContext")
        {
        }

		public System.Data.Entity.DbSet<MAXClockAPI.Models.Instructor> Instructors { get; set; }

		public System.Data.Entity.DbSet<MAXClockAPI.Models.Student> Students { get; set; }

		public System.Data.Entity.DbSet<MAXClockAPI.Models.Class> Classes { get; set; }

		public System.Data.Entity.DbSet<MAXClockAPI.Models.Timestamp> Timestamps { get; set; }
	}
}
