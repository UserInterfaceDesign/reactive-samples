using ReactiveSamples.Twitter.Common;
using ReactiveSamples.Twitter.Services;
using ReactiveSamples.Twitter.Services.Social;
using System;
using System.Reactive.Linq;

namespace ReactiveSamples.Twitter.ViewModel
{
    public class ShellViewModel : NotificationBase, IShellViewModel
    {
        #region Fields

        private readonly IEmotionService emotionService;
        private readonly IProvider twitter;
        private readonly IProvider synthetic;

        private bool isSimulating;
        private bool isStreaming;
        private ulong totalTweets;

        #endregion

        #region Constructor

        public ShellViewModel(IEmotionService service)
        {
            emotionService = service;

            twitter = new TwitterProvider();
            synthetic = new SyntheticProvider();

            Subscribe();

            RaisePropertyChanged(nameof(IsSimulating));
            IsStreaming = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the realtime service is simulated or not.
        /// </summary>
        public bool IsSimulating
        {
            get { return isSimulating; }
            set
            {
                if (isSimulating == value)
                    return;

                isSimulating = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether the stream is active or not.
        /// </summary>
        public bool IsStreaming
        {
            get { return isStreaming; }
            set
            {
                isStreaming = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the total amount of tweets received.
        /// </summary>
        public ulong TotalTweets
        {
            get { return totalTweets; }
            set
            {
                totalTweets = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void Subscribe()
        {
            emotionService.Emotions.Subscribe(_ => TotalTweets++);

            this.OnPropertyChanges(vm => vm.IsSimulating)
                .Throttle(TimeSpan.FromSeconds(1))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    if (x)
                    {
                        emotionService.AddProvider(synthetic);
                        emotionService.RemoveProvider(twitter);
                    }
                    else
                    {
                        emotionService.AddProvider(twitter);
                        emotionService.RemoveProvider(synthetic);
                    }
                });

            this.OnPropertyChanges(vm => vm.IsStreaming)
                .Throttle(TimeSpan.FromSeconds(1))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    if (x)
                        emotionService.ExplicitStart();
                    else
                    {
                        emotionService.ExplicitStop();
                    }
                });
        }

        #endregion

    }
}
