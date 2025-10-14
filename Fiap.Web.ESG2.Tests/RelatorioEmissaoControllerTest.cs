using System.Collections.Generic;
using Fiap.Web.ESG2.Controllers;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Fiap.Web.ESG2.Test
{
    public class RelatorioEmissaoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IRelatorioEmissaoService>();
            mockService.Setup(s => s.ListarRelatorios())
                       .Returns(new List<RelatorioEmissaoModel>());

            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<RelatorioEmissaoViewModel>>(It.IsAny<IEnumerable<RelatorioEmissaoModel>>()))
                      .Returns(new List<RelatorioEmissaoViewModel>());

            var controller = new RelatorioEmissaoController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<RelatorioEmissaoViewModel>>(ok.Value);
        }
    }
}
