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

        public async Task<Result<AuthorDtoResponse>> Update(AuthorDtoRequest authorDtoRequest)
        {
            Author author = _mapper.Map<Author>(authorDtoRequest);
            Author authorEntity = await _authorRepository.GetByIdAsync(author);
            if (authorEntity is null)
                return Result<AuthorDtoResponse>.Failure(AuthorErrors.NotFound);
            await _authorRepository.UpdateAsync(authorEntity);
            AuthorDtoResponse authorDtoResponse = _mapper.Map<AuthorDtoResponse>(authorEntity);
            return Result<AuthorDtoResponse>.Success(authorDtoResponse);

        }
        public async Task<Result<AuthorDtoResponse>> Diseble(AuthorDtoRequest authorDtoRequest)
        {
            Author author = _mapper.Map<Author>(authorDtoRequest);
            Author authorEntity = await _authorRepository.GetByIdAsync(author);
            if (authorEntity is null)
                return Result<AuthorDtoResponse>.Failure(AuthorErrors.NotFound);
            await _authorRepository.DisableAsync(authorEntity);
            AuthorDtoResponse authorDtoResponse = _mapper.Map<AuthorDtoResponse>(authorEntity);
            return Result<AuthorDtoResponse>.Success(authorDtoResponse);

        }

        public async Task<Result<AuthorDtoResponse>> Create(AuthorDtoRequest authorDtoRequest)
        {
            Author author = _mapper.Map<Author>(authorDtoRequest);
            await _authorRepository.CreateAsync(author);
            AuthorDtoResponse authorDtoResponse = _mapper.Map<AuthorDtoResponse>(author);
            return Result<AuthorDtoResponse>.Success(authorDtoResponse);

        }
    }
}
