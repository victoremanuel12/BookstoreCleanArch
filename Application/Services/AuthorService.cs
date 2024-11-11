using Application.Dtos.Author;
using Application.Errors;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;



        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<AuthorDtoResponse>>> GetAll()
        {
            var listAutor = await _unitOfWork.AuthorRepository.GetAllAsync();
            if (listAutor.Count() == 0)
                return Result<IEnumerable<AuthorDtoResponse>>.Failure(AuthorErrors.NotFound);

            IEnumerable<AuthorDtoResponse> listAuthorDto = _mapper.Map<IEnumerable<AuthorDtoResponse>>(listAutor);
            return Result<IEnumerable<AuthorDtoResponse>>.Success(listAuthorDto);
        }


        public async Task<Result<IEnumerable<AuthorWithBooksDtoRequest>>> GetAllWithBooks()
        {
            IEnumerable<Author> authorWithBooks = await _unitOfWork.AuthorRepository.GetAllWithBooks();
            if (authorWithBooks.Count() == 0)
                return Result<IEnumerable<AuthorWithBooksDtoRequest>>.Failure(AuthorErrors.NotFound);

            IEnumerable<AuthorWithBooksDtoRequest> authorWithBookDto = _mapper.Map<IEnumerable<AuthorWithBooksDtoRequest>>(authorWithBooks);
            return Result<IEnumerable<AuthorWithBooksDtoRequest>>.Success(authorWithBookDto);
        }

        public async Task<Result<AuthorDtoResponse>> Update(AuthorDtoRequest authorDtoRequest)
        {
            Author author = _mapper.Map<Author>(authorDtoRequest);
            Author authorEntity = await _unitOfWork.AuthorRepository.GetByIdAsync(author);
            if (authorEntity is null)
                return Result<AuthorDtoResponse>.Failure(AuthorErrors.NotFound);
            _mapper.Map(authorDtoRequest, authorEntity);
            _unitOfWork.AuthorRepository.Update(authorEntity);
            await _unitOfWork.CommitAsync();
            AuthorDtoResponse authorDtoResponse = _mapper.Map<AuthorDtoResponse>(authorEntity);
            return Result<AuthorDtoResponse>.Success(authorDtoResponse);

        }
        public async Task<Result<AuthorDtoResponse>> Diseble(AuthorDisableDto authorDisableDto)
        {
            Author author = _mapper.Map<Author>(authorDisableDto);
            Author authorEntity = await _unitOfWork.AuthorRepository.GetByIdAsync(author);
            if (authorEntity is null)
                return Result<AuthorDtoResponse>.Failure(AuthorErrors.NotFound);
            _mapper.Map(authorDisableDto, authorEntity);
            _authorRepository.Disable(authorEntity);
            await _unitOfWork.CommitAsync();
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
