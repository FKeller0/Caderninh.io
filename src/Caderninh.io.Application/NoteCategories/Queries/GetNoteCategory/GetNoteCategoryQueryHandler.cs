using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Queries.GetNoteCategory
{
    public class GetNoteCategoryQueryHandler(
        IUsersRepository usersRepository,
        INoteCategoriesRepository noteCategoryRepository,
        ICurrentUserProvider currentUserProvider)
            : IRequestHandler<GetNoteCategoryQuery, ErrorOr<NoteCategory>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly INoteCategoriesRepository _noteCategoryRepository = noteCategoryRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;

        public async Task<ErrorOr<NoteCategory>> Handle(GetNoteCategoryQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var user = await _usersRepository.GetByIdAsync(query.UserId);

            if (user is null)
                return Error.NotFound("User not found.");

            if (currentUser.Id != query.UserId)
                return Error.Unauthorized("User is forbidden from taking this action.");

            if (await _noteCategoryRepository.GetByIdAsync(query.NoteCategoryId) is not NoteCategory noteCategory)            
                return Error.NotFound(description: "Note Category not found");            

            return noteCategory;
        }
    }
}