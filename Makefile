.PHONY: help clean restore build test test-net8 test-coverage run-example run-test-runner

# Default target
help:
	@echo "OpenFGA .NET SDK - Makefile for macOS"
	@echo ""
	@echo "Available targets:"
	@echo "  make restore          - Restore NuGet packages"
	@echo "  make build            - Build the solution in Release configuration"
	@echo "  make test             - Run all tests"
	@echo "  make test-net8        - Run tests for .NET 8.0 framework only"
	@echo "  make test-coverage    - Run tests with code coverage (net8.0)"
	@echo "  make run-test-runner  - Run the simple test runner example"
	@echo "  make run-example      - Run the Example1 application"
	@echo "  make clean            - Clean build artifacts"
	@echo "  make all              - Restore, build, and run tests"
	@echo ""
	@echo "Requirements:"
	@echo "  - .NET 8.0 SDK or later"
	@echo "  - macOS or Linux"

# Restore NuGet packages
restore:
	@echo "Restoring NuGet packages..."
	dotnet restore OpenFga.Sdk.sln

# Build the solution
build: restore
	@echo "Building solution in Release configuration..."
	dotnet build OpenFga.Sdk.sln --configuration Release

# Run all tests across all frameworks
test: build
	@echo "Running all tests..."
	dotnet test src/OpenFga.Sdk.Test/OpenFga.Sdk.Test.csproj --no-build --configuration Release --verbosity normal

# Run tests for .NET 8.0 framework only
test-net8: build
	@echo "Running tests for .NET 8.0..."
	dotnet test src/OpenFga.Sdk.Test/OpenFga.Sdk.Test.csproj \
		--no-build \
		--configuration Release \
		--framework net8.0 \
		--verbosity normal

# Run tests with code coverage for .NET 8.0
test-coverage: build
	@echo "Running tests with code coverage for .NET 8.0..."
	@mkdir -p TestResults
	dotnet test src/OpenFga.Sdk.Test/OpenFga.Sdk.Test.csproj \
		--no-build \
		--configuration Release \
		--framework net8.0 \
		--verbosity normal \
		--logger trx \
		--results-directory TestResults/ \
		--collect:"XPlat Code Coverage" \
		-- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
	@echo ""
	@echo "Coverage report generated in TestResults/"
	@find TestResults/ -name "coverage.opencover.xml" -print | head -1

# Build and run the test runner example
run-test-runner: build
	@echo "Building and running test runner..."
	dotnet build TestRunner/TestRunner.csproj --configuration Release
	dotnet run --project TestRunner/TestRunner.csproj --configuration Release --no-build

# Run Example1 (requires OpenFGA server running)
run-example:
	@echo "Building and running Example1..."
	@echo "Note: Ensure OpenFGA server is running on http://localhost:8080"
	cd example && $(MAKE) run

# Clean build artifacts
clean:
	@echo "Cleaning build artifacts..."
	dotnet clean OpenFga.Sdk.sln --configuration Release
	rm -rf TestResults/
	rm -rf src/OpenFga.Sdk/bin/
	rm -rf src/OpenFga.Sdk/obj/
	rm -rf src/OpenFga.Sdk.Test/bin/
	rm -rf src/OpenFga.Sdk.Test/obj/
	rm -rf TestRunner/bin/
	rm -rf TestRunner/obj/
	rm -rf example/Example1/bin/
	rm -rf example/Example1/obj/

# Default full build and test
all: restore build test-net8
	@echo ""
	@echo "Build and test completed successfully!"
