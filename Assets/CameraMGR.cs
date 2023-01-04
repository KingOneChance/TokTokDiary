using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System.IO;
using TMPro;
public class CameraMGR : MonoBehaviour
{
    private WebCamTexture camTexture;
   [SerializeField] private RawImage cameraViewImage;

   [SerializeField] private RawImage loadImage;

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

        //���� ī�޶� ã�� 
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

            cameraViewImage.texture = camTexture;

            camTexture.Play();
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
    public void Camerashot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }
    private IEnumerator TakeScreenshotAndSave()
    {

        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(cameraViewImage.texture.width, cameraViewImage.texture.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(cameraViewImage.texture, ss);
        ss.Apply();
        byte[] imageData = ss.EncodeToPNG();
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(imageData, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
        
        Destroy(ss);

    }
}
