using System;
using System.Configuration;
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
            string sStorageAccountName = ConfigurationManager.AppSettings["StorageAccountName"];
            string sStorageAccountKey = ConfigurationManager.AppSettings["StorageAccountKey"];
            string sFile = ConfigurationManager.AppSettings["FileToUpload"];

            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName="+ sStorageAccountName + ";AccountKey="+ sStorageAccountKey);

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

            string fileToUpload = @sFile;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("MyFile.txt");
            using (var fileStream = System.IO.File.OpenRead(fileToUpload))
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
