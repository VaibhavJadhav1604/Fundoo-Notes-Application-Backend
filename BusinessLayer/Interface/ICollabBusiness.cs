﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollabBusiness
    {
        public CollabEntity AddCollab(long UserId, long NoteId, string Email);
        public List<CollabEntity> GetCollab(long NoteId);
        public bool RemoveCollab(long CollabId);
    }
}
