using MediatR;
using PhoneFinancialManagment.ApplicationServices.Command;
using PhoneFinancialManagment.Domain.Domains;

namespace PhoneFinancialManagment.ApplicationServices.CommandHandler;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserDomainContext _context;

    public DeleteUserCommandHandler(IUserDomainContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users.First(x => x.UserId == request.Id);
        user.IsDeleted = true;
        _context.Users.Update(user);
        int result = await _context.SaveChangesAsync();

        return result == 1;
    }
}