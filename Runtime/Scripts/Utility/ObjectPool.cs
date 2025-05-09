

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LJVoyage.Game.Runtime
{
    public class ObjectPool<T> where T : class , new()
    {
         Queue<T> queue = new Queue<T>();

         List<T> list = new List<T>();

        public T GetObject()
        {
            if (queue.Count > 0)
            {
                list.Add(queue.Dequeue());
                
                return list.Last(); 
            }
            else
            {
                var value = new T();
                list.Add(value);
                return value;
            }

        }

        public void Enqueue()
        {
            foreach (var item in list)
            {
                queue.Enqueue(item);
            }
            list.Clear();
        }


        public void AddObject(T obj)
        {
            queue.Enqueue(obj);
        }
      
        public void Clear()
        {
            queue.Clear();
        }

    }
}