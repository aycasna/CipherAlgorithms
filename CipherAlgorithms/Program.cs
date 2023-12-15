// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CipherAlgorithms
{
   

    class Program
	{
        
        static void Main(string[] args)
		{
            
            string choice = "";
            string text = "";
            Console.WriteLine("\t\t=====================================");
            Console.WriteLine("\t\t\tCipher Methods");
            

            Console.WriteLine("\t\t\t(1) Rot13\n\t\t\t(2) Autokey\n\t\t\t(3) Vernam\n\t\t\t(4) RSA");
            Console.WriteLine("\t\t=====================================");
            string cipherMethod = Console.ReadLine();

            if (cipherMethod == "4")
            {
                Console.WriteLine("\n\t(1) Generate public and private key\n\t(2) Encrypt\n\t(3) Decrypt");
                choice = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n\tPress 1 for encryption or 2 for decryption");
                choice = Console.ReadLine();

                Console.Write("Enter the text: ");
                text = Console.ReadLine();
            }
                


            

            switch ((cipherMethod, choice))
            {
                case ("1", "1"):
                    text = ROT13.ROT13_EncryptDecrypt(text);
                    Console.WriteLine($"Cipher text: {text}");
                    Main(args);
                    break;                 
                case ("1", "2"):
                    text = ROT13.ROT13_EncryptDecrypt(text);
                    Console.WriteLine($"Plain text: {text}");
                    Main(args);
                    break;
                case ("2", "1"):
                    Console.Write("Enter key: ");
                    string key = Console.ReadLine();
                    text = Autokey.Autokey_Encrypt(text, key);
                    Console.WriteLine($"Cipher text: {text}");
                    Main(args);
                    break;
                case ("2", "2"):
                    Console.Write("Enter key: ");
                    key = Console.ReadLine();
                    text = Autokey.Autokey_Decrypt(text, key);
                    Console.WriteLine($"Plain text: {text}");
                    Main(args);
                    break;
                case ("3", "1"):
                    Console.Write("Enter key: ");
                    key = Console.ReadLine();
                    text = Vernam.Vernam_Encrypt(text, key);
                    Console.WriteLine($"Cipher text: {text}");
                    Main(args);
                    break;
                case ("3", "2"):
                    Console.Write("Enter key: ");
                    char charKey = Convert.ToChar(Console.ReadLine());
                    int num = Convert.ToInt32(text);
                    text = Vernam.Vernam_Decrypt(num, charKey);
                    Console.WriteLine($"Plain text: {text}");
                    Main(args);
                    break;
                case ("4", "1"):
                    double P, Q;
                    (P, Q) = PrimeGenerator.GenerateTwoPrimes();
                    
                    double n, e, d;
                    (n, e, d) = RSA.RSA_GenerateKeys(P, Q);
                    Main(args);
                    break;
                case ("4", "2"):
                    Console.Write("Enter the plain text: ");
                    text = Console.ReadLine();
                    Console.Write("Enter public key n: ");
                    double publicKeyN = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter public key e: ");
                    double publicKeyE = Convert.ToDouble(Console.ReadLine());

                    text = RSA.RSA_Encrypt(text, publicKeyN, publicKeyE);
                    Console.WriteLine($"Cipher text: {text}");

                    Main(args);
                    break;
                case ("4", "3"):
                    Console.Write("Enter the cipher text: ");
                    text = Console.ReadLine();

                    Console.Write("Enter public key n: ");
                    publicKeyN = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter private key d: ");
                    double privateKeyD = Convert.ToDouble(Console.ReadLine());

                    

                    text = RSA.RSA_Decrypt(text, publicKeyN, privateKeyD);
                    Console.WriteLine($"Plain text: {text}");
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

    //Vernam cipher
    static class Vernam 
    {
        private static String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Vernam_Encrypt(string plainText, string key)
        {

            string cipherText = "";
            
            for (int i = 0; i < plainText.Length; i++)
            {
                int asciiValuePlainText = (int)plainText[i];
                int asciiValueKey = (int)key[i];

                int xorValue = asciiValuePlainText ^ asciiValueKey;
                

                //string binaryString = Convert.ToString(xorValue, 2).PadLeft(8, '0');
                Console.WriteLine($"decimal value {i+1}: {xorValue}");
                char asciiChar = Convert.ToChar(xorValue);
 

                cipherText += asciiChar;

            }
            return cipherText;

        }
        public static string Vernam_Decrypt(int cipherText, char key)
        {
            string plainText = "";

            //int asciiValueCipherText = Convert.ToInt32(cipherText, 2);
            int asciiValueCipherText = cipherText;
            int asciiValueKey = (int)key;

            int xorValue = asciiValueCipherText ^ asciiValueKey;
            char asciiChar = Convert.ToChar(xorValue);
            Console.WriteLine($"decimal value: {xorValue}");




            plainText += asciiChar;

            


            return plainText;
        }
    }



    public class PrimeGenerator
    {
        
        private static Random rng = new Random();

        public static (double, double) GenerateTwoPrimes()
        {
            double prime1=0;
            double prime2=0;
            (double, double) t1 = (prime1, prime2);//prime1 prime2

            do
            {
                t1.Item1 = GenerateRandomNumber();
                //prime1 = GenerateRandomNumber();
            }
            while (!IsPrime(t1.Item1)); //prime1

            do
            {
                t1.Item2 = GenerateRandomNumber();
                //prime2 = GenerateRandomNumber();
            }
            while (!IsPrime(t1.Item2) || t1.Item2 == t1.Item1);//prim2 prim2 prime1
            
            //Console.WriteLine($"Prime 1: {t1.Item1}, Prime 2: {t1.Item2}");//prime1 prime2
            //return new (double, double)(prime1, prime2);
            return (t1.Item1, t1.Item2);
            
        }

        private static int GenerateRandomNumber()
        {
            return rng.Next(1, 10);
        }

        private static bool IsPrime(double number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
    public static class RSA
    {

        private static String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        public static (double, double, double) RSA_GenerateKeys(double P, double Q)
        {
            static double gcd(double a, double h)//en buyuk bolen
            {
                //This function returns the gcd or greatest common divisor
                double temp;
                while (true)
                {
                    temp = a % h;
                    if (temp == 0)
                        return h;
                    a = h;
                    h = temp;
                }
            }
            double n = P * Q;
            double e = 2;
            double phi = (P - 1) * (Q - 1);
            while(e < phi)
            {
                if (gcd(e, phi) == 1)
                    break;
                else
                    e++;
            }
            double d = 0;
            
            for (d = 0; d < phi; d++)
            {
                if ((d * e) % phi == 1)
                {
                    break;
                }
            }
            //int k = 2; //constant value
            //double d = (1 + (k * phi)) / e;


            Console.WriteLine($"\nPublic keys => n = {n}, e = {e}\nPrivate key => d = {d}");
            return (n, e, d);
        }

        public static string RSA_Encrypt(string plainText, double n, double e)
        {

            List<double> cipherTextList = new List<double>();

            for (int i = 0; i < plainText.Length; i++)
            {
                int indexOfPlainText = alphabet.IndexOf(plainText[i]);
                Console.WriteLine($"index of plaintext {indexOfPlainText}");
                double message = Math.Pow(indexOfPlainText, e);
                message %= n;
                cipherTextList.Add(message);
            }
            
            
            string cipherText = string.Join("", cipherTextList);
            int cipherTextIndex = 0;
            
            for (int i = 0; i< cipherText.Length; i++)
            {
                cipherTextIndex = int.Parse(cipherText);
                
            }


            char cipherTextChar = alphabet[cipherTextIndex];



            cipherText = cipherTextChar.ToString();
            
            return cipherText;
        }



        public static string RSA_Decrypt(string cipherText, double n, double d)
        {
            List<double> plainTextList = new List<double>();
            for (int i = 0; i<cipherText.Length; i++)
            {
                int indexOfCipherText = alphabet.IndexOf(cipherText[i]);//4
                double message = Math.Pow(indexOfCipherText, d);
                message %= n;
                
                    
                plainTextList.Add(message);
            }

            string plainText = string.Join("", plainTextList);
            int plainTextIndex = int.Parse(plainText);
            char plainTextChar = alphabet[plainTextIndex];

            plainText = plainTextChar.ToString();


            
            return plainText;
            
        }

        public static List<double> storeAndAccessCipherTextList(List<double> cipherTextList)
        {
            return cipherTextList;
            
        }



            
     }
    
}

