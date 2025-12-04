namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 03, CodeType.Original)]
public class Day_03_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = input.Lines.Sum(l => GetMaxJoltage(l, 2)).ToString();

		var part2 = input.Lines.Sum(l => GetMaxJoltage(l, 12)).ToString();

		return (part1, part2);
	}

	private static long GetMaxJoltage(string line, int numBats)
	{
		Span<char> chars = stackalloc char[12];
		var nextChar = 0;
		for (var i = numBats; i > 0; i--)
		{
			var (index, nextDigit) = line.Index().Take(nextChar..^(i - 1)).MaxBy(b => b.Item);
			chars[numBats - i] = nextDigit;
			nextChar = index + 1;
		}

		return long.Parse(chars[..numBats]);
	}
}
