# OpenFGA SDK Test Runner

A simple example application demonstrating basic usage of the OpenFGA .NET SDK.

## Overview

This is a lightweight test runner that demonstrates the basic functionality of the OpenFGA SDK, including:
- Configuration setup
- Authorization model creation
- Tuple creation
- Check request creation

## Running

You can run this example using the Makefile:

```bash
make run-test-runner
```

Or directly with dotnet:

```bash
dotnet run --project TestRunner/TestRunner.csproj
```

## What It Tests

This test runner performs basic structure validation tests:
1. **SDK Configuration** - Creates and validates an OpenFgaClient configuration
2. **Authorization Model** - Creates an authorization model structure with user and document types
3. **Tuple Creation** - Creates a sample tuple with user, relation, and object
4. **Check Request** - Creates a sample check request

## Note

This test runner does not require a running OpenFGA server as it only validates the SDK's data structures and configuration. For full integration tests with an actual OpenFGA server, use:

```bash
make test
```

or

```bash
make test-net8
```
