using System;
using UnityEngine;

namespace RTPlugins.BarcodeScanning.Panels
{
    public abstract class BasePanel : MonoBehaviour
    {
        private Action _onPanelOpen;
        private Action _onPanelClose;

        public void OpenPanel()
        {
            gameObject.SetActive(true);
            _onPanelOpen?.Invoke();
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
            _onPanelClose?.Invoke();
        }

        public void AddOpenListener(Action action) => _onPanelOpen += action;

        public void AddCloseListener(Action action) => _onPanelClose += action;

        public void RemoveOpenListener(Action action) => _onPanelClose -= action;
    
        public void RemoveCloseListener(Action action) => _onPanelClose -= action;

        public void RemoveAllOpenListener() => _onPanelOpen = null;
    
        public void RemoveAllCloseListener() => _onPanelClose = null;
    }
}