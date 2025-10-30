using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace TestRunner;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("OpenFGA SDK Test Runner");
        Console.WriteLine("========================");
        Console.WriteLine();

        try
        {
            // Basic configuration test
            Console.WriteLine("1. Testing SDK Configuration...");
            var configuration = new ClientConfiguration
            {
                ApiUrl = "http://localhost:8080"
                // StoreId and AuthorizationModelId are optional
            };
            var client = new OpenFgaClient(configuration);
            Console.WriteLine("   ✓ SDK Configuration created successfully");
            Console.WriteLine($"   - API URL: {configuration.ApiUrl}");
            Console.WriteLine("   - Store ID: (not set - optional)");
            Console.WriteLine("   - Model ID: (not set - optional)");
            Console.WriteLine();

            // Test model creation
            Console.WriteLine("2. Testing Authorization Model Creation...");
            var modelRequest = new ClientWriteAuthorizationModelRequest
            {
                SchemaVersion = "1.1",
                TypeDefinitions = new List<TypeDefinition>
                {
                    new TypeDefinition
                    {
                        Type = "user",
                        Relations = new Dictionary<string, Userset>()
                    },
                    new TypeDefinition
                    {
                        Type = "document",
                        Relations = new Dictionary<string, Userset>
                        {
                            {
                                "reader", new Userset
                                {
                                    This = new object()
                                }
                            }
                        },
                        Metadata = new Metadata
                        {
                            Relations = new Dictionary<string, RelationMetadata>
                            {
                                {
                                    "reader", new RelationMetadata
                                    {
                                        DirectlyRelatedUserTypes = new List<RelationReference>
                                        {
                                            new RelationReference { Type = "user" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Console.WriteLine("   ✓ Authorization Model structure created successfully");
            Console.WriteLine($"   - Schema Version: {modelRequest.SchemaVersion}");
            Console.WriteLine($"   - Type Definitions: {modelRequest.TypeDefinitions?.Count}");
            Console.WriteLine();

            // Test tuple creation
            Console.WriteLine("3. Testing Tuple Creation...");
            var tupleKey = new ClientTupleKey
            {
                User = "user:alice",
                Relation = "reader",
                Object = "document:readme"
            };
            Console.WriteLine("   ✓ Tuple created successfully");
            Console.WriteLine($"   - User: {tupleKey.User}");
            Console.WriteLine($"   - Relation: {tupleKey.Relation}");
            Console.WriteLine($"   - Object: {tupleKey.Object}");
            Console.WriteLine();

            // Test check request creation
            Console.WriteLine("4. Testing Check Request Creation...");
            var checkRequest = new ClientCheckRequest
            {
                User = "user:alice",
                Relation = "reader",
                Object = "document:readme"
            };
            Console.WriteLine("   ✓ Check Request created successfully");
            Console.WriteLine($"   - User: {checkRequest.User}");
            Console.WriteLine($"   - Relation: {checkRequest.Relation}");
            Console.WriteLine($"   - Object: {checkRequest.Object}");
            Console.WriteLine();

            Console.WriteLine("========================");
            Console.WriteLine("All basic SDK tests passed! ✓");
            Console.WriteLine();
            Console.WriteLine("Note: These are basic structure tests.");
            Console.WriteLine("For full integration tests, run: make test");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
