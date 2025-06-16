using AutoMapper;
using Fiap.Web.ESG2.Models;
using Fiap.Web.ESG2.Services;
using Fiap.Web.ESG2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.ESG2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioViewModel>> Get()
        {
            var usuarios = _service.ListarUsuarios();
            var viewModelList = _mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioViewModel> Get(int id)
        {
            var usuario = _service.ObterPorId(id);
            if (usuario == null)
                return NotFound();

            var viewModel = _mapper.Map<UsuarioViewModel>(usuario);
            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] UsuarioViewModel viewModel)
        {
            var usuario = _mapper.Map<UsuarioModel>(viewModel);
            _service.Criar(usuario);
            return CreatedAtAction(nameof(Get), new { id = usuario.UsuarioId }, viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UsuarioViewModel viewModel)
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
