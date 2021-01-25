using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace FileCompressorDNA
{
    class Program
    {
        static int[] BinaryCoder(byte[] targetBytes, int start, int end)
        {
            int[] returnValue = new int[end-start];
            int indexValue;
            for(int i = start; i < end; i ++)
            {
                switch((char)targetBytes[i])
                {
                    case 'A':
                        indexValue = 00;
                        break;

                    case 'C':
                        indexValue = 01;
                        break;

                    case 'G':
                        indexValue = 10;
                        break;

                    case 'T':
                        indexValue = 11;
                        break;

                    default:
                        indexValue = 2;
                        break;
                }
                // A = 00
                // C = 01
                // G = 10
                // T = 11

                //73,74 145,146

                returnValue[i - start] = indexValue;
            }
            var t = returnValue.Where(x => x != 2).ToArray();

            return t;
        }
         
        static byte[] IntToByte(int[] targetInt)
        {
            StringBuilder sb = new StringBuilder();

            string newAdd;
            foreach (int temp in targetInt)
            {
                if(temp < 2)
                {
                    newAdd = '0' + temp.ToString();
                }
                else
                {
                    newAdd = temp.ToString();
                }
                sb.Append(newAdd);
            }
            int originalSize = targetInt.Length % 4;
            //for(int i = 0; i < extraSpace; i ++)
            //{
            //    sb.Append("00");
            //}

            string str = sb.ToString();

            byte[] returnArray = new byte[str.Length / 8 + 2];

            for(int i = 0; i < returnArray.Length - 2; i ++)
            {
                string byteAsString = null;
                for(int x = 0; x < 8; x ++)
                {
                    byteAsString += sb[8 * i + x];
                }
                int temp = BinaryToDecimal(byteAsString);
               


                returnArray[i] = (byte)(temp);
                string bin = Convert.ToString(returnArray[i], 2);


                //returnArray[i] += (byte)(targetInt[4 * i] << 6);
                //string bin1 = Convert.ToString(returnArray[4 * i], 2);

                //returnArray[i] += (byte)(targetInt[4 * i + 1] << 4);
                //string bin2 = Convert.ToString(returnArray[4 * i], 2);

                //returnArray[i] += (byte)(targetInt[4 * i + 2] << 2);
                //string bin3 = Convert.ToString(returnArray[4 * i], 2);

                //returnArray[i] += (byte)(targetInt[4 * i + 3]);
                //string bin4 = Convert.ToString(returnArray[4*i], 2);
            }
            StringBuilder sb2 = new StringBuilder();

            for(int i = 0; i < 4 - originalSize; i ++)
            {
                sb2.Append("00");
            }
            for(int i = targetInt.Length - originalSize; i < targetInt.Length; i ++)
            {
                if (targetInt[i] < 2)
                {
                    newAdd = '0' + targetInt[i].ToString();
                }
                else
                {
                    newAdd = targetInt[i].ToString();
                }
                sb2.Append(newAdd);
            }
            string str2 = sb2.ToString();

            returnArray[returnArray.Length - 2] = (byte)BinaryToDecimal(str2);
            returnArray[returnArray.Length - 1] = (byte)originalSize;
            return returnArray;
        }

        static int BinaryToDecimal(string targetBinary)
        {
            int returnInt = 0;

            for(int i = 0; i < 8; i ++)
            {
                returnInt += (targetBinary[8 - i - 1] - 48) * (int)(Math.Pow(2, i));
            }

            return returnInt;
        }

        static char[] SuperDecoder(byte[] targetBytes)
        {
            return null;
        }
        static char[] Decoder(string text)
        {
            string[] textPieces = text.Split(' ');
            for(int x = 1; x < textPieces.Length - 1; x ++)
            {
                textPieces[0] += ' ' + textPieces[x];
            }
            textPieces[1] = textPieces[textPieces.Length - 1];
            byte[] bytes = new byte[textPieces[0].Length];
            int i = 0;
            foreach(char letter in textPieces[0])
            {
                bytes[i] = (byte)letter;
                i ++;
            }
            int end = int.Parse(textPieces[1]);
            return BinaryDecoder(bytes, 0, end);
        }

        static char[] BinaryDecoder(byte[] targetArray, int start, int end)
        {
            char[] returnArray = new char[4 * (end - start)];
            char indexValue;

            for (int i = start; i < targetArray.Length; i++)
            {
                string simpleBinary = Convert.ToString(targetArray[i], 2);
                int extra = 8 - simpleBinary.Length;
                string finalBinary = "";
                for (int z = 0; z < extra; z++)
                {
                    finalBinary += "0";
                }
                foreach (char number in simpleBinary)
                {
                    finalBinary += number;
                }

                for (int x = 0; x < 4; x++)
                {

                    int targetIndex = (finalBinary[2 * x] - 48) * 10 + finalBinary[2 * x + 1] - 48;

                    switch (targetIndex)
                    {
                        case 0:
                            indexValue = 'A';
                            break;

                        case 1:
                            indexValue = 'C';
                            break;

                        case 10:
                            indexValue = 'G';
                            break;

                        case 11:
                            indexValue = 'T';
                            break;

                        default:
                            indexValue = 'Z';
                            break;

                    }
                    returnArray[4*i + x - start] = indexValue;
                }
            }

            return returnArray;
        }


        static int[] SliceArray(int[] targetArray, int startIndex, int endIndex)
        {
            int[] returnArray = new int[endIndex-startIndex + 1];
            for(int i = startIndex; i < endIndex + 1; i ++)
            {
                returnArray[i - startIndex] = targetArray[i];
            }
            return returnArray;
        }

        static char[] SliceArray(char[] targetArray, int startIndex, int endIndex)
        {
            char[] returnArray = new char[endIndex-startIndex + 1];
            for(int i = startIndex; i < endIndex + 1; i ++)
            {
                returnArray[i - startIndex] = targetArray[i];
            }
            return returnArray;
        }

        static byte[] SliceArray(byte[] targetArray, int startIndex, int endIndex)
        {
            byte[] returnArray = new byte[endIndex - startIndex + 1];
            for (int i = startIndex; i < endIndex + 1; i++)
            {
                returnArray[i - startIndex] = targetArray[i];
            }
            return returnArray;
        }

        static void Main(string[] args)
        {
            File.WriteAllText("../../../test.txt", "");

            byte[] DNAString = File.ReadAllBytes("DNA.txt");
            int start = 3;
            int end = DNAString.Length;


            int[] binary = BinaryCoder(DNAString, start, end);
            var result = SliceArray(binary, binary.Length - 50, binary.Length - 1);
            start = 0;
            end = binary.Length;


            byte[] CompressedBytes = IntToByte(binary);
            File.WriteAllBytes("../../../test.txt", CompressedBytes);
            var result2 = SliceArray(CompressedBytes, CompressedBytes.Length - 50, CompressedBytes.Length - 1);

            //read all text then split the string by ' '
            //string1 -> ASCIIEncoding.ASCII.GetBytes() -> CompressedBytes
            //string2 -> end

            byte[] readTest = File.ReadAllBytes("../../../test.txt");
            int readTestLength = readTest.Length;


            char[] DNADecoded = BinaryDecoder(readTest, 0, readTest.Length);
            //char[] DNADecoded = Decoder(readTest);
            var result3 = SliceArray(DNADecoded, DNADecoded.Length - 50, DNADecoded.Length - 1);


            // A = 00
            // C = 01
            // G = 10
            // T = 11

        }
    }
}
