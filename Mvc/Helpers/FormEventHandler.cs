using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Events;

namespace SitefinityWebApp.Mvc.Helpers
{
    public static class FormEventHandler
    {
        public static void Handler(FormSavedEvent eventInfo)
        {
            var formId = eventInfo.FormId;
            var controls = eventInfo.Controls;
            var selectors = FormsManager.GetManager().GetForm(formId).Controls.Where(c => c.Caption.Equals("OE EntitySelector"));
            if (selectors.Any()) {
                var entityName = selectors.FirstOrDefault().Properties.Where(p => p.Name.Equals("Settings")).FirstOrDefault().ChildProperties.Where(p => p.Name.Equals("Entity")).FirstOrDefault().Value;
                var entityMeta = OEEntityHelper.GetEntitiesMetadata(entityName);
               

                //-----------------If primaryKey not entered explicitly in the form, find max primaryKey and use maxValue + 1
                var client = new RestClient(OEEntityHelper.serviceURL);
                var request = new RestRequest("/" + entityName, Method.GET);
                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                int maxValue = 0;
                var keys = content.Split(new string[] { entityMeta.primaryKey }, StringSplitOptions.None);
                for (int i=1; i<keys.Count(); i++ ) { 
                    try{
                        var value = int.Parse(keys[i].Split(',')[0].Split(':')[1]);
                        if(value>maxValue){
                            maxValue = value;
                        }
                    }catch(Exception ex){
                    }
                    
                }
                //-----------------
                WebRequest wr = WebRequest.Create(OEEntityHelper.serviceURL + entityName);
                wr.Method = "POST";
                string postData = "{\"" + entityMeta.dsName + "\":{\"" + entityMeta.ttName + "\":[{\"" + entityMeta.primaryKey + "\":" + (maxValue + 1);
                foreach(var control in controls){
                    if (!control.FieldName.StartsWith("Form")) { 
                        postData +=",\""+control.FieldName+"\":\""+control.Value+"\"";
                    }
                }
                //add each property to body : ",\"Name\":\"Test4\"";
                postData += "}]}}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                wr.ContentType = "application/json";
                wr.ContentLength = byteArray.Length;
                Stream dataStream = wr.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse wresponse = wr.GetResponse();
                
            }
            
        }
    }
}