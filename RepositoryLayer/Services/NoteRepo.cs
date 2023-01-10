using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NoteRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public NoteEntity AddNote(NotesModel notesModel, long UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = notesModel.Title;
                noteEntity.Note = notesModel.Note;
                noteEntity.Reminder = notesModel.Reminder;
                noteEntity.Color = notesModel.Color;
                noteEntity.Image = notesModel.Image;
                noteEntity.IsArchive = notesModel.IsArchive;
                noteEntity.IsPin = notesModel.IsPin;
                noteEntity.IsTrash = notesModel.IsTrash;
                noteEntity.UserId = UserId;
                noteEntity.Created = notesModel.Created;
                noteEntity.Modified = notesModel.Modified;
                fundooContext.Notes.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
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
                var allnotes = fundooContext.Notes.Where(n => n.UserId == UserId).ToList();
                if (allnotes != null)
                {
                    return allnotes;
                }
                else
                {
                    return null;
                }
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
                var updatenotes = fundooContext.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);
                if (updatenotes != null)
                {
                    if (notesModel.Title != null)
                    {
                        updatenotes.Title = notesModel.Title;
                    }
                    if (notesModel.Note != null)
                    {
                        updatenotes.Note = notesModel.Note;
                    }
                    updatenotes.Modified = DateTime.Now;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var deletenotes = fundooContext.Notes.FirstOrDefault(n => n.NoteId == NoteId);
                if (deletenotes != null)
                {
                    fundooContext.Notes.Remove(deletenotes);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if (result.IsPin != null)
                {
                    result.IsPin = !result.IsPin;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsPin = !result.IsPin;
                    return true;
                }
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
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if (result.IsArchive != null)
                {
                    result.IsArchive = !result.IsArchive;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsArchive = !result.IsArchive;
                    return true;
                }
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
                NoteEntity result = fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if (result.IsTrash != null)
                {
                    result.IsTrash = !result.IsTrash;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsTrash = !result.IsTrash;
                    return true;
                }
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
                var result = this.fundooContext.Notes.FirstOrDefault(e => e.NoteId == NoteId);
                if (result != null)
                {
                    result.Color = Color;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
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
                var result = this.fundooContext.Notes.FirstOrDefault(e => e.NoteId == NoteId);
                if (result.IsTrash == true)
                {
                    this.fundooContext.Notes.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.IsTrash = true;
                    fundooContext.SaveChanges();
                    return false;
                }
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
                var allnotes = fundooContext.Notes.Where(n => n.UserId == UserId && n.IsArchive == true).ToList();
                if (allnotes != null)
                {
                    return allnotes;
                }
                else
                {
                    return null;
                }
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
                var allnotes = fundooContext.Notes.Where(n => n.UserId == UserId && n.IsTrash == true).ToList();
                if (allnotes != null)
                {
                    return allnotes;
                }
                else
                {
                    return null;
                }
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
                NoteEntity noteEntity = fundooContext.Notes.FirstOrDefault(e => e.NoteId == NoteId);
                if (noteEntity.Reminder != null)
                {
                    noteEntity.Reminder = dateTime;
                    return noteEntity;
                }
                else
                {
                    return null;
                }
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
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteId == NoteId && e.UserId == UserId);
                if (result != null)
                {
                    Account acc = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                        this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]);
                    Cloudinary cloudinary = new Cloudinary(acc);
                    var uploadpic = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadresult = cloudinary.Upload(uploadpic);
                    string imgpath = uploadresult.Url.ToString();
                    result.Image = imgpath;
                    fundooContext.SaveChanges();
                    return "Image Uploaded Successfully";
                }
                else
                {
                    return "Failed To Upload Image";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
