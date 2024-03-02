using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlwaysUp.Utils
{
    public interface IObjectPool<T>
    {
        T Get();
        void GiveBack(T @object);
    }
}
