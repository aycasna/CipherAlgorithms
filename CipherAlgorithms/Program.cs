// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace CipherAlgorithms
{
    public class Variables
    {
        public static char[] alphabet = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];


    }
    class Program
	{
        
        static void Main(string[] args)
		{
            //string[] alphabet = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];


            Console.WriteLine("Press 1 for encryption or 2 for decryption");
            string choice = Console.ReadLine();

            Console.WriteLine("(1) Rot13\n(2)second cipher\n(3)third cipher");
            string cipherMethod = Console.ReadLine();

            Console.Write("Enter the text: ");
            string text = Console.ReadLine();
            switch ((choice, cipherMethod))
            {
                case ("1", "1"):
                    ROT13.ROT13_Encrypt(text);

                    break;
                default:
                    break;
            }
        }


    }
    //hello
    //abcdefghijklmnopqrstuvwxyz

	static class ROT13
	{
        public static void ROT13_Encrypt(string plainText)
        {
            string[] cipherText;
            //string alphabet = Variables.alphabet[];
            
            for(int i = 0; i < plainText.Length; i++) 
            {
                for (int j = 0; j < Variables.alphabet.Length; j++)
                {
                    char characterInAlphabet = Variables.alphabet[j];

                    if (plainText[i] == characterInAlphabet)
                    {
                        char charInAlphabetPlus13 = Variables.alphabet[j+13];
                        string stringInAlphabetPlus13 = charInAlphabetPlus13.ToString();

                        //cipherText[i] = stringInAlphabetPlus13;
                        Console.Write(stringInAlphabetPlus13);
                        break;
                    }
                }
            }
            //string.Join("", cipherText);

            
            
        }

        public static string ROT13_Decrypt(string cipherText) 
        {

            return cipherText;
        }
	}



}
