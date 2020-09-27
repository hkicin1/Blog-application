using FluentValidation;

namespace Application.Likes.Commands.AddPostLike
{
    public class AddPostLikeCommandValidator : AbstractValidator<AddPostLikeCommand>
    {
        public AddPostLikeCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}