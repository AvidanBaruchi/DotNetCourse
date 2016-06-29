using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    class MultiDictionary<K,V> : IMultiDictionary<K, V>, IEnumerable<KeyValuePair<K, V>>
    {
        private Dictionary<K, LinkedList<V>> _innerDictionary = new Dictionary<K, LinkedList<V>>();
        private int _count = 0;

        public int Count
        {
            get
            {
                int count = 0;

                foreach (var list in _innerDictionary.Values)
                {
                    count += list.Count;
                }

                //return count;
                return _count;
            }
        }

        public ICollection<K> Keys
        {
            get
            {
                return _innerDictionary.Keys;
            }
        }

        public ICollection<V> Values
        {
            get
            {
                List<V> collection = new List<V>();

                foreach (var list in _innerDictionary.Values)
                {
                    collection.AddRange(list);
                }

                return collection;
            }
        }

        public void Add(K key, V value)
        {
            bool hasKey = _innerDictionary.ContainsKey(key);

            if (!hasKey)
            {
                _innerDictionary[key] = new LinkedList<V>();
            }

            if(!_innerDictionary[key].Contains(value))
            {
                _innerDictionary[key].AddLast(value);
                _count += 1;
            }
        }

        public void Clear()
        {
            _innerDictionary.Clear();
            _count = 0;
        }

        public bool Contains(K key, V value)
        {
            bool contains = false;

            if (_innerDictionary.ContainsKey(key))
            {
                contains = _innerDictionary[key].Contains(value);
            }

            return contains;
        }

        public bool ContainsKey(K key)
        {
            return _innerDictionary.ContainsKey(key);
        }

        public bool Remove(K key)
        {
            bool isRemoved = false;
            int count = 0;

            if (_innerDictionary.ContainsKey(key))
            {
                if (_innerDictionary[key] != null)
                {
                    count = _innerDictionary[key].Count; 
                }
            }

            isRemoved = _innerDictionary.Remove(key);

            if(isRemoved) { _count -= count; }

            return isRemoved;
        }

        public bool Remove(K key, V value)
        {
            bool isRemoved = false;
            LinkedList<V> listOfKey = null;

            if (_innerDictionary.ContainsKey(key))
            {
                listOfKey = _innerDictionary[key];

                if (listOfKey != null)
                {
                    isRemoved = listOfKey.Remove(value);

                    if (isRemoved) { _count -= 1; } 
                }

                if(listOfKey.Count == 0)
                {
                    Remove(key);
                }
            }
            
            return isRemoved;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            // Note: I made 3 implementation:
            // 1: Naive Implementation
            KeyValuePair<K, LinkedList<V>> currentPair;
            List<KeyValuePair<K,V >> collection = new List<KeyValuePair<K, V>>();

            foreach (var item in _innerDictionary)
            {
                currentPair = item;

                foreach (var value in currentPair.Value)
                {
                    collection.Add(new KeyValuePair<K, V>(currentPair.Key, value));
                }
            }

            // 1: Naive Implementation
            //return collection.GetEnumerator(); 

            // 2: Enumerator class inside MultiDictionary class
            //return new Enumerator(this);


            // 3: Yield Implementation
            foreach (var item in _innerDictionary)
            {
                currentPair = item;

                foreach (var value in currentPair.Value)
                {
                    yield return new KeyValuePair<K, V>(currentPair.Key, value);
                }
            }
        }

        /// <summary>
        /// Enumerator of class MultiDictionary.
        /// </summary>
        public class Enumerator : IEnumerator<KeyValuePair<K, V>>
        {
            private MultiDictionary<K, V> _parent;
            private Dictionary<K, LinkedList<V>>.KeyCollection.Enumerator _keysEnumerator;
            private LinkedList<V>.Enumerator _valuesEnumerator;
            private bool _movedNext = false;

            public Enumerator(MultiDictionary<K,V> parent)
            {
                _parent = parent;
                Reset();
            }

            public bool MoveNext()
            {
                if(_movedNext)
                {
                    _movedNext = _valuesEnumerator.MoveNext();

                    if(!_movedNext)
                    {
                        _movedNext = _keysEnumerator.MoveNext();

                        if(_movedNext)
                        {
                            _valuesEnumerator = _valuesEnumerator = _parent._innerDictionary[_keysEnumerator.Current].GetEnumerator();
                            _movedNext = _valuesEnumerator.MoveNext();
                        }
                    }
                }

                return _movedNext;
            }

            public KeyValuePair<K, V> Current
            {
                get
                {
                    K key = _keysEnumerator.Current;
                    V value = _valuesEnumerator.Current;

                    return new KeyValuePair<K, V>(key, value);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                _parent = null;
                _keysEnumerator.Dispose();
                _valuesEnumerator.Dispose();
                _movedNext = false;
            }

            public void Reset()
            {
                _keysEnumerator = _parent._innerDictionary.Keys.GetEnumerator();

                _movedNext = _keysEnumerator.MoveNext();

                if(_movedNext)
                {
                    _valuesEnumerator = _parent._innerDictionary[_keysEnumerator.Current].GetEnumerator();
                }
            }
        }
    }
}
