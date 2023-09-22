using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GameWorldFramework.RunTime.MVVM
{
    public abstract class ModelBase<T> : IDisposable
    {
        [NonSerialized]
        private Dictionary<string, List<Action<T>>> propertyChangedEvents = new Dictionary<string, List<Action<T>>>();

        internal void Set<Property>(T model, ref Property _oldValue, Property _newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(_oldValue, _newValue))
            {
                _oldValue = _newValue;

                if (propertyName != null && propertyChangedEvents.TryGetValue(propertyName, out var actions))
                {
                    foreach (var action in actions)
                    {
                        action(model);
                    }
                }
            }
        }

        public void Bind<TSource>(Expression<Func<T, TSource>> memberExpression, Action<T> action)
        {
            var propertyName = LambadUtilities.ParseMemberName(memberExpression);

            if (propertyChangedEvents.TryGetValue(propertyName, out var actions))
            {
                actions.Add(action);
            }
            else
            {
                actions = new List<Action<T>>
                {
                    action
                };

                propertyChangedEvents[propertyName] = actions;
            }

        }

        public void Unbind<TSource>(Expression<Func<T, TSource>> memberExpression, Action<T> action)
        {

            var propertyName = LambadUtilities.ParseMemberName(memberExpression);

            if (propertyChangedEvents.TryGetValue(propertyName, out var actions))
            {
                actions.Remove(action);
            }

        }


        public void Dispose()
        {

            foreach (var actions in propertyChangedEvents)
            {
                actions.Value.Clear();
            }

            propertyChangedEvents.Clear();
            propertyChangedEvents = null;
        }
    }
}

