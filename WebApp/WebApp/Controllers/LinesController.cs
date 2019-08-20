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

        //**

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
                foreach (Station s in linePlus.Stations)
                {
                    var station = db.Stations.GetAll().FirstOrDefault(u => u.Name == s.Name);
                    line.Stations.Add(station);
                    db.Stations.Update(station);
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
