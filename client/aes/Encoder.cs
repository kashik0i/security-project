using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algorithms.aes.AESEncoder
{
    public static class AESEncoder
    {
        private static readonly int Size = 16;
        public static List<byte[,]> EncodePlaneText(string planeText)
        {
            bool flage = IsValid(planeText);
            if (flage == false) { planeText = _Padding(planeText); }
            else { planeText = _EmptyPadding(planeText); }
            IEnumerable<string> chunks = planeText.Split(Size);
            List<byte[]> bytes = new();
            foreach (var item in chunks)
            {
                bytes.Add(Encoding.Latin1.GetBytes(item));
            }
            List<byte[,]> bytes2D = new();
            foreach (var item in bytes)
            {
                byte[,] eBytes2D = new byte[4, 4]
                {
                {item[0],item[4],item[8],item[12]},
                {item[1],item[5],item[9],item[13]},
                {item[2],item[6],item[10],item[14]},
                {item[3],item[7],item[11],item[15]}
                };
                bytes2D.Add(eBytes2D);
            }
            return bytes2D;
        }
        public static List<byte[,]> EncodePlaneTextWithoutPadding(string planeText)
        {
            IEnumerable<string> chunks = planeText.Split(Size);
            List<byte[]> bytes = new();
            foreach (var item in chunks)
            {
                bytes.Add(Encoding.Latin1.GetBytes(item));
            }
            List<byte[,]> bytes2D = new();
            foreach (var item in bytes)
            {
                byte[,] eBytes2D = new byte[4, 4]
                {
                {item[0],item[1],item[2],item[3]},
                {item[4],item[5],item[6],item[7]},
                {item[8],item[9],item[10],item[11]},
                {item[12],item[13],item[14],item[15]}
                };
                bytes2D.Add(eBytes2D);
            }
            return bytes2D;
        }
        public static string RemovePaddingAndDecode(List<byte[,]> planeText)
        {
            string decodedText = DecodePlaneText(planeText);
            byte[] bytes = Encoding.Latin1.GetBytes(decodedText);
            if (bytes.Length <= 0) return "";
            System.Console.WriteLine(bytes[5]);
            ArraySegment<byte> segBytes = new ArraySegment<byte>(bytes, 16, (bytes.Length - 16 * 2 - (16 - bytes[5])));
            bytes = segBytes.ToArray();
            return Encoding.Latin1.GetString(bytes);
        }
        public static string DecodePlaneText(List<byte[,]> planeText)
        {
            // list of 16 1d bytes
            List<byte[]> bytes1D = new();
            foreach (var item in planeText)
            {
                byte[] eBytes1D = new byte[16]
                {
                    item[0, 0],item[1, 0],item[2, 0],item[3, 0],
                    item[0, 1],item[1, 1],item[2, 1],item[3, 1],
                    item[0, 2],item[1, 2],item[2, 2],item[3, 2],
                    item[0, 3],item[1, 3],item[2, 3],item[3, 3]
                };
                bytes1D.Add(eBytes1D);
            }
            List<string> string1D = new();
            foreach (var item in bytes1D)
            {
                string1D.Add(Encoding.Latin1.GetString(item));
            }
            string decodedString = "";
            foreach (var item in string1D)
            {
                decodedString += item;
            }
            return decodedString;
        }
        public static string DecodeCypherText(List<byte[,]> planeText)
        {

            // list of 16 1d bytes
            List<byte[]> bytes1D = new();
            foreach (byte[,] item in planeText)
            {
                byte[] eBytes1D = new byte[16]
                {
                    item[0, 0],item[0, 1],item[0, 2],item[0, 3],
                    item[1, 0],item[1, 1],item[1, 2],item[1, 3],
                    item[2, 0],item[2, 1],item[2, 2],item[2, 3],
                    item[3, 0],item[3, 1],item[3, 2],item[3, 3]
                };
                bytes1D.Add(eBytes1D);
            }
            List<string> string1D = new();
            foreach (var item in bytes1D)
            {
                string1D.Add(Encoding.Latin1.GetString(item));
            }
            string decodedString = "";
            foreach (string item in string1D)
            {
                decodedString += item;
            }
            return decodedString;
        }
        public static byte[,] EncodeKey(string planeText)
        {
            bool flage = IsKeyValid(planeText);
            //System.Console.WriteLine(flage);
            if (flage == false) return null;
            IEnumerable<string> chunks = planeText.Split(Size);
            List<byte[]> bytes = new();//Encoding.Latin1.GetBytes(planeText); 
            foreach (var item in chunks)
            {
                bytes.Add(Encoding.Latin1.GetBytes(item));
            }
            List<byte[,]> bytes2D = new();
            foreach (var item in bytes)
            {
                byte[,] eBytes2D = new byte[4, 4]
                {
                {item[0],item[4],item[8],item[12]},
                {item[1],item[5],item[9],item[13]},
                {item[2],item[6],item[10],item[14]},
                {item[3],item[7],item[11],item[15]}
                };
                bytes2D.Add(eBytes2D);
            }
            return bytes2D[0];
        }
        public static IEnumerable<string> Split(this string str, int n)
        {
            if (String.IsNullOrEmpty(str) || n < 1)
            {
                throw new ArgumentException();
            }

            return Enumerable.Range(0, str.Length / n)
                            .Select(i => str.Substring(i * n, n));
        }
        public static bool IsValid(byte[] bytes)
        {
            if (bytes.Length % 16 != 0) { return false; }
            return true;
        }
        public static bool IsValid(string planeText)
        {
            if (planeText.Length % 16 != 0) { return false; }
            return true;
        }
        public static bool IsKeyValid(string planeText)
        {
            if (planeText.Length != 16) { return false; }
            return true;
        }
        private static string _Padding(string planeText)
        {
            byte pad = 0;
            byte padIndexSize = (byte)(planeText.Length % 16);
            byte[] padIndex = {0,0,0,0,
                            0,padIndexSize,padIndexSize,0,
                            0,padIndexSize,padIndexSize,0,
                            0,0,0,0,};
            string padIndexSizeString = Encoding.Latin1.GetString(padIndex);
            int pad_size = (planeText.Length + 16 - (planeText.Length % 16));
            return padIndexSizeString + (planeText.PadRight(pad_size, Convert.ToChar(pad))) + padIndexSizeString;
        }
        private static string _EmptyPadding(string planeText)
        {
            byte pad = 0;
            byte padIndexSize = (byte)(planeText.Length % 16);
            byte[] padIndex = {0,0,0,0,
                            0,padIndexSize,padIndexSize,0,
                            0,padIndexSize,padIndexSize,0,
                            0,0,0,0,};
            string padIndexSizeString = Encoding.Latin1.GetString(padIndex);
            return padIndexSizeString + planeText + padIndexSizeString;
        }
    }
}


