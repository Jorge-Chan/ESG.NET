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
    public class CompensacaoCarbonoController : ControllerBase
    {
        private readonly ICompensacaoCarbonoService _service;
        private readonly IMapper _mapper;

        public CompensacaoCarbonoController(ICompensacaoCarbonoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CompensacaoCarbonoViewModel>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var all = _service.ListarCompensacoes();
            var total = all.Count();
            var pageItems = all.Skip((page - 1) * pageSize).Take(pageSize);

            Response.Headers["X-Total-Count"] = total.ToString();
            Response.Headers["X-Page"] = page.ToString();
            Response.Headers["X-Page-Size"] = pageSize.ToString();

            var vms = _mapper.Map<IEnumerable<CompensacaoCarbonoViewModel>>(pageItems);
            return Ok(vms);
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public ActionResult<CompensacaoCarbonoViewModel> GetById(long id)
        {
            var compensacao = _service.ObterPorId((int)id); // << cast para int
            if (compensacao == null) return NotFound();

            var vm = _mapper.Map<CompensacaoCarbonoViewModel>(compensacao);
            return Ok(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Post([FromBody] CompensacaoCarbonoViewModel viewModel)
        {
            var entity = _mapper.Map<CompensacaoCarbonoModel>(viewModel);
            _service.Criar(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<CompensacaoCarbonoViewModel>(entity));
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "admin")]
        public ActionResult Put(long id, [FromBody] CompensacaoCarbonoViewModel viewModel)
        {
            var existente = _service.ObterPorId((int)id); // << cast
            if (existente == null) return NotFound();

            _mapper.Map(viewModel, existente);
            _service.Atualizar(existente);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long id)
        {
            var existente = _service.ObterPorId((int)id); // << cast
            if (existente == null) return NotFound();

            _service.Deletar((int)id); // << cast
            return NoContent();
        }
    }
}
