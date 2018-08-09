using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using MAXClockAPI.Models;
using MAXClockAPI.Utilities;

namespace MAXClockAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class StudentsController : ApiController
    {
        private MAXClockAPIContext db = new MAXClockAPIContext();

		[HttpGet]
		[ActionName("List")]
		public JSONResponse ListStudents() {

			//find all Students
			List<Student> Students = db.Students.ToList();

			//return Student List in JSON
			return new JSONResponse() {
				Action = "Listing all Students",
				Data = Students,
				Error = "N/A"
			};
		}

		[HttpGet]
		[ActionName("Get")]
		public JSONResponse GetStudent(int id) {

			//find Student by Id
			Student Student = db.Students.Find(id);

			//return Student in JSON
			return new JSONResponse() {
				Action = "Student Found",
				Data = Student,
				Error = "N/A"
			};
		}

		[HttpGet]
		[ActionName("GetStamps")]
		public JSONResponse GetStudentStamps(int id) {

			//find all Timestamps with Student Id
			List<Timestamp> Timestamps = db.Timestamps.Where(stamp => stamp.StudentId == id).ToList();

			//return JSON with Timestamp array
			return new JSONResponse() {
				Action = "All Student Timestamps",
				Data = Timestamps,
				Error = "N/A"
			};
		}

		[HttpGet]
		[ActionName("GetClassStamps")]
		public JSONResponse GetClassStamps(Timestamp timestamp) {

			//find all Timestamps with Student Id and Class Id
			List<Timestamp> Timestamps = StudentStamps(timestamp.StudentId, timestamp.ClassId);

			//return JSON with Timestamp Array
			return new JSONResponse() {
				Action = "All Student Timestamps For Class",
				Data = Timestamps,
				Error = "N/A"
			};
		}

		[HttpPost]
		[ActionName("Create")]
		public JSONResponse CreateStudent(Student student) {

			//create new Student
			Student Student = new Student() {
				Firstname = student.Firstname,
				Lastname = student.Lastname,
				PIN = student.PIN,
				Status = false,
				Timestamp = new Timestamp() { }
			};

			//add Student to DB
			db.Entry(Student).State = EntityState.Added;
			db.SaveChanges();

			//set Timestamp StudentId
			Student.Timestamp.StudentId = Student.Id;

			//create new Timestamp for Student
			db.Entry(Student.Timestamp).State = EntityState.Added;
			db.SaveChanges();

			//retrieve new Timestamp
			Timestamp Stamp = db.Timestamps.Where(stamp => stamp.StudentId == Student.Id).First();
			Student.Timestamp = Stamp;

			//modify Student Timestamp Id DB
			db.Entry(Student).State = EntityState.Modified;
			db.SaveChanges();

			//sanitize Student PIN
			Student.PIN = 0;

			//return action confirmation
			return new JSONResponse() {
				Action = "Student Created",
				Data = Student,
				Error = "N/A"
			};
		}

		[HttpPost]
		[ActionName("Delete")]
		public JSONResponse DeleteStudent(int id) {

			//find Student
			Student Student = db.Students.Find(id);

			//delete Student
			db.Entry(Student).State = EntityState.Deleted;
			db.SaveChanges();

			//return action confirmation
			return new JSONResponse() {
				Action = "Student Deleted",
				Data = null,
				Error = "N/A"
			};
		}

		[HttpPost]
		[ActionName("Stamp")]
		public JSONResponse Stamp(Student student) {

			Timestamp timestamp = student.Timestamp;
			//get Student
			Student Student = db.Students.Find(student.Id);

			//check PIN
			if (student.PIN != Student.PIN) {
				return new JSONResponse() {
					Action = "Failed to Stamp",
					Data = null,
					Error = $"Incorrect PIN"
				};
			}

			//get current Timestamp
			Timestamp Stamp = Student.Timestamp;

			//check status
			if (Student.Status) {
				//current Timestamp clocked in

				//toggle Student status
				Student.Status = !Student.Status;

				//update Student Timestamp / clock out /create new Timestamp
				Stamp.TimeOut = DateTime.Now;
				Student.Timestamp = new Timestamp() {
					StudentId = Student.Id,
					ClassId = timestamp.ClassId
				};

				//update Student in DB
				db.Entry(Student).State = EntityState.Modified;
				db.SaveChanges();

				//update Timestamp in DB
				db.Entry(Stamp).State = EntityState.Modified;
				db.SaveChanges();

				//sanitize Student PIN
				Student.PIN = 0;

				//update Timeout for JSON
				Student.Timestamp.TimeOut = Stamp.TimeOut;

				//return Student in JSON
				return new JSONResponse() {
					Action = "Student Stamped Out",
					Data = Student,
					Error = "N/A"
				};
			}
			//current Timestamp clocked out

			//update Student Status
			Student.Status = !Student.Status;

			//update Student Timestamp
			Student.Timestamp.TimeIn = DateTime.Now;

			//update Student in DB
			db.Entry(Student).State = EntityState.Modified;
			db.SaveChanges();

			//sanitize Student PIN
			Student.PIN = 0;

			//return Student in JSON
			return new JSONResponse() {
				Action = "Student Stamped In",
				Data = Student,
				Error = "N/A"
			};
		}

		public List<Timestamp> StudentStamps(int studentid, int classid) {

			//find all Timestamps for student in class
			return db.Timestamps.Where(
				stamp => studentid == stamp.StudentId
				&& classid == stamp.ClassId).ToList();
		}
	}
}