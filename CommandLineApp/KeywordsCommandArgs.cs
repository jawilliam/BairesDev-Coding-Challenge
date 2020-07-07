using CommandDotNet;

namespace CommandLineApp
{
    /// <summary>
    /// Provides data for commands as <see cref="PeopleCommands.RoleKeywords(PeopleListCommandArgs)"/> 
    /// and <see cref="PeopleCommands.IndustryKeywords(KeywordsCommandArgs)"/>.
    /// </summary>
    public class KeywordsCommandArgs : PeopleListCommandArgs 
    {
        /// <summary>
        /// Gets or sets the full path of the "people.in" file.
        /// </summary>
        [Option(LongName = "view", Description = "how to show, or what information must show, regarding the requested keywords: [Distinct | PerRow | Frequencies].")]
        public virtual string View { get; set; } = "Distinct";
    }
}
