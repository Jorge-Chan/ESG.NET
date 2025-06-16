using AutoMapper;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult<IEnumerable<CompensacaoCarbonoViewModel>> Get()
        {
            var compensacoes = _service.ListarCompensacoes();
            var viewModelList = _mapper.Map<IEnumerable<CompensacaoCarbonoViewModel>>(compensacoes);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<CompensacaoCarbonoViewModel> Get(int id)
        {
            var compensacao = _service.ObterPorId(id);
            if (compensacao == null)
                return NotFound();

            var viewModel = _mapper.Map<CompensacaoCarbonoViewModel>(compensacao);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CompensacaoCarbonoViewModel viewModel)
        {
            var compensacao = _mapper.Map<CompensacaoCarbonoModel>(viewModel);
            _service.Criar(compensacao);
            return CreatedAtAction(nameof(Get), new { id = compensacao.Id }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CompensacaoCarbonoViewModel viewModel)
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
