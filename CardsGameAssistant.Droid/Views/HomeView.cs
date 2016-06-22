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

namespace CardsGameAssistant.Droid.Views
{
    [Activity]
    public class HomeView : MvxActivity
    {

        private MvxListView _matchesListView;

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
            set.Apply();
        }


        private void RollListToBottom()
        {
            _matchesListView.SmoothScrollToPosition(_matchesListView.Count);
        }
    }
}