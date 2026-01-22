
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR;
using ZXing;
using System.Threading;
using System;

public class QRScanner : MonoBehaviour
{
    //public RawImage rawImage;
    string qrContent = null;
    PassthroughCameraAccess passthroughCameraAccess;
    public float initializationDelay = 10f;
    public float qrTimer = 4;
    public GameObject qrPrompt;
    bool passthroughInitialized = false;
    float elapsedTime = 0f;
    Texture2D cameraTexture;
    Color32[] cameraPixels;
    private Thread qrThread;
    bool quitting = false;
    int H = 1024;
    int W = 2048;
    bool readCode = false;
    void Start()
    {
        passthroughCameraAccess = GetComponent<PassthroughCameraAccess>();
        if (passthroughCameraAccess == null)
        {
            Debug.LogError("PassthroughCameraAccess component not found!");
            return;
        } else Debug.Log("PassthroughCameraAccess component found.");
        qrThread = new Thread(DecodeQR);
        qrThread.Start();
    }
    void OnEnable()
    {
        GetPassthroughTexture();
    }
    void OnApplicationQuit()
    {
        quitting = true;
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // wait to initialize the passthrough
        if (!passthroughInitialized) {
            if (elapsedTime >= initializationDelay)
            {
                Debug.Log(OVRPermissionsRequester.IsPermissionGranted(OVRPermissionsRequester.Permission.PassthroughCameraAccess) ? "Permission granted." : "No permission granted."); 
                GetPassthroughTexture();
                elapsedTime = 0f;
                passthroughInitialized = true;    
            }
            return;
        }
        cameraPixels = cameraTexture.GetPixels32();

        if (readCode && !(PaintingInfoUIManager.Instance.gameObject.activeInHierarchy || PaintingInfoUIManager.Instance.ShowingSplats))
        {
            //Debug.Log(qrContent);
            readCode = false;
            // Here, parse if the message is how we want it and request to open the corresponding menu
            if (Int32.TryParse(qrContent, out int identifier))
            {
                Debug.Log("Asking to open qr code: " + identifier);
                CollectionManager.Instance.QRPrompt(identifier);
            }

            elapsedTime = 0;
        }

        if (elapsedTime >= qrTimer)
        {
            qrPrompt.SetActive(false);
        }
    }
    public void GetPassthroughTexture()
    {
        Debug.Log("Getting texture");
        cameraTexture = (Texture2D)passthroughCameraAccess.GetTexture();
        //cameraTexture = (Texture2D)rawImage.mainTexture;
        H = cameraTexture.height;
        W = cameraTexture.width;
        cameraPixels = cameraTexture.GetPixels32();
        //rawImage.texture = cameraTexture;
    }
    void DecodeQR()
    {
        Debug.Log("DecodeQR");
        // create a reader with a custom luminance source
        var barcodeReader = new BarcodeReader { AutoRotate = false, Options = new ZXing.Common.DecodingOptions { TryHarder = false } };
        while (true)
        {
            if (quitting)
                break;
            try
            {
                // decode the current frame
                var result = barcodeReader.Decode(cameraPixels, W, H);
                if (result != null)
                {
                    qrContent = result.Text;
                    //Debug.Log(result.Text);
                    readCode = true;
                }
                // Sleep a little bit and set the signal to get the next frame
                Thread.Sleep(200);
            }
            catch
            {
            }
        }
    }
}
