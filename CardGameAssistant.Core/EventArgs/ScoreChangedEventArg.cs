using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameAssistant.Core.EventArgs
{
    public class ScoreChangedEventArg : System.EventArgs
    {
        public int Score { get; set; }
        public ScoreChangedEventArg(int score)
        {
            Score = score;
        }
    }
}
