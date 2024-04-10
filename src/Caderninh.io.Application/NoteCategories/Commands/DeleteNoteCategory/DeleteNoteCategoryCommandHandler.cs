using Caderninh.io.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Commands.DeleteNoteCategory
{
    public class DeleteNoteCategoryCommandHandler(
        IUsersRepository usersRepository,
        INoteCategoryRepository noteCategoryRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<DeleteNoteCategoryCommand, ErrorOr<Deleted>>
    {

        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly INoteCategoryRepository _noteCategoryRepository = noteCategoryRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteNoteCategoryCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (currentUser.Id != command.UserId)
                return Error.Unauthorized("User is forbidden from taking this action.");

            var noteCategory = await _noteCategoryRepository.GetByIdAsync(command.NoteCategoryId);

            if (noteCategory is null)
                return Error.NotFound("Note Category not found.");

            if (noteCategory.UserId != currentUser.Id)
                return Error.Unauthorized("User is forbidden from taking this action.");

            // TODO - Add Eventual Consistency for also deleting notes bellow an NoteCategory when deleting a NoteCategory

            await _noteCategoryRepository.RemoveAsync(noteCategory);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}