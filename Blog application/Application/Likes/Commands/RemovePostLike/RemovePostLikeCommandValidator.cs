using FluentValidation;

namespace Application.Likes.Commands.RemovePostLike
{
    public class RemovePostLikeCommandValidator : AbstractValidator<RemovePostLikeCommand>
    {
        public RemovePostLikeCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}