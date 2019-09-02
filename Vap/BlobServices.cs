using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Vap
{
    public interface IFileService
    {
        Task<string> SaveAsync(string folder, string name, Stream stream);
    }

    public class BlobService : IFileService
    {
        private readonly CloudBlobClient blobClient;
        private readonly string stringConn= "DefaultEndpointsProtocol=https;AccountName=vapdev;AccountKey=28hpcto4rpAQJcK/Zqmk5sa1Qm6OaKODliNulnwLl7FYcNBFeoGa5WKcaGYcHT0k1Q2oTYyCVmyhMCry2UetXA==;EndpointSuffix=core.windows.net";
        public BlobService()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(stringConn);
            this.blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<string> SaveAsync(string folder, string name, Stream stream)
        {
            //string connString = "DefaultEndpointsProtocol=https;AccountName=vapdev;AccountKey=28hpcto4rpAQJcK/Zqmk5sa1Qm6OaKODliNulnwLl7FYcNBFeoGa5WKcaGYcHT0k1Q2oTYyCVmyhMCry2UetXA==;EndpointSuffix=core.windows.net";
            //string destContainer = "uploads";

            //// Get a reference to the storage account  
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(folder);
            if (await blobContainer.CreateIfNotExistsAsync())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            }

            CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(name);
            await cloudBlockBlob.UploadFromStreamAsync(stream);
            //CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            //CloudBlockBlob blob = blobContainer.GetBlockBlobReference(file.FileName);
            //blob.UploadFromStream(file.InputStream);
            return cloudBlockBlob.Uri.ToString();
        }
    }
}
