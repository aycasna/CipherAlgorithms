// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace CipherAlgorithms
{
    /*
    public class Variables
    {
        //public static char[] alphabet = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
        

    }
    */

    class Program
	{
        
        static void Main(string[] args)
		{
            //string[] alphabet = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];

            Console.WriteLine("\n\t\tCipher");
            Console.WriteLine("Press 1 for encryption or 2 for decryption");
            string choice = Console.ReadLine();

            Console.WriteLine("(1) Rot13 Cipher\n(2) Autokey Cipher\n(3) third cipher");
            string cipherMethod = Console.ReadLine();

            Console.Write("Enter the text: ");
            string text = Console.ReadLine();

            switch ((choice, cipherMethod))
            {
                case ("1", "1"):
                    text = ROT13.ROT13_EncryptDecrypt(text);
                    Console.WriteLine(text);
                    Main(args);
                    break;                 
                case ("2", "1"):
                    text = ROT13.ROT13_EncryptDecrypt(text);
                    Console.WriteLine(text);
                    Main(args);
                    break;
                case ("1", "2"):
                    Console.Write("Enter key: ");
                    string key = Console.ReadLine();
                    text = Autokey.Autokey_Encrypt(text, key);
                    Console.WriteLine(text);
                    Main(args);
                    break;
                case ("2", "2"):
                    Console.Write("Enter key: ");
                    key = Console.ReadLine();
                    text = Autokey.Autokey_Decrypt(text, key);
                    Console.WriteLine(text);
                    Main(args);
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
        public static string ROT13_EncryptDecrypt(string plainText)
        {
            char[] array = plainText.ToCharArray();
            for(int i=0; i<array.Length; i++)
            {
                int number = (int)array[i];          
                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }  
	}

    static class Autokey
    {
        private static String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Autokey_Encrypt(string plainText, string key)
        {
            string message = "";
            for (int i  = 0; i<plainText.Length; i++)
            {
                string newKey = key + plainText;
                int first = alphabet.IndexOf(plainText[i]);
                int second = alphabet.IndexOf(newKey[i]);
                int total = (first + second) % 26;
                message += alphabet[total];
            }

            return message;
        }

        public static string Autokey_Decrypt(string cipherText, string key) 
        {

            string currentKey = key;
            string message = "";

            for (int i = 0; i<cipherText.Length; i++)
            {
                int first = alphabet.IndexOf(cipherText[i]);
                int second = alphabet.IndexOf(currentKey[i]);
                int total = (first - second) % 26;
                total = (total < 0) ? total + 26 : total;
                message += alphabet[total];
                currentKey += alphabet[total];
            }

            return message;
        }
    }


}
