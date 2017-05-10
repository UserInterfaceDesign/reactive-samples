using ReactiveSamples.Twitter.Model;
using ReactiveSamples.Twitter.Services.Social;
using System;

namespace ReactiveSamples.Twitter.Services
{
    /// <summary>
    /// Merges different providers into a single stream of emotions.
    /// </summary>
    public interface IEmotionService
    {
        /// <summary>
        /// Adds a new <see cref="IProvider"/> to the service. Merged into <see cref="Emotions"/>.
        /// </summary>
        /// <param name="provider"></param>
        void AddProvider(IProvider provider);

        /// <summary>
        /// Removed an existing <see cref="IProvider"/> from the service.
        /// </summary>
        /// <param name="provider"></param>
        void RemoveProvider(IProvider provider);

        /// <summary>
        /// Forces the service to start the providers.
        /// </summary>
        void ExplicitStart();

        /// <summary>
        /// Forces the service to stop the providers.
        /// </summary>
        void ExplicitStop();

        /// <summary>
        /// Listen to a specific emotion.
        /// </summary>
        /// <param name="emotion">The emotion to listen to.</param>
        /// <returns>An observable stream with the requested emotion.</returns>
        IObservable<IFeedItem> ListenTo(Emotion emotion);

        /// <summary>
        /// All emotions from the current providers.
        /// </summary>
        IObservable<IFeedItem> Emotions { get; }

    }
}
