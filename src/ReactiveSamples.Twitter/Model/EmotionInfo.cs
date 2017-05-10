using ReactiveSamples.Twitter.Common;
using System;
using System.Reactive.Linq;

namespace ReactiveSamples.Twitter.Model
{
    /// <summary>
    /// Represents real time information for an emotion.
    /// </summary>
    public class EmotionInfo : NotificationBase
    {
        #region Fields

        private ulong usage;

        #endregion

        #region Constructors

        public EmotionInfo(Emotion emotion, IObservable<IFeedItem> feed)
        {
            Emotion = emotion;
            Feed = feed;

            feed.ObserveOnDispatcher()
                .Subscribe(_ => Usage++);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the feed containing only feed items for this emotion.
        /// </summary>
        public IObservable<IFeedItem> Feed { get; }

        /// <summary>
        /// Gets or sets the emotion.
        /// </summary>
        public Emotion Emotion { get; }

        /// <summary>
        /// Gets or sets the total amount of usages of the emotion.
        /// </summary>
        public ulong Usage
        {
            get { return usage; }
            set
            {
                usage = value;
                RaisePropertyChanged();
            }
        }

        #endregion

    }
}
