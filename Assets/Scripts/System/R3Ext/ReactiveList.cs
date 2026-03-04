using System.Collections.Generic;
using R3;

namespace System.R3Ext
{
    public class ReactiveList<T>
    {
        private readonly List<T> _list = new();
    
        // Event streams
        private readonly Subject<T> _onAdd = new();
        private readonly Subject<int> _onRemove = new();

        public Observable<T> OnAdd => _onAdd;
        public Observable<int> OnRemove => _onRemove;

        public int Count => _list.Count;
        public T this[int index] => _list[index];

        public void Add(T item)
        {
            _list.Add(item);
            _onAdd.OnNext(item);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _list.Count)
            {
                _list.RemoveAt(index);
                _onRemove.OnNext(index);
            }
        }

        public T Last() => _list[_list.Count - 1];
    }
}