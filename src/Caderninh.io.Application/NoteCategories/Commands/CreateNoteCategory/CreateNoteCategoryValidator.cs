using Caderninh.io.Domain.Notes;
using FluentValidation;

namespace Caderninh.io.Application.NoteCategories.Commands.CreateNoteCategory
{
    public class CreateNoteCategoryValidator : AbstractValidator<NoteCategory>
    {
        public CreateNoteCategoryValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);
        }
    }
}