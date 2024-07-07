using MediatR;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.Domain.Domains;
using PhoneFinancialManagment.Domain.Dtos;
using PhoneFinancialManagment.Domain.Models;

namespace PhoneFinancialManagment.ApplicationServices.CommandHandler;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto?>
{
    private readonly IUserDomainContext _context;

    public CreateUserCommandHandler(IUserDomainContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            Name = request.Name,
            IsUserVerified = false,
            Balance = request.Balance,
        };

        _context.Users.Add(newUser);
        var isSaved = _context.SaveChanges() == 1;

        if (isSaved)
        {
            return await Task.FromResult(new UserDto
            {
                Id = newUser.UserId,
                Balance = newUser.Balance,
                IsVerified = newUser.IsUserVerified,
                Name = request.Name
            });
        }
        
        return null;
    }
}