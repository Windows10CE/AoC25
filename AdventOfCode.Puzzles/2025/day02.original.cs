namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 02, CodeType.Original)]
public class Day_02_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		ulong idSumPart1 = 0;
		ulong idSumPart2 = 0;
		Span<char> buffer = stackalloc char[32];
		foreach (var range in input.Text.AsSpan().Split(','))
		{
			var ids = input.Text.AsSpan(range);
			Span<Range> ranges = [default, default];
			ids.Split(ranges, '-');
			var startIdStr = ids[ranges[0]];
			var endIdStr = ids[ranges[1]];

			var startId = ulong.Parse(startIdStr);
			var endId = ulong.Parse(endIdStr);

			for (var i = startId; i <= endId; i++)
			{
				i.TryFormat(buffer, out var written);
				var anySuccess = false;
				for (var j = 1; j <= written / 2; j++)
				{
					if (written % j != 0)
					{
						continue;
					}

					var toCheck = buffer[..j];
					var success = true;
					for (var k = j; k < written; k += j)
					{
						if (!toCheck.SequenceEqual(buffer.Slice(k, j)))
						{
							success = false;
							break;
						}
					}

					if (success)
					{
						anySuccess = true;
						if (j == written / 2 && int.IsEvenInteger(written))
						{
							idSumPart1 += i;
						}
						else if (j != written / 2)
						{
							j = (written / 2) - 1;
						}
					}
				}

				if (anySuccess)
				{
					idSumPart2 += i;
				}
			}
		}

		var part1 = idSumPart1.ToString();

		var part2 = idSumPart2.ToString();

		return (part1, part2);
	}
}
