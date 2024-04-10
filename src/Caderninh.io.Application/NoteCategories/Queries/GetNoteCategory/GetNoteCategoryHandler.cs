using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Queries.GetNoteCategory
{
    public class GetNoteCategoryHandler(
        IUsersRepository usersRepository,
        INoteCategoryRepository noteCategoryRepository)
            : IRequestHandler<GetNoteCategoryQuery, ErrorOr<NoteCategory>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly INoteCategoryRepository _noteCategoryRepository = noteCategoryRepository;

        public async Task<ErrorOr<NoteCategory>> Handle(GetNoteCategoryQuery request, CancellationToken cancellationToken)
        {
            if (await _usersRepository.ExistsByIdAsync(request.UserId))
            {
                return Error.NotFound("Subscription not found");
            }

            if (await _noteCategoryRepository.GetByIdAsync(request.NoteCategoryId) is not NoteCategory noteCategory)
            {
                return Error.NotFound(description: "Note Category not found");
            }

            return noteCategory;
        }
    }
}