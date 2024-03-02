using UnityEngine;

namespace AlwaysUp.Utils
{
    public class ObjectID : MonoBehaviour
    {
        public int ID { get; private set; }

        public void SetID(int id) => ID = id;
    }
}
