namespace ReactiveSamples.Twitter.Model
{
    /// <summary>
    /// Generic item from a social media feed.
    /// </summary>
    public class FeedItem : IFeedItem
    {
        /// <summary>
        /// URL to the profile picture of the creator.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the actual name of the creator.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the nickname of the creator.
        /// </summary>
        public string Handle { get; set; }

        /// <summary>
        /// Gets or sets the message in plain text.
        /// </summary>
        public string Text { get; set; }
    }
}
