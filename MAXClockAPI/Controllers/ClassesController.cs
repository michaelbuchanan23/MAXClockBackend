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
	public class ClassesController : ApiController
    {
        private MAXClockAPIContext db = new MAXClockAPIContext();

        [HttpGet]
		[ActionName("List")]
		public JSONResponse ListClasses() {
			//list all classes
			List<Class> Classes = db.Classes.ToList();

			//add previous data to the JSON for initial stamp
			foreach (Class Class in Classes) {
				foreach (Student Student in Class.Students) {
					//if the student is clocked out
					if (!Student.Status) {
						//find the last clock out stamp
						List<Timestamp> Stamps = db.Timestamps.Where(stamp => stamp.StudentId == Student.Id).ToList();

						//prevent error if first stamp
						if (Stamps.Count > 1) {
							Student.Timestamp.TimeOut = Stamps[Stamps.Count - 2].TimeOut;
						}
					}
				}
			}

			return new JSONResponse() {
				Action = "Listing All Classes",
				Data = Classes,
				Error = "N/A"
			};
		}

		[HttpGet]
		[ActionName("Active")]
		public JSONResponse ActiveClasses() {
			List<Class> Classes = db.Classes.Where(cls => cls.Active == true).ToList();
			foreach (Class Class in Classes) {
				foreach (Student Student in Class.Students) {
					List<Timestamp> Stamps = db.Timestamps.Where(stamp => stamp.StudentId == Student.Id).ToList();
					Student.Timestamp.TimeOut = Stamps[Stamps.Count - 1].TimeOut;
				}
			}

			return new JSONResponse() {
				Action = "Listing Active Classes",
				Data = Classes,
				Error = "N/A"
			};
		}

		[HttpGet]
		[ActionName("InActive")]
		public JSONResponse InActiveClasses() {

			return new JSONResponse() {
				Data = db.Classes.Where(cls => cls.Active == false ).ToList()
			};
		}


		[HttpGet]
		[ActionName("Get")]
		public JSONResponse GetClass(int id) {

			Class @class = db.Classes.Find(id);
			if (@class == null) {
				return new JSONResponse() {
					Action = "Get Class",
					Data = null,
					Error = "Class Not Found"
				};
			}

			return new JSONResponse() {
				Action = "",
				Data = @class,
				Error = ""
			};
		}

		[HttpPost]
		[ActionName("Create")]
		public JSONResponse CreateClass(Class @class) {

			return new JSONResponse();
		}

		[HttpPost]
		[ActionName("AddStudent")]
		public JSONResponse AddStudent(StudentClasses studentclasses) {

			Class @Class = db.Classes.Find(studentclasses.ClassId);
			Student Student = db.Students.Find(studentclasses.StudentId);

			@Class.Students.Add(Student);
			db.Entry(@Class).State = EntityState.Modified;
			db.SaveChanges();

			return new JSONResponse() {
				Action = "Student Added",
				Data = null,
				Error = "N/A"
			};
		}

		[HttpPost]
		[ActionName("Delete")]
		public JSONResponse DeleteClass(int id) {

			return new JSONResponse();
		}


    }
}