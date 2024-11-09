using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using SSI.Ultils;

namespace SSI.Services.Service
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(string filePath)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(filePath),
                Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            else
            {
                throw new Exception("Upload failed: " + uploadResult.Error?.Message);
            }
        }

        public async Task<List<string>> UploadListImagesAsync(List<string> filePaths)
        {
            var uploadedUrls = new List<string>();

            foreach (var filePath in filePaths)
            {
                try
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(filePath),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        uploadedUrls.Add(uploadResult.SecureUrl.ToString());  
                    }
                    else
                    {
                        throw new Exception("Upload failed for file: " + filePath + ", Error: " + uploadResult.Error?.Message);
                    }
                }
                catch (Exception ex)
                {
                    uploadedUrls.Add($"Error uploading {filePath}: {ex.Message}");
                }
            }

            return uploadedUrls; 
        }
    }
}
