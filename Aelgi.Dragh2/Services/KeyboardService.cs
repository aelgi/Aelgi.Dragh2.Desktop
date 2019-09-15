using Aelgi.Dragh2.Core.Enums;
using Aelgi.Dragh2.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aelgi.Dragh2.Services
{
    public class KeyboardService : IKeyboardService
    {
        protected Dictionary<Key, bool> _keyState = new Dictionary<Key, bool>();

        public void Clear()
        {
            _keyState.Clear();
        }

        public void SetKey(Key key, bool isPressed)
        {
            if (_keyState.ContainsKey(key)) _keyState.Remove(key);
            _keyState.Add(key, isPressed);
        }

        public bool IsPressed(Key key)
        {
            if (_keyState.ContainsKey(key)) return _keyState[key];
            return false;
        }
    }
}
