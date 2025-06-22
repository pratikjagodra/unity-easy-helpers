using UnityEngine;

namespace EasyHelpers.Runtime.Tools.ScreenService
{
    public abstract class UIScreen : MonoBehaviour
    {
        [SerializeField] protected GameObject root;
        [SerializeField] protected bool isStackable = true;

        internal bool IsStackable => isStackable;

        internal virtual void OnShowScreen()
        {
            root.SetActive(true);
        }

        internal virtual void OnHideScreen()
        {
            root.SetActive(false);
        }
    }
}