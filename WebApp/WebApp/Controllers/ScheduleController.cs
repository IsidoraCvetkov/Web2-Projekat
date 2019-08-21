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
    [RoutePrefix("api/Schedule")]
    public class ScheduleController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IUnitOfWork db;
        public ScheduleController(IUnitOfWork db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        [Route("PostLineSchedule")]
        // POST: api/Schedules
        [ResponseType(typeof(Schadule))]
        public IHttpActionResult PostLineSchedule([FromBody]ScheduleLine sl)
        {
            //ne radi dobro izmeniti

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sl == null)
            {
                return NotFound();
            }

            DayType dd = DayType.Workday;
            if (sl.Day == "Work day")
            {
                dd = Enums.DayType.Workday;
            }
            else if (sl.Day == "Suburban")
            {
                dd = Enums.DayType.Weekend;
            }

            Schadule d = new Schadule { Day = dd, DepartureTime = sl.Time.ToString() };
            if (d.Lines == null)
            {
                d.Lines = new List<Line>();
            }
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == sl.Number);
            line.Stations = new List<Station>();


            Schadule exist = db.Schadules.GetAll().FirstOrDefault(u => (u.DepartureTime == sl.Time.ToString() /*&&  u.IdSchadule == dd.IDDay*/));
            if (exist == null)
            {

                d.Lines.Add(line);
                d.Line = line;
                db.Schadules.Add(d);
                line.Schadules.Add(d);
                db.Lines.Update(line);
            }
            else
            {
                if (line.Schadules.FirstOrDefault(u => (u.DepartureTime == sl.Time.ToString() /*&& u.Day == dd.IDDay*/)) == null)
                {
                    exist.Lines.Add(line);
                    db.Schadules.Update(exist);
                    line.Schadules.Add(exist);
                    db.Lines.Update(line);
                }
            }

            db.Complete();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("EditLineSchedule")]
        // POST: api/Schedules
        [ResponseType(typeof(Schadule))]
        public IHttpActionResult EditLineSchedule([FromBody]ScheduleLine sl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sl == null)
            {
                return NotFound();
            }

            DayType dd = DayType.Workday;
            if (sl.Day == "Work day")
            {
                dd = Enums.DayType.Workday;
            }
            else if (sl.Day == "Weekend")
            {
                dd = Enums.DayType.Weekend;
            }

            Schadule s = new Schadule {  Day = dd, DepartureTime = sl.Time.ToString() };
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == sl.Number);

            if (s.Lines == null)
            {
                s.Lines = new List<Line>();
            }

            if(sl.Number != "")
            {
                s.Lines.Add(line);
                s.Line = line;
                s.IdLine = line.IdLine;
                s.Type = line.RouteType;
            }

            List<Schadule> schadules = db.Schadules.GetAll().ToList();
            Schadule schaduleFromBase = null;

            foreach (var sc in schadules)
            {
                if (sc.Lines != null)
                {
                    foreach (var l in sc.Lines)
                    {
                        if (l.Number == sl.Number)
                        {
                            schaduleFromBase = sc;
                        }
                    }
                }

            }

            //Schadule schaduleFromBase = db.Schadules.GetAll().FirstOrDefault(u => u.Day == dd && u. );

            if (schaduleFromBase.Lines.Count == 1)
            {
                Schadule exist = db.Schadules.GetAll().FirstOrDefault(u => (u.DepartureTime == sl.Time.ToString()/*&& u.IdSchadule == sl.IDDay*/));
                if (exist == null)
                {
                    schaduleFromBase.DepartureTime = sl.Time.ToString();
                    schaduleFromBase.Day = dd;
                    db.Schadules.Update(schaduleFromBase);

                    for (int i = 0; i < line.Schadules.Count; i++)
                    {
                        if (line.Schadules[i].IdSchadule == schaduleFromBase.IdSchadule)
                        {
                            line.Schadules[i] = schaduleFromBase;
                        }
                    }

                    db.Lines.Update(line);
                }
                else
                {
                    db.Schadules.Remove(schaduleFromBase);
                    s.Lines.Add(line);
                    db.Schadules.Update(s);
                    line.Schadules.Remove(schaduleFromBase);
                    line.Schadules.Add(s);
                    db.Lines.Update(line);

                }

            }
            else if (schaduleFromBase.Lines.Count > 1)
            {
                Schadule exist = db.Schadules.GetAll().FirstOrDefault(u => (u.DepartureTime == sl.Time.ToString() && u.Day == dd));
                if (exist == null)
                {

                    schaduleFromBase.Lines.Remove(line);
                    line.Schadules.Remove(schaduleFromBase);
                    s.Lines.Add(line);
                    db.Schadules.Add(s);
                    line.Schadules.Add(s);
                    db.Lines.Update(line);
                }
                else
                {
                    schaduleFromBase.Lines.Remove(line);
                    line.Schadules.Remove(schaduleFromBase);
                    exist.Lines.Add(line);
                    db.Schadules.Update(exist);
                    line.Schadules.Add(exist);
                    db.Lines.Update(line);
                }
            }
            int r = 1;
            r = db.Complete();
            if (r == -1)
            {
                return BadRequest("bad");
            }


            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteLineSchedule/{Number}/{Day}")]
        // DELETE: api/Schedules/5
        [ResponseType(typeof(Schadule))]
        public IHttpActionResult DeleteLineSchedule(string Number, string Day)
        {
            if (Number == null)
            {
                return NotFound();
            }

            List<Schadule> schadules = db.Schadules.GetAll().ToList();

            List<Line> lines = db.Lines.GetAll().ToList();
            Line line = null;
            Schadule schadule = null;

            DayType dd = DayType.Workday;
            if (Day == "Work day")
            {
                dd = DayType.Workday;
            }
            else if(Day == "Weekend")
            {
                dd = DayType.Weekend;
            }

            foreach (var s in schadules)
            {
                if(s.Lines != null)
                {
                    foreach (var l in s.Lines)
                    {
                        if (Number == l.Number && s.Day == dd)
                        {
                            line = db.Lines.Get(l.IdLine);
                            schadule = db.Schadules.Get(s.IdSchadule);
                        }
                    }
                }
            }
            if (schadule == null)
            {
                return NotFound();
            }

            line.Schadules.Remove(schadule);
            db.Lines.Update(line);
            schadule.Lines.Remove(line);
            db.Schadules.Update(schadule);
            db.Complete();

            return Ok(schadule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchaduleExists(int id)
        {
            return db.Schadules.GetAll().Count(e => e.IdSchadule == id) > 0;
        }
        ////private ApplicationDbContext db = new ApplicationDbContext();
        //private IUnitOfWork db;
        //public ScheduleController(IUnitOfWork db)
        //{
        //    this.db = db;
        //}
        //// GET: api/Schedules
        //public IEnumerable<Schadule> GetSchedules()
        //{
        //    return db.Schadules.GetAll();
        //}

        //// GET: api/Schedules/5
        //[ResponseType(typeof(Schadule))]
        //public IHttpActionResult GetSchedule(int id)
        //{
        //    Schadule vehicle = db.Schadules.Get(id);
        //    if (vehicle == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(vehicle);
        //}

        //// PUT: api/Schedules/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutSchedule(int id, Schadule schadule)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != schadule.IdSchadule)
        //    {
        //        return BadRequest();
        //    }
        //    db.Schadules.Update(schadule);


        //    try
        //    {
        //        db.Complete();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ScheduleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Schedules
        //[ResponseType(typeof(Schadule))]
        //public IHttpActionResult PostSchedule(Schadule schadule)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Schadules.Add(schadule);
        //    db.Complete();

        //    return CreatedAtRoute("DefaultApi", new { id = schadule.IdSchadule }, schadule);
        //}

        //// DELETE: api/Schedules/5
        //[ResponseType(typeof(Schadule))]
        //public IHttpActionResult DeleteSchedule(int id)
        //{
        //    Schadule vehicle = db.Schadules.Get(id);
        //    if (vehicle == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Schadules.Remove(vehicle);
        //    db.Complete();

        //    return Ok(vehicle);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ScheduleExists(int id)
        //{
        //    return db.Schadules.GetAll().Count(e => e.IdSchadule == id) > 0;
        //}
    }
}
