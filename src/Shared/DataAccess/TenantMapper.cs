using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2.Model;

using Shared.Models;

namespace Shared.DataAccess
{
    public static class TenantMapper
    {
        public static readonly string Pk = "id";
        private static readonly string Name = "name";

        public static Tenant TenantFromDynamoDb(Dictionary<String, AttributeValue> items)
        {
            var product = new Tenant(items[Pk].S, items[Name].S);

            return product;
        }
    }
}