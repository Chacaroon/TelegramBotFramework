namespace TelegramBotLab.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class SubjectsResolver
    {
        private static readonly Dictionary<List<string>, string> Subjects = new()
        {
            { new List<string> { "пвмс", "программирование в сетевой среде", "програмування в мережевих середовищах" }, "pvms" },
        };

        private static readonly Dictionary<List<string>, string> Types = new()
        {
            { new List<string> { "ла?б(ораторн)?[aу]?[яю]?" }, "lab" },
            { new List<string> { "пз", "практичес.+" }, "pz" }
        };

        private static readonly Dictionary<List<string>, int?> Numbers = new()
        {
            { new List<string> { "певр" }, 1 },
            { new List<string> { "втор" }, 2 },
            { new List<string> { "трет" }, 3 },
            { new List<string> { "червёрт" }, 4 },
            { new List<string> { "пят" }, 5 },
            { new List<string> { "шест" }, 6 },
            { new List<string> { "седьм" }, 7 },
            { new List<string> { "восьм" }, 8 },
        };

        private static readonly Regex RedundantChars = new(@"!,\?", RegexOptions.Compiled);

        public static string? ResolveSubject(string text)
        {
            var words = RedundantChars.Replace(text.ToLower(), string.Empty).Split(" ");

            return Subjects
                .Where(keyValuePair => keyValuePair.Key.Any(x => words.Intersect(x.Split(" ")).Any()))
                .Select(x => x.Value)
                .FirstOrDefault();
        }

        public static string? ResolveType(string text)
        {
            var words = RedundantChars.Replace(text.ToLower(), string.Empty).Split(" ");

            return Types
                .Where(x => x.Key.Any(r => words.Any(z => new Regex(r).IsMatch(z))))
                .Select(x => x.Value)
                .FirstOrDefault();
        }

        public static int? ResolveNumber(string text)
        {
            var words = RedundantChars.Replace(text.ToLower(), string.Empty).Split(" ");

            return Numbers
                .Where(x => words.Any(z => x.Key.Any(z.StartsWith)))
                .Select(x => x.Value)
                .FirstOrDefault();
        }
    }
}
