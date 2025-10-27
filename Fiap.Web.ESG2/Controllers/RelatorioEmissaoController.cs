using AutoMapper;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RelatorioEmissaoController : ControllerBase
    {
        private readonly IRelatorioEmissaoService _service;
        private readonly IMapper _mapper;

        public RelatorioEmissaoController(IRelatorioEmissaoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RelatorioEmissaoViewModel>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 10 : pageSize;

                var all = _service.ListarRelatorios();
                var total = all.Count();
                var pageItems = all.Skip((page - 1) * pageSize).Take(pageSize);

                Response.Headers["X-Total-Count"] = total.ToString();
                Response.Headers["X-Page"] = page.ToString();
                Response.Headers["X-Page-Size"] = pageSize.ToString();

                var vms = _mapper.Map<IEnumerable<RelatorioEmissaoViewModel>>(pageItems);
                return Ok(vms);
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public ActionResult<RelatorioEmissaoViewModel> GetById(long id)
        {
            try
            {
                var relatorio = _service.ObterPorId((int)id);
                if (relatorio == null) return NotFound();

                var vm = _mapper.Map<RelatorioEmissaoViewModel>(relatorio);
                return Ok(vm);
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Post([FromBody] RelatorioEmissaoViewModel viewModel)
        {
            try
            {
                var entity = _mapper.Map<RelatorioEmissaoModel>(viewModel);
                _service.Criar(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<RelatorioEmissaoViewModel>(entity));
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "admin")]
        public ActionResult Put(long id, [FromBody] RelatorioEmissaoViewModel viewModel)
        {
            try
            {
                var existente = _service.ObterPorId((int)id);
                if (existente == null) return NotFound();

                _mapper.Map(viewModel, existente);
                _service.Atualizar(existente);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long id)
        {
            try
            {
                var existente = _service.ObterPorId((int)id);
                if (existente == null) return NotFound();

                _service.Deletar((int)id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}