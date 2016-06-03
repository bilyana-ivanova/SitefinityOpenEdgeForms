using Newtonsoft.Json;
using SitefinityWebApp.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace SitefinityWebApp.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms text field for OE.
    /// </summary>
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]

    public class OETextFieldController : FormFieldControllerBase<ITextFieldModel>

    {
        public OETextFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override ITextFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ITextFieldModel>(this.GetType());

                return this.model;
            }
        }

        private ITextFieldModel model;
    }
}