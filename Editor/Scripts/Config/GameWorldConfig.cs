using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using YLJVoyage.Game.Utility.Serializable;

namespace LJVoyage.Game.Editor
{
    public class GameWorldConfig
    {
        [MenuItem("Assets/Create/LJVoyage/Game/Config")]
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

            using var stream = new FileStream(path + @"\GameConfig.txt", FileMode.OpenOrCreate,
                FileAccess.ReadWrite);
            var bytes = Encoding.UTF8.GetBytes(str);

            stream.Write(bytes, 0, bytes.Length);

            stream.Flush();

            AssetDatabase.Refresh();
        }
    }
}