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
    public class HistoricoEmissaoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IHistoricoEmissaoService>();
            mockService.Setup(service => service.ListarHistoricos())
                .Returns(new List<HistoricoEmissaoModel>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<HistoricoEmissoesViewModel>>(It.IsAny<IEnumerable<HistoricoEmissaoModel>>()))
                .Returns(new List<HistoricoEmissoesViewModel>());

            var controller = new HistoricoEmissaoController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
