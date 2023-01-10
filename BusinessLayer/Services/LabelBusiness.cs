using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBusiness : ILabelBusiness
    {
        ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }
        public bool AddLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                return this.labelRepo.AddLabel(UserId, NoteId, LabelName);
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
                return this.labelRepo.GetLabels(LabelId);
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
                return this.labelRepo.UpdateLabel(LabelId, NewLabelName);
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
                return this.labelRepo.DeleteLabel(LabelId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
