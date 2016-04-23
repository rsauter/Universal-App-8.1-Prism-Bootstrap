using System;
using System.Collections.Generic;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace brevis.prism.app.Common
{
    public static class CryptoHelper
    {
        public static string GetHash(string toHash)
        {
            var h = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buffUtf8Msg = CryptographicBuffer.ConvertStringToBinary(toHash, BinaryStringEncoding.Utf8);
            var buffHashed = h.HashData(buffUtf8Msg);
            var hash = CryptographicBuffer.EncodeToBase64String(buffHashed);
            return hash;
        }
    }
}
