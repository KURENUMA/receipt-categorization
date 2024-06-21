using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ReceiptClient;
using ReceiptClient.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace Tests
{
    [TestClass]
    public class GridOrderManagerTests
    {
        [TestMethod]
        public void ShouldMutateConfig()
        {
            

            var manager = new GridOrderManager("test.json", ProjectDatumHelpers.ColumnConfigs);
            manager.SetSelectedPattern("PatternA");
            manager.SaveToFile();
            Assert.AreEqual("PatternA", manager.SelectedPattern);

            manager.SetSelectedPattern("PatternB");
            Assert.AreEqual("PatternB", manager.SelectedPattern);


        }

        [TestMethod]
        public void ShouldMutateConfig2()
        {


            var manager = new GridOrderManager("test.json", ProjectDatumHelpers.ColumnConfigs);
            manager.SetSelectedPattern("PatternA");
            Assert.AreEqual("PatternA", manager.SelectedPattern);

            manager.SetSelectedPattern("PatternB");
            manager.SaveToFile();
            Assert.AreEqual("PatternB", manager.SelectedPattern);


        }
    }
}
