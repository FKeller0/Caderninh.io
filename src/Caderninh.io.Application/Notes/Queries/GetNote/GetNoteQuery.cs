using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Queries.GetNote
{
    public record GetNoteQuery(Guid NoteCategoryId, Guid NoteId) : IRequest<ErrorOr<Note>>;
}