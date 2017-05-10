using ReactiveSamples.Twitter.Model;
using ReactiveSamples.Twitter.Services.Social;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using Tweetinvi.Core.Extensions;

namespace ReactiveSamples.Twitter.Services
{
    public class EmotionService : IEmotionService
    {
        #region Fields

        private const string ANGRY = "😠";
        private const string CRYING = "😢";
        private const string HAPPY = "😃";
        private const string LOVELY = "😍";
        private const string SMILING = "🙂";
        private const string WORRIED = "😟";

        private bool isStreaming;

        private IDictionary<IProvider, IDisposable> currentSubscriptions;
        private IList<IProvider> providers;
        private Subject<IFeedItem> fullStream;

        #endregion

        #region Constructor

        public EmotionService()
        {
            fullStream = new Subject<IFeedItem>();
            providers = new List<IProvider>();
            currentSubscriptions = new Dictionary<IProvider, IDisposable>();
        }

        #endregion

        #region Properties

        public IObservable<IFeedItem> Emotions => fullStream.AsObservable();

        #endregion

        #region Methods

        public void AddProvider(IProvider provider)
        {
            IDisposable subscription = null;

            if (isStreaming)
                subscription = provider.Feed.Subscribe(fullStream.OnNext);
            else
                providers.Add(provider);

            if (!currentSubscriptions.ContainsKey(provider))
                currentSubscriptions.Add(provider, subscription);
            else
                currentSubscriptions[provider] = subscription;
        }

        public void RemoveProvider(IProvider provider)
        {
            if (currentSubscriptions.ContainsKey(provider)) {
                currentSubscriptions[provider]?.Dispose();
                currentSubscriptions.Remove(provider);
            }
        }

        public IObservable<IFeedItem> ListenTo(Emotion emotion)
        {
            if (emotion == Emotion.Angry)
                return Emotions.Where(item => item.Text.Contains(ANGRY));

            if (emotion == Emotion.Crying)
                return Emotions.Where(item => item.Text.Contains(CRYING));

            if (emotion == Emotion.Happy)
                return Emotions.Where(item => item.Text.Contains(HAPPY));

            if (emotion == Emotion.Lovely)
                return Emotions.Where(item => item.Text.Contains(LOVELY));

            if (emotion == Emotion.Smiling)
                return Emotions.Where(item => item.Text.Contains(SMILING));

            if (emotion == Emotion.Worried)
                return Emotions.Where(item => item.Text.Contains(WORRIED));

            return Observable.Empty<IFeedItem>();
        }

        public void ExplicitStart()
        {
            isStreaming = true;

            providers.ForEach(AddProvider);
            providers.Clear();
        }

        public void ExplicitStop()
        {
            isStreaming = false;

            currentSubscriptions.ForEach(pair => providers.Add(pair.Key));

            currentSubscriptions
                .ToList()
                .ForEach(pair => RemoveProvider(pair.Key));
        }

        #endregion
    }
}
