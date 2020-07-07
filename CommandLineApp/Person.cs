namespace CommandLineApp
{
    /// <summary>
    /// Represents a row within "people.in".
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public virtual string PersonId { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the current role.
        /// </summary>
        public virtual string CurrentRole { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public virtual string Country { get; set; }

        /// <summary>
        /// Gets or sets the industry.
        /// </summary>
        public virtual string Industry { get; set; }

        /// <summary>
        /// Gets or sets the number of recommendations.
        /// </summary>
        /// <remarks>A null value means that this information is not available, 
        /// to distinguish from 0 recommendations.</remarks>
        public virtual int? NumberOfRecommendations { get; set; }

        /// <summary>
        /// Gets or sets the number of connections.
        /// </summary>
        /// <remarks>A null value means that this information is not available, 
        /// to distinguish from 0 connections.</remarks>
        public virtual int? NumberOfConnections { get; set; }
    }
}
