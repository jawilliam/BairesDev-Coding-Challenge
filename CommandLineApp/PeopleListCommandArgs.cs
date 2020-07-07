using CommandDotNet;

namespace CommandLineApp
{
    /// <summary>
    /// Provides data for the command <see cref="PeopleCommands.List(PeopleListCommandArgs)"/>.
    /// </summary>
    public class PeopleListCommandArgs : IArgumentModel
    {
        /// <summary>
        /// Gets or sets the full path of the "people.in" file.
        /// </summary>
        [Option(LongName = "in", Description = "Full path of the people.in file.")]
        public virtual string InFullPath { get; set; }
            = @$"{System.Environment.CurrentDirectory}\..\..\..\..\CommandLineApp\DataLake\people.in";

        ///// <summary>
        ///// Gets or sets the page size.
        ///// </summary>
        //[Option(LongName = "page-size", Description = "Page size, i.e., maximum of rows to be shown.")]
        //public virtual int PageSize { get; set; } = 20;

        ///// <summary>
        ///// Gets or sets the page number. 
        ///// </summary>
        //[Option(LongName = "page-number", Description = "The number of the page to be shown, considering that each page composed of (page-size)-number of rows.")]
        //public virtual int PageNumber { get; set; } = 1;
    }
}
