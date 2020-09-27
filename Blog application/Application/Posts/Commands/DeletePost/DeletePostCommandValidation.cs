using FluentValidation;

namespace Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandValidation : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidation()
        {
            RuleFor(c => c.)
        }
    }
}