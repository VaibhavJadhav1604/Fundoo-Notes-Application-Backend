using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRepo
    {
        public NoteEntity AddNote(NotesModel notesModel, long UserId);
        public List<NoteEntity> GetAllNotes(long UserId);
        public bool UpdateNotes(long NoteId, long UserId, NotesModel notesModel);
        public bool DeleteNote(long UserId, long NoteId);
        public bool IsPinOrNot(long NoteId);
        public bool IsArchiveOrNot(long NoteId);
        public bool IsTrashOrNot(long NoteId);
        public NoteEntity UpdateColor(long NoteId, string Color);
        public bool DeleteTrashedNote(long NoteId);
        public List<NoteEntity> DisplayArchivedNotes(long UserId);
        public List<NoteEntity> DisplayTrashedNotes(long UserId);
        public NoteEntity SetReminder(long NoteId, DateTime dateTime);
        public string UploadImage(long NoteId, long UserId, IFormFile img);
    }
}
