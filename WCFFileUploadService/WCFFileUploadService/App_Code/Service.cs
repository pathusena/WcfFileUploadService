using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    private static List<FileInfo> fileDatabase = new List<FileInfo>();
    private static readonly string uploadDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Uploads");

    public List<FileInfo> GetFileList()
    {
        return fileDatabase;
    }

    public void UploadFile(Stream fileStream)
    {
        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }
        
        using (var fileStreamOnDisk = File.Create(fileDatabase.Last().filePath))
        {
            fileStream.CopyTo(fileStreamOnDisk);
        }
    }

    public void UploadFileInfo(FileInfo fileInfo)
    {
        string fileName = Path.GetFileNameWithoutExtension(fileInfo.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(fileInfo.FileName);
        string filePath = Path.Combine(uploadDirectory, fileName); 
        fileInfo.FileName = fileName;
        fileInfo.filePath = filePath;
        fileDatabase.Add(fileInfo);
    }
}
