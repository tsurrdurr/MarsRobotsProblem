using System;

namespace MarsRobotsProblem
{
    public struct MarsCoordinates : IEquatable<MarsCoordinates>
    {
        public int PositionX;
        public int PositionY;

        public MarsCoordinates(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        public bool Equals(MarsCoordinates other)
        {
            return PositionX == other.PositionX
                && PositionY == other.PositionY;
        }

        public override bool Equals(object? obj)
        {
            return obj != null
                && obj is MarsCoordinates
                && Equals((MarsCoordinates)obj);
        }

        public override int GetHashCode() => HashCode.Combine(PositionX, PositionY);
    }
}
