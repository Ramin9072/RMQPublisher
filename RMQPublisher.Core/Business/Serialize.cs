using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQPublisher.Core.Business
{
    public static class Serialize
    {
        public static string ToJsonString<T>(this T input) => JsonConvert.SerializeObject(input);

        public static T FromJsonString<T>(this string input) => JsonConvert.DeserializeObject<T>(input);

        public static byte[] ToByteArray<T>(this T input) => Encoding.ASCII.GetBytes(input.ToJsonString());
        public static T FromByteArray<T>(this byte[] input) => Encoding.ASCII.GetString(input).FromJsonString<T>();
    }
}
