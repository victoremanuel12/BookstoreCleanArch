using Application.Errors;
using Application.Interfaces;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Abstraction;
using Domain.Dtos.Author;
using Domain.Entities;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;


        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AuthorDtoResponse>>> GetAll()
        {
            var listAutor = await _authorRepository.GetAllAsync();
            if (listAutor.Count() == 0)
                return Result<IEnumerable<AuthorDtoResponse>>.Failure(AuthorErrors.NotFound);

            IEnumerable<AuthorDtoResponse> listAuthorDto = _mapper.Map<IEnumerable<AuthorDtoResponse>>(listAutor);
            return Result<IEnumerable<AuthorDtoResponse>>.Success(listAuthorDto);
        }


        public async Task<Result<IEnumerable<AuthorWithBooksDtoRequest>>> GetAllWithBooks()
        {
            IEnumerable<Author> authorWithBooks = await _authorRepository.GetAllWithBooks();
            if (authorWithBooks.Count() == 0)
                return Result<IEnumerable<AuthorWithBooksDtoRequest>>.Failure(AuthorErrors.NotFound);

            IEnumerable<AuthorWithBooksDtoRequest> authorWithBookDto = _mapper.Map<IEnumerable<AuthorWithBooksDtoRequest>>(authorWithBooks);
            return Result<IEnumerable<AuthorWithBooksDtoRequest>>.Success(authorWithBookDto);
        }
        //public async task<result<authordtoresponse>> newauthor(authordtorequest authordtorequest)
        //{
        //    author author = _mapper.map<author>(authordtorequest);
        //    await _authorrepository.addasync(author);
        //    authordtoresponse authorresponse = _mapper.map<authordtoresponse>(author);
        //    return result<authordtoresponse>.success(authorresponse);
        //}

        //public Task<Result<AuthorDtoResponse>> Update(long id, AuthorDtoRequest authorDtoRequest)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
