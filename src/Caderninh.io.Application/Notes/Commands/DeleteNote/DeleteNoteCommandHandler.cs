using Caderninh.io.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler(
        INotesRepository notesRepository,
        INoteCategoriesRepository noteCategoriesRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<DeleteNoteCommand, ErrorOr<Deleted>>
    {
        private readonly INotesRepository _notesRepository = notesRepository;
        private readonly INoteCategoriesRepository _noteCategoriesRepository = noteCategoriesRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var noteCategory = await _noteCategoriesRepository.GetByIdAsync(command.NoteCategoryId);
            var note = await _notesRepository.GetByIdAsync(command.NoteId);

            if (noteCategory is null)
                return Error.NotFound("Note Category not found.");

            if (currentUser.Id != noteCategory.UserId)
                return Error.Unauthorized("User is forbidden from taking this action.");

            if (note is null)
                return Error.NotFound("Note not found.");

            await _notesRepository.RemoveAsync(note);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}