using System;
using System.IO;
using System.Text;

/*
Создание файла занимает около 20 минут
Увеличить скорость можно изменив batch_length
При первом запуске создается файл
При последующих идет дозапись файл
Поэтому можно прерывать и дозаписывать
 */

namespace BBD
{
    class Program
    {
        static void Main(string[] args)
        {
             string file_name = "Big Black.data";
             string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            try
            {
                // Add data to the file if it exists.
                if (File.Exists(file_name))
                {
                    Console.WriteLine($"{file_name} already exists!");
                    FileInfo fi = new FileInfo(file_name);
                    long size = fi.Length/(1024*1024*1024); //file size in GB
                    while(size < 33)
                    {
                        //creating batch for writing
                        Random rand = new Random();
                        int batch_length = 50;
                        string[] batch = new string[batch_length];
                        for (int i = 0; i < batch_length; i++)
                        {
                            batch[i] = GenerateRandomString(alphabet);
                        }
                        //write batch to file
                        using (FileStream fs = new FileStream(file_name, FileMode.Append))
                        {
                            using (StreamWriter w = new StreamWriter(fs))
                            {
                                foreach(string element in batch)
                                {
                                    w.Write(element);
                                }
                            }
                        }
                        fi.Refresh();
                        size = fi.Length/(1024*1024*1024); //file size in GB;
                        Console.WriteLine(size);
                    }
                }
                else
                {
                    // Create the file if it doesn`t exists.
                    using (FileStream fs = File.Create(file_name))
                    {
                        Console.WriteLine($"{file_name} created!");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static string GenerateRandomString(string Alphabet)
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            int length = rand.Next(3,7);
            int pos =0;
            //And random char from alphabet to string
            for (int i = 0; i < length; i++)
            {
                pos = rand.Next(0, Alphabet.Length);
                sb.Append(Alphabet[pos]);
            }
            sb.Append("\r\n");
            return sb.ToString();
        }
    }
}
