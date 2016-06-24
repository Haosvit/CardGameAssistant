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
    public class HomeViewModel : MvxViewModel
    {

        #region Members
        private int _matchNumber = 0;
        #endregion
        #region Properties

        private bool _isFinish;
        public bool IsFinish
        {
            get { return _isFinish; }
            set { _isFinish = value; RaisePropertyChanged(() => IsFinish); }
        }


        private bool _isFocusChanged;
        public bool IsFocusChanged
        {
            get { return _isFocusChanged; }
            set { _isFocusChanged = value; RaisePropertyChanged("IsFocusChanged"); }
        }        

        public string AppName { get { return "CARDS GAME ASSISTANT"; } }

        private string _finishOrNewGame;
        public string FinishOrNewGame { get { return _finishOrNewGame; } set { _finishOrNewGame = value; RaisePropertyChanged(() => FinishOrNewGame); } }

        public List<string> Players { get; set; }

        private List<int> _totals;
        public List<int> Totals
        {
            get { return _totals; }
            set { _totals = value; RaisePropertyChanged("Totals"); }
        }

        private ObservableCollection<MatchScoresItemViewModel> _matchScoresItemViewModels; 
        public ObservableCollection<MatchScoresItemViewModel> MatchScoresItemViewModels 
        {
            get {return _matchScoresItemViewModels; }
            set
            {
                _matchScoresItemViewModels = value;
                RaisePropertyChanged(() => MatchScoresItemViewModels);
            }
        }


        private bool _hasItemAdded;
        public bool HasItemAdded
        {
            get { return _hasItemAdded; }
            set { _hasItemAdded = value; RaisePropertyChanged("HasItemAdded"); }
        }


        #endregion

        #region Commands
        private ICommand _addOneCommand;
        public ICommand AddOneCommand { get { return _addOneCommand; } }

        private ICommand _finishCommand;

        public ICommand FinishCommand
        {
            get { return _finishCommand; }
            set { _finishCommand = value; }
        }

        #endregion
        public HomeViewModel()
        {
            FinishOrNewGame = "FINISH";
            IsFinish = false;
            Players = new List<string>();
            Totals = new List<int>();
            InitCommandMethods();
            MatchScoresItemViewModels = new ObservableCollection<MatchScoresItemViewModel>();
            AddOneMatchScoreItem();
        }

        private void InitCommandMethods()
        {
            _addOneCommand = new MvxCommand(OnAddOneCommand);
            _finishCommand = new MvxCommand(OnFinishCommand);
        }

        private void OnFinishCommand()
        {
            CalculateTotals();
            IsFinish = !_isFinish;
            if (IsFinish) {
               
                FinishOrNewGame = "NEW GAME";        
            }
            else
            {
                PrepareNewGame();
                FinishOrNewGame = "FINISH";
            }            
        }

        private void PrepareNewGame()
        {
            MatchScoresItemViewModels.Clear();
            _matchNumber = 0;
            Totals = new List<int>();
            AddOneMatchScoreItem();
        }

        private void OnAddOneCommand()
        {
            AddOneMatchScoreItem();
        }

        private void AddOneMatchScoreItem()
        {
            var matches = MatchScoresItemViewModels;
            var newMatch = new MatchScoresItemViewModel(++_matchNumber);
            newMatch.ScoresChanged += OnScoresChanged;
            newMatch.FocusChanged += OnFocusChanged;
            matches.Add(newMatch);

            HasItemAdded = true;

            MatchScoresItemViewModels = matches;
            if (MatchScoresItemViewModel.Current != null)
            {
                MatchScoresItemViewModel.Current.IsEditting = false;
            }
            MatchScoresItemViewModel.Current = newMatch;
        }

        private void OnFocusChanged(object sender, System.EventArgs e)
        {
            IsFocusChanged = true;
        }

        void OnScoresChanged(object sender, System.EventArgs e)
        {
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            int[] tTotals = new int[4];

            foreach (var match in MatchScoresItemViewModels)
            {
                var tTotal = 0;
                for (var i = 0; i < 4; i++)
                {
                    tTotals[i] += match.ScoreInputItems[i].Score;
                }
            }
            Totals = new List<int>(tTotals);
        }
    }
}
