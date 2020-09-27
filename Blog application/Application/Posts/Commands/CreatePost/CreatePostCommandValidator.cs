using FluentValidation;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidation : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidation()
        {
            RuleFor(c=> c.Title)
                .NotEmpty();
            RuleFor(c => c.Body)
                .NotEmpty();
        }
    }
}