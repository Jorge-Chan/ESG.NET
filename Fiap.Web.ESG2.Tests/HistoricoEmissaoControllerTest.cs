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
    public class HistoricoEmissaoControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            var svc = new Mock<IHistoricoEmissaoService>();
            svc.Setup(s => s.ListarHistoricos())
               .Returns(new List<HistoricoEmissaoModel>());

            var mapper = new Mock<AutoMapper.IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<HistoricoEmissoesViewModel>>(It.IsAny<IEnumerable<HistoricoEmissaoModel>>()))
                  .Returns(new List<HistoricoEmissoesViewModel>());

            var controller = new HistoricoEmissaoController(svc.Object, mapper.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<HistoricoEmissoesViewModel>>(ok.Value);
        }
    }
}
