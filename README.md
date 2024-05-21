# BeatGenerator Project

## Overview

This project is a .NET 8 API that generates a beat pattern based on input parameters. It includes various endpoints for configuration and status checks, unit tests, and Azure Functions for scheduled tasks. The project is designed to run locally.

## Prerequisites

- [.NET 8 SDK]
- [Azurite] for local Azure Storage development
- [Visual Studio 2022] or later with Azure development workload installed

## Setup Instructions

### Part I: API

1. **Clone the Repository:**
   Clone the repository to your local machine.

2. **Restore Dependencies:**
   Open the solution in Visual Studio and restore the NuGet packages.

3. **Run the API:**
   Set the `BeatGeneratorApi` project as the startup project and run it.

4. **API Endpoints:**
   - `/generator/{i}/{j}`: Generates a beat pattern from `i` to `j`.
   - `/livez`: Returns 200 to indicate the server is alive.
   - `/readyz`: Returns 200 when the server is ready to accept requests, 503 otherwise.
   - `/configure/{i}/{text}`: Configures the beat for a specific multiple.
   - `/reset`: Resets the configuration to default values.

### Part II: Testing

1. **Run Unit Tests:**
   Open the Test Explorer in Visual Studio and run all tests to ensure the endpoints are correctly implemented.

### Part III: Azure Functions

1. **Setup Azure Functions:**
   - Use the `local.settings.json` to configure the `BaseApiUrl`.

2. **Function Logic:**
   - The HTTP-triggered function will expose the `/beatgenerator` API.
   - The time-triggered function will update the configuration at specified times (8:00 AM, 12:30 PM, 5:00 PM).

3. **Run Azure Functions Locally:**
   - Set the Azure Functions project as the startup project.
   - Run the project to start the functions locally.

## Local Development Setup

### Configure Local Settings

Ensure that your `local.settings.json` in the Azure Functions project contains the following configuration:

{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "BaseApiUrl": "http://localhost:5025/api"
  }
}
