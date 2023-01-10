using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }
        public virtual UserTableEntity UserTable { get; set; }

        [ForeignKey("NoteEntity")]
        public long NoteId { get; set; }
        public virtual NoteEntity NoteEntity { get; set; }
    }
}
