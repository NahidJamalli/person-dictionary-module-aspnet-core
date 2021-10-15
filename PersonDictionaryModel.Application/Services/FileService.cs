using PersonDictionaryModel.Core.Application.Interfaces;
using System;
using System.IO;

namespace PersonDictionaryModel.Core.Application.Services
{
    public sealed class FileService : IFileService
    {
        public MemoryStream GetStream(string photoBase64)
        {
            return new MemoryStream(Convert.FromBase64String(photoBase64));
        }
    }
}
