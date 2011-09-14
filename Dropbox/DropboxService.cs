using System;
using DropNet;
using DropNet.Models;

namespace EckeSnuff.Dropbox {
    public class DropboxService : IDropboxService {
        private readonly DropNetClient _dropclient;
        private readonly string _userName;
        private readonly string _password;
        private bool _isAuthenticated;

        #region public DropboxService(string appKey, string appSecret, string userName, string password)
        /// <summary>
        /// Initializes a new instance of the <b>DropboxService</b> class.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public DropboxService(string appKey, string appSecret, string userName, string password) {
            _password = password;
            _userName = userName;
            _dropclient = new DropNetClient(appKey, appSecret);
        }
        #endregion
        #region public bool FileExists(string virtualPath)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public bool FileExists(string virtualPath) {
            Authenticate();
            try {
                var metadata = _dropclient.GetMetaData(virtualPath);
                return !string.IsNullOrEmpty(metadata.Path);
            }
            catch (Exception ex) {
                return false;
            }

        }
        #endregion
        #region public byte[] GetFile(string virtualPath)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public byte[] GetFile(string virtualPath) {
            Authenticate();
            return _dropclient.GetFile(virtualPath);
        }
        #endregion
        #region public MetaData GetMetadata(string path)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public MetaData GetMetadata(string path) {
            Authenticate();
            return _dropclient.GetMetaData(path);
        }
        #endregion
        #region private void Authenticate()
        /// <summary>
        /// 
        /// </summary>
        private void Authenticate() {
            if (!_isAuthenticated) {
                try {
                    _dropclient.Login(_userName, _password);
                    _isAuthenticated = true;
                }
                catch {
                    throw;
                }
            }
        }
        #endregion

    }
}
