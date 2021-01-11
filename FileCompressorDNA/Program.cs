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

                        break;

                    case 'C':

                        break;

                    case 'G':

                        break;

                    case 'T':

                        break;

                    default:

                        break;
                }
                // A = 00
                // C = 01
                // G = 10
                // T = 11
                if (targetBytes[i] == 'A')
                {

                }
                else if(targetBytes[i] == 'C')
                {

                }
                else if(targetBytes[i] == 'G')
                {

                }
                else if(targetBytes[i] == 'T')
                {

                }
                else
                {
                    throw new Exception("Read incorrectly");
                }
                returnValue[i - start] = indexValue;
            }


            return returnValue;
        }

        static byte[] BinaryDecoder(int targetInt)
        {



            return null;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            byte[] DNAString = File.ReadAllBytes("DNA.txt");
            int start = 3;
            int end = DNAString.Length;


            int[] binary = BinaryCoder(DNAString, start, end);
            // A = 00
            // C = 01
            // G = 10
            // T = 11

        }
    }
}
