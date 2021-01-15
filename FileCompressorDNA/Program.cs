using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
            byte[] returnArray = new byte[targetInt.Length / 4];
            for(int i = 0; i < returnArray.Length; i ++)
            {
                returnArray[i] += (byte)(targetInt[4 * i] << 6);
                returnArray[i] += (byte)(targetInt[4 * i + 1] << 4);
                returnArray[i] += (byte)(targetInt[4 * i + 2] << 2);
                returnArray[i] += (byte)(targetInt[4 * i + 3]);
            }

            return returnArray;
        }

        static char[] BinaryDecoder(int[] targetArray, int start, int end)
        {
            char[] returnArray = new char[end-start];
            char indexValue;
            for(int i = start; i < end; i ++)
            {
                switch(targetArray[i])
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
                        indexValue = '?';
                        throw new Exception("Default shouldn't run exception");
                        break;
                }
                returnArray[i - start] = indexValue;
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


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            byte[] DNAString = File.ReadAllBytes("DNA.txt");
            int start = 3;
            int end = DNAString.Length;


            int[] binary = BinaryCoder(DNAString, start, end);




            int[] test2 = SliceArray(binary, binary.Length - 1000, binary.Length - 1);

            int[] test = new int[1000];


            for (int i = 0; i < 1000; i++)
            {
                test[1000 - i - 1] = binary[binary.Length - 1 - i];
            }

            byte[] b = new byte[0];

            File.WriteAllBytes("../../../test.txt", IntToByte(binary));

            char[] DNADecoded = BinaryDecoder(binary, 0, binary.Length);

            // A = 00
            // C = 01
            // G = 10
            // T = 11

        }
    }
}
