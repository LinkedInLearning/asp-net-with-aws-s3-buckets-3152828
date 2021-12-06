# ASP.NET: Working with an AWS S3 Bucket
This is the repository for the LinkedIn Learning course ASP.NET: Working with an AWS S3 Bucket. The full course is available from [LinkedIn Learning][lil-course-url].

![ASP.NET: Working with an AWS S3 Bucket][lil-thumbnail-url] 

Setting up your own data infrastructure involves a lot of time and resources. With AWS S3 Buckets, instead of investing a lot of money in your data storage infrastructure, you can just pay for what you need. You don’t have to worry about the costs of support or development and can instead focus on creating new features for your customers using the API that Amazon uses to store and retrieve any amount of data, at any time, from anywhere on the web. It gives any developer access to the same highly scalable, reliable, fast, inexpensive data storage infrastructure that Amazon uses to run its own global network of websites. In this course, Ervis Trupja teaches you how to create a bucket, modify a bucket, and delete a bucket using C# .NET Core. He also covers tasks like how to store data in these buckets, add tags, and retrieve data. Ervis then demonstrates the important feature of multipart uploads, which is crucial for uploading large files in smaller parts.

## Instructions
This repository has branches for each of the videos in the course. You can use the branch pop up menu in github to switch to a specific branch and take a look at the course at that stage, or you can add `/tree/BRANCH_NAME` to the URL to go to the branch you want to access.

## Branches
The branches are structured to correspond to the videos in the course. The naming convention is `CHAPTER#_MOVIE#`. As an example, the branch named `02_03` corresponds to the second chapter and the third video in that chapter. 
Some branches will have a beginning and an end state. These are marked with the letters `b` for "beginning" and `e` for "end". The `b` branch contains the code as it is at the beginning of the movie. The `e` branch contains the code as it is at the end of the movie. The `main` branch holds the final state of the code when in the course.

When switching from one exercise files branch to the next after making changes to the files, you may get a message like this:

    error: Your local changes to the following files would be overwritten by checkout:        [files]
    Please commit your changes or stash them before you switch branches.
    Aborting

To resolve this issue:
	
    Add changes to git using this command: git add .
	Commit changes using this command: git commit -m "some message"


### Instructor

Ervis Trupja 
                            
Software Developer

                            

Check out my other courses on [LinkedIn Learning](https://www.linkedin.com/learning/instructors/ervis-trupja).

[lil-course-url]: https://www.linkedin.com/learning/asp-dot-net-working-with-an-aws-s3-bucket
[lil-thumbnail-url]: https://cdn.lynda.com/course/3153828/3153828-1638315276615-16x9.jpg
