using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        DbSet<Location> Locations { get; set; }
        DbSet<Line> Lines { get; set; }
        DbSet<Price> Prices { get; set; }
        DbSet<PriceList> PriceLists { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Schadule> Schadules { get; set; }
        DbSet<Station> Stations { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}