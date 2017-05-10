using ReactiveSamples.Twitter.Model;
using System;

namespace ReactiveSamples.Twitter.Services.Social
{
    /// <summary>
    /// Container for data from social networks.
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Gets a generic feed from a social network.
        /// </summary>
        IObservable<IFeedItem> Feed { get; }
    }
}
