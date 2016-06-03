using SitefinityWebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    public class OEGridController : Controller
    {
        private string entity;

        public OEGridController()
        {
        }

        public string Entity
        {
            get
            {
                return this.entity;
            }
            set
            {
                this.entity = value;
            }
        }

        public ActionResult Index()
        {
            var model = new OEGridModel();
            model.Entity = this.Entity;
            return View("Default", model);

        }

    }
}