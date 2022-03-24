using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace solve_matrix
{
    public class AlesiaBuyalskaya
    {
        private static readonly char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };

        public static int SolveMatrix()
        {
            char?[,] input = new char?[5, 5]
            {
                {'a', 'b', 'c', null, 'e' },
                { null, 'g', 'h', 'i', 'j' },
                { 'k', 'l', 'm', 'n', 'o' },
                { 'p', 'q', 'r', 's', 't'},
                { 'u', 'v', null, null, 'y'}
            };

            var possibleMoves = GetPossibleMoves(input);

            var allCombination = 0;

            foreach (var cell in possibleMoves.Keys)
            {
                allCombination += GetCombinationsCount(possibleMoves, cell, 0, 1);
            }

            return allCombination;
        }

        private static Dictionary<Cell, List<Cell>> GetPossibleMoves(char?[,] input)
        {
            Dictionary<Cell, List<Cell>> possibleMoves = new Dictionary<Cell, List<Cell>>(new CellComparer());
            var matrixSize = input.GetLength(0);

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    if (input[i, j] != null)
                    {
                        var cell = new Cell { X = i, Y = j, Value = input[i, j].Value };
                        var moves = GetValidMoves(cell, matrixSize)
                            .Where(move => input[move.X, move.Y] != null)
                            .Select(move => { move.Value = input[move.X, move.Y].Value; return move; })
                            .ToList();
                        possibleMoves.Add(cell, moves);
                    }
                }
            }

            return possibleMoves;
        }

        private static List<Cell> GetValidMoves(Cell cell, int matrixSize)
        {
            var moves = new List<Cell>();
            var stepsX = new[] { -1, -2, -1, -2, 1, 2, 1, 2 };
            var stepsY = new[] { -2, -1, 2, 1, -2, -1, 2, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (TryGetValidMove(cell.X + stepsX[i], cell.Y + stepsY[i], matrixSize, out var validMove))
                {
                    moves.Add(validMove);
                }
            }

            return moves;
        }

        private static bool TryGetValidMove(int possibleMoveX, int possibleMoveY, int matrixSize, out Cell validMove)
        {
            validMove = null;
            if (possibleMoveX >= 0 && possibleMoveX < matrixSize && possibleMoveY >= 0 && possibleMoveY < matrixSize)
            {
                validMove = new Cell { X = possibleMoveX, Y = possibleMoveY };
                return true;
            }

            return false;
        }

        private static int GetCombinationsCount(Dictionary<Cell, List<Cell>> possibleMoves, Cell cell, int vowelCount, int step)
        {
            if (vowels.Contains(cell.Value))
            {
                vowelCount++;
            }

            if (vowelCount > 2)
                return 0;

            if (step == 8)
                return 1;

            var nextCells = possibleMoves[cell];
            var count = 0;
            step++;

            foreach (var nextCell in nextCells)
            {
                count += GetCombinationsCount(possibleMoves, nextCell, vowelCount, step);
            }

            return count;
        }
    }

    class Cell
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char Value { get; set; }
    }

    class CellComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell x, Cell y) => x.X == y.X && x.Y == y.Y;

        public int GetHashCode([DisallowNull] Cell obj) => obj.X.GetHashCode() * obj.Y.GetHashCode();
    }
}
