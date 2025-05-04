using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Data;
using UnityEngine.Networking;

namespace Yun.Tools
{
    [Serializable]
    public class InformationNode
    {
        /// <summary>
        /// 名称
        /// </summary>
         
        public List<string> nameList = new List<string>();


        /// <summary>
        /// 图片列表
        /// </summary>
        public List<Texture> textureList = new List<Texture>();


        /// <summary>
        /// 声音列表
        /// </summary>
        public List<AudioClip> audioList = new List<AudioClip>();
    }
  


    public class MyWindow : EditorWindow
    {
        
        public List<AudioClip> audioList = new List<AudioClip>();
        public List<Sprite> spriteList = new List<Sprite>();
        private SerializedObject serializedObject;
        /// <summary>
        /// 序列化 Texture
        /// </summary>


        private SerializedProperty serializedAudioList;
        private SerializedProperty serializedSpriteList;


        [MenuItem("YunTools/MyWindow")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            MyWindow window = (MyWindow)GetWindow(typeof(MyWindow));
            window.Show();
            window.minSize = new Vector2(200, 300);
        }
        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            //根据名称序列化属性

            serializedSpriteList = serializedObject.FindProperty("spriteList");

            serializedAudioList = serializedObject.FindProperty("audioList");
        }
        private void OnGUI()
        {


            ToolMagazine.GetDragFilePath(this, FilePath);

            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(serializedAudioList, true);

            EditorGUILayout.PropertyField(serializedSpriteList, true);
            serializedObject.ApplyModifiedProperties();
           
            EditorGUILayout.EndVertical();


        }
        
        public void FilePath(string[] filepath)
        {
            foreach (var item in filepath)
            {
                if(ToolMagazine.IsDir(item))
                {

                    DirectoryInfo root = new DirectoryInfo(item);
                    FileInfo[] files = root.GetFiles();
                    foreach (FileInfo file in files) {
                        AnalysisFile(file.ToString());
                    }
                }
                else
                {
                    AnalysisFile(item);
                }
            }
            
        }
        /// <summary>
        /// texture列表中添加图片
        /// </summary>
        /// <param name="path"> 路径</param>
        void AddTexture(string path)
        {
            byte[] imgByte;
            Vector2 v;
            FileLoader.ImageFileInfo(path, out imgByte, out v);
            Texture2D tx = new Texture2D((int)v.x, (int)v.y);
            tx.LoadImage(imgByte);
           // textureList.Add(tx);
        }
        /// <summary>
        /// 分析文件添加至对应的list
        /// </summary>
        void AnalysisFile(string path)
        {
            string[] str = path.Split('.');
            if (str.Length > 1 && str.Length <= 2)
            {
                switch (str[1])
                {
                    case "png":
                        this.StartCoroutine("LoaderTexture", path);
                        break;
                    case "xlsx":
                        DataSet excel= FileLoader.ReadExcel(path);
                        break;
                    case "wav":
                        this.StartCoroutine("LoaderAudio", path);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 携程加载图片
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        IEnumerator LoaderTexture(string url)
        {
            //url = FileLoader.StreamingAssetsUrl() + url;
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                url = "file://" + url;
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    //Debug.Log(sprite.rect.size);
                    spriteList.Add(sprite);
                    // Get downloaded asset bundle
                }
            }
        }

        IEnumerator LoaderAudio(string url)
        {

            using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
            {
                url = "file://" + url;
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    audioList.Add(DownloadHandlerAudioClip.GetContent(uwr));
                }
            }
        }
    }
}
