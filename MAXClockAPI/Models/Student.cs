using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAXClockAPI.Models {
	public class Student {

		public int Id { get; set; }

		[JsonIgnore]
		public virtual List<Class> Classes { get; set; }

		public string Firstname { get; set; }
		public string Lastname { get; set; }

		public int PIN { get; set; }

		public bool ChekedIn { get; set; }

	}
}