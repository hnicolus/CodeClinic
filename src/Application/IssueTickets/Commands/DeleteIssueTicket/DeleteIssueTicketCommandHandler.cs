﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeClinic.Application.Common.Exceptions;
using CodeClinic.Application.Common.Interfaces;
using CodeClinic.Domain.Entities;
using MediatR;


namespace CodeClinic.Application.Issues.Commands.DeleteIssue
{
    public class DeleteIssueTicketCommandHandler : IRequestHandler<DeleteIssueTicketCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteIssueTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteIssueTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.IssueTickets.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(IssueTicket), request.Id);
            }

            _context.IssueTickets.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
