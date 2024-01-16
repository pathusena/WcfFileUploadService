using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfFileUploadClient.FileUploadService;

namespace WcfFileUploadClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter the full path of the file to upload: ");
                string filePath = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        var client = new ServiceClient();
                        string fileName = Path.GetFileName(filePath);


                        FileUploadService.FileInfo fileInfo = new FileUploadService.FileInfo();
                        fileInfo.FileName = fileName;
                        fileInfo.UploadDate = DateTime.Now;

                        client.UploadFileInfo(fileInfo);
                        client.UploadFile(fileStream);

                        var uploadedFiles = client.GetFileList();

                        if (uploadedFiles != null && uploadedFiles.Count() > 0)
                        {
                            foreach (var file in uploadedFiles)
                            {
                                Console.WriteLine($"Uploaded file: {file.FileName}");
                            }
                        }

                        client.Close();

                        Console.WriteLine("File uploaded successfully!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid file path or file does not exist.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
