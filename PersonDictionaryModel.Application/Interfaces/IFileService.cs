using System.IO;

namespace PersonDictionaryModel.Core.Application.Interfaces
{
    public interface IFileService
    {
        MemoryStream GetStream(string photoBase64);
    }
}
