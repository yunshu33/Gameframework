#region 
// **********************************************************************
// Create Time :		2022/06/23 18:09:42
// Description :
// **********************************************************************
#endregion


using UnityEngine;
using Yun.Util;
using UnityEngine.UI;

/// <summary>
/// 异步加载文件示例
/// </summary>
public class AsyncLoaderFilesExample : MonoBehaviour
{

    public Image image;
    // Start is called before the first frame update
    async void Start()
    {
        var load = new AsyncLoaderFiles();
        image.sprite = await load.AsyncLoadSprite(@"\Photo\Cat.png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
