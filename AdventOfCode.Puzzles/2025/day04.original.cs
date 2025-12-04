namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 04, CodeType.Original)]
public class Day_04_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		ReadOnlySpan<(int, int)> offsets =
		[
			(-1, -1),
			(-1, 0),
			(-1, 1),
			(0, -1),
			(0, 1),
			(1, -1),
			(1, 0),
			(1, 1),
		];

		HashSet<(int, int)> removedSet = [];
		HashSet<(int, int)> nextRemovedSet = [];

		var first = true;
		var firstCount = 0;
		do
		{
			removedSet.UnionWith(nextRemovedSet);

			for (var x = 0; x < input.Lines.Length; x++)
			{
				for (var y = 0; y < input.Lines[0].Length; y++)
				{
					if (!IsRoll(input.Lines, x, y, removedSet))
					{
						continue;
					}

					var currentCount = 0;
					foreach (var (xOff, yOff) in offsets)
					{
						if (IsRoll(input.Lines, x + xOff, y + yOff, removedSet))
						{
							currentCount += 1;
						}
					}

					if (currentCount < 4)
					{
						nextRemovedSet.Add((x, y));
					}
				}
			}

			if (first)
			{
				first = false;
				firstCount = nextRemovedSet.Count;
			}
		} while (removedSet.Count != nextRemovedSet.Count);

		var part1 = firstCount.ToString();

		var part2 = removedSet.Count.ToString();

		return (part1, part2);
	}

	private static bool IsRoll(string[] lines, int x, int y, HashSet<(int, int)> removed)
	{
		if (x >= 0 && x < lines.Length && y >= 0 && y < lines[0].Length)
		{
			return lines[x][y] == '@' && !removed.Contains((x, y));
		}

		return false;
	}
}
