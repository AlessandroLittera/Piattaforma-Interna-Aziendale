using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Helper.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Models.Interfaces.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vap
{
    public class BlobManager 
    {
        private CloudBlobContainer blobContainer;

        public BlobManager(string ContainerName)
        {
            // Check if Container Name is null or empty  
            if (string.IsNullOrEmpty(ContainerName))
            {
                throw new ArgumentNullException("ContainerName", "Container Name can't be empty");
            }
            try
            {
                // Get azure table storage connection string.  
                string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=vapdev;AccountKey=28hpcto4rpAQJcK/Zqmk5sa1Qm6OaKODliNulnwLl7FYcNBFeoGa5WKcaGYcHT0k1Q2oTYyCVmyhMCry2UetXA==;EndpointSuffix=core.windows.net";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                blobContainer = cloudBlobClient.GetContainerReference(ContainerName);

                // Create the container and set the permission  
                if (blobContainer.CreateIfNotExists())
                {
                    blobContainer.SetPermissions(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        }
                    );
                }
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }

        

    }
}
