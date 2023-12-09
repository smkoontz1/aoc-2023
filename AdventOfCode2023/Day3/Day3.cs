
using AdventOfCode2023.Day3.Models;
using System.Text.RegularExpressions;

public class Day3
{
    private static List<AsteriskPartNumber> AsteriskPartNumbers = new List<AsteriskPartNumber>();

    public void PartOne()
    {
        var testFile = "Day3/Day3TestInput.txt";
        var inputFile = "Day3/Day3Input.txt";

        var lines = FileReader.GetLines(inputFile);
        
        var partNumberSum = 0;

        var rowCount = lines.Length;

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var currRow = lines[rowIndex];

            string? prevRow = rowIndex > 0 ? lines[rowIndex - 1] : null;
            string? nextRow = rowIndex < rowCount - 1 ? lines[rowIndex + 1] : null;

            var numChars = new List<char>();
            int? numStartIndex = null;
            int? numEndIndex = null;
            
            for (var charIndex = 0; charIndex < currRow.Length; charIndex++)
            {
                var currChar = currRow[charIndex];
                var isLastChar = charIndex == currRow.Length - 1;

                var isNum = int.TryParse(currChar.ToString(), out _);

                if (isNum)
                {
                    if (!numChars.Any())
                    {
                        numStartIndex = charIndex;
                    }

                    numChars.Add(currChar);
                }

                if (!isNum || (isNum && isLastChar))
                {
                    if (numChars.Any())
                    {
                        numEndIndex = isLastChar ? charIndex : charIndex - 1;

                        var isPartNumber = IsPartNumber(
                            numStartIndex.Value,
                            numEndIndex.Value,
                            currRow,
                            nextRow,
                            prevRow);

                        if (isPartNumber)
                        {
                            var partNumber = int.Parse(new string(numChars.ToArray()));
                            partNumberSum += partNumber;
                        }

                        numChars = new List<char>();
                        numStartIndex = null;
                        numEndIndex = null;
                    }
                }
            }
        }

        Console.WriteLine($"Part number sum: {partNumberSum}");
    }

    public void PartTwo()
    {
        var testFile = "Day3/Day3TestInput.txt";
        var inputFile = "Day3/Day3Input.txt";

        var lines = FileReader.GetLines(inputFile);
        var rowCount = lines.Length;

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var currRow = lines[rowIndex];

            string? prevRow = rowIndex > 0 ? lines[rowIndex - 1] : null;
            string? nextRow = rowIndex < rowCount - 1 ? lines[rowIndex + 1] : null;

            var numChars = new List<char>();
            int? numStartIndex = null;
            int? numEndIndex = null;

            for (var charIndex = 0; charIndex < currRow.Length; charIndex++)
            {
                var currChar = currRow[charIndex];
                var isLastChar = charIndex == currRow.Length - 1;

                var isNum = int.TryParse(currChar.ToString(), out _);

                if (isNum)
                {
                    if (!numChars.Any())
                    {
                        numStartIndex = charIndex;
                    }

                    numChars.Add(currChar);
                }

                if (!isNum || (isNum && isLastChar))
                {
                    if (numChars.Any())
                    {
                        numEndIndex = isLastChar ? charIndex : charIndex - 1;

                        var asteriskCoordinates = CheckForAsterisk(
                            numStartIndex.Value,
                            numEndIndex.Value,
                            rowIndex,
                            currRow,
                            nextRow,
                            prevRow);

                        if (asteriskCoordinates.HasValue)
                        {
                            var partNumber = int.Parse(new string(numChars.ToArray()));

                            RecordAsteriskPartNumber(
                                asteriskCoordinates.Value.AsteriskRow,
                                asteriskCoordinates.Value.AsteriskColumn,
                                partNumber);
                        }

                        numChars = new List<char>();
                        numStartIndex = null;
                        numEndIndex = null;
                    }
                }
            }
        }

        var gearRatioSum = 0;
        var gearPartNumbers = AsteriskPartNumbers.Where(a => a.AdjacentPartNumbers.Count == 2);

        foreach (var gearPartNumber in gearPartNumbers)
        {
            var gearRatio = gearPartNumber.AdjacentPartNumbers[0] * gearPartNumber.AdjacentPartNumbers[1];
            gearRatioSum += gearRatio;
        }

        Console.WriteLine($"Gear ratio sum: {gearRatioSum}");
    }

    private (int AsteriskRow, int AsteriskColumn)? CheckForAsterisk(
        int numStartIndex,
        int numEndIndex,
        int currRowIndex,
        string currRow,
        string? nextRow,
        string? prevRow)
    {
        var checkSectionStartIndex = numStartIndex > 0 ? numStartIndex - 1 : numStartIndex;
        var checkSectionEndIndex = numEndIndex < currRow.Length - 1 ? numEndIndex + 1 : numEndIndex;
        var checkSectionLength = checkSectionEndIndex - checkSectionStartIndex + 1;

        // Check previous row
        if (prevRow is not null)
        {
            for (var i = checkSectionStartIndex; i <= checkSectionEndIndex; i++)
            {
                var rowChar = prevRow[i];

                if (rowChar == '*')
                {
                    return (currRowIndex - 1, i);
                }
            }
        }

        // Check current row
        if (numStartIndex > 0)
        {
            var leftChar = currRow[numStartIndex - 1];
            
            if (leftChar == '*')
            {
                return (currRowIndex, numStartIndex - 1);
            }
        }

        if (numEndIndex < currRow.Length - 1)
        {
            var rightChar = currRow[numEndIndex + 1];

            if (rightChar == '*')
            {
                return (currRowIndex, numEndIndex + 1);
            }
        }

        // Check next row
        if (nextRow is not null)
        {
            for (var i = checkSectionStartIndex; i <= checkSectionEndIndex; i++)
            {
                var rowChar = nextRow[i];

                if (rowChar == '*')
                {
                    return (currRowIndex + 1, i);
                }
            }
        }

        return null;
    }

    private void RecordAsteriskPartNumber(int rowIndex, int colIndex, int partNumber)
    {
        var existingAsteriskPartNumbers = AsteriskPartNumbers.FirstOrDefault(a => a.RowIndex == rowIndex && a.ColIndex == colIndex);

        if (existingAsteriskPartNumbers is not null)
        {
            existingAsteriskPartNumbers.AdjacentPartNumbers.Add(partNumber);
        }
        else
        {
            AsteriskPartNumbers.Add(new AsteriskPartNumber(
                rowIndex,
                colIndex,
                new List<int> { partNumber }));
        }
    }

    private bool IsPartNumber(
        int numStartIndex,
        int numEndIndex,
        string currRow,
        string? nextRow,
        string? prevRow)
    {
        var symbolRegex = new Regex(@"[^0-9.]");

        var checkSectionStartIndex = numStartIndex > 0 ? numStartIndex - 1 : numStartIndex;
        var checkSectionEndIndex = numEndIndex < currRow.Length - 1 ? numEndIndex + 1 : numEndIndex;
        var checkSectionLength = checkSectionEndIndex - checkSectionStartIndex + 1;

        // Check previous row
        if (prevRow is not null)
        {
            var prevRowCheckSection = prevRow.Substring(checkSectionStartIndex, checkSectionLength);

            var prevRowMatch = symbolRegex.Match(prevRowCheckSection);
            if (prevRowMatch.Success)
            {
                return true;
            }
        }

        // Check current row
        var currRowCheckSection = string.Empty;

        // Char before num
        if (numStartIndex > 0)
        {
            currRowCheckSection += currRow[numStartIndex - 1];
        }

        // Char after num
        if (numEndIndex < currRow.Length - 1)
        {
            currRowCheckSection += currRow[numEndIndex + 1];
        }

        var currRowMatch = symbolRegex.Match(currRowCheckSection);
        if (currRowMatch.Success)
        {
            return true;
        }

        // Check next row
        if (nextRow is not null)
        {
            var nextRowCheckSection = nextRow.Substring(checkSectionStartIndex, checkSectionLength);

            var nextRowMatch = symbolRegex.Match(nextRowCheckSection);
            if (nextRowMatch.Success)
            {
                return true;
            }
        }

        return false;
    }
}