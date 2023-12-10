internal class Day4
{
    public void PartOne()
    {
        var testFile = "Day4/Day4TestInput.txt";
        var inputFile = "Day4/Day4Input.txt";

        var cards = FileReader.GetLines(inputFile);

        var totalPoints = 0.0;

        foreach (var card in cards)
        {
            var numbers = card.Split(':')[1];
            var numberParts = numbers.Split('|');
            var winningNumbers = numberParts[0].Trim().Split(' ').Where(s => s != string.Empty).Select(int.Parse);
            var revealedNumbers = numberParts[1].Trim().Split(' ').Where(s => s != string.Empty).Select(int.Parse);

            var winningCount = revealedNumbers.Where(n => winningNumbers.Contains(n)).Count();
            if (winningCount > 0)
            {
                var cardPoints = Math.Pow(2, winningCount - 1);
                totalPoints += cardPoints;
            }
        }

        Console.WriteLine($"Total points: {totalPoints}");
    }

    public void PartTwo()
    {
        var testFile = "Day4/Day4TestInput.txt";
        var inputFile = "Day4/Day4Input.txt";

        var cards = FileReader.GetLines(inputFile);

        var cardCount = 0;

        for (var cardNum = 1; cardNum <= cards.Length; cardNum++)
        {
            Console.WriteLine($"Checking card: {cardNum}");
            cardCount++;

            cardCount += CountCopies(cardNum, cards);
        }

        Console.WriteLine($"Total card count: {cardCount}");
    }

    private int CountCopies(int cardNum, string[] cards)
    {
        var card = cards[cardNum - 1];
        var copyCount = 0;

        var winningCount = GetWinningCount(card);

        if (winningCount > 0)
        {
            var lastCopiedCardNum = cardNum + winningCount;

            for (var copiedCardNum = cardNum + 1; copiedCardNum <= lastCopiedCardNum; copiedCardNum++)
            {
                copyCount++;

                copyCount += CountCopies(copiedCardNum, cards);
            }
        }

        return copyCount;
    }

    private int GetWinningCount(string card)
    {
        var numbers = card.Split(':')[1];
        var numberParts = numbers.Split('|');
        var winningNumbers = numberParts[0].Trim().Split(' ').Where(s => s != string.Empty).Select(int.Parse);
        var revealedNumbers = numberParts[1].Trim().Split(' ').Where(s => s != string.Empty).Select(int.Parse);

        var winningCount = revealedNumbers.Where(n => winningNumbers.Contains(n)).Count();

        return winningCount;
    }
}