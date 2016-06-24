using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.BindingContext;
using CardGameAssistant.Core.ViewModels;
using Android.Views.InputMethods;

namespace CardsGameAssistant.Droid.Views
{
    [Activity]
    public class HomeView : MvxActivity
    {

        private MvxListView _matchesListView;

        private View _currentFocusView;
        private bool _hasItemAdded;
        public bool HasItemAdded 
        {
            get { return _hasItemAdded; }
            set 
            {
                _hasItemAdded = value;
                if (value == true)
                {
                    RollListToBottom();
                }
            } 
        }

        private bool _isFocusChanged;
        public bool IsFocusChanged
        {
            get { return _isFocusChanged; }
            set 
            {
                _isFocusChanged = value;
                if (value)
                {
                    _currentFocusView = CurrentFocus;
                    _currentFocusView.FocusChange += OnFocusChanged;
                }
            }
        }

        private void OnFocusChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                _currentFocusView.RequestFocus();
                _currentFocusView.FocusChange -= OnFocusChanged;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.home_view);
            SetTitle(Resource.String.ApplicationName);
            CreateBinding();
            _matchesListView = (MvxListView)FindViewById(Resource.Id.lv_matches);

        }

        private void CreateBinding()
        {
            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind().For(v => v.HasItemAdded).To(vm => vm.HasItemAdded);
            set.Bind().For(v => v.IsFocusChanged).To(vm => vm.IsFocusChanged);
            set.Apply();
        }

        private void RollListToBottom()
        {
            _matchesListView.SmoothScrollToPosition(_matchesListView.Count);
        }

    }
}