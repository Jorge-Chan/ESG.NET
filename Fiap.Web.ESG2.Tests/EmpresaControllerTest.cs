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
    public class EmpresaControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            var svc = new Mock<IEmpresaService>();
            svc.Setup(s => s.ListarEmpresas())
               .Returns(new List<EmpresaModel>());

            var mapper = new Mock<AutoMapper.IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<EmpresaViewModel>>(It.IsAny<IEnumerable<EmpresaModel>>()))
                  .Returns(new List<EmpresaViewModel>());

            var controller = new EmpresaController(svc.Object, mapper.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            var result = controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<EmpresaViewModel>>(ok.Value);
        }
    }
}
