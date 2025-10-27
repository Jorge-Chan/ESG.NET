// Fiap.Web.ESG2.Tests/Utils/ApiTestHelper.cs
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Fiap.Web.ESG2.Tests.Utils
{
    public static class ApiTestHelper
    {
        public static JToken ToJsonToken(object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return JToken.Parse(json);
        }

        // Schema mínimo para endpoints que retornam ARRAY de objetos
        public static JSchema ArrayOfObjectSchema() =>
            JSchema.Parse(@"{
              ""type"": ""array"",
              ""items"": { ""type"": ""object"" }
            }");

        // >>> AQUI: schema mínimo para PagedResult<T> (propriedades em PascalCase)
        // Troque para camelCase se o seu objeto tiver "page", "pageSize", etc.
        public static JSchema PagedOfObjectSchema() =>
            JSchema.Parse(@"{
              ""type"": ""object"",
              ""required"": [""Page"", ""PageSize"", ""TotalItems"", ""Items""],
              ""properties"": {
                ""Page"":       { ""type"": ""integer"" },
                ""PageSize"":   { ""type"": ""integer"" },
                ""TotalItems"": { ""type"": ""integer"" },
                ""Items"": {
                  ""type"": ""array"",
                  ""items"": { ""type"": ""object"" }
                }
              },
              ""additionalProperties"": true
            }");

        public static IList<string> ValidateAgainst(JToken token, JSchema schema)
        {
            var errors = new List<string>();
            token.Validate(schema, (o, a) => errors.Add(a.Message));
            return errors;
        }

        public static bool HasAtLeastNItems(object? value, int n)
        {
            if (value is IEnumerable enumerable)
            {
                int c = 0;
                foreach (var _ in enumerable) { c++; if (c >= n) return true; }
            }
            return false;
        }

        public static bool IsEmptyEnumerable(object? value)
        {
            if (value is IEnumerable enumerable)
            {
                foreach (var _ in enumerable) return false;
                return true;
            }
            return false;
        }
    }
}
