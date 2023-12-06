using System.Text.RegularExpressions;

public class Day1
{
    // Answer 54601
    public void PartOne()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var inputFile = Path.Combine(baseDirectory, "Day1/Day1Input.txt");

        var lines = File.ReadAllLines(inputFile);

        var calibrationSum = 0;

        foreach (var line in lines)
        {
            var lineJustNums = Regex.Replace(line, @"[^0-9]", string.Empty);
            var firstChar = lineJustNums.First();
            var lastChar = lineJustNums.Last();
            var calibrationNum = int.Parse(new string(new[] { firstChar, lastChar }));

            calibrationSum += calibrationNum;
        }

        Console.WriteLine($"Calibration sum: {calibrationSum}");
    }

    public void PartTwo()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var inputFile = Path.Combine(baseDirectory, "Day1/Day1Input.txt");

        var lines = File.ReadAllLines(inputFile);

        // var lines = new[]
        // {
        //     "two1nine",
        //     "eightwothree",
        //     "abcone2threexyz",
        //     "xtwone3four",
        //     "4nineeightseven2",
        //     "zoneight234",
        //     "7pqrstsixteen"
        // };

        var calibrationSum = 0;

        var digitMap = new Dictionary<string, string>
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" },
        };

        foreach (var line in lines)
        {
            var firstDigitRegex = new Regex(@"^.*?(one|two|three|four|five|six|seven|eight|nine|[0-9])");
            var lastDigitRegex = new Regex(".*(one|two|three|four|five|six|seven|eight|nine|[0-9]).*$");

            var firstDigitMatch = firstDigitRegex.Match(line);
            var firstDigit = firstDigitMatch.Groups[1].Value;

            var lastDigitMatch = lastDigitRegex.Match(line);
            var lastDigit = lastDigitMatch.Groups[1].Value;

            if (digitMap.ContainsKey(firstDigit))
            {
                firstDigit = digitMap[firstDigit];
            }

            if (digitMap.ContainsKey(lastDigit))
            {
                lastDigit = digitMap[lastDigit];
            }

            var calibrationNum = int.Parse(firstDigit + lastDigit);
            calibrationSum += calibrationNum;
        }

        Console.WriteLine($"Calibration sum: {calibrationSum}");
    }

}