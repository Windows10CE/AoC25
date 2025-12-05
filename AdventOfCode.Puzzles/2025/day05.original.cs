using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 05, CodeType.Original)]
public class Day_05_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		List<(long, long)> ranges = [];
		foreach (var line in input.Lines.TakeWhile(s => s is not ""))
		{
			var elementSpans = default(InlineArray2<Range>);
			line.Split(elementSpans, '-');
			ranges.Add((long.Parse(line.AsSpan(elementSpans[0])), long.Parse(line.AsSpan(elementSpans[1]))));
		}

		var count = 0;
		foreach (var item in input.Lines.SkipUntil(string.IsNullOrEmpty))
		{
			var itemNum = long.Parse(item);
			foreach (var range in ranges)
			{
				if (range.Item1 <= itemNum && range.Item2 >= itemNum)
				{
					count += 1;
					break;
				}
			}
		}

		ranges = ranges.OrderBy(range => range.Item1).ThenBy(range => range.Item2).ToList();

		var span = CollectionsMarshal.AsSpan(ranges);
		for (var i = span.Length - 2; i >= 0; i--)
		{
			if (span[i + 1].Item1 <= span[i].Item2 && span[i + 1].Item2 >= span[i].Item2)
			{
				span[i].Item2 = span[i + 1].Item2;
				ranges.RemoveAt(i + 1);
			}
			else if (span[i + 1].Item1 <= span[i].Item2)
			{
				ranges.RemoveAt(i + 1);
			}
		}

		var part1 = count.ToString();

		var part2 = ranges.Sum(range => range.Item2 - range.Item1 + 1).ToString();

		return (part1, part2);
	}
}
