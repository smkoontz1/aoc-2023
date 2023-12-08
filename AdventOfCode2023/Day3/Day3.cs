
using System.Text.RegularExpressions;

public class Day3
{
    public void PartOne()
    {
        var testFile = "Day3/Day3TestInput.txt";
        var inputFile = "Day3/Day3Input.txt";

        var lines = FileReader.GetLines(testFile);
        
        var partNumberSum = 0;

        var rowCount = lines.Length;

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var currRow = lines[rowIndex];

            string? prevRow = rowIndex > 0 ? lines[rowIndex - 1] : null;
            string? nextRow = rowIndex < rowCount - 1 ? lines[rowIndex + 1] : null;

            for (var charIndex = 0; charIndex < currRow.Length; charIndex++)
            {
                var currChar = currRow[charIndex];

                var numChars = new List<char>();
                int? numStartIndex = null;
                int? numEndIndex = null;

                if (int.TryParse(currChar.ToString(), out _))
                {
                    if (!numChars.Any())
                    {
                        numStartIndex = charIndex;
                    }

                    numChars.Add(currChar);
                }
                else
                {
                    if (numChars.Any())
                    {
                        numEndIndex = charIndex - 1;

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
            if (prevRowMatch is not null)
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
        if (currRowMatch is not null)
        {
            return true;
        }

        // Check next row
        if (nextRow is not null)
        {
            var nextRowCheckSection = nextRow.Substring(checkSectionStartIndex, checkSectionLength);

            var nextRowMatch = symbolRegex.Match(nextRowCheckSection);
            if (nextRowMatch is not null)
            {
                return true;
            }
        }

        return false;
    }
}