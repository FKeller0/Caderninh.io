using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caderninh.io.Application.NoteCategories.Commands.DeleteNoteCategory
{
    public record DeleteNoteCategoryCommand(Guid UserId, Guid NoteCategoryId) : IRequest<ErrorOr<Deleted>>;   
}