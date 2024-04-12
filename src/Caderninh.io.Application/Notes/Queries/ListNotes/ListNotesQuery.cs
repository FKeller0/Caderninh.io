using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Queries.ListNotes
{
    public record ListNotesQuery(Guid NoteCategoryId) : IRequest<ErrorOr<List<Note>>>;
}