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
    public class AWSController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAmazonS3 _client;
        public AWSController(IConfiguration config)
        {
            _config = config;

            var awsAccess = _config.GetValue<string>("AWSSDK:AccessKey");
            var awsSecret = _config.GetValue<string>("AWSSDK:SecretKey");

            _client = new AmazonS3Client(awsAccess, awsSecret, RegionEndpoint.EUCentral1);
        }

        [HttpGet("list-buckets")]
        public async Task<IActionResult> ListBuckets()
        {
            try
            {
                var result = await _client.ListBucketsAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Buckets could not be listed");
            }
        }

        [HttpPost("create-bucket/{name}")]
        public async Task<IActionResult> CreateBucket(string name)
        {
            try
            {
                PutBucketRequest request = new PutBucketRequest() { BucketName = name };
                await _client.PutBucketAsync(request);

                return Ok($"Bucket: {name} WAS created");
            }
            catch (Exception)
            {
                return BadRequest($"Bucket: {name} WAS NOT created");
            }
        }

        [HttpPost("create-object/{bucketName}/{objectName}")]
        public async Task<IActionResult> CreateObject(string bucketName, string objectName)
        {
            try
            {
                FileInfo file = new FileInfo(@"C:\AWSFiles\ThankYou.txt");

                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = file.OpenRead(),
                    BucketName = bucketName,
                    Key = "ThankYou.txt"
                };
                await _client.PutObjectAsync(request);

                ListObjectsRequest objectsRequest = new ListObjectsRequest()
                {
                    BucketName = bucketName
                };
                ListObjectsResponse response = await _client.ListObjectsAsync(objectsRequest);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest($"File WAS NOT created/uploaded");
            }
        }

        [HttpGet("list-objects/{bucketName}")]
        public async Task<IActionResult> ListObjects(string bucketName)
        {
            try
            {
                ListObjectsRequest objectsRequest = new ListObjectsRequest()
                {
                    BucketName = bucketName
                };
                ListObjectsResponse response = await _client.ListObjectsAsync(objectsRequest);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest($"File WAS NOT created/uploaded");
            }
        }

        [HttpPost("create-folder/{bucketName}/{folderName}")]
        public async Task<IActionResult> CreateFolder(string bucketName, string folderName)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = folderName.Replace("%2F","/")
                };

                await _client.PutObjectAsync(request);

                return Ok($"{folderName} folder was created inside {bucketName}");
            }
            catch (Exception)
            {
                return BadRequest("The folder COULD NOT be created");
            }
        }

        [HttpDelete("delete-bucket/{bucketName}")]
        public async Task<IActionResult> DeleteBucket(string bucketName)
        {
            try
            {
                DeleteBucketRequest request = new DeleteBucketRequest() { BucketName = bucketName };
                await _client.DeleteBucketAsync(request);

                return Ok($"{bucketName} bucket was deleted");
            }
            catch (Exception)
            {
                return BadRequest($"{bucketName} bucket WAS NOT deleted");
            }
        }

        [HttpPost("enable-versioning/{bucketName}")]
        public async Task<IActionResult> EnableVersioning(string bucketName)
        {
            try
            {
                PutBucketVersioningRequest request = new PutBucketVersioningRequest
                {
                    BucketName = bucketName,
                    VersioningConfig = new S3BucketVersioningConfig
                    {
                        Status = VersionStatus.Enabled
                    }
                };

                await _client.PutBucketVersioningAsync(request);

                return Ok($"Bucket {bucketName} versioning ENABLED!");
            }
            catch (Exception)
            {
                return BadRequest($"Bucket {bucketName} versioning NOT ENABLED!");
            }
        }

        [HttpPut("add-metadata/{bucketName}/{fileName}")]
        public async Task<IActionResult> AddMetadata(string bucketName, string fileName)
        {
            try
            {
                Tagging newTags = new Tagging()
                {
                    TagSet = new List<Tag>
                    {
                        new Tag {Key = "Key1", Value = "FirstTag"},
                        new Tag {Key = "Key2",Value = "SecondTag"}
                    }
                };

                PutObjectTaggingRequest request = new PutObjectTaggingRequest()
                {
                    BucketName = bucketName,
                    Key = fileName,
                    Tagging = newTags
                };

                await _client.PutObjectTaggingAsync(request);

                return Ok($"Tags added!");
            }
            catch (Exception)
            {
                return BadRequest($"Tags COULD NOT be added");
            }
        }

        [HttpPut("copy-file/{sourceBucket}/{sourceKey}/{destinationBucket}/{destinationKey}")]
        public async Task<IActionResult> CopyFile(string sourceBucket, string sourceKey, string destinationBucket, string destinationKey)
        {
            try
            {
                CopyObjectRequest request = new CopyObjectRequest()
                {
                    SourceBucket = sourceBucket,
                    SourceKey = sourceKey,
                    DestinationBucket = destinationBucket,
                    DestinationKey = destinationKey
                };

                await _client.CopyObjectAsync(request);

                return Ok($"Object/File copied");
            }
            catch (Exception)
            {
                return BadRequest($"Object/File WAS NOT copied");
            }
        }
    }
}
