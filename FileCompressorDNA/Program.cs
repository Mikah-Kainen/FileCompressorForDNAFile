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
            int extraSpace = 4 - targetInt.Length % 4;
            for(int i = 0; i < extraSpace; i ++)
            {
                sb.Append("00");
            }

            string str = sb.ToString();

            byte[] returnArray = new byte[str.Length / 8];

            for(int i = 0; i < returnArray.Length; i ++)
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

        static char[] BinaryDecoder(byte[] targetArray, int start, int end)
        {
            char[] returnArray = new char[(end - start) * 4];
            char indexValue;

            for (int i = start; i < end; i++)
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

        static bool AreEqual()
        {
            return true;
        }


        static void Main(string[] args)
        {
            File.WriteAllText("../../../test.txt", "");

            byte[] DNAString = File.ReadAllBytes("DNA.txt");
            int start = 3;
            int end = DNAString.Length;


            int[] binary = BinaryCoder(DNAString, start, end);
            var result = SliceArray(binary, binary.Length - 50, binary.Length - 1);


            byte[] CompressedBytes = IntToByte(binary);
            File.WriteAllBytes("../../../test.txt", CompressedBytes);
            File.AppendAllText("../../../test.txt", $" {end - start}");


            //read all text then split the string by ' '
            //string1 -> ASCIIEncoding.ASCII.GetBytes() -> CompressedBytes
            //string2 -> end


            char[] DNADecoded = BinaryDecoder(CompressedBytes, 0, CompressedBytes.Length);
            var result2 = SliceArray(DNADecoded, DNADecoded.Length - 50, DNADecoded.Length - 1);

            
            // A = 00
            // C = 01
            // G = 10
            // T = 11

        }
    }
}
