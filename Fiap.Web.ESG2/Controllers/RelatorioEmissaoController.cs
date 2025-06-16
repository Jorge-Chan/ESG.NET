using AutoMapper;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModel;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult<IEnumerable<RelotorioEmissaoViewModel>> Get()
        {
            var relatorios = _service.ListarRelatorios();
            var viewModelList = _mapper.Map<IEnumerable<RelotorioEmissaoViewModel>>(relatorios);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<RelotorioEmissaoViewModel> Get(int id)
        {
            var relatorio = _service.ObterPorId(id);
            if (relatorio == null)
                return NotFound();

            var viewModel = _mapper.Map<RelotorioEmissaoViewModel>(relatorio);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] RelotorioEmissaoViewModel viewModel)
        {
            var relatorio = _mapper.Map<RelotorioEmissaoModel>(viewModel);
            _service.Criar(relatorio);
            return CreatedAtAction(nameof(Get), new { id = relatorio.Id }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] RelotorioEmissaoViewModel viewModel)
        {
            var existente = _service.ObterPorId(id);
            if (existente == null)
                return NotFound();

            _mapper.Map(viewModel, existente);
            _service.Atualizar(existente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Deletar(id);
            return NoContent();
        }
    }
}
