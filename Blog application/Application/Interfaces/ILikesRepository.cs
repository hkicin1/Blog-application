using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILikesRepository
    {
        Task AddLike(int id, Guid userId);

        Task RemoveLike(int id, Guid userId);
    }
}