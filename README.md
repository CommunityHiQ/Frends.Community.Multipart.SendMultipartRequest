# Frends.Community.Multipart.SendMultipartRequest

A frends task for parsing multipart/form-data requests.<br/>
Only works on .NET6.0, since RestSharp doesn't support .NET Framework 4.7.1.

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.Multipart.SendMultipartRequest/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.Multipart.SendMultipartRequest/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [SendMultipartRequest](#SendMultipartRequest)
- [Building](#building)
- [License](#license)
- [Contributing](#contributing)
- [Changelog](#change-log)

# Installing

You can install the Task via Frends Control Panel from Package Manager.

# Tasks

## SendMultipartRequest

A frends task for sending multipart/form-data requests.

### Input

| Property  | Type                                                    | Description                                                                                           | Example                                               |
|-----------|---------------------------------------------------------|-------------------------------------------------------------------------------------------------------|-------------------------------------------------------|
| Method    | `Enum<POST, PUT>`                                       | HTTP method which should be used for the multipart/form-data request.                                 | `POST`                                                |
| Url       | `string`                                                | Target URL.                                                                                           | `https://httpbin.org/post`                            |
| FilePaths | `Array(Object<enum FileParameterKey, string FullPath>)` | List of files which will be sent to the target server.                                                | `FileParameterKey = file, FullPath = C:\tmp\test.txt` |
| Headers   | `Array(Object<string Name, string Value>)`              | List of headers for the request. No need to add Content-Type, since it is always multipart/form-data. | `Name = Accept, Value = application/json`             |
| TextData  | `Array(Object<string Key, string Data>)`                | List of custom parameters for the request.                                                            | `Key = channel, Value = G01QH4ES8SY`                  |

### Options

| Property                         | Type                      | Description                                                                            | Example                 |
|----------------------------------|---------------------------|----------------------------------------------------------------------------------------|-------------------------|
| Authentication                   | Enum<None, Basic, OAuth2> | Authentication method.                                                                 | `Basic`                 |
| Username                         | `string`                  | Username for authentication. Only required when Basic authentication is selected.      | `testuser`              |
| Password                         | `string`                  | Password for the user. Only required when Basic authentication is selected.            | `verysecretpassword123` |
| BearerToken                      | `string`                  | Bearer token for authentication. Only required when OAuth2 authentication is selected. | `token123`              |
| Timeout                          | `int`                     | Set timeout in seconds.                                                                | `30`                    |
| ThrowExceptionOnErrorResponse    | `bool`                    | Throw exception if return code of request is not successful.                           | `token123`              |

### Returns

| Property            | Type        | Description                                                    | Example                                             |
|---------------------|-------------|----------------------------------------------------------------|-----------------------------------------------------|
| Body                | `Object`    | Response from the target server.                               | `{"json": null, "url": "https://httpbin.org/post"}` |
| RequestIsSuccessful | `bool`      | Indicates if the request was successful or not.                | `true`                                              |
| ErrorException      | `Exception` | Exception thrown by the server. Null if no errors occured.     | `TimeoutException`                                  |
| ErrorMessage        | `string`    | Error message thrown by the server. Null if no errors occured. | `Connection timeout.`                               |

# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.Multipart.SendMultipartRequest.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# License

This project is licensed under the MIT License - see the LICENSE file for details.

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes                                                                                                              |
|---------|----------------------------------------------------------------------------------------------------------------------|
| 1.0.0   | Initial implementation of the task.                                                                                  |
| 1.0.1   | Task package description update.                                                                                     |
| 1.0.2   | TargetFramework update to net 6.0. Support for manual parameters.                                                    |
| 1.0.3   | Default value for timeout parameter.                                                                                 |
| 1.0.4   | Fixed issue with NuGet feed did not found Task package.                                                              |
| 1.0.5   | Removed unnecessary try-catch block to enable stacktracing                                                           |
| 1.1.0   | Fixed error message and added param ThrowExceptionOnErrorResponse                                                    |
| 1.2.0   | Added option to select which file parameter key will be used. Changed timeout from double to int for UI improvement. |
| 1.3.0   | Added option to select which HTTP method should be used for the request.                                             |
