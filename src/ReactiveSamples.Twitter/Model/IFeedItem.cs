namespace ReactiveSamples.Twitter.Model
{
    /// <summary>
    /// Generic item from a social media feed.
    /// </summary>
    public interface IFeedItem
    {
        /// <summary>
        /// URL to the profile picture of the creator.
        /// </summary>
        string AvatarUrl { get; }

        /// <summary>
        /// Gets or sets the actual name of the creator.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the nickname of the creator.
        /// </summary>
        string Handle { get; }

        /// <summary>
        /// Gets or sets the message in plain text.
        /// </summary>
        string Text { get; }
    }
}
