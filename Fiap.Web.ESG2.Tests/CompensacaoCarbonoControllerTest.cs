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
using Newtonsoft.Json.Schema;
using Xunit;

namespace Fiap.Web.ESG2.Tests
{
    public class CompensacaoCarbonoControllerTest
    {
        private static CompensacaoCarbonoController BuildController(
            ICompensacaoCarbonoService svc,
            IMapper mapper)
        {
            return new CompensacaoCarbonoController(svc, mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
        }

        [Fact(DisplayName = "[BDD] Dado que existem compensações, quando listar, então retorna 200 e JSON válido (com itens)")]
        public void Listar_ComDados_DeveRetornar200EArrayComItens_ContratoBasico()
        {
            // Dado
            var svc = new Mock<ICompensacaoCarbonoService>();
            svc.Setup(s => s.ListarCompensacoes())
               .Returns(new List<CompensacaoCarbonoModel> { new CompensacaoCarbonoModel() });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<CompensacaoCarbonoViewModel>>(It.IsAny<IEnumerable<CompensacaoCarbonoModel>>()))
                  .Returns(new List<CompensacaoCarbonoViewModel> { new CompensacaoCarbonoViewModel() });

            var controller = BuildController(svc.Object, mapper.Object);

            // Quando
            var action = controller.Get();
            var ok = Assert.IsType<OkObjectResult>(action.Result);

            // Então - status
            Assert.Equal(200, ok.StatusCode ?? 200);

            // Então - corpo JSON
            var json = ApiTestHelper.ToJsonToken(ok.Value);
            Assert.True(json.Type == JTokenType.Array);

            // Então - schema (contrato)
            var schema = ApiTestHelper.ArrayOfObjectSchema();
            var errors = ApiTestHelper.ValidateAgainst(json, schema);
            Assert.True(errors.Count == 0, "Schema inválido: " + string.Join("; ", errors));

            // E - tem itens
            Assert.True(ApiTestHelper.HasAtLeastNItems(ok.Value, 1));
        }

        [Fact(DisplayName = "[BDD] Dado que não há compensações, quando listar, então retorna 200 e JSON vazio")]
        public void Listar_SemDados_DeveRetornar200EArrayVazio_ContratoBasico()
        {
            // Dado
            var svc = new Mock<ICompensacaoCarbonoService>();
            svc.Setup(s => s.ListarCompensacoes())
               .Returns(new List<CompensacaoCarbonoModel>());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<CompensacaoCarbonoViewModel>>(It.IsAny<IEnumerable<CompensacaoCarbonoModel>>()))
                  .Returns(new List<CompensacaoCarbonoViewModel>());

            var controller = BuildController(svc.Object, mapper.Object);

            // Quando
            var action = controller.Get();
            var ok = Assert.IsType<OkObjectResult>(action.Result);

            // Então - status
            Assert.Equal(200, ok.StatusCode ?? 200);

            // Então - corpo JSON vazio
            var json = ApiTestHelper.ToJsonToken(ok.Value);
            Assert.True(json.Type == JTokenType.Array);
            Assert.True(ApiTestHelper.IsEmptyEnumerable(ok.Value));

            // Então - schema
            var schema = ApiTestHelper.ArrayOfObjectSchema();
            var errors = ApiTestHelper.ValidateAgainst(json, schema);
            Assert.True(errors.Count == 0, "Schema inválido: " + string.Join("; ", errors));
        }

        [Fact(DisplayName = "[BDD] Dado um erro inesperado no serviço, quando listar, então retorna 500")]
        public void Listar_ErroInterno_DeveRetornar500()
        {
            // Dado
            var svc = new Mock<ICompensacaoCarbonoService>();
            svc.Setup(s => s.ListarCompensacoes())
               .Throws(new Exception("falha"));

            var mapper = new Mock<IMapper>();

            // IMPORTANTE: isso pressupõe que o controller captura a exceção e devolve 500.
            // Se o controller não captura, esse teste vai falhar lançando exceção.
            var controller = BuildController(svc.Object, mapper.Object);

            // Quando
            var action = controller.Get();

            // Então
            var result = Assert.IsAssignableFrom<ActionResult<IEnumerable<CompensacaoCarbonoViewModel>>>(action);
            var obj = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, obj.StatusCode);
        }
    }
}
