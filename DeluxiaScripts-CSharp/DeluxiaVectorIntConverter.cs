using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Shims;
using UnityEngine;

namespace Newtonsoft.Json.Converters
{
    /// <summary>
    /// Json Converter for Vector2Int, Vector3Int and Vector4.  Only serializes x, y, (z) and (w) properties.
    /// </summary>
    [Preserve]
    public class DeluxiaVectorIntConverter : JsonConverter
    {
        private static readonly Type V2 = typeof(Vector2Int);
        private static readonly Type V3 = typeof(Vector3Int);
        private static readonly Type V4 = typeof(Vector4);

        public bool EnableVector2Int { get; set; }
        public bool EnableVector3Int { get; set; }
        public bool EnableVector4 { get; set; }

        /// <summary>
        /// Default Constructor - All Vector types enabled by default
        /// </summary>
        public DeluxiaVectorIntConverter()
        {
            EnableVector2Int = true;
            EnableVector3Int = true;
            EnableVector4 = false;
        }

        /// <summary>
        /// Selectively enable Vector types
        /// </summary>
        /// <param name="enableVector2Int">Use for Vector2Int objects</param>
        /// <param name="enableVector3Int">Use for Vector3Int objects</param>
        /// <param name="enableVector4">Use for Vector4 objects</param>
        public DeluxiaVectorIntConverter(bool enableVector2Int, bool enableVector3Int): this()
        {
            EnableVector2Int = enableVector2Int;
            EnableVector3Int = enableVector3Int;
            EnableVector4 = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var targetType = value.GetType();

            if (targetType == V2)
            {
                var targetVal = (Vector2Int)value;
                WriteVector(writer, targetVal.x, targetVal.y, null, null);
            }
            else if (targetType == V3)
            {
                var targetVal = (Vector3Int) value;
                WriteVector(writer, targetVal.x, targetVal.y, targetVal.z, null);
            }
            // else if (targetType == V4)
            // {
            //     var targetVal = (Vector4)value;
            //     WriteVector(writer, targetVal.x, targetVal.y, targetVal.z, targetVal.w);
            // }
            else
            {
                //Should never get here
                writer.WriteNull();
            }

        }

        private static void WriteVector(JsonWriter writer, int x, int y, int? z, int? w)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("x");
            writer.WriteValue(x);
            writer.WritePropertyName("y");
            writer.WriteValue(y);

            if (z.HasValue)
            {
                writer.WritePropertyName("z");
                writer.WriteValue(z.Value);

                if (w.HasValue)
                {
                    writer.WritePropertyName("w");
                    writer.WriteValue(w.Value);
                }
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == V2)
                return PopulateVector2Int(reader);


            if (objectType == V3)
                return PopulateVector3Int(reader);

            return PopulateVector4(reader);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return (EnableVector2Int && objectType == V2) || (EnableVector3Int && objectType == V3) || (EnableVector4 && objectType == V4);
        }

        private static Vector2Int PopulateVector2Int(JsonReader reader)
        {
            var result = new Vector2Int();

            if (reader.TokenType != JsonToken.Null)
            {
                var jo = JObject.Load(reader);
                result.x = jo["x"].Value<int>();
                result.y = jo["y"].Value<int>();
            }

            return result;
        }

        private static Vector3Int PopulateVector3Int(JsonReader reader)
        {
            var result = new Vector3Int();

            if (reader.TokenType != JsonToken.Null)
            {
                var jo = JObject.Load(reader);
                result.x = jo["x"].Value<int>();
                result.y = jo["y"].Value<int>();
                result.z = jo["z"].Value<int>();
            }

            return result;
        }

        private static Vector4 PopulateVector4(JsonReader reader)
        {
            var result = new Vector4();

            if (reader.TokenType != JsonToken.Null)
            {
                var jo = JObject.Load(reader);
                result.x = jo["x"].Value<int>();
                result.y = jo["y"].Value<int>();
                result.z = jo["z"].Value<int>();
                result.w = jo["w"].Value<int>();
            }

            return result;
        }
    }
}
