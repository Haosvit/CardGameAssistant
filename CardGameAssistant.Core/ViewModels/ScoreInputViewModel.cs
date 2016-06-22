using CardGameAssistant.Core.EventArgs;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGameAssistant.Core.ViewModels
{
    public class ScoreInputViewModel : MvxViewModel
    {
        public event EventHandler ScoreChanged;
        public event EventHandler ScoreInputClicked;

        #region Properties
        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value; RaisePropertyChanged(() => Score);
                RaiseScoreChangedEvent(value);
            }
        }


        private bool _isEditting;
        public bool IsEditting
        {
            get { return _isEditting; }
            set { _isEditting = value; RaisePropertyChanged("IsEditting"); }
        }
        #endregion

        #region Members
        private MatchScoresItemViewModel parent;
        #endregion

        #region Commands

        private ICommand _upButtonCommand;
        public ICommand UpButtonCommand { get { return _upButtonCommand; } }

        private ICommand _downButtonCommand;

        public ICommand DownButtonCommand { get { return _downButtonCommand; } }


        private ICommand _scoreInputCommand;
        public ICommand ScoreInputCommand
        {
            get { return _scoreInputCommand; }
        }
        private byte DEFAULT_SCORE;
        #endregion

        public ScoreInputViewModel(int score, MatchScoresItemViewModel matchScoresItemViewModel)
        {
            Score = score;
            IsEditting = true;
            parent = matchScoresItemViewModel;
            parent.EdittingStateChanged += OnParentEdittingStateChanged;
            InitCommandMethods();
        }

        private void OnParentEdittingStateChanged(object sender, EdittingStateEventArg e)
        {
            IsEditting = e.IsEditting;
        }

        private void InitCommandMethods()
        {
            _upButtonCommand = new MvxCommand(OnUpButtonCommand);
            _downButtonCommand = new MvxCommand(OnDownButtonCommand);
            _scoreInputCommand = new MvxCommand(OnScoreInputCommand);
        }

        private void OnScoreInputCommand()
        {
            RaiseScoreInputClickEvent();
        }

        private void RaiseScoreInputClickEvent()
        {
            var handlers = ScoreInputClicked;
            if (handlers != null)
            {
                handlers(this, System.EventArgs.Empty);
            }
        }

        private void OnDownButtonCommand()
        {
            Score--;
        }

        private void OnUpButtonCommand()
        {
            Score++;
        }


        private void RaiseScoreChangedEvent(int score)
        {
            var handlers = ScoreChanged;
            if (handlers != null)
            {
                handlers(this, new ScoreChangedEventArg(score));
            }
        }
    }
}
