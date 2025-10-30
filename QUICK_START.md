# Quick Start Guide

## Running Tests

### macOS / Linux

```bash
# Run tests for .NET 8.0 framework
make test-net8

# Run tests with code coverage
make test-coverage

# Run simple test example (no server required)
make run-test-runner

# Full build and test
make all
```

### Direct Commands (Any Platform)

```bash
# Build the solution
dotnet build OpenFga.Sdk.sln --configuration Release

# Run tests for .NET 8.0
dotnet test src/OpenFga.Sdk.Test/OpenFga.Sdk.Test.csproj \
  --no-build \
  --configuration Release \
  --framework net8.0 \
  --verbosity normal

# Run tests with code coverage
dotnet test src/OpenFga.Sdk.Test/OpenFga.Sdk.Test.csproj \
  --no-build \
  --configuration Release \
  --framework net8.0 \
  --verbosity normal \
  --logger trx \
  --results-directory TestResults/ \
  --collect:"XPlat Code Coverage" \
  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
```

## Test Results

After running tests:
- **Test Results**: `TestResults/*.trx`
- **Coverage Report**: `TestResults/<timestamp-guid>/coverage.opencover.xml` (guid is auto-generated)

## Quick Example

Run the TestRunner for a quick SDK validation:

```bash
dotnet run --project TestRunner/TestRunner.csproj
```

## More Information

- [Makefile Usage Guide](MAKEFILE_USAGE.md) - Complete Makefile documentation
- [Test Verification Summary](TEST_VERIFICATION_SUMMARY.md) - Details on changes made
- [TestRunner README](TestRunner/README.md) - TestRunner documentation
- [Main README](README.md) - Full project documentation
