using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Queries.ListNoteCategory
{
    public record ListNoteCategoriesQuery(Guid UserId) : IRequest<ErrorOr<List<NoteCategory>>>;
}