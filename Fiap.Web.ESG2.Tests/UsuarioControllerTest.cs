using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using Fiap.Web.ESG2.Controllers;   // UsuariosController
using Fiap.Web.ESG2.Models;        // UsuarioModel
using Fiap.Web.ESG2.Services;      // IUsuarioService
using Fiap.Web.ESG2.ViewModels;    // PagedResult<T>
using Fiap.Web.ESG2.Tests.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Fiap.Web.ESG2.Tests
{
    public class UsuariosControllerTest
    {
        [Fact(DisplayName = "[BDD] Usuários: 200 com itens (PagedResult) + contrato")]
        public async Task ListarUsuarios_Paginado_ComDados_200_ComContrato()
        {
            // Dado
            var paged = new PagedResult<UsuarioModel>
            {
                Page = 1,
                PageSize = 10,
                TotalItems = 1,
                Items = new List<UsuarioModel> { new UsuarioModel() }
            };

            var mockSvc = new Mock<IUsuarioService>();
            mockSvc.Setup(s => s.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(paged);

            var controller = new UsuariosController(mockSvc.Object);

            // Quando
            var result = await controller.Get(page: 1, pageSize: 10, ct: CancellationToken.None);

            // Então – status
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            // Então – corpo JSON e schema
            var json = ApiTestHelper.ToJsonToken(ok.Value);
            var errors = ApiTestHelper.ValidateAgainst(json, ApiTestHelper.PagedOfObjectSchema());
            Assert.True(errors.Count == 0, "Schema inválido: " + string.Join("; ", errors));

            // E – items possui pelo menos 1
            var obj = Assert.IsType<PagedResult<UsuarioModel>>(ok.Value);
            Assert.NotEmpty(obj.Items);
        }

        [Fact(DisplayName = "[BDD] Usuários: 200 com lista vazia (PagedResult) + contrato")]
        public async Task ListarUsuarios_Paginado_SemDados_200_Vazio_ComContrato()
        {
            // Dado
            var paged = new PagedResult<UsuarioModel>
            {
                Page = 1,
                PageSize = 10,
                TotalItems = 0,
                Items = new List<UsuarioModel>()
            };

            var mockSvc = new Mock<IUsuarioService>();
            mockSvc.Setup(s => s.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(paged);

            var controller = new UsuariosController(mockSvc.Object);

            // Quando
            var result = await controller.Get(page: 1, pageSize: 10, ct: CancellationToken.None);

            // Então – status
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            // Então – corpo JSON e schema
            var json = ApiTestHelper.ToJsonToken(ok.Value);
            var errors = ApiTestHelper.ValidateAgainst(json, ApiTestHelper.PagedOfObjectSchema());
            Assert.True(errors.Count == 0);

            // E – items vazio
            var obj = Assert.IsType<PagedResult<UsuarioModel>>(ok.Value);
            Assert.Empty(obj.Items);
            Assert.Equal(0, obj.TotalItems);
        }

        [Fact(DisplayName = "[BDD] Usuários: 500 em erro interno (PagedResult)")]
        public async Task ListarUsuarios_Paginado_ErroInterno_500()
        {
            // Dado
            var mockSvc = new Mock<IUsuarioService>();
            mockSvc.Setup(s => s.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                   .ThrowsAsync(new Exception("falha inesperada"));

            var controller = new UsuariosController(mockSvc.Object);

            // Quando
            var result = await controller.Get(page: 1, pageSize: 10, ct: CancellationToken.None);

            // Então – requer que o controller trate exceções e retorne 500
            var obj = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, obj.StatusCode);
        }
    }
}
