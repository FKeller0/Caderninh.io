using Caderninh.io.Application.NoteCategories.Commands.CreateNoteCategory;
using Caderninh.io.Application.NoteCategories.Queries.GetNoteCategory;
using Caderninh.io.Contracts.NoteCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Caderninh.io.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class NoteCategoriesController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateNoteCategory(
            CreateNoteCategoryRequest request,
            Guid userId) 
        {
            var command = new CreateNoteCategoryCommand(
                request.Name,
                userId);

            var createNoteCategoryResult = await _mediator.Send(command);

            return createNoteCategoryResult.Match(
                noteCategory => CreatedAtAction(
                    nameof(GetNoteCategory),
                    new { userId, NoteCategoryId = noteCategory.Id },
                    new NoteCategoryResponse(noteCategory.Id, noteCategory.Name)),
                Problem);
        }

        [HttpGet("{noteCategoryId:guid}")]
        public async Task<IActionResult> GetNoteCategory(Guid userId, Guid noteCategoryId)
        {
            var command = new GetNoteCategoryQuery(userId, noteCategoryId);

            var getNoteCategoryResult = await _mediator.Send(command);

            return getNoteCategoryResult.Match(
                noteCategory => Ok(new NoteCategoryResponse(noteCategory.Id, noteCategory.Name)),
                Problem);
        }
    }
}