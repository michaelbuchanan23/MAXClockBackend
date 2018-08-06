using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using MAXClockAPI.Models;
using MAXClockAPI.Utilities;

namespace MAXClockAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class TimestampsController : ApiController
    {
        private MAXClockAPIContext db = new MAXClockAPIContext();

		[HttpGet]
		[ActionName("List")]
		public JSONResponse ListClasses() {

			return new JSONResponse() {
				Data = db.Timestamps.ToList()
			};
		}

		[HttpGet]
		[ActionName("Get")]
		public JSONResponse GetClass(int id) {

			return new JSONResponse();
		}

		[HttpPost]
		[ActionName("Create")]
		public JSONResponse CreateClass(Timestamp timestamp) {

			return new JSONResponse();
		}

		[HttpPost]
		[ActionName("Delete")]
		public JSONResponse DeleteClass(int id) {

			return new JSONResponse();
		}

		[HttpPost]
		[ActionName("Toggle")]
		public JSONResponse TimeIn(Timestamp timestamp) {

			Student student = db.Students.Find(timestamp.StudentId);

			DateTime Time = DateTime.Now;
			if (student.ChekedIn == true) {
			timestamp.TimeOut = Time;
			} else {
			timestamp.TimeIn = Time;
			}

			student.ChekedIn = !student.ChekedIn;

			db.Entry(timestamp).State = EntityState.Added;
			db.Entry(student).State = EntityState.Modified;
			db.SaveChanges();
			return new JSONResponse() {
				Action = "Student Clocked",
				Data = student,
				Error = "N/A"
			};
		}

	}
}