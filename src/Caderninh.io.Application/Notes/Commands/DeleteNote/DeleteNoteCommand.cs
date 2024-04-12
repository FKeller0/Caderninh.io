using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Commands.DeleteNote
{
    public record DeleteNoteCommand(Guid NoteCategoryId, Guid NoteId) : IRequest<ErrorOr<Deleted>>;
}