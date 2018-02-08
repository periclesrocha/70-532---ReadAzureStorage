using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ReadAzureStorage
{
    class Program
    {

        static void Main(string[] args)
        {
            fazAPorraToda();
        }

        static void fazAPorraToda()
        {
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=peri70532storage;AccountKey=Tpe5/9h8T2LueYhg2J7Rt0MvmSo1Us7mefwAWHagg4KAefDOkBPH4e2s086gri+i3UYh7kCwq+OOAMFEIAtFAg==;EndpointSuffix=core.windows.net");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("democontainerblockblob");

            try
            {
                container.CreateIfNotExistsAsync();
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }

            const string imageToUpload = @"C:\Users\procha\Desktop\untitled.png";
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("untitled.png");
            using (var fileStream = System.IO.File.OpenRead(imageToUpload))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            foreach (IListBlobItem blob in container.ListBlobs())
            {
                Console.WriteLine("- {0} (type: {1})", blob.Uri, blob.GetType());
            }
            Console.ReadLine();
        }
    }
}
