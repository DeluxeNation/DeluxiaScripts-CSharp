using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Deluxia {
    /// <summary>
    /// Various methods to serialize data types to byte arrays. This is used for online sending or storing stuff.
    /// </summary>
    public static class DeluxiaSerialize {
        /// <summary>
        /// Converts a string to a byte[]
        /// </summary>
        public static byte[] StringToByteArray(string text,bool compress) {
            bool extra = false;
            if(string.IsNullOrEmpty(text)) {
                return new byte[0];
            }
            if(compress) {
                if(text.Zip().Length < text.Length) {
					text = text.Zip();
				}
                else {
					extra = true;
				}
            }
            char[] text2 = text.ToCharArray();
            byte[] toSend = new byte[text.Length+(extra?1:0)];
            int spot = 0;
            for(int i = 0;i < toSend.Length;i++, spot++) {
                if(i == 0 && extra) {
                    toSend[i] = 0;
                    spot--;
                }
                else {
                    toSend[i] = (byte)text2[spot];
                }
            }
            return toSend;
        }
        /// <summary>
        /// Converts a serialized string back to a string.
        /// </summary>
        public static string ByteArrayToString(byte[] data,bool compressed) {
            if(data == null || data.Length == 0) {
                return "";
            }
            char[] toSend = new char[data.Length];
            for(int i = 0;i < toSend.Length;i++) {
                toSend[i] = (char)data[i];
            }
            if(compressed && data[0] != 0) {
                return new string(toSend).Unzip();
            }
            return new string(toSend);
        }
        /// <summary>
        /// Converts a string[] to a byte[]
        /// </summary>
        public static byte[] FromStringArray(string[] stringArr) {
            if(stringArr == null) {
                return null;//ew byte[]{0};
            }
            List<byte[]> nextData = new List<byte[]>();
            int total = 0;
            foreach(string start in stringArr) {
                byte[] add = Encoding.ASCII.GetBytes(start);
                nextData.Add(add);
                total += add.Length;
            }
            List<byte> nextData2 = new List<byte>();
            foreach(byte[] data in nextData) {
                foreach(byte d in data) {
                    nextData2.Add(d);
                }
                nextData2.Add(1);
            }
            return nextData2.ToArray();
        }
        /// <summary>
        /// Converts a serialized string[] back to a string[].
        /// </summary>
        public static string[] ToStringArray(byte[] ser) {
            if(ser == null || ser.Length == 0) {
                return null;
            }
            if(ser[0] == 1 && ser.Length == 1) {
                return new string[] { "" };
            }
            List<string> data = new List<string>();
            int spot = 0;
            while(spot < ser.Length) {
                string word = "";
                while(spot < ser.Length) {
                    if(ser[spot] == 1) {
                        break;
                    }
                    word += (char)ser[spot];
                    spot++;
                }
                data.Add(word);
                spot++;
            }
            return data.ToArray();
        }
        /// <summary>
        /// Converts a T[][][] to a readable string.
        /// </summary>
        public static string SerializeToString<T>(this T[][][] encoded,bool newLine) {
            if(encoded == null || encoded.Length == 0) {
                return "null";
            }
            string toSend = "";
            try {
                foreach(T[][] top in encoded) {
                    toSend += "[";
                    foreach(T[] middle in top) {
                        if(middle == null) {
                            UnityEngine.Debug.LogError("NULL " +toSend);
                            toSend += ";";
                            continue;
                        }
                        else {
                            foreach(T val in middle) {
                                toSend += val + ",";
                            }
                        }
                        toSend = toSend[..^1];
                        toSend += ";";
                    }
                    toSend += "]" + (newLine ? "\n" : "");
                }
                return toSend;
            }
            catch(Exception e) {
				UnityEngine.Debug.LogError("Failed to complete "+toSend);
				UnityEngine.Debug.LogException(e);

                return "";
            }
        }
        /// <summary>
        /// Converts a T[][] to a readable string.
        /// </summary>
        public static string SerializeToString<T>(this T[][] encoded) {
            if(encoded == null || encoded.Length == 0) {
                return "null";
            }
            string toSend = "";
            int spot = 0;
            foreach(T[] top in encoded) {
                if(top == null) {
                    break;
                }
                if(top.Length == 0) {
                    toSend += ";";
                    spot++;
                    continue;
                }
                foreach(T val in top) {
                    toSend += val + ",";
                }
                toSend = toSend.Substring(0,toSend.Length - 1);
                toSend += ";";
                spot++;
            }
            return toSend;
        }
        /// <summary>
        /// Converts a collection to a readable string.
        /// </summary>
        public static string SerializeToString<T>(this IEnumerable<T> encoded) {
            if(encoded == null) {
                return "null";
            }
            if(encoded.Count() == 0) {
                return "{Empty}";
            }
            string toSend = "";
            //Debug.Log(encoded.Length);
            //int times = 0;
            foreach(T val in encoded) {
                //Debug.Log("Time "+ (times++));
                toSend += val + ",";
            }
            toSend = toSend[..^1];
            toSend += ";";
            return toSend;
        }
        /// <summary>
        /// Converts a 2D array to a readable string.
        /// </summary>
        /// <param name="encoded"></param>
        /// <param name="showSize">This will output the size of the array. This is required if you want to turn the string back to an array.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string SerializeToString<T>(this T[,] encoded,bool showSize) {
            if(encoded == null || encoded.GetLength(0) == 0 || encoded.GetLength(1) == 0) {
                return "null";
            }
            string toSend = "";
            if(showSize) {
                toSend = $"[{encoded.GetLength(0)},{encoded.GetLength(1)}]";
            }
            //Debug.Log(encoded.Length);
            for(int i = 0;i < encoded.GetLength(0);i++) {
                toSend += "{";
                //int times = 0;
                if(encoded[i,0] == null) {
                    break;
                }
                //Debug.Log(spot+1);
                for(int j = 0;j < encoded.GetLength(1);j++) {
                    //Debug.Log("Time "+ (times++));
                    toSend += encoded[i,j] + ",";
                }
                toSend = toSend.Substring(0,toSend.Length - 1);
                toSend += "}";
            }
            return toSend;
        }
        public static int[,] StringTo2DArray(string code) {
            int xx, yy;
            string codeClone = code.Remove(0,1);
            xx = int.Parse(codeClone.Substring(0,codeClone.IndexOf(",") + 1));
            codeClone = codeClone.Substring(codeClone.IndexOf(",") + 1);
            yy = int.Parse(codeClone.Substring(0,codeClone.IndexOf("]") + 1));
            codeClone = codeClone.Substring(codeClone.IndexOf("]") + 1);
            int[,] toSend = new int[xx,yy];
            //return null;
            for(int x = 0;x < xx;x++) {
                for(int y = 0;y < yy;y++) {
                    bool end = code.IndexOf(";") < code.IndexOf(",") || code.IndexOf(",") == -1;
                    string stopAt = end ? "}" : ",";
                    toSend[x,y] = (byte.Parse(code.Substring(0,code.IndexOf(stopAt))));
                    code = code.Remove(0,code.IndexOf(stopAt) + 1);
                    //Debug.Log(end);
                }
            }
            return toSend;
        }
        /// <summary>
        /// Converts a serialized string to a short[].
        /// </summary>
        public static short[] StringToShortArray(string code) {
            List<short> data = new List<short>();
            bool end = false;
            while(!end) {
                if(code.IndexOf(",") == -1 && code.CountElementInString(";") <= 1) {
                    //Debug.Log("END");
                    end = true;
                }
                else {
                    end = code.IndexOf(";") < code.IndexOf(",") || code.IndexOf(",") == -1;
                }
                // if(code.Substring(0,1) == ";"){
                //     data.Add(null);
                //     code = code.Remove(0,1);
                //     break;
                // }
                string stopAt = end ? ";" : ",";
                //Debug.Log(stopAt+code);
                data.Add(short.Parse(code.Substring(0,code.IndexOf(stopAt))));
                code = code.Remove(0,code.IndexOf(stopAt) + 1);
                //Debug.Log(end);
            }
            return data.ToArray();
        }
        /// <summary>
        /// Converts a serialized string to a byte[][]
        /// </summary>
        public static byte[][] StringToByte2Array(string code) {
            List<byte[]> toSend = new List<byte[]>();
            bool trueEnd = false;
            while(!trueEnd) {
                List<byte> data = new List<byte>();
                bool end = false;
                while(!end) {
                    if(code.IndexOf(",") == -1 && code.CountElementInString(";") <= 1) {
                        //Debug.Log("END");
                        trueEnd = true;
                        end = true;
                    }
                    else {
                        end = code.IndexOf(";") < code.IndexOf(",") || code.IndexOf(",") == -1;
                    }
                    if(code.Substring(0,1) == ";") {
                        toSend.Add(null);
                        code = code.Remove(0,1);
                        break;
                    }
                    string stopAt = end ? ";" : ",";
                    //Debug.Log(stopAt+code);
                    data.Add(byte.Parse(code.Substring(0,code.IndexOf(stopAt))));
                    code = code.Remove(0,code.IndexOf(stopAt) + 1);
                    //Debug.Log(end);
                }
                toSend.Add(data.ToArray());
            }
            return toSend.ToArray();
        }
        /// <summary>
        /// Converts a serialized string to a byte[][][]
        /// </summary>
        public static byte[][][] StringToByte3Array(string code) {
            if(string.IsNullOrWhiteSpace(code)) {
                return null;
            }
            //byte[][][] toSend = new byte[length1][][];
            List<byte[][]> toSend = new List<byte[][]>();
            code = code.Remove(0,1);
            int bracketCount = code.CountElementInString("]");
            for(int h = 0;h < bracketCount;h++) {
                List<byte[]> toSend2 = new List<byte[]>();
                int totalTimes = code.Substring(0,code.IndexOf("]")).CountElementInString(";");
                //UnityEngine.Debug.LogError(code);
				if(code.IndexOf(";") == 0) {
                    code = code.Remove(0,1);
                    continue;
				}
				for(int i = 0;i < totalTimes;i++) {
                    List<byte> data = new List<byte>();
                    bool end = false;
                    while(!end) {
                        if(code.Substring(0,1) == "]") {
                            code.Remove(0,2);
                            end = true;
                            break;
                        }
                        if(code.IndexOf(",") == -1 || code.IndexOf(",") > code.IndexOf("]")) {
                            end = true;
                        }
						else {
                            end = code.IndexOf(";") < code.IndexOf(",");
                        }
                        string stopAt = end ? ";" : ",";
                        
                        data.Add(byte.Parse(code.Substring(0,code.IndexOf(stopAt))));
                        code = code.Remove(0,code.IndexOf(stopAt) + 1);
                    }
                    toSend2.Add(data.ToArray());
                }
                toSend.Add(toSend2.ToArray());
                if(code.IndexOf("[") == -1) {
                    code = code.Remove(0);
                }
                else {
                    code = code.Remove(0,2);
                }
            }
            return toSend.ToArray();
        }
        /// <summary>
        /// Converts an int to a byte. All elements except possibly the last one will have values of 255. A value of 1 at 0 means this is a negative number
        /// </summary>
        public static byte[] IntToByteArray(int num) {
            
            byte[] toSend = new byte[(num / 255) + 1+(num < 0?1:0)];
			if(num < 0) {
                toSend[0] = 1;
			}
			for(int i = num < 0?1:0;i < toSend.Length;i++) {
                if(i + 1 == toSend.Length) {
                    toSend[i] = (byte)(num - (255 * i));
                }
                else {
                    toSend[i] = 255;
                }
            }
            return toSend;
        }
        public static int ByteArrayToInt(byte[] serialized) {
            bool isNegative = serialized.Length > 1 && serialized[0] == 1;
            return DeluxiaMethods.GetSum(serialized)*(isNegative?-1:1);

        }
        /// <summary>
        /// Compresses a string into a shorter string.
        /// </summary>
        public static string Zip(this string text) {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using(var gZipStream = new GZipStream(memoryStream,CompressionMode.Compress,true)) {
                gZipStream.Write(buffer,0,buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData,0,compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData,0,gZipBuffer,4,compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length),0,gZipBuffer,0,4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        public static string Unzip(this string compressedText) {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using(var memoryStream = new MemoryStream()) {
                int dataLength = BitConverter.ToInt32(gZipBuffer,0);
                memoryStream.Write(gZipBuffer,4,gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using(var gZipStream = new GZipStream(memoryStream,CompressionMode.Decompress)) {
                    gZipStream.Read(buffer,0,buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
        /*public static string Zip(this byte[][] data) {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            var memoryStream = new MemoryStream();
            using(var gZipStream = new GZipStream(memoryStream,CompressionMode.Compress,true)) {
                gZipStream.Write(buffer,0,buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData,0,compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData,0,gZipBuffer,4,compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length),0,gZipBuffer,0,4);
            return Convert.ToBase64String(gZipBuffer);
        }*/
    }
}