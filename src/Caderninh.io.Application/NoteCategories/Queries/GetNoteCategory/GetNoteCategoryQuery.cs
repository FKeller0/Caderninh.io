using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Queries.GetNoteCategory
{
    public record GetNoteCategoryQuery(Guid UserId, Guid NoteCategoryId) : IRequest<ErrorOr<NoteCategory>>;
}