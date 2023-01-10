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
    public class CollabRepo:ICollabRepo
    {
        FundooContext fundooContext;
        IConfiguration configuration;
        public CollabRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public CollabEntity AddCollab(long UserId, long NoteId, string Email)
        {
            try
            {
                CollabEntity collabEntity= new CollabEntity();
                collabEntity.CollabEmail= Email;
                collabEntity.UserId= UserId;
                collabEntity.NoteId= NoteId;
                collabEntity.ModifiedDate= DateTime.Now;
                fundooContext.Collab.Add(collabEntity);
                int result= fundooContext.SaveChanges();
                if(result != null )
                {
                    return collabEntity;
                }
                else
                {
                    return null;
                }
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
                var result=this.fundooContext.Collab.Where(e=>e.NoteId==NoteId).ToList();
                return result;
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
                var result=this.fundooContext.Collab.FirstOrDefault(e=>e.CollabId==CollabId);
                if(result!=null)
                {
                    fundooContext.Remove(result);
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
