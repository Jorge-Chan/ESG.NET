using System.Collections.Generic;
using Fiap.Web.ESG2.Controllers;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Fiap.Web.ESG2.Tests
{
    public class RelatorioEmissaoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            var svc = new Mock<IRelatorioEmissaoService>();
            svc.Setup(s => s.ListarRelatorios())
               .Returns(new List<RelatorioEmissaoModel>());

            var mapper = new Mock<AutoMapper.IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<RelatorioEmissaoViewModel>>(It.IsAny<IEnumerable<RelatorioEmissaoModel>>()))
                  .Returns(new List<RelatorioEmissaoViewModel>());

            var controller = new RelatorioEmissaoController(svc.Object, mapper.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<RelatorioEmissaoViewModel>>(ok.Value);
        }
    }
}
