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
    public class EmpresaControllerTest
    {
        private static EmpresaController BuildController(IEmpresaService svc, IMapper mapper) =>
            new EmpresaController(svc, mapper)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

        [Fact(DisplayName = "[BDD] Empresa: 200 com itens + contrato")]
        public void ListarEmpresas_ComDados_200_ComContrato()
        {
            var svc = new Mock<IEmpresaService>();
            svc.Setup(s => s.ListarEmpresas())
               .Returns(new List<EmpresaModel> { new EmpresaModel() });

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<EmpresaViewModel>>(It.IsAny<IEnumerable<EmpresaModel>>()))
                  .Returns(new List<EmpresaViewModel> { new EmpresaViewModel() });

            var controller = BuildController(svc.Object, mapper.Object);

            var action = controller.Get();
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            var json = ApiTestHelper.ToJsonToken(ok.Value);
            var errors = ApiTestHelper.ValidateAgainst(json, ApiTestHelper.ArrayOfObjectSchema());
            Assert.True(errors.Count == 0);
            Assert.True(ApiTestHelper.HasAtLeastNItems(ok.Value, 1));
        }

        [Fact(DisplayName = "[BDD] Empresa: 200 com lista vazia + contrato")]
        public void ListarEmpresas_SemDados_200_Vazio_ComContrato()
        {
            var svc = new Mock<IEmpresaService>();
            svc.Setup(s => s.ListarEmpresas())
               .Returns(new List<EmpresaModel>());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<EmpresaViewModel>>(It.IsAny<IEnumerable<EmpresaModel>>()))
                  .Returns(new List<EmpresaViewModel>());

            var controller = BuildController(svc.Object, mapper.Object);

            var action = controller.Get();
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            var json = ApiTestHelper.ToJsonToken(ok.Value);
            var errors = ApiTestHelper.ValidateAgainst(json, ApiTestHelper.ArrayOfObjectSchema());
            Assert.True(errors.Count == 0);
            Assert.True(ApiTestHelper.IsEmptyEnumerable(ok.Value));
        }

        [Fact(DisplayName = "[BDD] Empresa: 500 em erro interno")]
        public void ListarEmpresas_ErroInterno_500()
        {
            var svc = new Mock<IEmpresaService>();
            svc.Setup(s => s.ListarEmpresas())
               .Throws(new Exception("falha"));

            var mapper = new Mock<IMapper>();
            var controller = BuildController(svc.Object, mapper.Object);

            var action = controller.Get();
            var obj = Assert.IsType<ObjectResult>(action.Result);
            Assert.Equal(500, obj.StatusCode);
        }
    }
}
