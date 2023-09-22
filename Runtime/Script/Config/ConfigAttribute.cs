using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorldFramework.RunTime.Module
{

    /// <summary>
    /// 模块配置
    /// </summary>
    public class ConfigAssetAttribute : Attribute
    {
        Type assetType;

        public double version;

        public Type AssetType { get => assetType; private set => assetType = value; }

        public ConfigAssetAttribute(Type type)
        {
            AssetType = type;

            // Default value.  
            version = 1.0;
        }
    }
}
