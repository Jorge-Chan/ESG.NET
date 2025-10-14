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
    public class CompensacaoCarbonoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ICompensacaoCarbonoService>();
            mockService.Setup(s => s.ListarCompensacoes())
                       .Returns(new List<CompensacaoCarbonoModel>());

            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<CompensacaoCarbonoViewModel>>(It.IsAny<IEnumerable<CompensacaoCarbonoModel>>()))
                      .Returns(new List<CompensacaoCarbonoViewModel>());

            var controller = new CompensacaoCarbonoController(mockService.Object, mockMapper.Object);

            // Act (sem await – método é síncrono)
            var result = controller.Get(1, 10);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<CompensacaoCarbonoViewModel>>(ok.Value);
        }
    }
}
