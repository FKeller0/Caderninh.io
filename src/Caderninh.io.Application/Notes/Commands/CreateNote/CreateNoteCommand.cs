using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Commands.CreateNote
{
    public record CreateNoteCommand(string Body, Guid NoteCategoryId) : IRequest<ErrorOr<Note>>;    
}