using Api.Extensions;
using Application.Dtos.Author;
using Application.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _authorService.GetAll();
            return Results.Extensions.MapResult(result);
        }
        [HttpGet("books")]
        public async Task<IResult> GetAllWithBooks()
        {
            var result = await _authorService.GetAllWithBooks();
            return Results.Extensions.MapResult(result);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] AuthorDtoRequest authorDtoRequest)
        {
            var result = await _authorService.NewAuthor(authorDtoRequest);
            return Results.Extensions.MapResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] AuthorDtoRequest authorDtoRequest)
        {
            var result = await _authorService.NewAuthor(authorDtoRequest);
            return Results.Extensions.MapResult(result);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
