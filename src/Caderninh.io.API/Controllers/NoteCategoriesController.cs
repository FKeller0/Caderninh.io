using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Caderninh.io.Contracts.NoteCategories;
using Caderninh.io.Application.NoteCategories.Commands.CreateNoteCategory;
using Caderninh.io.Application.NoteCategories.Commands.DeleteNoteCategory;
using Caderninh.io.Application.NoteCategories.Queries.GetNoteCategory;
using Caderninh.io.Application.NoteCategories.Queries.ListNoteCategory;


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

        [HttpGet]
        public async Task<IActionResult> ListNoteCategories(Guid userId) 
        {
            var query = new ListNoteCategoriesQuery(userId);

            var listNoteCategoriesQueryResult = await _mediator.Send(query);

            return listNoteCategoriesQueryResult.Match(
                noteCategories => Ok(noteCategories.ConvertAll(noteCategory => new NoteCategoryResponse(noteCategory.Id, noteCategory.Name))),
                Problem);
        }

        [HttpDelete("{noteCategoryId:guid}")]
        public async Task<IActionResult> DeleteNoteCategory(Guid userId, Guid noteCategoryId)
        {
            var command = new DeleteNoteCategoryCommand(userId, noteCategoryId);

            var deleteNoteCategoryResult = await _mediator.Send(command);

            return deleteNoteCategoryResult.Match(
                _ => NoContent(),
                Problem);
        }
    }
}