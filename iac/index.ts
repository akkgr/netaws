import * as aws from "@pulumi/aws-native";

const lambda = new aws.lambda.Function("mylambda", {  
    runtime: "dotnet8",
    handler: "bootstrap",
    role: "arn:aws:iam::761059477267:role/lambdaRole",
    code: { 
        s3Bucket: "akk-lambda-code",
        s3Key: "bootstrap.zip"
    }
});

export const lambdaName = lambda.arn;
