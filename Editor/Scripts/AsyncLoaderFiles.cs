#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		LoaderFiles.cs
// Author Name :		YunShu
// Create Time :		2022/06/02 10:33:30
// Description :         加载文件
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Runtime.CompilerServices;

namespace LJVoyage.Game.Editor
{
    /// <summary>
    /// 异步加载文件
    /// </summary>
    public class AsyncLoaderFiles
    {
        public float DownloadProgress
        {
            get
            {
                if (requestCache != null)
                    return requestCache.downloadProgress;
                else
                    //UnityWebRequest对象是一次性的，下载完成后就被销毁，所以不能再去访问，否则会出现错误。
                    return -1;
            }
        }

        public UnityWebRequest requestCache;
        public async Task<Sprite> AsyncLoadSprite(string url)
        {
            //判断文件夹是否存在 不能用于判断文件是否存在。
            //Directory.Exists("D:\\DiscBurn")
            

            //判断文件是否存在 不能用于判断文件夹是否存在
            //File.Exists("D:\\DiscBurn\\aa.txt")
            url = Application.streamingAssetsPath + url;

            Debug.Log(url);
            if (!File.Exists(url))
            {
                return null;
            }

            var getRequest = UnityWebRequestTexture.GetTexture(url);
            requestCache = getRequest;

            await getRequest.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (getRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(getRequest.error);
            }
#else
            if (getRequest.isNetworkError && getRequest.isHttpError)
            {
                Debug.Log(getRequest.error);
            }
#endif
            var texture = DownloadHandlerTexture.GetContent(getRequest);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), pivot, 100.0f);
            return sprite;
        }

        public async Task<AudioClip> AsyncLoadAudioClip(string url)
        {
            if (!File.Exists(url))
            {
                return null;
            }

            var getRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
            requestCache = getRequest;
            await getRequest.SendWebRequest();

#if UNITY_2020_1_OR_NEWER

            if (getRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(getRequest.error);
            }

#else
            if (getRequest.isNetworkError && getRequest.isHttpError)
            {
                Debug.Log(getRequest.error);
            }

#endif

            return DownloadHandlerAudioClip.GetContent(getRequest);

        }

    }


    /// <summary>
    /// 异步拓展
    /// </summary>
    public static class AsyncExpand
    {
        /// <summary>
        /// 扩展方法
        /// https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        /// </summary>
        /// <param name="asyncOp"></param>
        /// <returns></returns>
        public  static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
        {
            var tcs = new TaskCompletionSource<object>();
            asyncOp.completed += obj => { tcs.SetResult(null); };
            return ((System.Threading.Tasks.Task)tcs.Task).GetAwaiter();
        }
    }

}