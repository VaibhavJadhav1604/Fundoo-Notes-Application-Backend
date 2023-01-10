using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }
        public virtual UserTableEntity UserTable { get; set; }

        [ForeignKey("NoteEntity")]
        public long NoteId { get; set; }
        public virtual NoteEntity NoteEntity { get; set; }
    }
}
