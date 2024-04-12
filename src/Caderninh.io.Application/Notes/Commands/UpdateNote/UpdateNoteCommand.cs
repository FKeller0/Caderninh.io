using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Commands.UpdateNote
{
    public record UpdateNoteCommand(string Body, Guid NoteId, Guid NoteCategoryId) : IRequest<ErrorOr<Note>>;
}