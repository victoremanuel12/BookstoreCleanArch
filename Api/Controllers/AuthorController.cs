using Api.Extensions;
using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction;
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
            Result<IEnumerable<AuthorDtoResponse>> listAuthor = await _authorService.Authors();
            return Results.Extensions.MapResult(listAuthor);
        }
        [HttpGet("books")]
        public async Task<IEnumerable<AuthorDtoResponse>> GetAllWithBooks()
        {
            IEnumerable<AuthorDtoResponse> listAuthor = await _authorService.AuthorsWithBooks();
            return listAuthor;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task Post([FromBody] AuthorDtoRequest authorDtoRequest)
        {
            await _authorService.NewAuthor(authorDtoRequest);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
