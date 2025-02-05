using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// HeapQueue provides a queue collection that is always ordered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HeapQueue<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new();

        public int Count => _items.Count;

        public bool IsEmpty => _items.Count == 0;

        public T First => _items[0];

        public void Clear() => _items.Clear();

        public bool Contains(T item) => _items.Contains(item);

        public void Remove(T item) => _items.Remove(item);

        public T Peek() => _items[0];

        public void Push(T item)
        {
            //add item to end of tree to extend the list
            _items.Add(item);
            //find correct position for new item.
            SiftDown(0, _items.Count - 1);
        }

        public T Pop()
        {

            //if there are more than 1 items, returned item will be first in tree.
            //then, add last item to front of tree, shrink the list
            //and find correct index in tree for first item.
            T item;
            var last = _items[^1];
            _items.RemoveAt(_items.Count - 1);
            if (_items.Count > 0)
            {
                item = _items[0];
                _items[0] = last;
                SiftUp();
            }
            else
            {
                item = last;
            }
            return item;
        }


        private static int Compare(T A, T B) => A.CompareTo(B);

        private void SiftDown(int startpos, int pos)
        {
            //preserve the newly added item.
            var newitem = _items[pos];
            while (pos > startpos)
            {
                //find parent index in binary tree
                var parentpos = (pos - 1) >> 1;
                var parent = _items[parentpos];
                //if new item precedes or equal to parent, pos is new item position.
                if (Compare(parent, newitem) <= 0)
                    break;
                //else move parent into pos, then repeat for grand parent.
                _items[pos] = parent;
                pos = parentpos;
            }
            _items[pos] = newitem;
        }

        private void SiftUp()
        {
            var endpos = _items.Count;
            var startpos = 0;
            //preserve the inserted item
            var newitem = _items[0];
            var childpos = 1;
            var pos = 0;
            //find child position to insert into binary tree
            while (childpos < endpos)
            {
                //get right branch
                var rightpos = childpos + 1;
                //if right branch should precede left branch, move right branch up the tree
                if (rightpos < endpos && Compare(_items[rightpos], _items[childpos]) <= 0)
                    childpos = rightpos;
                //move child up the tree
                _items[pos] = _items[childpos];
                pos = childpos;
                //move down the tree and repeat.
                childpos = 2 * pos + 1;
            }
            //the child position for the new item.
            _items[pos] = newitem;
            SiftDown(startpos, pos);
        }
    }
}