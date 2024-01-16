using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService
{

    [OperationContract]
    void UploadFile(Stream fileStream);
    [OperationContract]
    void UploadFileInfo(FileInfo fileInfo);

    [OperationContract]
    List<FileInfo> GetFileList();
}

[DataContract]
public class FileInfo
{
    [DataMember]
    public string FileName { get; set; }
    [DataMember]
    public string filePath { get; set; }
    [DataMember]
    public DateTime UploadDate { get; set; }
}
