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
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooContext fundooContext;
        public LabelRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public bool AddLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                var result = this.fundooContext.Notes.FirstOrDefault(e => e.NoteId == NoteId);
                if (result != null) 
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.UserId = UserId;
                    labelEntity.NoteId = NoteId;
                    labelEntity.LabelName = LabelName;
                    this.fundooContext.Label.Add(labelEntity);
                    this.fundooContext.SaveChanges();
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
        public List<LabelEntity> GetLabels(long LabelId)
        {
            try
            {
                var result = this.fundooContext.Label.Where(e => e.LabelId == LabelId).ToList();
                if (result != null)
                {
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
        public bool UpdateLabel(long LabelId, string NewLabelName)
        {
            try
            {
                var result = fundooContext.Label.FirstOrDefault(e => e.LabelId == LabelId);
                if (result != null)
                {
                    result.LabelName = NewLabelName;
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
        public bool DeleteLabel(long LabelId)
        {
            try
            {
                var result=fundooContext.Label.FirstOrDefault(e=>e.LabelId== LabelId);
                if(result!=null)
                {
                    fundooContext.Label.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex) 
            {
                throw ex; 
            }
        }
    }
}
