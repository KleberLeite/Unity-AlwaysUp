using UnityEngine;

namespace AlwaysUp.Menus.Core
{
    public class Menu : MonoBehaviour
    {
        public bool IsOpen => gameObject.activeInHierarchy;

        virtual public void Open()
        {
            gameObject.SetActive(true);
        }

        virtual public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
