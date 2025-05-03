#region

// **********************************************************************
// Create Time :		2022/11/22 12:46:57
// Description :
// **********************************************************************

#endregion


using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using YGameFramework.Runtime.Script.Config;

namespace YunFramework.RunTime
{
    public class GameWorldConfig : IXmlSerializable
    {
        private List<string> autoSystemNames;

        public List<string> AutoSystemNames
        {
            get { return autoSystemNames ??= new List<string>(); }
            private set => autoSystemNames = value;
        }

        private TimeSystemConfig timeSystemConfig;

        public TimeSystemConfig TimeSystemConfig
        {
            get { return timeSystemConfig ??= new TimeSystemConfig(); }
            private set => timeSystemConfig = value;
        }


        public GameWorldConfig(List<string> autoSystemNames)
        {
            this.autoSystemNames = autoSystemNames;
        }

        public GameWorldConfig()
        {
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "自启动系统":
                        AutoSystemNames.Add(reader.GetAttribute("Name"));
                        break;
                    case "TimeSystemConfig":
                        TimeSystemConfig = new TimeSystemConfig();
                        timeSystemConfig.ReadXml(reader);
                        break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("自启动系统类名列表");

            foreach (var name in AutoSystemNames)
            {
                writer.WriteStartElement("自启动系统");
                writer.WriteAttributeString("Name", name);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement("TimeSystemConfig");
            TimeSystemConfig.WriteXml(writer);
            writer.WriteEndElement();
        }
    }
}