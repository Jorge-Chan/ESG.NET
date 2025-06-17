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
    public class RelatorioEmissaoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IRelatorioEmissaoService>();
            mockService.Setup(service => service.ListarRelatorios())
                .Returns(new List<RelotorioEmissaoModel>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<RelotorioEmissaoViewModel>>(It.IsAny<IEnumerable<RelotorioEmissaoModel>>()))
                .Returns(new List<RelotorioEmissaoViewModel>());

            var controller = new RelatorioEmissaoController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
