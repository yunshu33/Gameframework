using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace YunFramework.RunTime.Utility.Serializable
{
    public static class XmlSerializable
    {
        //序列化Xml
        public static string Serializer(Object Obj)
        {
            string XmlString = "";
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            using (System.IO.MemoryStream mem = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(mem, settings))
                {
                    //去除默认命名空间xmlns:xsd和xmlns:xsi
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    XmlSerializer formatter = new XmlSerializer(Obj.GetType());
                    formatter.Serialize(writer, Obj, ns);
                }

                XmlString = Encoding.UTF8.GetString(mem.ToArray());
            }

            return XmlString;
        }

        //反序列化
        public static T Deserialize<T>(string str) where T : class
        {
            object obj;
            using (System.IO.MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                using (XmlReader reader = XmlReader.Create(mem))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    obj = formatter.Deserialize(reader);
                }
            }

            return obj as T;
        }
    }
}