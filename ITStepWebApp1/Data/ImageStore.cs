using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ITStepWebApp1.Data
{
    public class ImageStore
    {
        public ImageStore()
        {
            _client = new CloudBlobClient(_baseUri, new StorageCredentials("itstep2017", "nhtWDa+F01qZHzfMEIo2Vbvls0Jk1sayCeLZGvZB3QvAju8AzOQV2lU4qogSS8tqwCOIuOfDAg+vizJvCMEBsA=="));
        }

        public async Task<string>SaveImage(Stream stream)
        {
            var id = Guid.NewGuid().ToString();
            var container = _client.GetContainerReference("storagetest");
            var blob = container.GetBlockBlobReference(id);
            await blob.UploadFromStreamAsync(stream);
            return id;
        }

        public Uri UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.Now.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30)
            };
            var container = _client.GetContainerReference("storagetest");
            var blob = container.GetBlockBlobReference(imageId);
            var sasToken = blob.GetSharedAccessSignature(sasPolicy);

            return new Uri(_baseUri, $"/images/{imageId}{sasToken}");
        }

        Uri _baseUri = new Uri("https://itstep2017.blob.core.windows.net/");
        CloudBlobClient _client;
    }
}