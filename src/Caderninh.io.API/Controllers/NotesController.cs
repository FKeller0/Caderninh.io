using Caderninh.io.Application.Notes.Commands.CreateNote;
using Caderninh.io.Application.Notes.Commands.DeleteNote;
using Caderninh.io.Application.Notes.Commands.UpdateNote;
using Caderninh.io.Application.Notes.Queries.GetNote;
using Caderninh.io.Application.Notes.Queries.ListNotes;
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

        [HttpGet]
        public async Task<IActionResult> ListNotes(Guid noteCategoryId)
        {
            var query = new ListNotesQuery(noteCategoryId);

            var listNotesResult = await _mediator.Send(query);

            return listNotesResult.Match(
                note => Ok(note.ConvertAll(note => new NoteResponse(note.Id, note.Body))),
                Problem);
        }

        [HttpPut("{noteId:guid}")]
        public async Task<IActionResult> UpdateNote(string body, Guid noteId, Guid noteCategoryId ) 
        {
            var command = new UpdateNoteCommand(
                body,
                noteId,
                noteCategoryId);

            var updateNoteResult = await _mediator.Send(command);

            return updateNoteResult.Match(
                note => Ok(new NoteResponse(note.Id, note.Body)),
                Problem);

        }

        [HttpDelete("{noteId:guid}")]
        public async Task<IActionResult> DeleteNote(Guid noteCategoryId, Guid noteId)
        {
            var command = new DeleteNoteCommand(
                noteCategoryId,
                noteId);

            var deleteNoteResult = await _mediator.Send(command);

            return deleteNoteResult.Match(
                _ => NoContent(),
                Problem);
        }
    }
}