import * as pulumi from "@pulumi/pulumi";
import * as awsNative from "@pulumi/aws-native";
import * as aws from "@pulumi/aws";
import { v4 as uuidv4 } from 'uuid';

const bucket = new aws.s3.Bucket("my-bucket");
const fileToUpload = new pulumi.asset.FileAsset("../artifacts/bootstrap.zip");
const bucketObject = new aws.s3.BucketObject("my-bucket-object", {
    bucket: bucket.id,
    key: uuidv4(),
    source: fileToUpload,
});

const lambda = new awsNative.lambda.Function("mylambda", {  
    runtime: "dotnet8",
    handler: "GetTenant::GetTenant.Function_FunctionHandler_Generated::FunctionHandler",
    role: "arn:aws:iam::761059477267:role/lambdaRole",
    code: { 
        s3Bucket: bucket.id,
        s3Key: bucketObject.key
    },
    timeout: 30,
    memorySize: 256,
},
{
    replaceOnChanges:["*"]
});

export const lambdaName = lambda.arn;
