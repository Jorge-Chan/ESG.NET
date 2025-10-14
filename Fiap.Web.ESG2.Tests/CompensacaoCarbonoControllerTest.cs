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
    public class CompensacaoCarbonoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var svc = new Mock<ICompensacaoCarbonoService>();
            svc.Setup(s => s.ListarCompensacoes())
               .Returns(new List<CompensacaoCarbonoModel>());

            var mapper = new Mock<AutoMapper.IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<CompensacaoCarbonoViewModel>>(It.IsAny<IEnumerable<CompensacaoCarbonoModel>>()))
                  .Returns(new List<CompensacaoCarbonoViewModel>());

            var controller = new CompensacaoCarbonoController(svc.Object, mapper.Object);

            // Act (síncrono)
            var result = controller.Get();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<CompensacaoCarbonoViewModel>>(ok.Value);
        }
    }
}
