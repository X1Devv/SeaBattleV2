using Agar.io_sfml.Game.Scripts.GameObjects;

namespace Agar.io_sfml.Game.Scripts.GameRule
{
    public class ShipGenerator
    {
        private Random _random;
        private int _gridWidth;
        private int _gridHeight;
        private readonly int[] _shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        public ShipGenerator(int gridWidth, int gridHeight)
        {
            _random = new Random();
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
        }

        public List<Ship> GenerateShips(int cellSize)
        {
            List<Ship> ships = new();
            HashSet<(int, int)> occupied = new();

            foreach (int size in _shipSizes)
            {
                bool placed = false;
                bool horizontal = _random.Next(0, 2) == 0;

                while (!placed)
                {
                    int x = _random.Next(0, horizontal ? _gridWidth - size + 1 : _gridWidth);
                    int y = _random.Next(0, horizontal ? _gridHeight : _gridHeight - size + 1);

                    if (CanPlaceShip(x, y, size, horizontal, occupied))
                    {
                        for (int i = 0; i < size; i++)
                        {
                            int shipX = horizontal ? x + i : x;
                            int shipY = horizontal ? y : y + i;
                            ships.Add(new Ship(shipX, shipY, cellSize));
                            occupied.Add((shipX, shipY));
                        }
                        placed = true;
                    }
                }
            }
            return ships;
        }

        private bool CanPlaceShip(int x, int y, int size, bool horizontal, HashSet<(int, int)> occupied)
        {
            for (int i = 0; i < size; i++)
            {
                int checkX = horizontal ? x + i : x;
                int checkY = horizontal ? y : y + i;

                if (occupied.Contains((checkX, checkY)))
                    return false;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int adjX = checkX + dx;
                        int adjY = checkY + dy;
                        if (occupied.Contains((adjX, adjY)))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}