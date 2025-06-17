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
    public class CompensacaoCarbonoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ICompensacaoCarbonoService>();
            mockService.Setup(service => service.ListarCompensacoes())
                .Returns(new List<CompensacaoCarbonoModel>());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<CompensacaoCarbonoViewModel>>(It.IsAny<IEnumerable<CompensacaoCarbonoModel>>()))
                .Returns(new List<CompensacaoCarbonoViewModel>());

            var controller = new CompensacaoCarbonoController(mockService.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
