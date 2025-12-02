namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 01, CodeType.Original)]
public class Day_01_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var countZeroPart1 = 0;
		var countZeroPart2 = 0;
		var current = 50;
		foreach (var line in input.Lines)
		{
			var num = int.Parse(line.AsSpan(1));
			num = line[0] == 'L' ? -num : num;

			var next = current + num;
			if (next == 0 || (current != 0 && next < 0))
			{
				countZeroPart2 += 1;
			}

			(var count, current) = int.DivRem(next, 100);
			countZeroPart2 += int.Abs(count);

			if (current == 0)
			{
				countZeroPart1 += 1;
			}
			else if (current < 0)
			{
				current += 100;
			}
		}

		var part1 = countZeroPart1.ToString();

		var part2 = countZeroPart2.ToString();

		return (part1, part2);
	}
}
