using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.NoteCategories.Queries.GetNoteCategory
{
    public class GetNoteCategoryQueryHandler(
        IUsersRepository usersRepository,
        INoteCategoriesRepository noteCategoryRepository)
            : IRequestHandler<GetNoteCategoryQuery, ErrorOr<NoteCategory>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly INoteCategoriesRepository _noteCategoryRepository = noteCategoryRepository;

        public async Task<ErrorOr<NoteCategory>> Handle(GetNoteCategoryQuery request, CancellationToken cancellationToken)
        {
            if (await _usersRepository.ExistsByIdAsync(request.UserId))           
                return Error.NotFound("User not found");            

            if (await _noteCategoryRepository.GetByIdAsync(request.NoteCategoryId) is not NoteCategory noteCategory)            
                return Error.NotFound(description: "Note Category not found");            

            return noteCategory;
        }
    }
}