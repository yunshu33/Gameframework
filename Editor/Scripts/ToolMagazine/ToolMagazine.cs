#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		ToolMagazine.cs
// Author Name :		YunShu
// Create Time :		2022/05/12 10:34:04
// Description :        YunTools 工具方法合集
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Excel;
using System.Data;
using UnityEngine.Events;

namespace Yun.Tools
{
    /// <summary>
    /// YunTools 工具方法合集
    /// </summary>
    public static class ToolMagazine
    {

        /// <summary>
        /// 获得拖入文件地址   待完善指定区域
        /// </summary>
        /// <param name="window">继承EditorWindow类的实例</param>
        /// <param name="Callback">回调函数 传入string数组</param>
        public static void GetDragFilePath(EditorWindow window, UnityAction<string[]> Callback)
        {
            if (EditorWindow.mouseOverWindow == window)
            {
                //鼠标位于当前窗口
                if (Event.current.type == EventType.DragUpdated)
                {
                    //拖入窗口未松开鼠标
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                    //改变鼠标外观
                }
                else if (Event.current.type == EventType.DragExited)
                {
                    //拖入窗口并松开鼠标


                    window.Focus();
                    //获取焦点，使unity置顶(在其他窗口的前面)
                    Rect rect = EditorGUILayout.GetControlRect();

                    rect.Contains(Event.current.mousePosition);
                    //可以使用鼠标位置判断进入指定区域

                    if (DragAndDrop.paths != null)
                    {
                        int len = DragAndDrop.paths.Length;

                        Callback(DragAndDrop.paths);
                        
                    }
                }
            }
        }



        /// <summary>
        /// 判断目标是文件夹还是目录(目录包括磁盘)
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <returns>返回true为一个文件夹，返回false为一个文件</returns>
        public static bool IsDir(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            if ((fi.Attributes & FileAttributes.Directory) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}