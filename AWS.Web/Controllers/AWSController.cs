using Amazon;
using Amazon.S3;
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
        public AWSController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> TestAWSClient()
        {
            var awsAccess = _config.GetValue<string>("AWSSDK:AccessKey");
            var awsSecret = _config.GetValue<string>("AWSSDK:SecretKey");

            var client = new AmazonS3Client(awsAccess, awsSecret,RegionEndpoint.EUCentral1);

            try
            {
                var result = await client.ListBucketsAsync();
                return Ok("AmazonS3Client works fine");
            }
            catch (Exception)
            {
                return BadRequest("AmazonS3Client does NOT work");
            }
        }
    }
}
