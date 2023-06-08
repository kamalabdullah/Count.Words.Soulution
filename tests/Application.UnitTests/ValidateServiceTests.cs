using Application.Interfaces;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    public class ValidateServiceTests
    {
        private IValidateService validateService;
        public ValidateServiceTests()
        {
            validateService = new ValidateService();
        }
        [Fact]
        public void ValidatePath_ShouldReturnFalse()
        {
            // Arrange
            string directoryPath = @"C:invalidPath";

            // Act
            var valide = validateService.ValidatePath(directoryPath);

            // Assert
            Assert.False(valide);
        }
        [Fact]
        public void ValidatePath_ShouldReturnTrue()
        {
            // Arrange
            string directoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            // Act
            var valide = validateService.ValidatePath(directoryPath);

            // Assert
            Assert.True(valide);
        }
    }
}
