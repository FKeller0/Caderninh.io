using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Commands.CreateNoteCategory
{
    public record CreateNoteCategoryCommand(string Name, Guid UserId) : IRequest<ErrorOr<NoteCategory>>;
}