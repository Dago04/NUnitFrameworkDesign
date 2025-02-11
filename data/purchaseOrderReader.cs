using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitFrameworkDesign.data
{
    public class purchaseOrderReader
    {

        public static IEnumerable<object[]> GetTestData() { 
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(projectRoot, "data", "purchaseOrder.json");

            var jsonData = File.ReadAllText(filePath);
            var testCases = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PurchaseOrder>>(jsonData);

            foreach (var testCase in testCases)
            {
                yield return new object[] { testCase.Email, testCase.Password, testCase.Product };
            }
        }

        public class PurchaseOrder
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Product { get; set; }
        }
    }
}
