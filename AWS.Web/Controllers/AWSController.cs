using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
    }
}
