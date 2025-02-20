using Microsoft.AspNetCore.Http;
using UAParser;

namespace IdentityProvider.Core;

public class UserAgentInformation
{
    public UserAgent GetBrowserInfo(HttpContext context)
    {
        var uaString = GetUserAgent(context);

        var uaParser = Parser.GetDefault();

        var c = uaParser.Parse(uaString);

        return new UserAgent
        {
            UserAgentFamily = c.UA.Family,
            UserAgentMajor = c.UA.Major,
            UserAgentMinor = c.UA.Minor,
            OperatingSystemFamily = c.OS.Family,
            OperatingSystemMajor = c.OS.Major,
            OperatingSystemMinor = c.OS.Minor,
            DeviceFamily = c.Device.Family,
            UserAgentString = uaString,
            IpAddress = context.Connection.RemoteIpAddress?.ToString()
        };
    }

    private static string GetUserAgent(HttpContext context) => context.Request.Headers["User-Agent"];
}