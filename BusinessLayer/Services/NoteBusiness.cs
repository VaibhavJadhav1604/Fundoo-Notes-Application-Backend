using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBusiness : INoteBusiness
    {
        INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public NoteEntity AddNote(NotesModel notesModel, long UserId)
        {
            try
            {
                return this.noteRepo.AddNote(notesModel, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NoteEntity> GetAllNotes(long UserId)
        {
            try
            {
                return this.noteRepo.GetAllNotes(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateNotes(long NoteId, long UserId, NotesModel notesModel)
        {
            try
            {
                return this.noteRepo.UpdateNotes(NoteId, UserId, notesModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteNote(long UserId, long NoteId)
        {
            try
            {
                return this.noteRepo.DeleteNote(UserId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsPinOrNot(long NoteId)
        {
            try
            {
                return this.noteRepo.IsPinOrNot(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsArchiveOrNot(long NoteId)
        {
            try
            {
                return this.noteRepo.IsArchiveOrNot(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsTrashOrNot(long NoteId)
        {
            try
            {
                return this.noteRepo.IsTrashOrNot(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity UpdateColor(long NoteId, string Color)
        {
            try
            {
                return this.noteRepo.UpdateColor(NoteId, Color);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteTrashedNote(long NoteId)
        {
            try
            {
                return this.noteRepo.DeleteTrashedNote(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NoteEntity> DisplayArchivedNotes(long UserId)
        {
            try
            {
                return this.noteRepo.DisplayArchivedNotes(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NoteEntity> DisplayTrashedNotes(long UserId)
        {
            try
            {
                return this.noteRepo.DisplayTrashedNotes(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity SetReminder(long NoteId, DateTime dateTime)
        {
            try
            {
                return this.noteRepo.SetReminder(NoteId, dateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UploadImage(long NoteId, long UserId, IFormFile img)
        {
            try
            {
                return this.noteRepo.UploadImage(NoteId, UserId, img);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
