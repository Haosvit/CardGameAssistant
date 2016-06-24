using CardGameAssistant.Core.EventArgs;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGameAssistant.Core.ViewModels
{
    public class MatchScoresItemViewModel : MvxViewModel
    {
        public static MatchScoresItemViewModel Current { get; set; }

        public event EventHandler ScoresChanged;
        public event EventHandler<EdittingStateEventArg> EdittingStateChanged;
        public event EventHandler FocusChanged;
        private const byte NUM_OF_PLAYERS = 4;
        private const byte DEFAULT_SCORE = 0;
        #region Properties
        public int MatchNumber { get; set; }

        private int _totalMatchScore;
        public int TotalMatchScore
        {
            get { return _totalMatchScore; }
            set { _totalMatchScore = value; RaisePropertyChanged("TotalMatchScore"); }
        }


        private bool _isEdditting;
        public bool IsEditting
        {
            get { return _isEdditting; }
            set             
            { 
                _isEdditting = value; 
                RaisePropertyChanged("IsEditting");
                RaiseEdittingStateChangedEvent(); 
            }
        }
        private ObservableCollection<ScoreInputViewModel> _scoreInputItems;
        public ObservableCollection<ScoreInputViewModel> ScoreInputItems
        {
            get { return _scoreInputItems; }
            set { _scoreInputItems = value; RaisePropertyChanged("ScoreInputItems"); }
        }
        #endregion
        #region Commands

        private ICommand _itemClickCommand;
        public ICommand ItemClickCommand
        {
            get { return _itemClickCommand; }
            set { _itemClickCommand = value; RaisePropertyChanged("ItemClickCommand"); }
        }


        #endregion
        public MatchScoresItemViewModel(int matchNumber)
        {
            MatchNumber = matchNumber;
            InitScoreInputItems();
            _itemClickCommand = new MvxCommand(OnItemClickCommand);
        }

        private void OnItemClickCommand()
        {
            UpdateCurrent();
            if (FocusChanged != null)
            {
                FocusChanged(this, System.EventArgs.Empty);
            }
        }

        private void UpdateCurrent()
        {
            Current.IsEditting = false;
            Current = this;
            IsEditting = true;
        }

        private void RaiseEdittingStateChangedEvent()
        {
            var handlers = EdittingStateChanged;
            if (handlers != null)
            {
                handlers(this, new EdittingStateEventArg(IsEditting));
            }
        }

        private void InitScoreInputItems()
        {
            ScoreInputItems = new ObservableCollection<ScoreInputViewModel>();
            for (var i = 0; i < NUM_OF_PLAYERS; i++)
            {
                var item = new ScoreInputViewModel(DEFAULT_SCORE, this);
                ScoreInputItems.Add(item);
                item.ScoreChanged += OnScoreChanged;
                item.ScoreInputClicked += OnScoreItemClicked;
            }
        }

        private void OnScoreItemClicked(object sender, System.EventArgs e)
        {
            UpdateCurrent();
        }

        private void OnScoreChanged(object sender, System.EventArgs e)
        {
            RaiseScoresChanged();
        }       


        private void RaiseScoresChanged()
        {
            var handlers = ScoresChanged;
            if (handlers != null)
            {
                handlers(this, System.EventArgs.Empty);
            }
        }
    }
}
