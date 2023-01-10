using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        FundooContext fundooContext;
        ILabelBusiness labelBusiness;
        public LabelController(FundooContext fundooContext, ILabelBusiness labelBusiness)
        {
            this.fundooContext = fundooContext;
            this.labelBusiness = labelBusiness;
        }
        [HttpPost("AddLabel")]
        public ActionResult AddLabel(long NoteId, string LabelName)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBusiness.AddLabel(UserId, NoteId, LabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label Adding Failed", data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetLabels")]
        public ActionResult GetLabels(long LabelId)
        {
            try
            {
                var result = labelBusiness.GetLabels(LabelId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting Labels", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Getting Labels Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateLabel")]
        public ActionResult UpdateLabel(long LabelId, string newLabelName)
        {
            try
            {
                var result = labelBusiness.UpdateLabel(LabelId, newLabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Updated", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label Updation Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteLabel")]
        public ActionResult DeleteLabel(long LabelId)
        {
            try
            {
                var result=labelBusiness.DeleteLabel(LabelId);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Label Deleted", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label Deletion Failed"});
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
