using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAXClockAPI.Models {
	public class Class {

		public int Id { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public bool Active { get; set; }
	
		public virtual Instructor Instructor { get; set; }

		public virtual List<Student> Students { get; set; }

	}
}