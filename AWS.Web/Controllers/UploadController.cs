using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAmazonS3 _client;
        public UploadController(IConfiguration config)
        {
            _config = config;

            var awsAccess = _config.GetValue<string>("AWSSDK:AccessKey");
            var awsSecret = _config.GetValue<string>("AWSSDK:SecretKey");

            _client = new AmazonS3Client(awsAccess, awsSecret, RegionEndpoint.EUCentral1);
        }

        [HttpPost("upload-file/{bucketName}")]
        public async Task<IActionResult> UploadFile(string bucketName)
        {
            var fileStream = CreateDataStream();
            var fileKey = "multipart_upload_file.mp4";

            try
            {
                //Initiate multipart upload
                InitiateMultipartUploadRequest initiateRequest = new InitiateMultipartUploadRequest()
                {
                    BucketName = bucketName,
                    Key = fileKey
                };
                InitiateMultipartUploadResponse initiateResponse = await _client.InitiateMultipartUploadAsync(initiateRequest);

            }
            catch (Exception)
            {

                throw;
            }


            return Ok();
        }

        private FileStream CreateDataStream()
        {
            FileStream fileStream = System.IO.File.OpenRead("Uploads/27mb_file.mp4");
            return fileStream;
        }
    }
}
