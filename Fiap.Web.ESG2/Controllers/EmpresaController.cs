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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;
        private readonly IMapper _mapper;

        public EmpresaController(IEmpresaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<EmpresaViewModel>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var all = _service.ListarEmpresas();
            var total = all.Count();
            var pageItems = all.Skip((page - 1) * pageSize).Take(pageSize);

            Response.Headers["X-Total-Count"] = total.ToString();
            Response.Headers["X-Page"] = page.ToString();
            Response.Headers["X-Page-Size"] = pageSize.ToString();

            var vms = _mapper.Map<IEnumerable<EmpresaViewModel>>(pageItems);
            return Ok(vms);
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public ActionResult<EmpresaViewModel> GetById(long id)
        {
            var empresa = _service.ObterEmpresaPorId((int)id); // << cast
            if (empresa == null) return NotFound();

            var vm = _mapper.Map<EmpresaViewModel>(empresa);
            return Ok(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Post([FromBody] EmpresaViewModel viewModel)
        {
            var entity = _mapper.Map<EmpresaModel>(viewModel);
            _service.CriarEmpresa(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<EmpresaViewModel>(entity));
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "admin")]
        public ActionResult Put(long id, [FromBody] EmpresaViewModel viewModel)
        {
            var existente = _service.ObterEmpresaPorId((int)id); // << cast
            if (existente == null) return NotFound();

            _mapper.Map(viewModel, existente);
            _service.AtualizarEmpresa(existente);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long id)
        {
            var existente = _service.ObterEmpresaPorId((int)id); // << cast
            if (existente == null) return NotFound();

            _service.DeletarEmpresa((int)id); // << cast
            return NoContent();
        }
    }
}
