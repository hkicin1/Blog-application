using FluentValidation;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidation : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidation()
        {
            RuleFor(c => c.Title)
                .NotEmpty();
            RuleFor(c => c.Body)
                .NotEmpty();
        }
    }
}