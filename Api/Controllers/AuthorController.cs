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
        public async Task<IEnumerable<AuthorDtoResponse>> Get()
        {
            ICollection<AuthorDtoResponse> listAuthor = await _authorService.Authors();
            return listAuthor;
        }
        [HttpGet("books")]
        public async Task<IEnumerable<AuthorDtoResponse>> GetAllWithBooks()
        {
            ICollection<AuthorDtoResponse> listAuthor = await _authorService.AuthorsWithBooks();
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
