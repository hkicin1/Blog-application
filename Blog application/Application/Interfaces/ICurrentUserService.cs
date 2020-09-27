using System;

namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; set; }

        string Email { get; }

        bool IsAuthenticated { get; }
    }
}
