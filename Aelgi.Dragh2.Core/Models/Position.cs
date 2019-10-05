using System;

namespace Aelgi.Dragh2.Core.Models
{
    public class Position : IEquatable<Position>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Position() : this(0, 0) { }

        public Position Clone()
        {
            return new Position(X, Y);
        }

        public Position Add(double x, double y)
        {
            return new Position(X + x, Y + y);
        }

        public static Position operator +(Position pos) => new Position(pos.X, pos.Y);
        public static Position operator -(Position pos) => new Position(-pos.X, -pos.Y);
        public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);
        public static bool operator >(Position a, Position b) => a.X > b.X && a.Y > b.Y;
        public static bool operator <(Position a, Position b) => a.X < b.X && a.Y < b.Y;
        public static bool operator >=(Position a, Position b) => a.X >= b.X && a.Y >= b.Y;
        public static bool operator <=(Position a, Position b) => a.X <= b.X && a.Y <= b.Y;
        public static Position operator /(Position pos, double scalar) => new Position(pos.X / scalar, pos.Y / scalar);
        public static Position operator *(Position pos, double scalar) => new Position(pos.X * scalar, pos.Y * scalar);

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * 1000 + Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"Position {{{X}, {Y}}}";
        }
    }
}
