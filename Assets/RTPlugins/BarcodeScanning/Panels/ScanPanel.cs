using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;

namespace RTPlugins.BarcodeScanning.Panels
{
    public class ScanPanel : BasePanel
    {
        [SerializeField] private RawImage targetImage;
        [SerializeField] private Text text;

        private WebCamTexture _cameraTexture;

        public string CurrentBarcode { get; private set; }
        private static readonly BarcodeReader Barcode;

        static ScanPanel()
        {
            Barcode = new BarcodeReader
            {
                AutoRotate = true,
                Options =
                {
                    PossibleFormats = new List<BarcodeFormat>(new[] { BarcodeFormat.All_1D }),
                }
            };
        }

        private void Awake()
        {
            var webCamDeviceIndex = -1;
            for (var i = 0; i < WebCamTexture.devices.Length; i++)
            {
                if (!WebCamTexture.devices[i].isFrontFacing)
                {
                    webCamDeviceIndex = i;
                    break;
                }
            }
            if (webCamDeviceIndex == -1 && WebCamTexture.devices.Length > 0) webCamDeviceIndex = 0;
            
            _cameraTexture = new WebCamTexture(WebCamTexture.devices[webCamDeviceIndex].name, 1000, 1000);
            targetImage.texture = _cameraTexture;
            _cameraTexture.Play();
            
            AddOpenListener(() =>
            {
                StartCoroutine(OnScanning());
            });
            AddCloseListener(() =>
            {
                StopAllCoroutines();
                CurrentBarcode = null;
                _cameraTexture.Stop();
                targetImage.texture = null;
                Destroy(_cameraTexture);
            });
        }
        
        private IEnumerator OnScanning()
        {
            while (_cameraTexture)
            {
                var result = Barcode.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);

                if (result != null)
                {
                    CurrentBarcode = result.Text;
                    text.text = CurrentBarcode;

                    Debug.Log(CurrentBarcode);
                }
                else
                {
                    text.text = "Scanning...";
                }

                yield return new WaitForSeconds(1);
            }
        }
    }
}
