using Caderninh.io.Application.Notes.Commands.CreateNote;
using Caderninh.io.Application.Notes.Queries.GetNote;
using Caderninh.io.Contracts.Notes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caderninh.io.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class NotesController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateNote(
            CreateNoteRequest request,
            Guid noteCategoryId) 
        {
            var command = new CreateNoteCommand(
                request.Body,
                noteCategoryId);

            var createNoteResult = await _mediator.Send(command);

            return createNoteResult.Match(
                note => CreatedAtAction(
                    nameof(GetNote),
                    new { noteCategoryId, NoteId = note.Id },
                    new NoteResponse(note.Id, note.Body)),
                Problem);
        }

        [HttpGet("{noteId:guid}")]
        public async Task<IActionResult> GetNote(Guid noteCategoryId, Guid noteId) 
        {
            var query = new GetNoteQuery(
                noteCategoryId,
                noteId);

            var getNoteResult = await _mediator.Send(query);

            return getNoteResult.Match(
                note => Ok(new NoteResponse(note.Id, note.Body)),
                Problem);
        }
    }
}