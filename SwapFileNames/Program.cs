using System;
using System.IO;
using System.Threading;

namespace SwapFileNames
{
    abstract class Program
    {
        private static void Main()
        {
            string folderPath = @"This PC\Zenfone 8\Internal shared storage\DCIM\Camera\Music mp3";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder path is invalid or does not exist.");
                return;
            }

            string[] files = Directory.GetFiles(folderPath);

            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the folder.");
                return;
            }

            Console.WriteLine("Processing files...");

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string newFileName = RearrangeFileName(fileName);

                string newFilePath = Path.Combine(folderPath, newFileName);
                File.Move(filePath, newFilePath);

                Console.WriteLine($"Changed file name: {fileName} -> {newFileName}");

                Thread.Sleep(10); // Delay of 10ms
            }

            Console.WriteLine("File names rearranged successfully.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static string RearrangeFileName(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string[] parts = fileNameWithoutExtension.Split('-');

            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid file name format: {fileName}");
                return fileName;
            }

            string newName = $"{parts[1].Trim()} - {parts[0].Trim()}{extension}";
            return newName;
        }
    }
}