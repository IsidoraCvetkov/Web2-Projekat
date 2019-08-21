using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Dto;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;
using static WebApp.Models.Enums;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Line")]
    public class LinesController : ApiController
    {
        private IUnitOfWork db;

        public LinesController()
        {

        }

        public LinesController(IUnitOfWork db)
        {

            //var Line = db.Lines.GetAll().Where(l => l.Number == "11").FirstOrDefault();

            //var s1 = db.Stations.GetAll().Where(s => s.Name == "B").FirstOrDefault();
            //var s2 = db.Stations.GetAll().Where(s => s.Name == "mapica").FirstOrDefault();
            //var s3 = db.Stations.GetAll().Where(s => s.Name == "maza").FirstOrDefault();
            //s1.Lines = new List<Line>();
            //s1.Lines.Add(Line);
            //db.Stations.Update(s1);

            //db.Complete();

            this.db = db;
        }

        [AllowAnonymous]
        [Route("GetLines")]
        public IEnumerable<LinePlus> GetLines()
        {
            List<Line> lines = db.Lines.GetAll().ToList();
            List<LinePlus> ret = new List<LinePlus>();

            foreach (Line l in lines)
            {
                RouteType type = l.RouteType; //db.TypesOfLine.GetAll().FirstOrDefault(u => u.IDtypeOfLine == l.IDtypeOfLine).typeOfLine;
                LinePlus lp = new LinePlus() { Number = l.Number, IDtypeOfLine = 0, TypeOfLine = type.ToString(), Stations = l.Stations};
                ret.Add(lp);
            }

            return ret;
        }

        [AllowAnonymous]
        [Route("GetScheduleLines")]
        public IEnumerable<Line> GetScheduleLines(string typeOfLine)
        {

            if (typeOfLine == null)
            {
                var type = db.Lines.GetAll().FirstOrDefault(u => u.RouteType == Enums.RouteType.Town);
                return db.Lines.GetAll().Where(u => u.RouteType == type.RouteType);
            }
            else
            {
                //var type = db.Lines.GetAll().FirstOrDefault(u => u.RouteType == (RouteType)typeOfLine);
                RouteType type = Enums.RouteType.Town;
                if (typeOfLine == "Town")
                {
                    type = Enums.RouteType.Town;
                }
                else if (typeOfLine == "Suburban")
                {
                    type = Enums.RouteType.Suburban;
                }
                return db.Lines.GetAll().Where(u => u.RouteType == type);
            }
        }


        //testirati
        [AllowAnonymous]
        [Route("GetSchedule")]
        public string GetSchedule(string typeOfLine, string typeOfDay, string Number)
        {

            if (typeOfLine == null || typeOfDay == null || Number == null)
            {
                return "error";
            }

            RouteType type = Enums.RouteType.Town;//db.Lines.GetAll().FirstOrDefault(u => u.RouteType == typeOfLine);
            if (typeOfLine == "Town")
            {
                type = Enums.RouteType.Town;
            }
            else if (typeOfLine == "Suburban")
            {
                type = Enums.RouteType.Suburban;
            }

            DayType day = DayType.Workday; //= db.Days.GetAll().FirstOrDefault(u => u.KindOfDay == typeOfDay);
            if (typeOfDay == "Work day")
            {
                day = Enums.DayType.Workday;
            }
            else if (typeOfDay == "Suburban")
            {
                day = Enums.DayType.Weekend;
            }


            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == Number);

            string dep = "";
            int i = 0;
            foreach (Schadule s in line.Schadules)
            {
                if (s.Day == day)
                {
                    i++;
                    //dep += s.Time.Hour.ToString() + ":" + s.Time.Minute.ToString() + " ";
                    dep = s.DepartureTime;
                }
            }
            if (line.Schadules.Count > 0)
            {
                if (dep.Length != 0)
                    dep = dep.Substring(0, dep.Length - 1);
            }
            List<Schadule> sch = new List<Schadule>();
            sch = db.Schadules.GetAll().Where(u => u.Day == day).ToList();

            if (dep == "")
            {
                dep = "empty";
            }

            return dep;
        }


        [Authorize(Roles = "Admin")]
        [Route("GetScheduleAdmin")]
        public IEnumerable<ScheduleLine> GetScheduleAdmin()
        {
            List<ScheduleLine> schedule = new List<ScheduleLine>();
            var lines = db.Lines.GetAll();
            foreach (var line in lines)
            {
                foreach (var dep in line.Schadules)
                {
                    //  Day day = db.Days.GetAll().FirstOrDefault(u => u.IDDay == dep.IDDay);

                    ScheduleLine sl = new ScheduleLine();
                    sl.Number = line.Number;
                    sl.Time = DateTime.Parse(dep.DepartureTime);
                    if (dep.Day == DayType.Weekend)
                        sl.Day = "Weekend";
                    else if (true)
                        sl.Day = "Work day";
                    schedule.Add(sl);
                }
            }

            return schedule;
        }

        //// GET: api/Lines/5
        [ResponseType(typeof(Line))]
        public IHttpActionResult GetLine(string id)
        {
            List<Line> lines = db.Lines.GetAll().ToList();
            Line line = null;

            foreach (var l in lines)
            {
                if (id == l.Number)
                {
                    line = db.Lines.Get(l.IdLine);
                }
            }

            if (line == null)
            {
                return NotFound();
            }

            return Ok(line);
        }

        [Authorize(Roles = "Admin")]
        [Route("AddLine")]
        public string AddLine(LinePlus linePlus)
        {
            Line line = db.Lines.GetAll().FirstOrDefault(u => u.Number == linePlus.Number);


            if (line != null)
            {
                return "Line with that number already exist";
            }
            else
            {
                RouteType id = RouteType.Town;

                if (linePlus.TypeOfLine == "Town")
                {
                    id = Enums.RouteType.Town;
                }
                else if (linePlus.TypeOfLine == "Suburban")
                {
                    id = Enums.RouteType.Suburban;
                }

                //RouteType id = Enums.RouteType.Suburban; //= db.Lines.GetAll().FirstOrDefault(u => u.RouteType == linePlus.TypeOfLine).RouteType;
                Line newLine = new Line() { Number = linePlus.Number, RouteType = id };
                newLine.Stations = new List<Station>();
                foreach (Station s in linePlus.Stations)
                {
                    var station = db.Stations.GetAll().FirstOrDefault(u => u.Name == s.Name);
                    newLine.Stations.Add(station);
                    db.Stations.Update(station);
                }

                db.Lines.Add(newLine);
                try
                {
                    db.Complete();
                }
                catch (Exception e)
                {

                }
            }

            return "ok";
        }

        [Authorize(Roles = "Admin")]
        [Route("EditLine")]
        public string EditLine(LinePlus linePlus)
        {
            int result = 1;
            Line line = db.Lines.GetAll().FirstOrDefault(u => u.Number == linePlus.Number);

            if (line == null)
            {
                return "Line can't be changed";
            }
            else
            {
                if (line.Number != linePlus.Number)
                {
                    return "Data was modified in meantime, please try again!";
                }

                //int id = //db.TypesOfLine.GetAll().FirstOrDefault(u => u.typeOfLine == linePlus.TypeOfLine).IDtypeOfLine;

                //line.IdLine = id;

                line.Stations = new List<Station>();
                if (linePlus.Stations != null)
                {
                    foreach (Station s in linePlus.Stations)
                    {
                        var station = db.Stations.GetAll().FirstOrDefault(u => u.Name == s.Name);
                        line.Stations.Add(station);
                        db.Stations.Update(station);
                    }
                }

                if (linePlus.TypeOfLine == "Town")
                {
                    line.RouteType = Enums.RouteType.Town;
                }
                else if(linePlus.TypeOfLine == "Suburban")
                {
                    line.RouteType = Enums.RouteType.Suburban;
                }

                db.Lines.Update(line);
                result = db.Complete();
                if (result == 0)
                {
                    return "conflict";
                }
                else if (result == -1)
                {
                    return "Data was modified in meantime, please try again!";
                }
            }

            return "ok";
        }

        [Authorize(Roles = "Admin")]
        [Route("PostLineSchedule")]
        // POST: api/Lines
        // [ResponseType(typeof(Line))]
        public IHttpActionResult PostLine([FromBody] ScheduleLine sl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DayType idd = DayType.Workday;

            if (sl.Day == "Work day")
                idd = DayType.Workday; 
            else
                idd = DayType.Weekend; 

            Schadule s = new Schadule { Day = idd, DepartureTime = sl.Time.ToString() };
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == sl.Number);
            s.Lines.Add(line);

            db.Schadules.Add(s);

            line.Schadules.Add(s);
            db.Lines.Update(line);

            try
            {
                db.Complete();
            }
            catch (DbUpdateException)
            {

            }

            return CreatedAtRoute("DefaultApi", new { id = line.Number }, line);
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteLine/{Number}")]
        // DELETE: api/Lines/5
        [ResponseType(typeof(Line))]
        public IHttpActionResult DeleteLine(string Number)
        {
            //Line line = db.Lines.GetAll().FirstOrDefault(u => u.Number == linePlus.Number);

            //Line line = db.Lines.Get(Number);
            List<Line> lines = db.Lines.GetAll().ToList();
            Line line = null;

            foreach (var l in lines)
            {
                if (Number == l.Number)
                {
                    line = db.Lines.Get(l.IdLine);
                }
            }
            if (line == null)
            {
                return NotFound();
            }

            db.Lines.Remove(line);
            db.Complete();

            return Ok(line);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LineExists(string id)
        {
            return db.Lines.GetAll().Count(e => e.Number == id) > 0;

        }
    }
}
