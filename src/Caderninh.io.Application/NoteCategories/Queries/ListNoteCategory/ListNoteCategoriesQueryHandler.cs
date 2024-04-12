

using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Queries.ListNoteCategory
{
    public class ListNoteCategoriesQueryHandler(
        IUsersRepository usersRepository,
        INoteCategoriesRepository noteCategoryRepository,
        ICurrentUserProvider currentUserProvider)
            : IRequestHandler<ListNoteCategoriesQuery, ErrorOr<List<NoteCategory>>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly INoteCategoriesRepository _noteCategoryRepository = noteCategoryRepository;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;

        public async Task<ErrorOr<List<NoteCategory>>> Handle(ListNoteCategoriesQuery query, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var user = await _usersRepository.GetByIdAsync(query.UserId);

            if (currentUser.Id != query.UserId)
                return Error.Unauthorized("User is forbidden from taking this action.");

            if (user is null)
                return Error.NotFound("User not found.");

            return await _noteCategoryRepository.ListByUserIdAsync(query.UserId);
        }
    }
}