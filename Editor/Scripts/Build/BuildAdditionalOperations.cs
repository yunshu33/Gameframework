using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEditor.Rendering;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace LJVoyage.GameEditor
{
    /// <summary>
    /// 打包附加操作
    /// </summary>
    public class BuildAdditionalOperations : IPreprocessBuildWithReport, IPostprocessBuildWithReport, IPreprocessShaders,
        IProcessSceneWithReport, IActiveBuildTargetChanged
    {
        /// <summary>
        /// 切换平台
        /// </summary>
        /// <param name="previousTarget"></param>
        /// <param name="newTarget"></param>
        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
        }


        /// <summary>
        /// 打包后
        /// </summary>
        /// <param name="report"></param>
        public void OnPostprocessBuild(BuildReport report)
        {
        }

        public int callbackOrder => 0;

        /// <summary>
        /// 打包前
        /// </summary>
        /// <param name="report"></param>
        /// <exception cref="Exception"></exception>
        public void OnPreprocessBuild(BuildReport report)
        {
            // var guids = AssetDatabase.FindAssets("t:" + nameof(YGameWorldEditorConfig));
            //
            // var editorConfig =
            //     AssetDatabase.LoadAssetAtPath<YGameWorldEditorConfig>(
            //         AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault()));
            //
            // if (editorConfig is null)
            //
            //     throw new Exception("未创建 YunFramework EditorConfig");
            //
            // Debug.Log($"当前版本号为:{Application.version}");
            //
            // var str = PlayerSettings.bundleVersion.Split('.');
            //
            // if (str.Length < 3 || str.Length > 3)
            // {
            //     throw new Exception("版本号解析错误");
            // }
            //
            // PlayerSettings.bundleVersion =
            //     $"{int.Parse(str[0]) + editorConfig.versionStacking.x}.{int.Parse(str[1]) + editorConfig.versionStacking.y}.{int.Parse(str[2]) + editorConfig.versionStacking.z}";
            //
            // Debug.Log($"修改后的版本号为:{Application.version}");
        }


        /// <summary>
        /// 编译 shader 前
        /// </summary>
        /// <param name="shader"></param>
        /// <param name="snippet"></param>
        /// <param name="data"></param>
        public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
        {
        }

        /// <summary>
        /// 打包场景
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="report"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnProcessScene(Scene scene, BuildReport report)
        {
        }
    }
}