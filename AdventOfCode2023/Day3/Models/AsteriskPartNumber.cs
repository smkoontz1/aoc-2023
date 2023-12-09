namespace AdventOfCode2023.Day3.Models
{
    internal record AsteriskPartNumber(
        int RowIndex,
        int ColIndex,
        List<int> AdjacentPartNumbers);
}
