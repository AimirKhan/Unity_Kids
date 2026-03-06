using System.Collections.Generic;
using R3;

namespace System.R3Ext
{
    public class ReactiveList<T>
    {
        private readonly List<T> list = new();
    
        // Event streams
        private readonly Subject<T> onAdd = new();
        private readonly Subject<int> onRemove = new();

        public Observable<T> OnAdd => onAdd;
        public Observable<int> OnRemove => onRemove;

        public int Count => list.Count;
        public T this[int index] => list[index];

        public void Add(T item)
        {
            list.Add(item);
            onAdd.OnNext(item);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < list.Count)
            {
                list.RemoveAt(index);
                onRemove.OnNext(index);
            }
        }

        public List<T> ToList()
        {
            return new List<T>(list);
        }
        
        public T Last() => list[list.Count - 1];
    }
}