using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Core.Models
{
    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Position() : this(0, 0) { }

        public Position Clone()
        {
            return new Position(X, Y);
        }

        public static Position operator +(Position pos) => new Position(pos.X, pos.Y);
        public static Position operator -(Position pos) => new Position(-pos.X, -pos.Y);
        public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);
        public static bool operator >(Position a, Position b) => a.X > b.X && a.Y > b.Y;
        public static bool operator <(Position a, Position b) => a.X < b.X && a.Y < b.Y;
        public static bool operator >=(Position a, Position b) => a.X >= b.X && a.Y >= b.Y;
        public static bool operator <=(Position a, Position b) => a.X <= b.X && a.Y <= b.Y;
        public static Position operator /(Position pos, int scalar) => new Position(pos.X / scalar, pos.Y / scalar);
        public static Position operator *(Position pos, int scalar) => new Position(pos.X * scalar, pos.Y * scalar);

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X * 1000 + Y;
        }

        public override string ToString()
        {
            return $"Position {{{X}, {Y}}}";
        }
    }
}
