using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using ReceiptClient.ViewModels;

namespace Tests
{
    [TestClass]
    public class ControlPropertiesJsonManagerTests
    {
        private const string FrmName = "FrmTemplate";

        [TestMethod]
        public void ShouldAddControlPropertyToJson()
        {

            TextBox textBox = new TextBox
            {
                Name = "textBox1",
                Text = "Hello json!",
                Visible = true,
            };

            var manager = new ControlPropertiesJsonManager(FrmName);
            manager.AddControl(textBox.Name, nameof(textBox.Text), textBox.Text);
            manager.AddControl(textBox.Name, nameof(textBox.Visible), textBox.Visible);

            string json = manager.getJsonString();
            var hasFormName = json.Contains(FrmName);
            var hasControlName = json.Contains(textBox.Name);
            var hasTextProperty = json.Contains("Text");
            var hasVisibleProperty = json.Contains("Visible");

            Assert.IsTrue(hasFormName);
            Assert.IsTrue(hasControlName);
            Assert.IsTrue(hasTextProperty);
            Assert.IsTrue(hasVisibleProperty);
        }
    }
}
