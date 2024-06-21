using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ReceiptClient.ViewModels
{
    public class ControlPropertiesJsonManager
    {
        private string formName;
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> jsonFormMap = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        public ControlPropertiesJsonManager(string v)
        {
            this.formName = v;
            jsonFormMap[v] = new Dictionary<string, Dictionary<string, string>>();
        }

        public void AddControl(string controlName, string propName, object propValue)
        {
            Dictionary<string, Dictionary<string, string>> controlsMap = jsonFormMap[formName];
            
            if(!controlsMap.ContainsKey(controlName))
            {
                controlsMap[controlName] = new Dictionary<string, string>();
            }
            var propsMap = controlsMap[controlName];
            
            string propJsonValue = JsonConvert.SerializeObject(propValue);
            propsMap[propName] = propJsonValue;
            
        }

        public string getJsonString()
        {
            return JsonConvert.SerializeObject(jsonFormMap);
        }
    }
}
