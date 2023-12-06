
public class Day2
{
    public void PartOne()
    {
        var bagRedCount = 12;
        var bagGreenCount = 13;
        var bagBlueCount = 14;

        var possibleGameSum = 0;

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var inputFile = Path.Combine(baseDirectory, "Day2/Day2Input.txt");

        var lines = File.ReadAllLines(inputFile);

        foreach (var line in lines)
        {
            var isGamePossible = true;

            var gameParts = line.Split(':');
            var gameNum = int.Parse(gameParts[0].Trim().Split(' ')[1]);
            
            var subsetStr = gameParts[1];
            var subsets = subsetStr.Split(';');

            foreach(var subset in subsets)
            {
                var colors = subset.Split(',');

                foreach (var color in colors)
                {
                    var colorTrimmed = color.Trim();
                    var colorParts = colorTrimmed.Split(' ');
                    var count = int.Parse(colorParts[0]);
                    var colorStr = colorParts[1];

                    switch(colorStr)
                    {
                        case "red":
                            if (count > bagRedCount)
                            {
                                isGamePossible = false;
                            }
                            break;
                        case "green":
                            if (count > bagGreenCount)
                            {
                                isGamePossible = false;
                            }
                            break;
                        case "blue":
                            if (count > bagBlueCount)
                            {
                                isGamePossible = false;
                            }
                            break;
                    }
                }
            }

            if (isGamePossible)
            {
                possibleGameSum += gameNum;
            }
        }

        Console.WriteLine($"ID Sum: {possibleGameSum}");
    }

    public void PartTwo()
    {
        var powerSum = 0;

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var inputFile = Path.Combine(baseDirectory, "Day2/Day2Input.txt");

        var lines = File.ReadAllLines(inputFile);

        foreach (var line in lines)
        {
            var gameParts = line.Split(':');
            var gameNum = int.Parse(gameParts[0].Trim().Split(' ')[1]);
            
            var subsetStr = gameParts[1];
            var subsets = subsetStr.Split(';');

            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;

            foreach(var subset in subsets)
            {
                var colors = subset.Split(',');

                foreach (var color in colors)
                {
                    var colorTrimmed = color.Trim();
                    var colorParts = colorTrimmed.Split(' ');
                    var count = int.Parse(colorParts[0]);
                    var colorStr = colorParts[1];

                    switch(colorStr)
                    {
                        case "red":
                            if (count > maxRed)
                            {
                                maxRed = count;
                            }
                            break;
                        case "green":
                            if (count > maxGreen)
                            {
                                maxGreen = count;
                            }
                            break;
                        case "blue":
                            if (count > maxBlue)
                            {
                                maxBlue = count;
                            }
                            break;
                    }
                }
            }

            var power = maxRed * maxGreen * maxBlue;
            powerSum += power;
        }

        Console.WriteLine($"Power Sum: {powerSum}");
    }
}