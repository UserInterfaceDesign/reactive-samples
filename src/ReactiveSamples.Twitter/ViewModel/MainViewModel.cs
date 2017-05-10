using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Collections.ObjectModel;

using ReactiveSamples.Twitter.Common;
using ReactiveSamples.Twitter.Services;
using ReactiveSamples.Twitter.Model;

namespace ReactiveSamples.Twitter.ViewModel
{
    public class MainViewModel : NotificationBase, IMainViewModel
    {
        #region Fields

        private readonly IEmotionService emotionService;

        private Subject<IObservable<IFeedItem>> selectedFeed;
        private EmotionInfo currentEmotion;

        #endregion

        #region Constructor

        public MainViewModel(IEmotionService service)
        {
            emotionService = service;

            Emotions = new ObservableCollection<EmotionInfo>();
            selectedFeed = new Subject<IObservable<IFeedItem>>();

            SubscribeToEmotions();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets all available emotions.
        /// </summary>
        public ObservableCollection<EmotionInfo> Emotions { get; }

        /// <summary>
        /// Gets the current feed related to the <see cref="CurrentEmotion"/>.
        /// </summary>
        public IObservable<IObservable<IFeedItem>> CurrentFeed => selectedFeed.AsObservable();

        /// <summary>
        /// Gets or sets the current emotion.
        /// </summary>
        public EmotionInfo CurrentEmotion
        {
            get { return currentEmotion; }
            set
            {
                if (currentEmotion == value)
                    return;

                currentEmotion = value;
                RaisePropertyChanged();

                selectedFeed.OnNext(currentEmotion.Feed);
            }
        }

        #endregion

        #region Methods

        private void SubscribeToEmotions()
        {
            Emotions.Add(new EmotionInfo(Emotion.Angry, emotionService.ListenTo(Emotion.Angry)));
            Emotions.Add(new EmotionInfo(Emotion.Crying, emotionService.ListenTo(Emotion.Crying)));
            Emotions.Add(new EmotionInfo(Emotion.Happy, emotionService.ListenTo(Emotion.Happy)));
            Emotions.Add(new EmotionInfo(Emotion.Lovely, emotionService.ListenTo(Emotion.Lovely)));
            Emotions.Add(new EmotionInfo(Emotion.Smiling, emotionService.ListenTo(Emotion.Smiling)));
            Emotions.Add(new EmotionInfo(Emotion.Worried, emotionService.ListenTo(Emotion.Worried)));
        }

        #endregion

    }
}
