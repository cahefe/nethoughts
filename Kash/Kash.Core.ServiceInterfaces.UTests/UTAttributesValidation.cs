using Xunit;
using Kash.Core.Models;
using Kash.Core.Models.Validations;
using System.Diagnostics;

namespace Kash.Core.ServiceInterfaces.UTests
{
    public class UTAttributesValidation
    {
        [Fact]
        public void Test1()
        {
            //  Arrange...
            var validationRule = new BR0001_ValuesGreaterThanZeroAttribute();
            Entry entry = new Entry
            {
                ID = 5,
                Value = 1,
            };
            //  Act...
            var result = validationRule.IsValid(entry.Value);
            Debug.Print(validationRule.ErrorMessage);
            //  Assert
            Assert.True(result);
        }
    }
}
