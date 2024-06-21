using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

public static class GoogleDriveHelper
{
    static string[] Scopes = { DriveService.Scope.DriveReadonly };
    static string ApplicationName = "ReceiptClient";

    public static DriveService GetDriveService()
    {
        UserCredential credential;

        using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        return service;
    }

    public static Stream GetFileStream(string fileId)
    {
        var service = GetDriveService();
        var request = service.Files.Get(fileId);
        var stream = new MemoryStream();
        request.Download(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }
}
