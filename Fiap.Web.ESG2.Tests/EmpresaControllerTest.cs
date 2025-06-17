using AutoMapper;
using Fiap.Web.ESG2.Controllers;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fiap.Web.ESG2.Tests
{
    public class EmpresaControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IEmpresaService>();
            mockService.Setup(service => service.ListarEmpresas())
                .Returns(new List<EmpresaModel>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<EmpresaViewModel>>(It.IsAny<IEnumerable<EmpresaModel>>()))
                .Returns(new List<EmpresaViewModel>());

            var controller = new EmpresaController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
