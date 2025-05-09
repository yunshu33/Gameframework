using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LJVoyage.Game.Runtime
{
    public class TimeSystemConfig : IXmlSerializable
    {
        private int dailyInterval = 10 * 60;

        public int DailyInterval
        {
            get => dailyInterval;
            private set => dailyInterval = value;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            DailyInterval = int.Parse(reader.GetAttribute("每日时间间隔") ?? string.Empty);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("每日时间间隔", DailyInterval.ToString());
        }
    }
}