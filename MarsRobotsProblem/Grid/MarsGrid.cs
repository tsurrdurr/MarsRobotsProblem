using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MarsRobotsProblem.Grid
{
    public class MarsGrid
    {
        public readonly int MaxX;
        public readonly int MaxY;
        public readonly int MinX;
        public readonly int MinY;
        public HashSet<Scent> Scents;

        public MarsGrid(int maxX, int maxY, int minX = 0, int minY = 0)
        {
            MaxX = maxX;
            MaxY = maxY;
            MinX = minX;
            MinY = minY;
            Scents = new HashSet<Scent>();
        }

        public void AddScent(MarsCoordinates coordinates, RobotDirection direction)
        {
            Scents.Add(new Scent(coordinates, direction));
        }

        public bool HasScent(MarsCoordinates coordinates, RobotDirection direction)
        {
            return Scents.Contains(new Scent(coordinates, direction));
        }
    }
}
