using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Commands.CreateNoteCategory
{
    public class CreateNoteCategoryCommandHandler(
        IUsersRepository usersRepository,
        INoteCategoriesRepository noteCategoryRepository,
        ICurrentUserProvider currentUserProvider,
        IUnitOfWork unitOfWork)
            : IRequestHandler<CreateNoteCategoryCommand, ErrorOr<NoteCategory>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly INoteCategoriesRepository _noteCategoryRepository = noteCategoryRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<NoteCategory>> Handle(CreateNoteCategoryCommand command, CancellationToken cancellationToken)
        {            
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (currentUser.Id != command.UserId)             
                return Error.Unauthorized("User is forbidden from taking this action.");            

            var user = await _usersRepository.GetByIdAsync(command.UserId);

            if (user is null)            
                return Error.NotFound(description: "User not found");            

            var noteCategory = new NoteCategory(
                name: command.Name,
                userId: user.Id);            

            await _noteCategoryRepository.AddNoteCategoryAsync(noteCategory);
            await _unitOfWork.CommitChangesAsync();

            return noteCategory;
        }
    }
}