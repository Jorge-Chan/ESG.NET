using AutoMapper;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoEmissaoController : ControllerBase
    {
        private readonly IHistoricoEmissaoService _service;
        private readonly IMapper _mapper;

        public HistoricoEmissaoController(IHistoricoEmissaoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HistoricoEmissoesViewModel>> Get()
        {
            var historicos = _service.ListarHistoricos();
            var viewModelList = _mapper.Map<IEnumerable<HistoricoEmissoesViewModel>>(historicos);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<HistoricoEmissoesViewModel> Get(int id)
        {
            var historico = _service.ObterPorId(id);
            if (historico == null)
                return NotFound();

            var viewModel = _mapper.Map<HistoricoEmissoesViewModel>(historico);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] HistoricoEmissoesViewModel viewModel)
        {
            var historico = _mapper.Map<HistoricoEmissaoModel>(viewModel);
            _service.Criar(historico);
            return CreatedAtAction(nameof(Get), new { id = historico.Id }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] HistoricoEmissoesViewModel viewModel)
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
