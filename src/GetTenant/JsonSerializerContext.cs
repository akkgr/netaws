using System.Text.Json.Serialization;
using Amazon.Lambda.APIGatewayEvents;
using Shared.Models;
using System.Collections.Generic;

namespace GetTenant;

[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(Tenant))]
public partial class CustomJsonSerializerContext : JsonSerializerContext;