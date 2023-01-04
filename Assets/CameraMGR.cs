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
        //카메라 권한 확인
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        //카메라가 없다면
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("no camera!");
            return;
        }
        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCameraIndex = -1;

        //전면 카메라 찾기 
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == true)
            {
                selectedCameraIndex = i;
                break;
            }
        }

        //카메라 켜기
        if (selectedCameraIndex >= 0)
        {
            camTexture = new WebCamTexture(devices[selectedCameraIndex].name);
            camTexture.filterMode = FilterMode.Trilinear;

            camTexture.requestedFPS = 30;//카메라 프레임 설정

            cameraViewImage.texture = camTexture;

            camTexture.Play();
        }
    }
    //카메라 끄기 
    public void CameraOff()
    {
        if (camTexture != null)
        {
            camTexture.Stop();//카메라 정지
            WebCamTexture.Destroy(camTexture);//카메라 객체 삭제
            camTexture = null;//
        }
    }

    public void Onclick_LoadImage()
    {
        Debug.Log("file Path : ");
        NativeGallery.GetImageFromGallery((file) =>
        {
            //용량제한
            Debug.Log("file Path : 1번째줄" + file);
            FileInfo selected = new FileInfo(file);
            //용량제한
            Debug.Log("file Path : 2번째줄" + file);
            if (selected.Length > 50000000)
            {
                Debug.Log("file Path 없음");
                return;
            }
            if (!string.IsNullOrEmpty(file))
            {
                Debug.Log("file Path 있음");

                //불러와라
                StartCoroutine(LoadImage(file));
            }
        });
        Debug.Log("file Path 탐색끝 ");
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
