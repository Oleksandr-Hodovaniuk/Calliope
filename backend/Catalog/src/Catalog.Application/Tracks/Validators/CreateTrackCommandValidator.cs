using Catalog.Application.Tracks.Commands;
using FluentValidation;

namespace Catalog.Application.Tracks.Validators;

public class CreateTrackCommandValidator : AbstractValidator<CreateTrackCommand>
{
    public CreateTrackCommandValidator()
    {
        RuleFor(x => x.dto.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
    