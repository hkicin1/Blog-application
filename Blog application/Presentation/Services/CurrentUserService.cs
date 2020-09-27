using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Presentation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            var userId = contextAccessor.HttpContext?.User?.Claims?
                .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
                .Select(v => v.Value).FirstOrDefault();
            UserId = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
            Email = contextAccessor.HttpContext?.User?.Claims?
                .Where(claim => claim.Type == ClaimTypes.Email)
                .Select(v => v.Value).FirstOrDefault();
            IsAuthenticated = UserId != null;
        }

        public Guid UserId { get; set; }

        public string Email { get; }

        public bool IsAuthenticated { get; }
    }
}
