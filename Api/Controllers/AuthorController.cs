using Api.Extensions;
using Application.CQRS.Author.Command.CreateAuthorCommand;
using Application.CQRS.Author.Query.GetAllAuthorWithBooksQuery;
using Application.CQRS.Author.Query.GettAllQuery;
using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction.PaginationFilter;
using Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AuthorController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService, IMediator mediator)
        {
            _authorService = authorService;
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Policy = Policies.HorarioComercial)]
        [Authorize(Roles = Roles.Admin)]
        //[ClaimsAuthorize(ClaimTypes.author,"Read")]
        public async Task<IResult> Get([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = await _mediator.Send(new GetAllAuthorQuery(filter, route));
            return Results.Extensions.MapResult(result);
        }
        [HttpGet("books")]
        public async Task<IResult> GetAllWithBooks([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = await _mediator.Send(new GetAllAuthorWithBooksQuery(filter, route));
            return Results.Extensions.MapResult(result);
        }

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";   
        //}

        [HttpPost]
        //[ClaimsAuthorize(ClaimTypes.author, "Write")]

        public async Task<IResult> Post([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return Results.Extensions.MapResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] AuthorDtoRequest authorDtoRequest)
        {
            var result = await _authorService.Update(authorDtoRequest);
            return Results.Extensions.MapResult(result);
        }

        [HttpPut("{id}/desable")]
        public async Task<IResult> Disable(int id, [FromBody] AuthorDtoDisableRequest authorDisableDto)
        {
            var result = await _authorService.Diseble(authorDisableDto);
            return Results.Extensions.MapResult(result);
        }


        //[HttpDelete("{id}")]
        //[ClaimsAuthorize(ClaimTypes.author, "Delete")]

        //public void Delete(int id)
        //{
        //}
    }
}
