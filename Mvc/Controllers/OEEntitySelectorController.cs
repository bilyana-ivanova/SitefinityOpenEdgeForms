//using SitefinityWebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace SitefinityWebApp.Mvc.Controllers
{
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    public class OEEntitySelectorController : Controller
    {
        private string entity;

        public OEEntitySelectorController() {
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
    }
}