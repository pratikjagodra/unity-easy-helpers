using UnityEngine;

namespace EasyHelpers.Runtime.Tools.ScreenService
{
    public abstract class UIScreen : MonoBehaviour
    {
        [SerializeField] protected GameObject root;
        [SerializeField] protected bool isStackable = true;

        public bool IsStackable => isStackable;

        public virtual void OnShowScreen()
        {
            root.SetActive(true);
        }

        public virtual void OnHideScreen()
        {
            root.SetActive(false);
        }
    }
}