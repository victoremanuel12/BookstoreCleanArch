﻿using Application.Dtos.Author;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.Author.Command.CreateAuthorCommand;

public sealed record CreateAuthorCommand(string Nome, bool IsActive) : IRequest<Result<AuthorDtoResponse>>;