using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 06, CodeType.Original)]
public class Day_06_Original : IPuzzle
{
	private class Problem
	{
		public List<long> Numbers { get; } = new();
		public char Operator { get; set; }
	}

	public (string, string) Solve(PuzzleInput input)
	{
		var split = input.Lines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();
		var transposed = new string[split[0].Length][];
		for (var i = 0; i < transposed.Length; i++)
		{
			transposed[i] = new string[split.Length];
			for (var j = 0; j < split.Length; j++)
			{
				transposed[i][j] = split[j][i];
			}
		}

		var totalPart1 = transposed.Sum(col =>
			col[^1] == "+" ? col.SkipLast(1).Sum(long.Parse) : col.SkipLast(1).Aggregate(1L, (current, e) => current * long.Parse(e)));

		List<Problem> problems = [new()];
		List<char> numBuffer = [];
		for (var x = 0; x < input.Lines[0].Length; x++)
		{
			var hadAnything = false;
			numBuffer.Clear();
			for (var y = 0; y < input.Lines.Length; y++)
			{
				var c = input.Lines[y][x];
				if (c == ' ')
				{
					continue;
				}

				hadAnything = true;

				if (c is '+' or '*')
				{
					problems[^1].Operator = c;
					continue;
				}

				numBuffer.Add(c);
			}

			if (hadAnything)
			{
				problems[^1].Numbers.Add(long.Parse(CollectionsMarshal.AsSpan(numBuffer)));
			}
			else
			{
				problems.Add(new Problem());
			}
		}

		var totalPart2 = problems.Sum(p => p.Operator == '+' ? p.Numbers.Sum() : p.Numbers.Aggregate(1L, (current, next) => current * next));

		var part1 = totalPart1.ToString();

		var part2 = totalPart2.ToString();

		return (part1, part2);
	}
}
