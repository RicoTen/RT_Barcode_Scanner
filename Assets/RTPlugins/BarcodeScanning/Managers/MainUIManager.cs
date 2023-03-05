using System;
using RTPlugins.BarcodeScanning.Panels;
using RTPlugins.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace RTPlugins.BarcodeScanning.Managers
{
    public class MainUIManager : MonoSingleton<MainUIManager>
    {
        [SerializeField] private RectTransform entryTransform;
        [SerializeField] private Button toEditBtn, toPayBtn;
        [SerializeField] private BasePanel editPanel, payPanel;

        private void Awake()
        {
            toEditBtn.onClick.AddListener(() =>
            {
                editPanel.OpenPanel();
                entryTransform.gameObject.SetActive(false);
            });
            toPayBtn.onClick.AddListener(() =>
            {
                payPanel.OpenPanel();
                entryTransform.gameObject.SetActive(false);
            });
        }
    }
}
