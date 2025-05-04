using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class ParentScriptableObject : ScriptableObject
{
    //--------------父资源内容
    [SerializeField] public List<ChildScriptableObject> childs = new List<ChildScriptableObject>();
    //------------------------


    [SerializeField] public string str = string.Empty;

    const string PATH = "Assets/UIToolkit/Dialogue/Editor/Test/New ParentScriptableObject.asset";

    /// <summary>
    /// 创建父资源、子资源，并建立父子关系
    /// </summary>
    [MenuItem("Assets/Create ScriptableObject")]
    static void CreateScriptableObject()
    {
        //父物体实例化
        var parent = ScriptableObject.CreateInstance<ParentScriptableObject>();

        var childSO = ScriptableObject.CreateInstance<ChildScriptableObject>();

        //子物体实例化
        parent.childs.Add(childSO);

        //保存资源到本地
        AssetDatabase.CreateAsset(parent, PATH);

        //创建父子关系
        AssetDatabase.AddObjectToAsset(childSO, PATH);

        AssetDatabase.SaveAssets();

        //更新
        AssetDatabase.ImportAsset(PATH);
    }

    /// <summary>
    /// 删除子资源
    /// </summary>
    [MenuItem("Assets/Remove ChildScriptableObject")]
    static void Remove()
    {
        var parent = AssetDatabase.LoadAssetAtPath<ParentScriptableObject>(PATH);
        //アセットの parentScriptableObject を破棄
        // Object.DestroyImmediate(parent.child, true);
        //破棄したら Missing 状態になるので null を代入
        // parent.child = null;
        //再インポートして最新の情報に更新
        AssetDatabase.ImportAsset(PATH);
    }

    /// <summary>
    /// 删除子资源
    /// </summary>
    [MenuItem("Assets/Add ChildScriptableObject")]
    static void Add()
    {
        var parent = AssetDatabase.LoadAssetAtPath<ParentScriptableObject>(PATH);

        var a = ScriptableObject.CreateInstance<AScriptableObject>();

        var b = ScriptableObject.CreateInstance<BScriptableObject>();


        //创建父子关系
        AssetDatabase.AddObjectToAsset(a, PATH);
        //创建父子关系
        AssetDatabase.AddObjectToAsset(b, PATH);

        parent.childs.Add(a);
        parent.childs.Add(b);

        AssetDatabase.ImportAsset(PATH);
    }
}