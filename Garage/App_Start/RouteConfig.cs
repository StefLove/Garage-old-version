using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Garage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HemsidaOm",
                url: "Hem/Om",
                defaults: new { controller = "Home", action = "About" /*, lang = "sw"*/ },
                namespaces: new string[] { "Garage.Controllers" }
            );
            routes.MapRoute(
                name: "HemsidaKontakt",
                url: "Hem/Kontakt",
                defaults: new { controller = "Home", action = "Contact" /*, lang = "sw"*/ },
                namespaces: new string[] { "Garage.Controllers" }
            );
            routes.MapRoute(
                name: "Hemsida",
                url: "Hem/{action}",
                defaults: new { controller = "Home", action = "Index" /*, lang = "sw"*/ },
                namespaces: new string[] { "Garage.Controllers" }
            );

            routes.MapRoute(
               name: "MedlemsvyRegistrera",
               url: "Medlemmar/Registrera",
               defaults: new { controller = "Members", action = "Create" /*, lang = "sw"*/ },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "MedlemsvyAvregistrera",
               url: "Medlemmar/Avregistrera/{id}",
               defaults: new { controller = "Members", action = "Delete", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
                name: "MedlemsvyDetaljer",
                url: "Medlemmar/Detaljer/{id}",
                defaults: new { controller = "Members", action = "Details", id = UrlParameter.Optional /*, lang = "sw"*/ },
                //constraints: new { id = @"\d+" },
                namespaces: new string[] { "Garage.Controllers" }
            );
            routes.MapRoute(
               name: "MedlemsvyEditera",
               url: "Medlemmar/Ändra/{id}",
               defaults: new { controller = "Members", action = "Edit", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "Medlemsvy",
               url: "Medlemmar/{action}/{id}",
               defaults: new { controller = "Members", action = "Index", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );

            routes.MapRoute(
               name: "FordonsvyCheckaUt",
               url: "Fordon/CheckaUt/{id}",
               defaults: new { controller = "Vehicles", action = "Delete", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonsvyDetaljeratIndex",
               url: "Fordon/DetaljeratIndex/{id}",
               defaults: new { controller = "Vehicles", action = "DetailedIndex", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonsvyDetaljer",
               url: "Fordon/Detaljer/{id}",
               defaults: new { controller = "Vehicles", action = "Details", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonsvyÄndra",
               url: "Fordon/Ändra/{id}",
               defaults: new { controller = "Vehicles", action = "Edit", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonsvyParkera",
               url: "Fordon/Parkera",
               defaults: new { controller = "Vehicles", action = "Park" /*, lang = "sw"*/ },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonsvyKvitto",
               url: "Fordon/Kvitto/{id}",
               defaults: new { controller = "Vehicles", action = "Receipt", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
              name: "Fordonsvy",
              url: "Fordon/{action}",
              defaults: new { controller = "Vehicles", action = "Index" /*, lang = "sw"*/ },
              namespaces: new string[] { "Garage.Controllers" }
          );

            routes.MapRoute(
               name: "FordonstypvySkapa",
               url: "Fordonstyper/Skapa/{id}",
               defaults: new { controller = "VehicleTypes", action = "Create", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonstypvyTaBort",
               url: "Fordonstyper/TaBort/{id}",
               defaults: new { controller = "VehicleTypes", action = "Delete", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonstypvyDetaljer",
               url: "Fordonstyper/Detaljer/{id}",
               defaults: new { controller = "VehicleTypes", action = "Details", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "FordonstypvyÄndra",
               url: "Fordonstyper/Ändra/{id}",
               defaults: new { controller = "VehicleTypes", action = "Edit", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );
            routes.MapRoute(
               name: "Fordonstypvy",
               url: "Fordonstyper/{action}/{id}",
               defaults: new { controller = "VehicleTypes", action = "Index", id = UrlParameter.Optional /*, lang = "sw"*/ },
               //constraints: new { id = @"\d+" },
               namespaces: new string[] { "Garage.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Garage.Controllers" }
            );
        }
    }
}
