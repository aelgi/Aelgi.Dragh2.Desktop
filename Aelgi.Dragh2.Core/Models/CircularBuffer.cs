using System;
using System.Collections;
using System.Collections.Generic;

namespace Aelgi.Dragh2.Core.Models
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        protected T[] _buffer;
        protected int _nextPos;

        public CircularBuffer(int size)
        {
            _buffer = new T[size];
            _nextPos = 0;
        }

        public void Add(T o)
        {
            _buffer[_nextPos] = o;
            _nextPos = ++_nextPos % _buffer.Length;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_buffer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_buffer).GetEnumerator();
        }
    }
}
