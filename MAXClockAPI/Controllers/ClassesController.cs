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

			return new JSONResponse() {
				Data = db.Classes.ToList()
			};
		}

		[HttpGet]
		[ActionName("Active")]
		public JSONResponse ActiveClasses() {

			return new JSONResponse() {
				Data = db.Classes.Where(cls => cls.Active == true).ToList()
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

			return new JSONResponse();
		}

		[HttpPost]
		[ActionName("Create")]
		public JSONResponse CreateClass(Class @class) {

			return new JSONResponse();
		}

		[HttpPost]
		[ActionName("Delete")]
		public JSONResponse DeleteClass(int id) {

			return new JSONResponse();
		}


    }
}