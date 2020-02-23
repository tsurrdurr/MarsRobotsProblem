using System;
using System.Diagnostics.CodeAnalysis;

namespace MarsRobotsProblem.Grid
{
    public struct Scent : IEquatable<Scent>
    {
        private readonly MarsCoordinates Coordinates;
        private readonly RobotDirection Direction;

        public Scent(MarsCoordinates coordinates, RobotDirection direction)
        {
            Coordinates = coordinates;
            Direction = direction;
        }

        public bool Equals([AllowNull] Scent other)
        {
            return Coordinates.Equals(other.Coordinates) && Direction == other.Direction;
        }

        public override bool Equals(object? obj)
        {
            return obj != null
                && obj is Scent
                && Equals((Scent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Coordinates, Direction);
        }
    }
}
