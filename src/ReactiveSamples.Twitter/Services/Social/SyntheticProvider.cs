using ReactiveSamples.Twitter.Model;
using System;
using System.Reactive.Linq;

namespace ReactiveSamples.Twitter.Services.Social
{
    /// <summary>
    /// Provides synthetic (fake) data.
    /// </summary>
    public class SyntheticProvider : IProvider
    {
        private const string DEFAULT_AVATAR = "pack://application:,,,/ReactiveSamples.Twitter;component/Assets/twitter_default_avatar.jpg";

        private readonly string[] texts = 
        {
            "some test 😢",
            "some other test 😢  #doubleTest",
            "enumerables make me feel so 😟",
            "reactive + LINQ + schedulers = 😍  #sogreat #rxNET",
            "😠 !!!",
            "Why do Java developers have to wear glasses? Because they do not C# 😃",
            "What is a programmers favourite bar? Foo bar. 😃",
            "exceptions make me mad 😠",
            "The truth is out there. Anybody got the URL? 🙂",
            "I resist the temptation of using emojis..."
        };

        private Random random = new Random();

        public IObservable<IFeedItem> Feed
        {
            get
            {
                return Observable.Interval(TimeSpan.FromSeconds(1))
                    .Select(_ => new FeedItem
                    {
                        AvatarUrl = DEFAULT_AVATAR,
                        Handle = "@test",
                        Name = "test",
                        Text = texts[random.Next(0, texts.Length)]
                    })
                    .Publish()
                    .RefCount();
            }
        }
    }
}
