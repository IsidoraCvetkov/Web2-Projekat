namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var t1 = new Ticket { IdTicket = 1, Type = Enums.TicketType.Hourly, From = DateTime.Now, To = DateTime.Now };
            var t2 = new Ticket { IdTicket = 2, Type = Enums.TicketType.Daily, From = DateTime.Now, To = DateTime.Now };
            var t3 = new Ticket { IdTicket = 3, Type = Enums.TicketType.Monthly, From = DateTime.Now, To = DateTime.Now };
            var t4 = new Ticket { IdTicket = 4, Type = Enums.TicketType.Annual, From = DateTime.Now, To = DateTime.Now };

            context.Tickets.AddOrUpdate(a => a.IdTicket, t1);
            context.Tickets.AddOrUpdate(a => a.IdTicket, t2);
            context.Tickets.AddOrUpdate(a => a.IdTicket, t3);
            context.Tickets.AddOrUpdate(a => a.IdTicket, t4);

            context.SaveChanges();


            var s1 = new Station { IdStation = 1, Name = "Elektrovojvodina", Address = "Bulevar oslobodjenja 100", X = 45.24549443049722, Y = 19.840106964111328 };
            var s2 = new Station { IdStation = 2, Name = "Merkator", Address = "Cara lazara 51", X = 45.24300160275933, Y = 19.84081506729126 };
            var s3 = new Station { IdStation = 3, Name = "Most slobode", Address = "Bulevar oslobodjenja 143", X = 45.2406748648461, Y = 19.843926429748535 };
            var s4 = new Station { IdStation = 4, Name = "Jugodrvo", Address = "Bulevar oslobodjenja 133", X = 45.24217819017106, Y = 19.84303593635559 };
            var s5 = new Station { IdStation = 5, Name = "FTN", Address = "	Trg Dositeja Obradovica 6", X = 45.24610628967663, Y = 19.851694107055664 };
            var s6 = new Station { IdStation = 6, Name = "SPC Vojvodina", Address = "Sutjeska 2", X = 45.24695985999101, Y = 19.84529972076416 };
            var s7 = new Station { IdStation = 7, Name = "Karadjordjev Stadion", Address = "Dimitrija Tucovica 1", X = 45.24689943065613, Y = 19.842188358306885 };
            var s8 = new Station { IdStation = 8, Name = "Zeleznicka stanica", Address = "Bulevar Jase Tomica 4", X = 45.265493524101366, Y = 19.82954978942871 };

            context.Stations.AddOrUpdate(s => s.IdStation, s1);
            context.Stations.AddOrUpdate(s => s.IdStation, s2);
            context.Stations.AddOrUpdate(s => s.IdStation, s3);
            context.Stations.AddOrUpdate(s => s.IdStation, s4);
            context.Stations.AddOrUpdate(s => s.IdStation, s5);
            context.Stations.AddOrUpdate(s => s.IdStation, s6);
            context.Stations.AddOrUpdate(s => s.IdStation, s7);
            context.Stations.AddOrUpdate(s => s.IdStation, s8);

            context.SaveChanges();

            List<Station> stations = new List<Station>();
            stations.Add(s1);
            stations.Add(s2);
            stations.Add(s3);
            stations.Add(s4);
            stations.Add(s5);

            var r1 = new Line { IdLine = 1, Number = "4a", RouteType = Enums.RouteType.Town, Stations = stations };
            var r2 = new Line { IdLine = 2, Number = "56b", RouteType = Enums.RouteType.Suburban, Stations = stations };
            var r3 = new Line { IdLine = 3, Number = "7b", RouteType = Enums.RouteType.Town, Stations = stations };
            var r4 = new Line { IdLine = 4, Number = "54a", RouteType = Enums.RouteType.Suburban, Stations = stations };
            var r5 = new Line { IdLine = 5, Number = "11a", RouteType = Enums.RouteType.Town, Stations = stations };

            context.Lines.AddOrUpdate(r => r.IdLine, r1);
            context.Lines.AddOrUpdate(r => r.IdLine, r2);
            context.Lines.AddOrUpdate(r => r.IdLine, r3);
            context.Lines.AddOrUpdate(r => r.IdLine, r4);
            context.Lines.AddOrUpdate(r => r.IdLine, r5);
            context.SaveChanges();

            var sc1 = new Schadule { IdSchadule = 1, Day = Enums.DayType.Weekend, Type = Enums.RouteType.Suburban, DepartureTime = "28.8.2019 15:45:00" };
            var sc2 = new Schadule { IdSchadule = 2, Day = Enums.DayType.Workday, Type = Enums.RouteType.Town, DepartureTime = "28.8.2019 00:00:00" };
            var sc3 = new Schadule { IdSchadule = 3, Day = Enums.DayType.Weekend, Type = Enums.RouteType.Suburban, DepartureTime = "28.8.2019 16:35:00" };
            var sc4 = new Schadule { IdSchadule = 4, Day = Enums.DayType.Workday, Type = Enums.RouteType.Town, DepartureTime = "28.8.2019 08:15:00" };
            var sc5 = new Schadule { IdSchadule = 5, Day = Enums.DayType.Weekend, Type = Enums.RouteType.Town, DepartureTime = "28.8.2019 06:45:00" };

            context.Schadules.AddOrUpdate(sc => sc.IdSchadule, sc1);
            context.Schadules.AddOrUpdate(sc => sc.IdSchadule, sc2);
            context.Schadules.AddOrUpdate(sc => sc.IdSchadule, sc3);
            context.Schadules.AddOrUpdate(sc => sc.IdSchadule, sc4);
            context.Schadules.AddOrUpdate(sc => sc.IdSchadule, sc5);
            context.SaveChanges();

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "admin", BirthdayDate = DateTime.Now, UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", BirthdayDate = DateTime.Now, UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            if (!context.Users.Any(u => u.UserName == "kontroler@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "kontroler", BirthdayDate = DateTime.Now, UserName = "kontroler@yahoo.com", Email = "kontroler@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Kontroler123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Controller");
            }
        }
    }
}
