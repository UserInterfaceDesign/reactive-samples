using ReactiveSamples.Twitter.Common;
using ReactiveSamples.Twitter.Model;

using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace ReactiveSamples.Twitter.ViewModel
{
    public class DetailsViewModel : NotificationBase, IDetailsViewModel
    {
        #region Fields

        private IDisposable subscription;
        private bool isIntroVisible;

        #endregion

        #region Constructor

        public DetailsViewModel(IMainViewModel mainViewModel)
        {
            Items = new ObservableCollection<IFeedItem>();
            IsIntroVisible = true;

            mainViewModel.CurrentFeed.Subscribe(feed =>
            {
                IsIntroVisible = false;
                subscription?.Dispose();
                Items.Clear();

                subscription = feed.ObserveOnDispatcher().Subscribe(item => Items.Insert(0, item));
            });

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items of the feed.
        /// </summary>
        public ObservableCollection<IFeedItem> Items { get; set; }

        /// <summary>
        /// Gets or sets whether an explanatory info text is visible.
        /// </summary>
        public bool IsIntroVisible
        {
            get { return isIntroVisible; }
            set
            {
                isIntroVisible = value;
                RaisePropertyChanged();
            }
        }

        #endregion

    }
}
    