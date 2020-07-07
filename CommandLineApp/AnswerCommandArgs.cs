using CommandDotNet;

namespace CommandLineApp
{
    /// <summary>
    /// Provides data for the command <see cref="PeopleCommands.Answer(AnswerCommandArgs)"/>.
    /// </summary>
    public class AnswerCommandArgs : PeopleListCommandArgs
    {
        /// <summary>
        /// Gets or sets the full path of the "people.out" file.
        /// </summary>
        [Option(LongName = "out", Description = "Full path of the people.out file.")]
        public virtual string OutFullPath { get; set; }
            = @$"{System.Environment.CurrentDirectory}\..\..\..\..\CommandLineApp\DataLake\people.out";

        /// <summary>
        /// Gets or sets the full path of the "RelevantKeywords.Role.txt" file.
        /// </summary>
        [Option(LongName = "keywords-role", Description = "Full path of the RelevantKeywords.Role.txt file.")]
        public virtual string RoleKeywordsFullPath { get; set; }
            = @$"{System.Environment.CurrentDirectory}\..\..\..\..\CommandLineApp\DataLake\RelevantKeywords.Role.txt";

        /// <summary>
        /// Gets or sets the full path of the "RelevantKeywords.Industry.txt" file.
        /// </summary>
        [Option(LongName = "keywords-industry", Description = "Full path of the RelevantKeywords.Role.txt file.")]
        public virtual string IndustryKeywordsFullPath { get; set; }
            = @$"{System.Environment.CurrentDirectory}\..\..\..\..\CommandLineApp\DataLake\RelevantKeywords.Industry.txt";

        /// <summary>
        /// Gets or sets the full path of the "Prioritize.TheseCountriesBecauseOfOurExpansionInLatinoamerica.txt" file.
        /// </summary>
        [Option(LongName = "latino-countries", Description = "Full path of the Prioritize.TheseCountriesBecauseOfOurExpansionInLatinoamerica.txt file.")]
        public virtual string LatinoCountriesFullPath { get; set; }
            = @$"{System.Environment.CurrentDirectory}\..\..\..\..\CommandLineApp\DataLake\Prioritize.TheseCountriesBecauseOfOurExpansionInLatinoamerica.txt";

        /// <summary>
        /// Gets or sets the full path of the "Prioritize.TheseCountries.BecauseOfOurLocations.txt" file.
        /// </summary>
        [Option(LongName = "ourlocations-countries", Description = "Full path of the Prioritize.TheseCountries.BecauseOfOurLocations.txt file.")]
        public virtual string OurLocationsCountriesFullPath { get; set; }
            = @$"{System.Environment.CurrentDirectory}\..\..\..\..\CommandLineApp\DataLake\Prioritize.TheseCountries.BecauseOfOurLocations.txt";
        

    }
}
