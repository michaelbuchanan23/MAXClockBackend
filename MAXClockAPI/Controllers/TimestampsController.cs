using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
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
		public JSONResponse ListTimestamps() {

			//return Timestamps List JSON
			return new JSONResponse() {
				Data = db.Timestamps.ToList()
			};
		}

		[HttpPost]
		[ActionName("Edit")]
		public JSONResponse EditTimestamp(Timestamp timestamp) {

			//validate Timestamp exists
			if(db.Timestamps.Find(timestamp.Id) == null) {
				return new JSONResponse() {
					Action = "Failed to Edit Timestamp",
					Data = null,
					Error = "No Timestamp Found"
				};
			}

			//validate Student exists
			if (db.Students.Find(timestamp.StudentId) == null) {
				return new JSONResponse() {
					Action = "Failed to Edit Timestamp",
					Data = null,
					Error = "No Student Found"
				};
			}

			//validate Class exists
			if (db.Classes.Find(timestamp.ClassId) == null) {
				return new JSONResponse() {
					Action = "Failed to Edit Timestamp",
					Data = null,
					Error = "No Class Found"
				};
			}

			//validate Timestamp dates format
			DateTime? Time = new DateTime();
			try {
				Time = timestamp.TimeIn;
				Time = timestamp.TimeOut;
			} catch (Exception e) {
				return new JSONResponse() {
					Action = "Failed to Edit Timestamp",
					Data = e.Message,
					Error = "Timestamp DateTime Format Error"
				};
			}

			//validated Timestamp model
			Timestamp Timestamp = new Timestamp() {
				Id = timestamp.Id,
				TimeIn = timestamp.TimeIn,
				TimeOut = timestamp.TimeOut,
				StudentId = timestamp.StudentId,
				ClassId = timestamp.ClassId
			};

			//modify timestamp in DB
			db.Entry(Timestamp).State = EntityState.Modified;
			db.SaveChanges();

			//return Timestamp JSON
			return new JSONResponse() {
				Action = "Timestamp Edited",
				Data = Timestamp,
				Error = "N/A"
			};
		}

		[HttpPost]
		[ActionName("Create")]
		public JSONResponse CreateTimestamp(Timestamp timestamp) {

			//validate Student exists
			if(db.Students.Find(timestamp.StudentId) == null) {
				return new JSONResponse() {
					Action = "Failed to Create Timestamp",
					Data = null,
					Error = "No Student Found"
				};
			}

			//validate Class exists
			if(db.Classes.Find(timestamp.ClassId) == null) {
				return new JSONResponse() {
					Action = "Failed to Create Timestamp",
					Data = null,
					Error = "No Class Found"
				};
			}

			//validate Timestamp dates format
			DateTime? Time = new DateTime();
			try {
				Time = timestamp.TimeIn;
				Time = timestamp.TimeOut;
			}catch (Exception e) {
				return new JSONResponse() {
					Action = "Failed to Create Timestamp",
					Data = e.Message,
					Error = "Timestamp DateTime Format Error"
				};
			}

			//validated Timestamp model
			Timestamp Timestamp = new Timestamp();
			Timestamp.TimeIn = timestamp.TimeIn;
			Timestamp.TimeOut = timestamp.TimeOut;
			Timestamp.StudentId = timestamp.StudentId;
			Timestamp.ClassId = timestamp.ClassId;

			//Add timestamp to DB
			db.Entry(Timestamp).State = EntityState.Added;
			db.SaveChanges();

			//return Timestamp JSON
			return new JSONResponse() {
				Action = "Timestamp Created",
				Data = Timestamp,
				Error = "N/A"
			};
		}

		[HttpPost]
		[ActionName("Delete")]
		public JSONResponse DeleteTimestamp(int id) {

			//find and delete Timestamp
			db.Entry(db.Timestamps.Find(id)).State = EntityState.Deleted;
			return new JSONResponse() {
				Action = "Timestamp Deleted",
				Data = null,
				Error = null
			};
		}

	}
}