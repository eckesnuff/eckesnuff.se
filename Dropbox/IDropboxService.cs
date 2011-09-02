using DropNet.Models;

namespace EckeSnuff.Dropbox {
    public interface IDropboxService {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        bool FileExists(string virtualPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        byte[] GetFile(string virtualPath);

        MetaData GetMetadata(string directory);
    }
}