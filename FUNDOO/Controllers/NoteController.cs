using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Models;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        FundooContext fundooContext;
        INoteBusiness noteBusiness;
        private readonly ILogger<NoteController> logger;

        public NoteController(FundooContext fundooContext, INoteBusiness noteBusiness, ILogger<NoteController> logger)
        {
            this.fundooContext = fundooContext;
            this.noteBusiness = noteBusiness;
            this.logger = logger;
            logger.LogDebug(1, "NLog injected into NoteController");
        }
        [HttpPost("AddNotes")]
        public ActionResult AddNotes(NotesModel notesModel)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.AddNote(notesModel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllNotes")]
        public ActionResult GetAllNotes()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.GetAllNotes(UserId);
                if (result != null)
                {
                    logger.LogInformation("Get User Notes SuccessFull");
                    return this.Ok(new { success = true, message = "Getting User Notes", data = result });
                }
                else
                {
                    logger.LogInformation("Failed To Get User Notes");
                    return this.BadRequest(new { success = false, message = "Failed To Load Notes" });
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("UpdateNotes")]
        public ActionResult UpdateNotes(long NoteId, NotesModel notesModel)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.UpdateNotes(NoteId, UserId, notesModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Updating Notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Update Notes" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteNote")]
        public ActionResult DeleteNote(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.DeleteNote(UserId, NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Delete Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsPin")]
        public ActionResult IsPin(long NoteId)
        {
            try
            {
                var result = noteBusiness.IsPinOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Pinned Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Pin Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsArchive")]
        public ActionResult IsArchive(long NoteId)
        {
            try
            {
                var result = noteBusiness.IsArchiveOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Archived Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Archive Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsTrash")]
        public ActionResult IsTrash(long NoteId)
        {
            try
            {
                var result = noteBusiness.IsTrashOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Trashed Successfully"});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Trash Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateColor")]
        public ActionResult UpdateColor(long NoteId, string Color)
        {
            try
            {
                var result = noteBusiness.UpdateColor(NoteId, Color);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Color Updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Color Updation Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("DisplayTrashedNotes")]
        public ActionResult DisplayTrashedNotes()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.DisplayTrashedNotes(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting Trashed Notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To Load Trashed Notes" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteTrashedNote")]
        public ActionResult DeleteTrashedNote(long NoteId)
        {
            try
            {
                var result = noteBusiness.DeleteTrashedNote(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Deleted Permanently", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("DisplayArchivedNotes")]
        public ActionResult DisplayArchivedNotes()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.DisplayArchivedNotes(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting Archived Notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To Load Archived Notes" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("SetReminder")]
        public ActionResult SetReminder(long NoteId, DateTime dateTime)
        {
            try
            {
                var result = noteBusiness.SetReminder(NoteId, dateTime);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Reminder Setted", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To Set Reminder" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("UploadImage")]
        public ActionResult UploadImage(long NoteId, IFormFile img)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBusiness.UploadImage(NoteId, UserId, img);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Image Uploaded Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To Upload Image" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
