using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler(
        INoteCategoriesRepository noteCategoriesRepository,
        INotesRepository notesRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<CreateNoteCommand, ErrorOr<Note>>
    {
        private readonly INotesRepository _notesRepository = notesRepository;
        private readonly INoteCategoriesRepository _noteCategoriesRepository = noteCategoriesRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Note>> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var noteCategory = await _noteCategoriesRepository.GetByIdAsync(command.NoteCategoryId);

            if (noteCategory is null)
                return Error.NotFound("Note category not found");

            if (noteCategory.UserId != currentUser.Id)
                return Error.Unauthorized("User is forbidden from taking this action.");

            var note = new Note(
                noteCategoryId: command.NoteCategoryId,
                body: command.Body);

            await _notesRepository.AddNoteAsync(note);
            await _unitOfWork.CommitChangesAsync();

            return note;
        }
    }
}