using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ReceiptClient.ViewModels;

namespace Tests
{
    [TestClass]
    public class ControlPropertiesManagerTests
    {
        private Form form;
        [TestInitialize]
        public void Initialize()
        {
            form = new Form()
            {
                Name = "FrmTemplate",
                Text = "FrmTemplate"
            };


            TextBox textBox = new TextBox
            {
                Name = "textBox1",
            };
            Button button = new Button
            {
                Name = "button1",
            };
            button.Location = new Point(0, 0);

            form.Controls.Add(textBox);
            form.Controls.Add(button);
        }
        
        [TestMethod]
        public void WhenValidJsonProvided_PropertiesSetCorrectly()
        {
            // Arrange
            var textBox = form.Controls["textBox1"];

            ControlPropertiesManager manager = new ControlPropertiesManager(form.Controls);
            string properties = @"
                    {
                        ""Text"": ""\""Hello, JSON!\"""",
                        ""ForeColor"": ""\""Red\"""",
                        ""BackColor"": ""\""Green\""""
                    }
            ";


            // Act
            var propertiesMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(properties);
            manager.SetControlPropertiesFromJsonString("textBox1", propertiesMap);
            
            // Assert
            Assert.AreEqual("Hello, JSON!", textBox.Text);
            Assert.AreEqual(Color.Red, textBox.ForeColor);
            Assert.AreEqual(Color.Green, textBox.BackColor);

        }

        [TestMethod] 
        public void ShouldBindFormControls_ifFormFoundByNameInJsonObject()
        {
            var textBox = form.Controls["textBox1"];
            var button = form.Controls["button1"];
            string json = @"
            {
                ""FrmTemplate"": {
                    ""textBox1"": {
                        ""Text"": ""\""Hello, JSON!\"""",
                        ""ForeColor"": ""\""Red\"""",
                        ""BackColor"": ""\""Green\""""
                    },
                    ""button1"": {
                        ""Text"": ""\""Click me!\"""",
                        ""Enabled"": ""true""
                    }
                },
            }";
            ControlPropertiesManager manager = new ControlPropertiesManager(form.Name ,form.Controls, json);
            manager.SetProperties();

            // Assert
            Assert.AreEqual("Hello, JSON!", textBox.Text);
            Assert.AreEqual(Color.Red, textBox.ForeColor);
            Assert.AreEqual(Color.Green, textBox.BackColor);

            Assert.AreEqual("Click me!", button.Text);
            Assert.AreEqual(true, button.Enabled);

        }

    }
}
