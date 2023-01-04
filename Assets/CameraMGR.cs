using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System.IO;
using TMPro;
public class CameraMGR : MonoBehaviour
{
    WebCamTexture camTexture;
    public TextMeshProUGUI wherePath;
    public RawImage cameraViewImgae;
    public RectTransform purposePos;

    public RawImage loadImage;


    [SerializeField] float width;
    [SerializeField] float height;

    [SerializeField] int rectwidth;
    [SerializeField] int rectheight;

    private void Start()
    {
        Debug.Log("�ʱ� ��δ� : " + Application.persistentDataPath);
        wherePath.text = Application.persistentDataPath;

    }


    public void CameraOn()
    {
        //ī�޶� ���� Ȯ��
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        //ī�޶� ���ٸ�
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("no camera!");
            return;
        }
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndex = -1;

        //�ĸ� ī�޶� ã�� 
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == true)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        //ī�޶� �ѱ�
        if (selectedCameraIndex >= 0)
        {
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);
            camTexture.filterMode = FilterMode.Trilinear;

            camTexture.requestedFPS = 30;//ī�޶� ������ ����

            cameraViewImgae.texture = camTexture;

            camTexture.Play();

            width = cameraViewImgae.texture.width;
            height = cameraViewImgae.texture.height;
            rectwidth = (int)cameraViewImgae.rectTransform.rect.width;
            rectheight = (int)cameraViewImgae.rectTransform.rect.height;
            Debug.Log(camTexture.width + " " + camTexture.height);
        }
    }
    //ī�޶� ���� 
    public void CameraOff()
    {
        if (camTexture != null)
        {
            camTexture.Stop();//ī�޶� ����
            WebCamTexture.Destroy(camTexture);//ī�޶� ��ü ����
            camTexture = null;//
        }
    }
    #region onechance camera'move
    //ī�޶� ���
    public void CameraShot()
    {
        if (camTexture != null)
        {
            camTexture.Stop();

            GameObject captured = new GameObject("CapturedImage");
            RawImage captureImage = captured.AddComponent<RawImage>();

            captureImage.transform.SetParent(FindObjectOfType<Canvas>().transform);
            captureImage.texture = cameraViewImgae.texture;
            captureImage.transform.localScale = cameraViewImgae.transform.localScale;
            captureImage.transform.position = this.transform.position;
            StartCoroutine(Co_ShotMove(captureImage.rectTransform));
        }
    }
    WaitForFixedUpdate time = new WaitForFixedUpdate();
    IEnumerator Co_ShotMove(RectTransform target)
    {
        while (true)
        {
            //    Debug.Log("������������ �����Ÿ� : " + Vector3.Distance(target.transform.position, Vector3.zero));
            if (Vector3.Distance(target.transform.position, Vector3.zero) < 1f)
            {
                yield break;
            }
            target.position = Vector3.Lerp(target.position, purposePos.position, 0.2f);
            yield return time;
            yield return null;
        }
        /*        CameraOff();
                Invoke("CameraOn", 0.5f);*/
    }
    #endregion

    public void Onclick_LoadImage()
    {
        Debug.Log("file Path : ");
        NativeGallery.GetImageFromGallery((file) =>
        {
            //�뷮����
            Debug.Log("file Path : 1��°��" + file);
            FileInfo selected = new FileInfo(file);
            //�뷮����
            Debug.Log("file Path : 2��°��" + file);
            if (selected.Length > 50000000)
            {
                Debug.Log("file Path ����");
                return;
            }
            if (!string.IsNullOrEmpty(file))
            {
                Debug.Log("file Path ����");

                //�ҷ��Ͷ�
                StartCoroutine(LoadImage(file));
            }
        });
        Debug.Log("file Path Ž���� ");
        if (loadImage.rectTransform.rotation != Quaternion.identity)
            loadImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

    }
    IEnumerator LoadImage(string path)
    {
        yield return null;
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/TestImage";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        File.WriteAllBytes(savePath + fileName + ".png", fileData);

        var temp = File.ReadAllBytes(savePath + fileName + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);
        loadImage.texture = tex;
        loadImage.rectTransform.rotation = Quaternion.identity;
    }
    public bool takingScreenshot = false;
    public void CaptureScreenshot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }
    private IEnumerator TakeScreenshotAndSave()
    {
        takingScreenshot = true;
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(rectwidth, rectheight, TextureFormat.RGB24, false); //���� �ΰ� ���ڴ� �ػ󵵸� �ǹ��Ѵ�.
        ss.ReadPixels(new Rect(0, 0, rectwidth, rectheight), 0, 0);  //190,1385�� ������ �ٻ�ġ
        //  ss.ReadPixels(new Rect(190,1385, Screen.width,Screen.height), 0 , 0);
        //  ss.ReadPixels(new Rect(190,1385,0,0), 0, 0); //���� �ΰ� ���ڴ� ��ġ, �ΰ��� ũ�⸦ �ǹ��Ѵ�. 
        ss.Apply();
        // Save the screenshot to Gallery/Photos
        string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName + " Captures", name));
        Destroy(ss);
        takingScreenshot = false;
    }
}
