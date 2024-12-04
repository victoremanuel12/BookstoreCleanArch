using Application.CQRS.Author.Command.CreateAuthorCommand;
using Application.Dtos.Author;
using Application.Errors;
using Application.Helpers;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Abstraction;
using Domain.Abstraction.PaginationFilter;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService _uriService;


        public AuthorService(IMapper mapper, IUnitOfWork unitOfWork, IUriService uriService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _uriService = uriService;
        }


        public async Task<Result<PagedResult<IEnumerable<AuthorDtoResponse>>>> GetAll(PaginationFilter filter, string route)
        {
            var listAuthor = await _unitOfWork.AuthorRepository.GetAllAsync(filter);
            if (!listAuthor.Any())
                return Result<PagedResult<IEnumerable<AuthorDtoResponse>>>.Failure(AuthorErrors.NotFound);

            IEnumerable<AuthorDtoResponse> listAuthorDto = _mapper.Map<IEnumerable<AuthorDtoResponse>>(listAuthor);

            int totalRecords = await _unitOfWork.AuthorRepository.CountAllRecords();

            var pagedResponse = PagedResponseHelper.CreatePagedResponse(
                pagedData: listAuthorDto.ToList(),
                filter: filter,
                totalRecords: totalRecords,
                uriService: _uriService,
                route
            );

            return pagedResponse;
        }



        public async Task<Result<IEnumerable<AuthorDtoWithBooksResponse>>> GetAllWithBooks(PaginationFilter filter, string route)
        {
            IEnumerable<Author> authorWithBooks = await _unitOfWork.AuthorRepository.GetAllWithBooks(filter);
            if (authorWithBooks.Count() == 0)
                return Result<IEnumerable<AuthorDtoWithBooksResponse>>.Failure(AuthorErrors.NotFound);

            IEnumerable<AuthorDtoWithBooksResponse> authorWithBookDto = _mapper.Map<IEnumerable<AuthorDtoWithBooksResponse>>(authorWithBooks);
            return Result<IEnumerable<AuthorDtoWithBooksResponse>>.Success(authorWithBookDto);
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
        public async Task<Result<AuthorDtoResponse>> Diseble(AuthorDtoDisableRequest authorDisableDto)
        {
            Author author = _mapper.Map<Author>(authorDisableDto);
            Author authorEntity = await _unitOfWork.AuthorRepository.GetByIdAsync(author);
            if (authorEntity is null)
                return Result<AuthorDtoResponse>.Failure(AuthorErrors.NotFound);
            _mapper.Map(authorDisableDto, authorEntity);
            _unitOfWork.AuthorRepository.Disable(authorEntity);
            await _unitOfWork.CommitAsync();
            AuthorDtoResponse authorDtoResponse = _mapper.Map<AuthorDtoResponse>(authorEntity);
            return Result<AuthorDtoResponse>.Success(authorDtoResponse);

        }

        public async Task<Result<AuthorDtoResponse>> Create(CreateAuthorCommand command)
        {
            Author author = _mapper.Map<Author>(command);
            await _unitOfWork.AuthorRepository.CreateAsync(author);
            await _unitOfWork.CommitAsync();
            AuthorDtoResponse authorDtoResponse = _mapper.Map<AuthorDtoResponse>(author);
            return Result<AuthorDtoResponse>.Success(authorDtoResponse);

        }


    }
}
//public async Task<Result<IEnumerable<AuthorDtoResponse>>> GetAll(PaginationFilter validFilter)
//{
//    var listAutor = await _unitOfWork.AuthorRepository.GetAllAsync(validFilter);
//    if (listAutor.Count() == 0)
//        return Result<IEnumerable<AuthorDtoResponse>>.Failure(AuthorErrors.NotFound);

//    IEnumerable<AuthorDtoResponse> listAuthorDto = _mapper.Map<IEnumerable<AuthorDtoResponse>>(listAutor);
//    return Result<IEnumerable<AuthorDtoResponse>>.Success(listAuthorDto);
//}