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
    public class CollabBusiness:ICollabBusiness
    {
        ICollabRepo collabRepo;

        public CollabBusiness(ICollabRepo collabRepo)
        {
            this.collabRepo = collabRepo;
        }

        public CollabEntity AddCollab(long UserId, long NoteId, string Email)
        {
            try
            {
                return this.collabRepo.AddCollab(UserId, NoteId, Email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<CollabEntity> GetCollab(long NoteId)
        {
            try
            {
                return this.collabRepo.GetCollab(NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveCollab(long CollabId)
        {
            try
            {
                return this.collabRepo.RemoveCollab(CollabId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
