using System.Collections.Generic;
using Fiap.Web.ESG2.Controllers;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            mockService.Setup(s => s.ListarEmpresas())
                       .Returns(new List<EmpresaModel>());

            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<EmpresaViewModel>>(It.IsAny<IEnumerable<EmpresaModel>>()))
                      .Returns(new List<EmpresaViewModel>());

            var controller = new EmpresaController(mockService.Object, mockMapper.Object);

            // Act (sem await – método é síncrono)
            var result = controller.Get(1, 10);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<EmpresaViewModel>>(ok.Value);
        }
    }
}
