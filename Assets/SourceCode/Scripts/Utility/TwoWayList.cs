using System.Collections.Generic;

namespace GameDevsSourceCode
{
    namespace Utility
    {
        public class TwoWayList<T, U>
        {
            public class KeyValuePair
            {
                private T _key;
                private U _value;

                public T Key { get { return _key; } }
                public U Value { get { return _value; } }

                public KeyValuePair(T key, U value)
                {
                    this._key = key;
                    this._value = value;
                }

                public static implicit operator bool(KeyValuePair val)
                {
                    return (val != null ? true : false);
                }
            }

            private List<T> _keys;
            private List<U> _values;

            public TwoWayList()
            {
                _keys = new List<T>();
                _values = new List<U>();
            }
            public TwoWayList(List<T> keys, List<U> values)
            {
                this._keys = keys;
                this._values = values;
            }

            public KeyValuePair Add(T key, U value)
            {
                _keys.Add(key);
                _values.Add(value);

                return (new KeyValuePair(key, value));
            }
            public KeyValuePair Remove(T key)
            {
                int index = _keys.IndexOf(key);
                if (index >= 0)
                {
                    KeyValuePair pair = new KeyValuePair(_keys[index], _values[index]);
                    _values.Remove(this[key]);
                    _keys.Remove(key);

                    return (pair);
                }

                return (null);
            }
            public KeyValuePair Remove(U value)
            {
                int index = _values.IndexOf(value);
                if (index >= 0)
                {
                    KeyValuePair pair = new KeyValuePair(_keys[index], _values[index]);
                    _keys.Remove(this[value]);
                    _values.Remove(value);

                    return (pair);
                }

                return (null);
            }

            public bool Contains(T key)
            {
                return (_keys.Contains(key));
            }
            public bool Contains(U value)
            {
                return (_values.Contains(value));
            }

            public T this[U value]
            {
                get
                {
                    if (_keys != null && _values != null)
                    {
                        int __keysIndex = _values.IndexOf(value);
                        if (__keysIndex >= 0)
                            return (_keys[__keysIndex]);
                    }

                    return default(T);
                }
            }
            public U this[T key]
            {
                get
                {
                    if (_keys != null && _values != null)
                    {
                        int __valueIndex = _keys.IndexOf(key);
                        if (__valueIndex >= 0)
                            return (_values[__valueIndex]);
                    }

                    return default(U);
                }
            }

            public int Count { get { if (_keys.Count == _values.Count) return _keys.Count; else return -1; } }

            public List<T> Keys { get { return _keys; } }
            public List<U> Values { get { return _values; } }
        }



    }
}