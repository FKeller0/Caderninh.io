using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Queries.GetNote
{
    public class GetNoteQueryHandler(
        INotesRepository notesRepository,
        INoteCategoriesRepository noteCategoriesRepository,
        ICurrentUserProvider currentUserProvider)
            : IRequestHandler<GetNoteQuery, ErrorOr<Note>>
    {
        private readonly INotesRepository _notesRepository = notesRepository;
        private readonly INoteCategoriesRepository _noteCategoriesRepository = noteCategoriesRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;

        public async Task<ErrorOr<Note>> Handle(GetNoteQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var noteCategory = await _noteCategoriesRepository.GetByIdAsync(query.NoteCategoryId);

            if (noteCategory is null)
                return Error.NotFound("Note category not found");

            if (currentUser.Id != noteCategory.UserId)
                return Error.Unauthorized("User is forbidden from taking this action");

            if (await _notesRepository.GetByIdAsync(query.NoteId) is not Note note)
                return Error.NotFound(description: "Note not found");

            return note;
        }
    }
}