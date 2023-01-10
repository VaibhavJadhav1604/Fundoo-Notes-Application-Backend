using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepo
    {
        public bool AddLabel(long UserId, long NoteId, string LabelName);
        public List<LabelEntity> GetLabels(long LabelId);
        public bool UpdateLabel(long LabelId, string NewLabelName);
        public bool DeleteLabel(long LabelId);
    }
}
