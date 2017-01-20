using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Server.Utils
{
    /// <summary>
    ///     HttpWebResponse extension
    /// </summary>
    public static class HttpWebResponseExtension
    {
        /// <summary>
        ///     Extracts body from response
        /// </summary>
        /// <typeparam name="T">body type</typeparam>
        /// <param name="response">response</param>
        /// <returns>body</returns>
        public static T GetBody<T>(this HttpWebResponse response)
        {
            var stream = response.GetResponseStream();
            if (stream == null)
                return default(T);
            var streamReader = new StreamReader(stream);
            var body = streamReader.ReadToEnd();
            streamReader.Close();
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(body);
        }
    }
}