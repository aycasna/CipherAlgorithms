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
            string choice = "";
            string text = "";
            Console.WriteLine("\n\t\tCipher");
            

            Console.WriteLine("(1) Rot13 Cipher\n(2) Autokey Cipher\n(3) Random Autokey ROT13 (RAKROT13)\n(4) RSA");
            string cipherMethod = Console.ReadLine();

            if (cipherMethod == "4")
            {
                Console.WriteLine("\n\t(1) Generate public and private key\n\t(2) Encrypt\n\t(3)Decrypt");
                choice = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Press 1 for encryption or 2 for decryption");
                choice = Console.ReadLine();

                Console.Write("Enter the text: ");
                text = Console.ReadLine();
            }
                


            

            switch ((cipherMethod, choice))
            {
                case ("1", "1"):
                    text = ROT13.ROT13_EncryptDecrypt(text);
                    Console.WriteLine(text);
                    Main(args);
                    break;                 
                case ("1", "2"):
                    text = ROT13.ROT13_EncryptDecrypt(text);
                    Console.WriteLine(text);
                    Main(args);
                    break;
                case ("2", "1"):
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
                case ("3", "1"):
                    text = RAKROT13.RAKROT13_Encrypt(text);
                    Console.WriteLine(text);
                    Main(args);
                    break;
                case ("3", "2"):
                    //rakrot13 decrypt
                    break;
                case ("4", "1"):
                    double P, Q;
                    (P, Q) = PrimeGenerator.GenerateTwoPrimes();
                    
                    double n, e, d;
                    (n, e, d) = RSA.RSA_GenerateKeys(P, Q);
                    break;
                case ("4", "2"):
                    Console.Write("Enter the plain text: ");
                    text = Console.ReadLine();
                    Console.Write("Enter public key n: ");
                    double publicKeyN = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter public key e: ");
                    double publicKeyE = Convert.ToDouble(Console.ReadLine());
                    text = RSA.RSA_Encrypt(text, publicKeyN, publicKeyE);
                    Console.WriteLine(text);
                    Main(args);
                    break;
                case ("4", "3"):
                    Console.Write("Enter the cipher text: ");
                    text = Console.ReadLine();
                    Console.WriteLine("Enter private key d: ");
                    double privateKeyD = Console.Read();
                    //rsa decrypt
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

    //Random Autokey ROT13
    static class RAKROT13 
    {
        private static String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string RAKROT13_Encrypt(string plainText)
        {

            Random random = new Random();
            int keyLen = random.Next(4, 10);
            int randValue;
            string key = "";
            char letter;
            for (int i = 0; i < keyLen; i++)
            {
                randValue = random.Next(0, 26);
                letter = Convert.ToChar(randValue + 65); //generating random character by converting the random number to a character.
                key = key + letter;
            }

            string randomAutokeyMessage = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                string newKey = key + plainText;
                Console.WriteLine($"KEY: {newKey}");
                int first = alphabet.IndexOf(plainText[i]);
                int second = alphabet.IndexOf(newKey[i]);
                int total = (first + second) % 26;
                randomAutokeyMessage += alphabet[total];
            }

            
            char[] array = randomAutokeyMessage.ToCharArray();
            for (int i = 0; i < array.Length; i++)
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
            
            Console.WriteLine($"Prime 1: {t1.Item1}, Prime 2: {t1.Item2}");//prime1 prime2
            //return new (double, double)(prime1, prime2);
            return (t1.Item1, t1.Item2);
            
        }

        private static int GenerateRandomNumber()
        {
            return rng.Next(50, 200);
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

            int k = 2; //constant value
            double d = (1 + (k * phi)) / e;
            

            Console.WriteLine($"\n\tPublic keys => n = {n}, e = {e}\n\tPrivate key => d = {d}");
            return (n, e, d);
        }

        public static string RSA_Encrypt(string plainText, double n, double e)
        {
            double Encrypt(double message)
            {
                
                double cipherText = 1;
                
                cipherText = Math.Pow(message, e);
                Console.WriteLine($"cipherText exponent 5={cipherText}");
                cipherText %= n;
                Console.WriteLine($"cipherText % 5={cipherText}");
                return cipherText;
            }

            double message = 1;
            List<double> cipherTextList = new List<double>();
            

            ;
            foreach(char letter in plainText) 
            {
                

                cipherTextList.Add(Encrypt((double)letter));

            }


            string cipherText = string.Join("",cipherTextList);


            return cipherText;
        }



            
     }

}

