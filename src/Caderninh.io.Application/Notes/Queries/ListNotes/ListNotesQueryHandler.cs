using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Queries.ListNotes
{
    public class ListNotesQueryHandler(
        INotesRepository notesRepository,
        INoteCategoriesRepository noteCategoriesRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<ListNotesQuery, ErrorOr<List<Note>>>
    {
        private readonly INotesRepository _notesRepository = notesRepository;
        private readonly INoteCategoriesRepository _noteCategoriesRepository = noteCategoriesRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<List<Note>>> Handle(ListNotesQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var noteCategory = await _noteCategoriesRepository.GetByIdAsync(query.NoteCategoryId);

            if (noteCategory is null)
                return Error.NotFound("Note Category not found.");

            if (currentUser.Id != noteCategory.UserId)
                return Error.Unauthorized("User is forbidden from taking this action.");

            var notes = await _notesRepository.ListByNoteCategoryIdAsync(query.NoteCategoryId);

            return notes;
        }
    }
}