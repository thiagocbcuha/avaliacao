using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Domain.Contracts.Infra;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Request;
using CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model.Response;
using Flurl;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Infra
{
    public class FileRepository : IWriter, IReader
    {
        private readonly string _fileName;

        public FileRepository(IConfiguration configuration)
        {
            _fileName = configuration.GetSection("FileName").Value;
        }

        public async Task<FileResponse> GetData(FileRequest fileRequest)
        {
            var stringBuilder = new StringBuilder();
            var directoryInfo = new DirectoryInfo(fileRequest.Path);
            if (!directoryInfo.Exists)
                throw new Exception("Files directory doesn't exists.");

            var files = directoryInfo.GetFiles($"*.{ fileRequest.Type }");
            foreach (var file in files)
                stringBuilder.AppendLine(File.ReadAllText(file.FullName));

            return await Task.FromResult(new FileResponse { Value = stringBuilder.ToString() });
        }

        public async Task<LogResponse> SaveData(LogRequest logRequest)
        {
            var result = false;

            try
            {
                Console.Write(logRequest.Content.ToString());

                var fileInfo = new FileInfo(logRequest.Path);

                if (fileInfo.Attributes == FileAttributes.Directory)
                    result = CreateFileInRelativePath(logRequest);
                else
                    result = CreateFileInFullPath(logRequest);

                return await Task.FromResult(new LogResponse { Result = result });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new LogResponse { Result = result });
            }
        }

        private bool CreateFileInFullPath(LogRequest logRequest)
        {
            try
            {
                var fileInfo = new FileInfo(logRequest.Path);
                if (!fileInfo.Directory.Exists)
                    Directory.CreateDirectory(fileInfo.Directory.ToString());

                File.WriteAllText(fileInfo.ToString(), logRequest.Content.ToString());

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CreateFileInRelativePath(LogRequest logRequest)
        {
            var directory = new DirectoryInfo(logRequest.Path);
            if (!directory.Exists)
                Directory.CreateDirectory(logRequest.Path);

            FileInfo file;
            var fileName = "";
            var versionFile = 0;
            var files = directory.GetFiles().Select(i => i.Name);

            do
            {
                fileName = string.Format(_fileName, ++versionFile);
                file = new FileInfo(Path.Combine(logRequest.Path, fileName));
            } while (files.Any(i => i.Contains(fileName)));


            File.WriteAllText(file.ToString(), logRequest.Content.ToString());

            return !file.Exists;
        }
    }
}
