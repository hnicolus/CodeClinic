﻿using CodeClinic.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CodeClinic.Application.IssueTickets.Commands.UpdateIssueTicket
{

    public partial class UpdateIssueTicketCommandHandler
    {
        public class UpdateIssueTicketCommandValidator : AbstractValidator<UpdateIssueTicketCommand>
        {
            private readonly IApplicationDbContext _context;


            public UpdateIssueTicketCommandValidator(IApplicationDbContext context)
            {
                RuleFor(s => s.Stars)
                    .GreaterThanOrEqualTo(0).NotNull().NotEmpty()
                    .WithMessage("Star Rating Cannot be less than Zero");

                RuleFor(a => a.Status)
                    .NotNull().NotEmpty()
                    .WithMessage("Status cannot be null or empty");

                RuleFor(c => c.CategoryId).NotNull().GreaterThan(0).MustAsync(HaveAnExistingCategory)
         .WithMessage("Choose a Category that exists in the system");
                _context = context;
            }

            public async Task<bool> HaveAnExistingCategory(UpdateIssueTicketCommand request, int CategoryId, CancellationToken cancellationToken)
            {
                return await _context.Categories.AnyAsync(c => c.Id == CategoryId);
            }
        }
    }
}
