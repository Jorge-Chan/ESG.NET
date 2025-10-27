using System.Collections.Generic;
using System;
using AutoMapper;
using Fiap.Web.ESG2.Controllers;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Fiap.Web.ESG2.Tests.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Fiap.Web.ESG2.Tests
{
    public class RelatorioEmissaoControllerTest
    {
        private static RelatorioEmissaoController BuildController(IRelatorioEmissaoService svc, IMapper mapper) =>
            new RelatorioEmissaoController(svc, mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

        [Fact(DisplayName = "[BDD] Relatório: 200 com itens + contrato")]
        public void ListarRelatorios_ComDados_200_ComContrato()
        {
            var svc = new Mock<IRelatorioEmissaoService>();
            svc.Setup(s => s.ListarRelatorios())
               .Returns(new List<RelatorioEmissaoModel> { new RelatorioEmissaoModel() });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<RelatorioEmissaoViewModel>>(It.IsAny<IEnumerable<RelatorioEmissaoModel>>()))
                  .Returns(new List<RelatorioEmissaoViewModel> { new RelatorioEmissaoViewModel() });

            var controller = BuildController(svc.Object, mapper.Object);

            var action = controller.Get();
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            var json = ApiTestHelper.ToJsonToken(ok.Value);
            var errors = ApiTestHelper.ValidateAgainst(json, ApiTestHelper.ArrayOfObjectSchema());
            Assert.True(errors.Count == 0);
            Assert.True(ApiTestHelper.HasAtLeastNItems(ok.Value, 1));
        }

        [Fact(DisplayName = "[BDD] Relatório: 200 vazio + contrato")]
        public void ListarRelatorios_SemDados_200_Vazio_ComContrato()
        {
            var svc = new Mock<IRelatorioEmissaoService>();
            svc.Setup(s => s.ListarRelatorios())
               .Returns(new List<RelatorioEmissaoModel>());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<RelatorioEmissaoViewModel>>(It.IsAny<IEnumerable<RelatorioEmissaoModel>>()))
                  .Returns(new List<RelatorioEmissaoViewModel>());

            var controller = BuildController(svc.Object, mapper.Object);

            var action = controller.Get();
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            var json = ApiTestHelper.ToJsonToken(ok.Value);
            var errors = ApiTestHelper.ValidateAgainst(json, ApiTestHelper.ArrayOfObjectSchema());
            Assert.True(errors.Count == 0);
            Assert.True(ApiTestHelper.IsEmptyEnumerable(ok.Value));
        }

        [Fact(DisplayName = "[BDD] Relatório: 500 em erro interno")]
        public void ListarRelatorios_ErroInterno_500()
        {
            var svc = new Mock<IRelatorioEmissaoService>();
            svc.Setup(s => s.ListarRelatorios())
               .Throws(new Exception("falha"));

            var mapper = new Mock<IMapper>();
            var controller = BuildController(svc.Object, mapper.Object);

            var action = controller.Get();
            var obj = Assert.IsType<ObjectResult>(action.Result);
            Assert.Equal(500, obj.StatusCode);
        }
    }
}
