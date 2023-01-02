using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;

namespace FUNDOO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        FundooContext fundooContext;
        ICollabBusiness collabBusiness;

        public CollabController(FundooContext fundooContext, ICollabBusiness collabBusiness)
        {
            this.fundooContext = fundooContext;
            this.collabBusiness = collabBusiness;
        }
        [HttpPost("AddCollab")]
        public ActionResult AddCollab(long NoteId,string Email)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result=collabBusiness.AddCollab(UserId,NoteId,Email);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Collab Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Collab Adding Failed"});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetCollab")]
        public ActionResult GetCollab(long NoteId) 
        {
            try
            {
                var result=collabBusiness.GetCollab(NoteId);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Getting Collabs", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To Get Collabs" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("RemoveCollab")]
        public ActionResult RemoveCollab(long CollabId)
        {
            try
            {
                var result=collabBusiness.RemoveCollab(CollabId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Collab Removed Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Can't Remove Collab" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
