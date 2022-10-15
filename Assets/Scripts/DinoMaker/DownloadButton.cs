using MolkExtras;
using UnityEngine;

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
using System.Diagnostics;
using System.IO;
using DinoMaker.Utils;
using Debug = UnityEngine.Debug;
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace DinoMaker
{
    public class DownloadButton : MonoBehaviour
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void DownloadFile(byte[] array, int byteLength, string fileName);
        #endif
        
        [SerializeField] private RenderTexture dinoTexture;
        [SerializeField] private Transform canvas;
        [SerializeField] private GameObject dinoCam;
        [SerializeField] private float scaleFactor = 3.2f;

        private RectTransform _dinoInstance;
        
        public void Download()
        {
            dinoCam.SetActive(true);
            _dinoInstance = Instantiate(DinoController.Instance.DinoBody, canvas);
            _dinoInstance.anchoredPosition = Vector2.zero;
            _dinoInstance.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            
            string fileName = NCuid.Cuid.Generate();
            
            #if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            string filePath = ProjectConsts.NewFilePath(fileName);
            #endif
            
            this.ExecuteAtEndOfFrame(() =>
            {
                byte[] imageData = GetJpgBytesFromRenderTexture(dinoTexture);
                
                #if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                File.WriteAllBytes(filePath, imageData);
                ShowInFinder(filePath);
                #elif UNITY_WEBGL && !UNITY_EDITOR
                DownloadFile(imageData, imageData.Length, fileName);
                #endif

                Destroy(_dinoInstance.gameObject);
                dinoCam.SetActive(false);
            });
        }
        
        private static byte[] GetJpgBytesFromRenderTexture(RenderTexture renderTexture)
        {
            RenderTexture previousRenderTexture = RenderTexture.active;
            Vector2Int textureSize = new Vector2Int(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            Texture2D texture2D = new Texture2D(textureSize.x, textureSize.y, TextureFormat.RGBA32, false);
            texture2D.ReadPixels(new Rect(0, 0, textureSize.x, textureSize.y), 0, 0);
            RenderTexture.active = previousRenderTexture;
            return texture2D.EncodeToJPG();
        }

        #if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        private void ShowInFinder(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = "open";
            process.StartInfo.Arguments = "-n -R \"" + path + "\"";
            
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.ErrorDataReceived += OnFinderError;

            if (!process.Start())
            {
                Debug.LogError("Finder process start encountered an error."); 
                return;
            }
            
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
        }
        
        private static void OnFinderError(object sender, DataReceivedEventArgs args)
        {
            string data = args.Data;
            
            if (!string.IsNullOrWhiteSpace(data))
            {
                Debug.LogError($"Finder Error: {data}");
            }
        }
        #endif
    }
}
