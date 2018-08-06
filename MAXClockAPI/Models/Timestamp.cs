using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAXClockAPI.Models {
	public class Timestamp {

		public int Id { get; set; }

		public int StudentId { get; set; }
		public int ClassId { get; set; }

		public DateTime? TimeIn { get; set; }
		public DateTime? TimeOut { get; set; }

	}
}