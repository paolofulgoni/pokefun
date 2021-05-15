using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NUnit.Framework;
using PokeFun.Controllers;

namespace PokeFun.Tests.Controllers
{
    public class ErrorControllerTest
    {
        [Test]
        public void Error_ReturnsCode500InternalServerError()
        {
            // Arrange

            var controller = new ErrorController();

            // Act

            var result = controller.Error();

            // Assert

            result.Should().NotBeNull().And.BeAssignableTo<IStatusCodeActionResult>();
            ((IStatusCodeActionResult)result).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public void ErrorLocalDevelopment_ReturnsCode500InternalServerError()
        {
            // Arrange

            var controller = new ErrorController();

            // Act

            var result = controller.ErrorLocalDevelopment();

            // Assert

            result.Should().NotBeNull().And.BeAssignableTo<IStatusCodeActionResult>();
            ((IStatusCodeActionResult)result).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
