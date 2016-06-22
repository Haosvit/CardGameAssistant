using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameAssistant.Core.EventArgs
{
    public class EdittingStateEventArg : System.EventArgs
    {
        public bool IsEditting { get; set; }
        public EdittingStateEventArg(bool isEditting)
        {
            IsEditting = isEditting;
        }
    }
}
