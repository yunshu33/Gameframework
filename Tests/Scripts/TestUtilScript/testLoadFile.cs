#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		testLoadFile.cs
// Author Name :		YunShu
// Create Time :		2022/06/02 13:50:36
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using LJVoyage.GameEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



namespace LJVoyage.Game.Test
{
   
    public class testLoadFile : MonoBehaviour
    {
        public float i;

        public Image image;

        public AsyncLoaderFiles asyncLoaderFiles;
        void Start()
        {
            //test();
            var loopList = new LoopList<string>();
            
            Debug.Log(loopList.count);
        }

        private void Update()
        {

        }
        async  void test()
        {
            asyncLoaderFiles = new AsyncLoaderFiles();
            image.sprite = await asyncLoaderFiles.AsyncLoadSprite(@"\Photo\1.png");
        }


    }
}