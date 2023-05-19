using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;


namespace WinCleaner
{
    internal static class GoogleDriveStatisticFolder
    {
        private static readonly DriveService _service;
        public const string TextType = "text/plain";
        public const string FolderType = "application/vnd.google-apps.folder";


        static GoogleDriveStatisticFolder()
        {
            var key = Encoding.UTF8.GetString(DriveResources.Key);
            var credential = GoogleCredential.FromJson(key).CreateScoped(DriveService.ScopeConstants.Drive);
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }

        public static IUploadProgress UploadFile(Stream content, string name, string mimeType, string parent)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Parents = new List<string>() { parent },
                MimeType = mimeType
            };

            var request = _service.Files.Create(fileMetadata, content, mimeType);
            var result = request.Upload();

            return result;
        }

        public static Google.Apis.Drive.v3.Data.File CreateFolder(string name, string parentId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Parents = new List<string>() { parentId },
                MimeType = FolderType
            };

            var request = _service.Files.Create(fileMetadata);
            var result = request.Execute();

            return result;
        }

        public static List<Google.Apis.Drive.v3.Data.File> ListFiles(string parentId, string mimeType)
        {
            var files = new List<Google.Apis.Drive.v3.Data.File>();

            string pageToken = null;
            do
            {
                var request = _service.Files.List();
                request.Q = $"mimeType = '{mimeType}' and '{parentId}' in parents";
                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name)";
                request.PageToken = pageToken;

                var response = request.Execute();
                files.AddRange(response.Files);

                pageToken = response.NextPageToken;
            } while (pageToken != null);

            return files;
        }
    }
}
