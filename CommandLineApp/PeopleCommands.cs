using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CommandDotNet;

namespace CommandLineApp
{
    /// <summary>
    /// Implements command line functionalities.
    /// </summary>
    [Command(Name = "people")]
    public class PeopleCommands
    {
        [Command(Name = "list", Description = "Lists people.in data.")]
        public virtual void List(PeopleListCommandArgs args)
        {
            var people = this.ReadPeople(args).ToList();
            this.View(people);
        }

        private void View(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine($"{Environment.NewLine}{person.PersonId}" +
                                  $"{Environment.NewLine}{person.Name}" +
                                  $"{Environment.NewLine}{person.LastName}" +
                                  $"{Environment.NewLine}{person.CurrentRole}" +
                                  $"{Environment.NewLine}{person.Country}" +
                                  $"{Environment.NewLine}{person.Industry}" +
                                  $"{Environment.NewLine}{person.NumberOfConnections}" +
                                  $"{Environment.NewLine}{person.NumberOfRecommendations}");
            }
        }

        /// <summary>
        /// Reads the persons existing in the given people.in file.
        /// </summary>
        /// <param name="args">contains the input information, included the full path of the "people.in".</param>
        /// <returns>A list of persons representing each of lines existing in "people.in".</returns>
        protected virtual IEnumerable<Person> ReadPeople(PeopleListCommandArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.InFullPath))
                throw new ArgumentNullException(nameof(args.InFullPath));

            string[] lines;
            try
            {
                lines = ReadLines(args.InFullPath, "people.in");
            }
            catch (Exception)
            {
                throw;
            }

            foreach (var line in lines)
            {
                var values = line.Split("|");
                if ((values?.Length ?? 0) != 8)
                    throw new ApplicationException($"Bad formed line: {line}");

                yield return new Person 
                {
                    PersonId = values[0],
                    Name = values[1],
                    LastName = values[2],
                    CurrentRole = values[3],
                    Country = values[4],
                    Industry = values[5],
                    NumberOfRecommendations = int.Parse(values[6], CultureInfo.InvariantCulture),
                    NumberOfConnections = int.Parse(values[7], CultureInfo.InvariantCulture)
                };
            }
        }

        private string[] ReadLines(string path, string filename = "")
        {
            string[] lines;
            var fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath))
                throw new ApplicationException($"The path given to load {filename} might be not valid: " +
                    $"(does {fullPath} exist?).");

            lines = File.ReadAllLines(fullPath);
            return lines;
        }

        /// <summary>
        /// Lists the distinct keywords used in the role of people.in data.
        /// </summary>
        /// <param name="args">contains the input information, included how to display the resulting keywords.</param>
        [Command(Name = "keywords-role", Description = "Lists the distinct keywords used in the role of people.in data.")]
        public virtual void RoleKeywords(KeywordsCommandArgs args)
        {
            var people = this.ReadPeople(args).ToList();
            var keywords = new Analytics<Person> { Sink = people }.Tokenize(p => p.CurrentRole);
            this.View(args.View, keywords);
        }

        /// <summary>
        /// Lists the distinct keywords used in the industry of people.in data.
        /// </summary>
        /// <param name="args">contains the input information, included how to display the resulting keywords.</param>
        [Command(Name = "keywords-industry", Description = "Lists the distinct keywords used in the industry of people.in data.")]
        public virtual void IndustryKeywords(KeywordsCommandArgs args)
        {
            var people = this.ReadPeople(args).ToList();
            var keywords = new Analytics<Person> { Sink = people }.Tokenize(p => p.Industry);
            this.View(args.View, keywords);
        }

        /// <summary>
        /// Lists the distinct countries of people.in data.
        /// </summary>
        /// <param name="args">contains the input information, included how to display the resulting values.</param>
        [Command(Name = "countries", Description = "Lists the distinct countries of people.in data.")]
        public virtual void Countries(KeywordsCommandArgs args)
        {
            var people = this.ReadPeople(args).ToList();
            var keywords = people.Select(p => (p, new[] { p.Country }));
            this.View(args.View, keywords);
        }

        private void View(string view, IEnumerable<(Person, string[])> keywords)
        {
            switch (view)
            {
                case "Distinct":
                    int i = 0;
                    foreach (var keyword in keywords
                        .SelectMany(k => k.Item2).Distinct()
                        .OrderBy(k => k))
                    {
                        Console.WriteLine($"{++i}) {keyword}");
                    }
                    break;
                case "PerRow":
                    foreach (var keyword in keywords)
                    {
                        Console.WriteLine($"{keyword.Item1.PersonId}" +
                                          $";{keyword.Item1.Name}" +
                                          $";{keyword.Item1.LastName}" +
                                          $";{keyword.Item2.Aggregate("", (a, c) => $"{a} {c}")}");
                    }
                    break;
                case "Frequencies":
                    var hash = new Dictionary<string, int>();
                    foreach (var keyword in keywords.SelectMany(k => k.Item2))
                    {
                        if (hash.ContainsKey(keyword))
                            hash[keyword] = hash[keyword] + 1;
                        else
                            hash[keyword] = 1;
                    }

                    i = 0;
                    foreach (var keyword in hash.Keys
                        .Select(v => new { Value = v, Frequency = hash[v] })
                        .OrderByDescending(v => v.Frequency))
                    {
                        Console.WriteLine($"{++i}) {keyword.Value} ({keyword.Frequency})");
                    }
                    break;
                default:
                    throw new ApplicationException($"Unsupported view: {view}");
            }
        }

        /// <summary>
        /// Computes people.out data.
        /// </summary>
        /// <param name="args">contains the input information, included how to display the resulting values.</param>
        [Command(Name = "answer", Description = "Lists los potenciales clientes, y salva sus ids in people.out.")]
        public virtual void Answer(AnswerCommandArgs args)
        {
            var people = this.ReadPeople(args).ToList();
            var analitycs = new Analytics<Person> { Sink = people };            

            // hashing people to access them during the term frequency computing optimally.
            var peopleTf = new Dictionary<string, float>(people.Count);
            foreach (var person in people)
            {
                peopleTf[person.PersonId] = 0;
            }

            // role term frequencies
            var roleKeywords = ReadLines(args.RoleKeywordsFullPath, "RelevantKeywords.Role.txt")
                    .Select(kw => kw.ToLowerInvariant())
                    .ToHashSet();
            foreach (var person in analitycs.Tokenize(p => p.CurrentRole))
            {
                var roleTf = person.Item2
                    .Select(kw => kw.ToLowerInvariant())
                    .Distinct()
                    .Count(kw => roleKeywords.Contains(kw));
                if(roleTf != 0)
                    peopleTf[person.Item1.PersonId] = peopleTf[person.Item1.PersonId] + roleTf;
            }

            // industry term frequencies
            var industryKeywords = ReadLines(args.IndustryKeywordsFullPath, "RelevantKeywords.Industry.txt")
                    .Select(kw => kw.ToLowerInvariant())
                    .ToHashSet();
            foreach (var person in analitycs.Tokenize(p => p.Industry))
            {
                var industryTf = person.Item2
                    .Select(kw => kw.ToLowerInvariant())
                    .Distinct()
                    .Count(kw => industryKeywords.Contains(kw));
                if (industryTf != 0)
                    peopleTf[person.Item1.PersonId] = peopleTf[person.Item1.PersonId] + industryTf;
            }

            // Let us Prioritize These Countries Because Of Our Expansion In Latinoamerica
            // can be a Persuasive Plus (e.g., Idiom, Time Zone).
            var latinoCountries = ReadLines(args.LatinoCountriesFullPath,
                "Prioritize.TheseCountriesBecauseOfOurExpansionInLatinoamerica.txt")
                    .Select(kw => kw.ToLowerInvariant())
                    .ToHashSet();
            foreach (var person in people)
            {
                var countryTf = latinoCountries.Contains(person.Country?.ToLowerInvariant() ?? "") ? 0.25f : 0;
                if (countryTf != 0)
                    peopleTf[person.PersonId] = peopleTf[person.PersonId] + countryTf;
            }

            // Let us Prioritize These Countries Because Of Our Locations In Such Countries
            // can be a Persuasive Plus (e.g., Domestic Regulatory Laws, Talent in Place)
            var ourLocationCountries = ReadLines(args.OurLocationsCountriesFullPath,
                "Prioritize.TheseCountries.BecauseOfOurLocations.txt")
                    .Select(kw => kw.ToLowerInvariant())
                    .ToHashSet();
            foreach (var person in people)
            {
                var countryTf = ourLocationCountries.Contains(person.Country?.ToLowerInvariant() ?? "") ? 0.25f : 0;
                if (countryTf != 0)
                    peopleTf[person.PersonId] = peopleTf[person.PersonId] + countryTf;
            }

            foreach (var person in people)
            {
                var numberOfConnectionsTf = 0f;
                if (person.NumberOfConnections >= 1 && person.NumberOfConnections < 3)
                    numberOfConnectionsTf = 0.16f;
                else if (person.NumberOfConnections >= 3 && person.NumberOfConnections < 21)
                    numberOfConnectionsTf = 0.16f * 2;
                else if (person.NumberOfConnections >= 21 && person.NumberOfConnections < 95.45)
                    numberOfConnectionsTf = 0.16f * 3;
                else if (person.NumberOfConnections >= 95.45 && person.NumberOfConnections < 128.5)
                    numberOfConnectionsTf = 0.16f * 4;
                else if (person.NumberOfConnections >= 128.5 && person.NumberOfConnections < 500)
                    numberOfConnectionsTf = 0.16f * 5;
                else if (person.NumberOfConnections >= 500)
                    numberOfConnectionsTf = 0.16f * 6;
                if (numberOfConnectionsTf != 0)
                    peopleTf[person.PersonId] = peopleTf[person.PersonId] + numberOfConnectionsTf;
            }

            var @out = (from personTf in people.Select(p => new { Person = p, Tf = peopleTf[p.PersonId] })
                       where personTf.Tf > 0 //&& personTf.Person.NumberOfConnections > 0
                       orderby personTf.Tf descending
                       //select personTf.Person;
                       select personTf);

            var emailTo = @out.Take(100).Select(p => p.Person).ToList();
            this.View(emailTo);

            File.WriteAllLines(args.OutFullPath, emailTo.Select(p => p.PersonId));
        }
    }
}
