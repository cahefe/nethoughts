using Xunit;
using Kash.Core.Models;
using Kash.Core.Models.Validations;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kash.Core.ServiceInterfaces.UTests
{
    public class UTAttributesValidation
    {
        [Fact]
        public void StringComValorMinimo()
        {
            //  Arrange...
            var validationRule = new BR0001_ValorDeveSerMaiorQueParametroAttribute();
            Entry entry = new Entry
            {
                ID = 5,
                Value = 0,
            };
            //  Act...
            var result = validationRule.IsValid(entry.Value);
            Debug.Print(validationRule.Code + ": " + validationRule.ErrorMessage);
            //  Assert
            Assert.True(result);
        }
        [Fact]
        public void EntryComSomaValorMinimo()
        {
            //  Arrange...
            var validationRule = new BR0003_SomaDeveTerValorMinimoAttribute(20);
            Entry entry = new Entry
            {
                ID = 5,
                Value = 0,
                FeesValue = 20
            };
            //  Act...
            var result = validationRule.IsValid(entry);
            Debug.Print(validationRule.Code + ": " + validationRule.ErrorMessage);
            //  Assert
            Assert.False(result);
        }
        [Fact]
        public void EntryValidarTodoModelo()
        {
            //  Arrange...
            var result = new List<ValidationResult>();
            Entry entry = new Entry
            {
                ID = 5,
                Value = 1,
                FeesValue = 5,
                Description = "123456",
            };
            //  Act...
            var isValid = Validator.TryValidateObject(entry, new ValidationContext(entry), result, true);

            //  Assert
            Assert.False(isValid);
        }
    }
}
