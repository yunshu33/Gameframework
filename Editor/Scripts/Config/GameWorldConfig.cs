using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YunFramework.RunTime;
using YunFramework.RunTime.Utility.Serializable;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.GameEditor
{
    public class GameWorldConfig
    {
        [MenuItem("Assets/Create/GameWorld/Config")]
        public static void CreateGameWorldConfig()
        {
            var path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs.FirstOrDefault());

            var systemNames = new List<string>();

            var type = typeof(GameWorldConfig);

            var assembly = type.Assembly;

            foreach (var runTimeType in assembly.GetTypes())
            {
                if (runTimeType.BaseType == typeof(SystemBase))
                {
                    systemNames.Add(runTimeType.FullName);
                }
            }

            var config = new GameWorldConfig();

            var str = XmlSerializable.Serializer(config);

            using var stream = new FileStream(path + @"\GameWorldConfig.txt", FileMode.OpenOrCreate,
                FileAccess.ReadWrite);
            var bytes = Encoding.UTF8.GetBytes(str);

            stream.Write(bytes, 0, bytes.Length);

            stream.Flush();

            AssetDatabase.Refresh();
        }
    }
}