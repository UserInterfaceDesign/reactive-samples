using ReactiveSamples.Twitter.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using Tweetinvi;
using Tweetinvi.Events;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace ReactiveSamples.Twitter.Services.Social
{
    /// <summary>
    /// Provides tweets from Twitter's service in realtime.
    /// </summary>
    public class TwitterProvider : IProvider
    {
        #region Fields

        private ISampleStream stream;
        private const string AT = "@";

        #endregion

        #region Properties

        public IObservable<IFeedItem> Feed
        {
            get
            {
                return Observable.Create((IObserver<FeedItem> obs) =>
                {
                    ITwitterCredentials credentials;
                    if (!GetCredentials(out credentials))
                    {
                        obs.OnCompleted();
                        return Disposable.Empty;
                    }

                    stream = Stream.CreateSampleStream(credentials);

                    var disconnected = Observable.FromEventPattern<DisconnectedEventArgs>(stream, "DisconnectMessageReceived");
                    var limited = Observable.FromEventPattern<LimitReachedEventArgs>(stream, "LimitReached");

                    var subscription = Observable.FromEventPattern<TweetReceivedEventArgs>(stream, "TweetReceived")
                        .Select(args => CreateFeedItem(args.EventArgs.Tweet))
                        .TakeUntil(disconnected)
                        .TakeUntil(limited)
                        .SubscribeOn(ThreadPoolScheduler.Instance)
                        .Subscribe(obs);

                    stream.StartStreamAsync().ConfigureAwait(false);

                    return new CompositeDisposable(subscription, Disposable.Create(() => stream.StopStream()));
                })
                .Publish()
                .RefCount();
            }
        }

        #endregion

        #region Methods

        private bool GetCredentials(out ITwitterCredentials credentials)
        {
            var keys = GetKeysFromConfig();

            if(keys.Any())
            {
                credentials = new TwitterCredentials(keys["ConsumerKey"], keys["ConsumerSecret"], keys["AccessToken"], keys["AccessTokenSecret"]);
                return true;
            }
            credentials = null;
            return false;
        }

        private IDictionary<string, string> GetKeysFromConfig()
        {
            var keys = new Dictionary<string, string>();

            if (System.IO.File.Exists("Keys.cfg"))
            {
                var values = System.IO.File.ReadAllText("Keys.cfg").Split(new char[] { '\r', '\n', '=' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < values.Length; i++)
                {
                    keys.Add(values[i++], values[i]);
                }
            }

            return keys;
        }

        private FeedItem CreateFeedItem(ITweet tweet)
        {
            return new FeedItem
            {
                AvatarUrl = tweet.CreatedBy.ProfileImageUrl,
                Handle = $"{AT}{tweet.CreatedBy.ScreenName}",
                Name = tweet.CreatedBy.Name,
                Text = tweet.Text
            };
        }

        #endregion
    }
}
