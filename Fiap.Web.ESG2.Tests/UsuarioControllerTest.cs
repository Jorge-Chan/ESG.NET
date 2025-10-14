using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fiap.Web.ESG2.Controllers;          // UsuariosController
using Fiap.Web.ESG2.Models;               // UsuarioModel
using Fiap.Web.ESG2.Services;             // IUsuarioService
using Fiap.Web.ESG2.ViewModels;           // PagedResult<T>
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Fiap.Web.ESG2.Tests
{
    public class UsuariosControllerTest
    {
        [Fact]
        public async Task Get_ReturnsOkWithPagedResult()
        {
            // Arrange
            var mockSvc = new Mock<IUsuarioService>();
            var paged = new PagedResult<UsuarioModel>
            {
                Page = 1,
                PageSize = 10,
                TotalItems = 0,
                Items = new List<UsuarioModel>()
            };

            mockSvc
                .Setup(s => s.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(paged);

            var controller = new UsuariosController(mockSvc.Object);

            // Act
            var result = await controller.Get(page: 1, pageSize: 10, ct: CancellationToken.None);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<PagedResult<UsuarioModel>>(ok.Value);
            Assert.Equal(0, value.TotalItems);
            Assert.Equal(1, value.Page);
            Assert.Equal(10, value.PageSize);
        }
    }
}
