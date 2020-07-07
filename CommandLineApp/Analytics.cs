using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineApp
{
    /// <summary>
    /// Implements a very conservative way to make text analytics.
    /// </summary>
    /// <typeparam name="TData">Type of the elements structuring the text source.</typeparam>
    public class Analytics<TData>
    {
        private IEnumerable<TData> _sink;
        private readonly string[] _separators = 
            new[] { ",", "&", ".", /*" - ", "- ", " -"*/"-", " and ", " ", "\\", "/", ":", "(", ")"};

        /// <summary>
        /// Gets or sets the structured data to be analyzed.
        /// </summary>
        public virtual IEnumerable<TData> Sink
        { 
            get => this._sink ??= Array.Empty<TData>();
            set => this._sink = value;
        }

        /// <summary>
        /// Extract tokens across a given feature within the data.
        /// </summary>
        /// <typeparam name="TFeature"></typeparam>
        /// <param name="feature">given feature</param>
        /// <returns>A list of tokens per record, i.e., (record, tokens) </returns>
        public virtual IEnumerable<(TData, string[])> Tokenize(Func<TData, string> feature)
        {
            if (feature == null)
                throw new ArgumentNullException(nameof(feature));

            foreach (var record in this.Sink)
            {
                var tokens = record == null 
                    ? Array.Empty<string>()
                    : feature(record).Split(this._separators, StringSplitOptions.RemoveEmptyEntries);

                yield return (record, tokens);
            }
        }

        ///// <summary>
        ///// Computes the total raw count of these terms within every data.
        ///// </summary>
        ///// <typeparam name="TFeature"></typeparam>
        ///// <param name="feature">given feature</param>
        ///// <returns>A list of tokens per record, i.e., (record, tokens) </returns>
        //public virtual IEnumerable<(TData, int)> CountOfTerms(Func<TData, string> feature, string[] terms)
        //{
        //    if (feature == null)
        //        throw new ArgumentNullException(nameof(feature));

        //    var hashedData = new Dictionary<TData, int>(this.Sink.Count());
        //    foreach (var d in this.Sink)
        //    {
        //        hashedData[d] = 0;
        //    }

        //    var hashedTerms = terms.ToDictionary(t => t);
        //    foreach (var tokensPerData in this.Tokenize(feature))
        //    {
        //        var tf = tokensPerData.Item2.Count(kw => terms.Contains(kw));
        //        if (tf != 0)
        //            hashedData[tokensPerData.Item1] += tf;
        //    }
        //}
    }
}
