using Caderninh.io.Domain.Notes;
using FluentValidation;

namespace Caderninh.io.Application.Notes.Commands.CreateNote
{
    public class CreateNoteValidator : AbstractValidator<Note>
    {
        public CreateNoteValidator() 
        {
            RuleFor(n => n.Body).NotEmpty();
        }
    }
}