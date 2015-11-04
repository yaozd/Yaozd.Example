using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DotNet.Utilities.Serialize
{
    public class SerializeHelper
    {
        #region Json序列化
        public static string ToJson(object item)
        {
            return JsonConvert.SerializeObject(item);
        }
        public static T JsonDeserialize<T>(string str)
        {
            return (T)JsonConvert.DeserializeObject(str, typeof(T));
        }
        #endregion
        #region XML序列化
        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToXml<T>(T item)
        {
            var serializer = new XmlSerializer(item.GetType());
            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T XmlDeserialize<T>(string str)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        #endregion
    }
}
