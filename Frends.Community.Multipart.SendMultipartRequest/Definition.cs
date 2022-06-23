using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS1591

namespace Frends.Community.Multipart;

/// <summary>
/// Input-class for SendMultipartRequest
/// </summary>
public class SendInput
{
    /// <summary>
    /// Target URL.
    /// </summary>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("https://example.org/path/to/endpoint")]
    public string Url { get; set; }

    /// <summary>
    /// Array of files.
    /// </summary>
    public SendFile[] FilePaths { get; set; }

    /// <summary>
    /// Headers for the request.
    /// No need to add Content-Type, since it is always Multipart/form-data.
    /// </summary>
    public InputHeader[] Headers { get; set; }

    /// <summary>
    /// Set manual parameters.
    /// </summary>
    /// <example>Key = channel, Value = G01QH4ES8SY</example>
    public TextData[] TextData { get; set; }
}

/// <summary>
/// Options-class for SendMultipartRequest
/// </summary>
public class SendOptions
{
    /// <summary>
    /// Method for authenticating to the server.
    /// </summary>
    [DefaultValue(AuthenticationMethod.None)]
    public AuthenticationMethod Authentication { get; set; }

    /// <summary>
    /// Username for authentication.
    /// </summary>
    [DisplayFormat(DataFormatString = "Text")]
    [UIHint(nameof(Authentication), "", AuthenticationMethod.Basic)]
    public string Username { get; set; }

    /// <summary>
    /// Password for the user.
    /// </summary>
    [PasswordPropertyText]
    [DisplayFormat(DataFormatString = "Text")]
    [UIHint(nameof(Authentication), "", AuthenticationMethod.Basic)]
    public string Password { get; set; }

    /// <summary>
    /// Bearer token for OAuth2 authentication.
    /// </summary>
    [PasswordPropertyText]
    [DisplayFormat(DataFormatString = "Text")]
    [UIHint(nameof(Authentication), "", AuthenticationMethod.OAuth2)]
    public string BearerToken { get; set; }

    /// <summary>
    /// Set timeout in seconds.
    /// </summary>
    /// <example>30</example>
    public double Timeout { get; set; }

    /// <summary>
    /// Log exceptions.
    /// </summary>
    /// <example>true</example>
    public bool HandleErrors { get; set; }

}

/// <summary>
/// Result-class for SendMultipartRequest.
/// </summary>
public class SendResult
{
    /// <summary>
    /// Body of the response.
    /// </summary>
    public object Body { get; set; }

    /// <summary>
    /// Was the request successful?
    /// </summary>
    public bool RequestIsSuccessful { get; set; }

    /// <summary>
    /// Exception that was thrown by the server.
    /// </summary>
    public Exception ErrorException { get; set; }

    /// <summary>
    /// Error message from the server.
    /// </summary>
    public string ErrorMessage { get; set; }
}

/// <summary>
/// Header-class for SendMultipartRequest.
/// </summary>
public class InputHeader
{
    /// <summary>
    /// Name of the header.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Value for the header.
    /// </summary>
    public string Value { get; set; }
}

public class TextData
{
    /// <summary>
    /// Name of the header.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Value for the header.
    /// </summary>
    public string Value { get; set; }
}

/// <summary>
/// File which will be sent.
/// </summary>
public class SendFile
{
    /// <summary>
    /// Name of the file.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Full path to the file.
    /// </summary>
    public string Fullpath { get; set; }
}

/// <summary>
/// Selection of authentication methods.
/// </summary>
public enum AuthenticationMethod
{
    None,
    Basic,
    OAuth2
}
