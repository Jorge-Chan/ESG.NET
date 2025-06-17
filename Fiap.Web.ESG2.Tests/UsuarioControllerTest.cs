using AutoMapper;
using Fiap.Web.ESG2.Controllers;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace Fiap.Web.ESG2.Test
{
    public class UsuarioControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IUsuarioService>();
            mockService.Setup(service => service.ListarUsuarios())
                .Returns(new List<UsuarioModel>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<UsuarioViewModel>>(It.IsAny<IEnumerable<UsuarioModel>>()))
                .Returns(new List<UsuarioViewModel>());

            var controller = new UsuarioController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
