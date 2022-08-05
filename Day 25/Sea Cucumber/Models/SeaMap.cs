using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sea_Cucumber.Models
{
    /// <summary>
    /// This value object represents the oceans' seacucumber population as a grid.
    /// </summary>
    internal class SeaMap
    {
        public SeaMap(List<List<char>> map, int width, int height)
        {
            Map = map;
            Width = width;
            Height = height;

            ValidateCharacters();
        }

        internal List<List<char>> Map { get; set; } = new List<List<char>>();
        internal int Width { get; set; }
        internal int Height { get; set; }

        private void ValidateCharacters()
        {
            char[] acceptedCharacters = new char[]
            {
                '.',
                'v',
                '>',
            };

            if (Map.Any(x => x.Any(z => !acceptedCharacters.Contains(z))))
            {
                throw new FormatException("Map contains invalid character");
            }
        }

        internal string AsString()
        {
            var sb = new StringBuilder();
            var newLineLength = "\r\n".Length;

            foreach (var col in Map.Select(x => x))
            {
                sb.AppendLine(new string(col.ToArray()));
            }

            sb = sb.Remove(sb.Length - newLineLength, 2);

            return sb.ToString();
        }

        internal static SeaMap FromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new SeaMap(new List<List<char>>(), 0, 0);
            }

            input = RemoveWhiteSpaces(input);
            var width = IsMultiline(input) ? input.IndexOf("\r\n") : input.Length;
            var height = input.Count(x => x == '\n') + 1;

            if (IsMultiline(input))
            {
                if (!IsValidRectangle(width, height, input))
                {
                    throw new FormatException("Input is not a perfect rectangle.");
                }
            }

            var rows = input.Split("\r\n").Select(x => x.ToCharArray().ToList()).ToList();

            return new SeaMap(rows, width, height);
        }

        private static string RemoveWhiteSpaces(string str)
        {
            return String.Concat(str.Where(c => c != ' '));
        }

        private static bool IsMultiline(string input)
        {
            return input.Contains("\r\n");
        }

        private static bool IsValidRectangle(int width, int height, string input)
        {
            var newLineLength = "\r\n".Length;
            return ((width + newLineLength) * height) - newLineLength == input.Length;
        }
    }
}