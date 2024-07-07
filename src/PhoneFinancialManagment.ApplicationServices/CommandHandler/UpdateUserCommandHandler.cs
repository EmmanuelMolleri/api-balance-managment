using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;

namespace PhoneFinancialManagment.ApplicationServices.CommandHandler;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserDomainContext _context;

    public UpdateUserCommandHandler(IUserDomainContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.Id);

        user.Name = request.Name;

        _context.Users.Update(user);
        _context.SaveChanges();

        return new UserDto
        {
            Id = user.UserId,
            Name = user.Name,
            Balance = user.Balance,
            IsVerified = user.IsUserVerified
        };
    }
}