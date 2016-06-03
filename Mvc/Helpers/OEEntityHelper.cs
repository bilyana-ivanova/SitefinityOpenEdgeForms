using Newtonsoft.Json;
using SitefinityWebApp.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
namespace SitefinityWebApp.Mvc.Helpers
{
    public static class OEEntityHelper
    {
        public static string serviceDefinition = "http://<host>:<port>/<ServiceName>/rest/static/<ServicePath>.json";//host:port/ServiceName/rest/static/ServicePath.json
        public static string serviceURL = "http://<host>:<port>/<ServiceName>/rest/<ServicePath>/"; //host:port/ServiceName/rest/ServicePath
        public static IEnumerable<SitefinityWebApp.OEEntity.SingleProperty> GetProperties(Guid controlId) 
        {
            var objData = PageManager.GetManager().GetControl<ObjectData>(controlId); //find the currently edited control
            var controls = ((Telerik.Sitefinity.Forms.Model.FormDraftControl)objData).Form.Controls; //find the form for the control
            var selector = controls.Where(c => c.Caption.Equals("OE EntitySelector")).FirstOrDefault(); //find the entity selector control on the form
            var name = selector.Properties.Where(p => p.Name.Equals("Settings")).FirstOrDefault().ChildProperties.Where(p => p.Name.Equals("Entity")).FirstOrDefault().Value;//read Entity property
            List<SitefinityWebApp.OEEntity.SingleProperty> entity = GetEntities().Where(e => e.Key.Equals(name)).FirstOrDefault().Value;
            return entity == null ? new List<SitefinityWebApp.OEEntity.SingleProperty>() : entity
                .OrderBy(e=>e.title).ToList();
        }

        public static Dictionary<string, List<SitefinityWebApp.OEEntity.SingleProperty>> GetEntities()
        {
            entities = GetEntitiesInternal();
            return entities;
        }
        
        private static Dictionary<string, List<SitefinityWebApp.OEEntity.SingleProperty>> GetEntitiesInternal()
        {
            var entities = new Dictionary<string, List<SitefinityWebApp.OEEntity.SingleProperty>>();
            var entitiesMetadata = new List<SitefinityWebApp.OEEntity.Metadata>();
                
            using (var client = new WebClient())
                {
                    var jsonString = client.DownloadString(serviceDefinition);
                    var service = JsonConvert.DeserializeObject<SitefinityWebApp.OEEntity.Rootobject>(jsonString);
                    int i = 0;
                    foreach (var resource in service.services[0].resources) {
                        var properties = new List<SitefinityWebApp.OEEntity.SingleProperty>();
                        try
                        {
                            Regex rgx = new Regex(@"{[^{}]*}", RegexOptions.IgnoreCase);
                            MatchCollection matches = rgx.Matches(resource.schema.properties.ToString().Split(new string[] { "properties\": {" }, StringSplitOptions.None)[2]);
                            if (matches.Count > 0)
                            {
                                foreach (Match match in matches)
                                {
                                    var prop = JsonConvert.DeserializeObject<SitefinityWebApp.OEEntity.SingleProperty>(match.Value);
                                    if (!prop.title.IsNullOrEmpty()) properties.Add(prop);
                                }
                            }
                        }
                        catch (Exception ex){ 
                        }
                        
                        entities.Add(resource.path.Replace("/",""), properties);

                        var entityMetadata = new SitefinityWebApp.OEEntity.Metadata();
                        var objectName = resource.path.Replace("/", "").Substring(0, resource.path.Replace("/", "").Length - 1);
                        entityMetadata.entityName = resource.path.Replace("/", "");
                        entityMetadata.dsName = "ds" + objectName;
                        entityMetadata.ttName = "tt" + objectName;
                        try
                        {
                            entityMetadata.primaryKey = resource.schema.properties.ToString().Split(new string[] { "properties\": {" }, StringSplitOptions.None)[1].Split(new string[] { "primaryKey" }, StringSplitOptions.None)[1].Split(']')[0].Split('"')[2];
                        }
                        catch (Exception ex) { 
                        }
                        

                        entitiesMetadata.Add(entityMetadata);
                    }
                
                
                }
            
            entitiesMeta = entitiesMetadata;
            return entities;
        }

        public static SitefinityWebApp.OEEntity.Metadata GetEntitiesMetadata(string entityName) {
            GetEntitiesInternal();
            return entitiesMeta.Where(e => e.entityName.Equals(entityName)).FirstOrDefault();
        }

        private static List<SitefinityWebApp.OEEntity.Metadata> entitiesMeta;
        private static Dictionary<string, List<SitefinityWebApp.OEEntity.SingleProperty>> entities;
        
    }
}