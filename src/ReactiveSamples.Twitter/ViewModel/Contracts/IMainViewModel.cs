using ReactiveSamples.Twitter.Model;
using System;

namespace ReactiveSamples.Twitter.ViewModel
{
    public interface IMainViewModel
    {
        IObservable<IObservable<IFeedItem>> CurrentFeed { get; }
    }
}
