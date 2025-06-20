﻿using MediatR;
using Microsoft.VisualBasic;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Shared.Exceptions;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Users.Messaging.ToggleConversationLike;

public class ToggleConversationStarCommandHandler : ICommandHandler<ToggleConversationStarCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public ToggleConversationStarCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }


    public async Task<Unit> Handle(ToggleConversationStarCommand request, CancellationToken cancellationToken)
    {
        var currentUserIdAndMode = _userService.GetCurrentUserIdAndMode();

        if (request.MessageId.HasValue)
        {
            var conversationsQueryable = _repository.GetAllIncluding<Conversation>(x => x.Messages)
           .Where(x => (x.ParticipantAId == currentUserIdAndMode.UserId && x.ParticipantAMode == currentUserIdAndMode.Mode) || (x.ParticipantBId == currentUserIdAndMode.UserId && x.ParticipantBMode == currentUserIdAndMode.Mode));

            conversationsQueryable = conversationsQueryable.Where(x => x.Messages.Any(x => x.Id == request.MessageId));
            var conversation = await _repository.FirstOrDefaultAsync(conversationsQueryable);
            if (conversation == null)
            {
                throw new NotFoundException(nameof(Conversation), "Conversation not found for the given message ID.");
            }

            if (conversation.ParticipantAId == currentUserIdAndMode.UserId && conversation.ParticipantAMode == currentUserIdAndMode.Mode)
            {
                conversation.IsStarredByParticipantA = !conversation.IsStarredByParticipantA;
            }
            else if (conversation.ParticipantBId == currentUserIdAndMode.UserId && conversation.ParticipantBMode == currentUserIdAndMode.Mode)
            {
                conversation.IsStarredByParticipantB = !conversation.IsStarredByParticipantB;
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have permission to star this conversation.");
            }
            await _repository.UpdateAsync(conversation);
        }
        else if (request.ConversationId.HasValue)
        {
            var conversation = await _repository.GetByIdAsync<Conversation, int>(request.ConversationId.Value);
            if (conversation == null)
            {
                throw new NotFoundException(nameof(Conversation), request.ConversationId.Value);
            }

            if (conversation.ParticipantAId == currentUserIdAndMode.UserId && conversation.ParticipantAMode == currentUserIdAndMode.Mode)
            {
                conversation.IsStarredByParticipantA = !conversation.IsStarredByParticipantA;
            }
            else if (conversation.ParticipantBId == currentUserIdAndMode.UserId && conversation.ParticipantBMode == currentUserIdAndMode.Mode)
            {
                conversation.IsStarredByParticipantB = !conversation.IsStarredByParticipantB;
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have permission to star this conversation.");
            }
            await _repository.UpdateAsync(conversation);
        }

        return Unit.Value;
    }
}
