#region Copyright

// **********************************************************************
// Copyright (C) 2023 
//
// Script Name :		CreateScript.cs
// Author Name :		云舒
// Create Time :		2023/04/10 12:56:01
// Description :
// **********************************************************************

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LJVoyage.Game.Editor
{
    public static class CreateScript
    {
        private static string DateFormat = "yyy/MM/dd HH:mm:ss";

        private static Texture2D scriptIcon = (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);

        [MenuItem("Assets/Create/GameWorld Script/Mono Script", false, 80)]
        static void CreateMonoScript()
        {
            string[] guids = AssetDatabase.FindAssets("NewBehaviourScriptTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("脚本模板丢失");
                return;
            }

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                ScriptableObject.CreateInstance<DoCreateMonoCodeFile>(),
                "NewBehaviourScript.cs",
                scriptIcon,
                path
            );
        }

        [MenuItem("Assets/Create/LJVoyage/Game Script/Mvvm Script", false, 80)]
        static void CreateMvvmScript()
        {
            string[] guids = AssetDatabase.FindAssets("NewViewModelScriptTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("脚本模板丢失");
                return;
            }

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                ScriptableObject.CreateInstance<DoCreateMvvmCodeFile>(),
                "NewViewModelScriptTemplate.cs",
                scriptIcon,
                path
            );
        }


        /// <summary>
        /// 创建c# 脚本
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        internal static UnityEngine.Object CreateCSharpScript(string pathName, string templatePath, string className)
        {
            string templateText = string.Empty;

            UTF8Encoding encoding = new UTF8Encoding(true, false);

            if (File.Exists(templatePath))
            {
                // Read procedures.
                StreamReader reader = new StreamReader(templatePath);
                templateText = reader.ReadToEnd();
                reader.Close();

                templateText = templateText.Replace("#SCRIPTNAME#", className);
                templateText = templateText.Replace("#NOTRIM#", string.Empty);
                // You can replace as many tags you make on your templates, just repeat Replace function
                // e.g.:
                // templateText = templateText.Replace("#NEWTAG#", "MyText");

                //Write procedures.

                StreamWriter writer = new StreamWriter(Path.GetFullPath(pathName), false, encoding);
                writer.Write(templateText);
                writer.Close();

                AssetDatabase.ImportAsset(pathName);
                return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
            }
            else
            {
                Debug.LogError(string.Format("The template file was not found: {0}", templatePath));
                return null;
            }
        }


        /// <summary>
        /// 创建mono代码文件
        /// </summary>
        public class DoCreateMonoCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                string className = Path.GetFileNameWithoutExtension(pathName).Replace(" ", string.Empty);

                UnityEngine.Object o = CreateScript.CreateCSharpScript(pathName, resourceFile, className);

                CopyrightAnnotation(pathName);

                ProjectWindowUtil.ShowCreatedAsset(o);
            }
        }

        /// <summary>
        /// 创建Mvvm代码文件
        /// </summary>
        public class DoCreateMvvmCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                string className = Path.GetFileNameWithoutExtension(pathName).Replace(" ", string.Empty);

                var name = Path.GetFileNameWithoutExtension(pathName);

                var dir = Path.GetDirectoryName(pathName);

                pathName = $"{dir}/{name}ViewModel.cs";

                UnityEngine.Object vm = CreateScript.CreateCSharpScript(pathName, resourceFile, className);

                var vPath = $"{dir}/{name}View.cs";

                var mPath = $"{dir}/{name}Model.cs";

                string[] guids = AssetDatabase.FindAssets("NewViewScriptTemplate.cs");
                
                if (guids.Length == 0)
                {
                    Debug.LogWarning("脚本模板丢失");
                    return;
                }

                UnityEngine.Object v =
                    CreateScript.CreateCSharpScript(vPath, AssetDatabase.GUIDToAssetPath(guids[0]), className);

                guids = AssetDatabase.FindAssets("NewModelScriptTemplate.cs");
                if (guids.Length == 0)
                {
                    Debug.LogWarning("脚本模板丢失");
                    return;
                }

                UnityEngine.Object m =
                    CreateScript.CreateCSharpScript(mPath, AssetDatabase.GUIDToAssetPath(guids[0]), className);

                CopyrightAnnotation(pathName);

                CopyrightAnnotation(vPath);

                CopyrightAnnotation(mPath);

                ProjectWindowUtil.ShowCreatedAsset(vm);

                ProjectWindowUtil.ShowCreatedAsset(v);

                ProjectWindowUtil.ShowCreatedAsset(m);
            }
        }

        /// <summary>
        /// 版权信息注释
        /// </summary>
        /// <param name="path"> 脚本路径 </param>
        static void CopyrightAnnotation(string path)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + nameof(GameWorldEditorConfig));

            var editorConfig =
                AssetDatabase.LoadAssetAtPath<GameWorldEditorConfig>(
                    AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault()));

            string _fileText = File.ReadAllText(path);

            _fileText = _fileText.Replace("#COPYRIGHTYEAR#", System.DateTime.Now.Year.ToString())
                .Replace("#AuthorName#", editorConfig.m_authorName)
                .Replace("#CreateTime#", System.DateTime.Now.ToString(DateFormat))
                .Replace("#NAMESPACE#", editorConfig.m_namespace)
                .Replace("#COMPANYNAME#", editorConfig.m_companyName);

            File.WriteAllText(path, _fileText);

            AssetDatabase.Refresh();
        }
    }
}