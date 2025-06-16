using AutoMapper;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult<IEnumerable<EmpresaViewModel>> Get()
        {
            var empresas = _service.ListarEmpresas();
            var viewModelList = _mapper.Map<IEnumerable<EmpresaViewModel>>(empresas);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<EmpresaViewModel> Get(int id)
        {
            var empresa = _service.ObterEmpresaPorId(id);
            if (empresa == null)
                return NotFound();

            var viewModel = _mapper.Map<EmpresaViewModel>(empresa);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] EmpresaViewModel viewModel)
        {
            var empresa = _mapper.Map<EmpresaModel>(viewModel);
            _service.CriarEmpresa(empresa);
            return CreatedAtAction(nameof(Get), new { id = empresa.Id }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EmpresaViewModel viewModel)
        {
            var empresaExistente = _service.ObterEmpresaPorId(id);
            if (empresaExistente == null)
                return NotFound();

            _mapper.Map(viewModel, empresaExistente);
            _service.AtualizarEmpresa(empresaExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletarEmpresa(id);
            return NoContent();
        }
    }
}
