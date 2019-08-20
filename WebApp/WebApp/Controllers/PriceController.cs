using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;
using static WebApp.Models.Enums;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Price")]
    public class PriceController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        private DbContext _db;

        public PriceController(IUnitOfWork unitOfWork, DbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public Price getLatestPrice(int ticket)
        {
            //if (ticket == null)
            //{
            //    return null;
            //}
            List<PriceList> priceLists = _unitOfWork.PriceLists.GetAll().OrderByDescending(u => u.StartDate).ToList();
            TicketType idType = _unitOfWork.Tickets.GetAll().FirstOrDefault(u => u.Type == (TicketType)ticket).Type;
            foreach (PriceList pl in priceLists)
            {
                foreach (Price p in pl.Prices)
                {
                    if (p.Type == idType)
                    {
                        return p;
                    }
                }
            }

            return null;
        }

        //Mora se izmeniti

        //[AllowAnonymous]
        //[Route("GetOnePrice")]
        //public double GetOnePrice(int ticket, string user)
        //{
        //    if (/*ticket == null ||*/ user == null)
        //    {
        //        return 0;
        //    }

        //    var userr = _unitOfWork.TypesOfUser.GetAll().FirstOrDefault(u => u.typeOfUser == user);
        //    double pretenge = 1; //popust
        //    double popust = (double)userr.Percentage;


        //    var tickett = _unitOfWork.Tickets.GetAll().FirstOrDefault(u => u.Type == (TicketType)ticket);

        //    Price pricee = getLatestPrice(ticket);// _unitOfWork.Prices.GetAll().FirstOrDefault(u => u.IDtypeOfTicket == tickett.IDtypeOfTicket);
        //    if (pricee == null)
        //        return 0;

        //    if (userr.Percentage != 0)
        //        pretenge = popust / 100;


        //    return pricee.Value * pretenge; //popust


        //}

        //[Authorize(Roles = "AppUser")]
        //[Route("GetPrice")]
        //public double GetPrice(int ticket, string email)
        //{
        //    double pretenge = 1; //popust

        //    var email1 = Request.GetOwinContext().Authentication.User.Identity.Name;
        //    ApplicationUserManager cont = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    List<ApplicationUser> app = cont.Users.ToList();

        //    ApplicationUser apUs = app.Where(u => u.Email == email).FirstOrDefault();

        //    var tickett = _unitOfWork.Tickets.GetAll().FirstOrDefault(u => u.Type == (TicketType)ticket); //koja karta

        //    var pricee = getLatestPrice(ticket);// _unitOfWork.Prices.GetAll().FirstOrDefault(u => u.IDtypeOfTicket == tickett.IDtypeOfTicket);//koliko kosta
        //    var userr = _unitOfWork.TypesOfUser.GetAll().FirstOrDefault(u => u.IDtypeOfUser == apUs.IDtypeOfUser);
        //    double popust = (double)userr.Percentage;

        //    pretenge = popust / 100;
        //    return pricee.Value * pretenge;
        //}


    }
}
