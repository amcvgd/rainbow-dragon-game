using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowDragon.HelperClasses
{
    class MoveQueue
    {
        public Move first, last;

        public int size, capacity;

        public MoveQueue(int capacity)
        {
            this.capacity = capacity;
            size = 0;
        }

        public void Push(Move move)
        {
            if (size == 0)
                first = last = move;
            else
            {
                last.next = move;
                last = last.next;
            }
            size++;
        }

        public Move Pop()
        {
            if (IsEmpty()) return null;

            Move temp = first;
            first = first.next;
            size--;
            return temp;
        }

        public bool IsFull()
        {
            return size >= capacity;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public Move GetNextMove()
        {
            if (IsFull())
                return Pop();
            else
                return null;
        }
    }
}
