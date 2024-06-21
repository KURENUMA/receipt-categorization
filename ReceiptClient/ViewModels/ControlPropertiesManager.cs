using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Windows.Forms.Control;

namespace ReceiptClient.ViewModels
{
    public class ControlPropertiesManager
    {
        private ControlCollection controls;
        private Dictionary<string, Dictionary<string, string>> controlPropertiesPairs = new Dictionary<string, Dictionary<string, string>>();
        

        public ControlPropertiesManager(ControlCollection controls)
        {
            this.controls = controls;
        }

        public ControlPropertiesManager(string formName, ControlCollection controls1, string jsonStr)
        {
            this.controls = controls1;
            var json = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(jsonStr);
            if(json.ContainsKey(formName))
            {
                controlPropertiesPairs = json[formName];
            }
        }

        public void SetControlPropertiesFromJsonString(string controlName, Dictionary<string, string> propertiesMap)
        {
            var control = controls[controlName];

            if (control != null)
            {
                foreach (var prop in propertiesMap)
                {
                    
                    var propertyInfo = control.GetType().GetProperty(prop.Key);
                    if(propertyInfo != null)
                    { 
                        var convertedValue = JsonConvert.DeserializeObject(prop.Value, propertyInfo.PropertyType);
                        propertyInfo.SetValue(control, convertedValue);
                    }
                }

            }
        }

        public void SetProperties()
        {
            foreach(var control in controlPropertiesPairs)
            {
                SetControlPropertiesFromJsonString(control.Key, control.Value);
            }
        }
    }
}
