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
    public class HistoricoEmissaoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IHistoricoEmissaoService>();
            mockService.Setup(s => s.ListarHistoricos())
                       .Returns(new List<HistoricoEmissaoModel>());

            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<HistoricoEmissoesViewModel>>(It.IsAny<IEnumerable<HistoricoEmissaoModel>>()))
                      .Returns(new List<HistoricoEmissoesViewModel>());

            var controller = new HistoricoEmissaoController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<HistoricoEmissoesViewModel>>(ok.Value);
        }
    }
}
