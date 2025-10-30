# Makefile Usage Guide

This repository includes a Makefile for convenient building, testing, and running the OpenFGA .NET SDK on macOS and Linux.

## Prerequisites

- .NET 8.0 SDK or later
- macOS or Linux operating system
- Make utility (pre-installed on macOS)

## Quick Start

View all available commands:
```bash
make help
```

Build and run tests:
```bash
make all
```

## Available Commands

### Build Commands

#### `make restore`
Restores all NuGet packages for the solution.
```bash
make restore
```

#### `make build`
Builds the entire solution in Release configuration. Automatically runs `restore` first.
```bash
make build
```

#### `make clean`
Cleans all build artifacts including:
- Binary output directories (bin/)
- Object files (obj/)
- Test results
```bash
make clean
```

### Test Commands

#### `make test`
Runs all tests across all target frameworks (netcoreapp3.1, net48, net8.0, net9.0).
```bash
make test
```

#### `make test-net8`
Runs tests only for the .NET 8.0 framework.
```bash
make test-net8
```

#### `make test-coverage`
Runs tests for .NET 8.0 with code coverage collection in OpenCover format. Results are saved in the `TestResults/` directory.
```bash
make test-coverage
```

After running, you can find the coverage report at:
```
TestResults/<guid>/coverage.opencover.xml
```

### Example Commands

#### `make run-test-runner`
Builds and runs the TestRunner example, which demonstrates basic SDK usage without requiring a running OpenFGA server.
```bash
make run-test-runner
```

#### `make run-example`
Runs the Example1 application. **Note:** This requires an OpenFGA server running on `http://localhost:8080`.
```bash
make run-example
```

To start an OpenFGA server:
```bash
docker pull docker.io/openfga/openfga:latest
docker run -p 8080:8080 docker.io/openfga/openfga:latest run
```

### Complete Workflow

#### `make all`
Performs a complete workflow: restore → build → test (net8.0 only).
```bash
make all
```

## Common Use Cases

### First-Time Setup
```bash
make restore
make build
```

### Development Workflow
```bash
# After making changes
make build
make test-net8

# Or combine both
make all
```

### Testing with Coverage
```bash
make test-coverage

# View the coverage file
find TestResults/ -name "coverage.opencover.xml" -print
```

### Clean Build
```bash
make clean
make all
```

## CI/CD Integration

The Makefile targets are designed to work well in CI/CD pipelines:

```bash
# Standard CI workflow
make restore
make build
make test-coverage
```

## Troubleshooting

### "make: command not found"
Make should be pre-installed on macOS. If missing, install Xcode Command Line Tools:
```bash
xcode-select --install
```

### ".NET SDK not found"
Install the .NET SDK from: https://dotnet.microsoft.com/download

### "Permission denied" errors
Ensure you have write permissions in the project directory:
```bash
chmod -R u+w .
```

## Additional Resources

- [Main README](README.md) - Project overview and documentation
- [TestRunner README](TestRunner/README.md) - TestRunner example documentation
- [Example README](example/README.md) - Example applications documentation

## Platform Notes

This Makefile is designed for macOS but also works on Linux systems. For Windows users, consider using:
- Windows Subsystem for Linux (WSL)
- Git Bash with Make
- PowerShell alternatives (not included)
