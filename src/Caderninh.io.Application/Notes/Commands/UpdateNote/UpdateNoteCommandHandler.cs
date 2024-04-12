using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler(
        INotesRepository notesRepository,
        INoteCategoriesRepository noteCategoriesRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<UpdateNoteCommand, ErrorOr<Note>>
    {
        private readonly INotesRepository _notesRepository = notesRepository;
        private readonly INoteCategoriesRepository _noteCategoriesRepository = noteCategoriesRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Note>> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var noteCategory = await _noteCategoriesRepository.GetByIdAsync(command.NoteCategoryId);

            if (noteCategory is null)
                return Error.NotFound("Note Category not found.");

            if (currentUser.Id != noteCategory.UserId)
                return Error.Unauthorized("User is forbidden from taking this action.");

            var note = await _notesRepository.GetByIdAsync(command.NoteId);

            if (note is null)
                return Error.NotFound("Note not found.");

            note.Body = command.Body;
            note.UpdatedAt = DateTime.UtcNow;

            await _notesRepository.UpdateAsync(note);
            await _unitOfWork.CommitChangesAsync();

            return note;
        }
    }
}