using UnityEngine;

namespace EasyHelpers.Runtime.Tools.ScreenService
{
    public abstract class UIScreen : MonoBehaviour
    {
        [SerializeField] protected GameObject root;
        public bool isStackable = true;

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