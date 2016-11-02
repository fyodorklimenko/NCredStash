using System;
using System.Security.Cryptography;

namespace NCredStash.Crypto
{
    public sealed class HmacSha256Verifier : IHmacVerifier
    {
        public void Verify(byte[] hmacKey, byte[] contentsBytes, string hmac)
        {
            var computedHmac = new HMACSHA256(hmacKey);
            var computedHmacBytes = computedHmac.ComputeHash(contentsBytes);
            var computedHmacHex = BitConverter.ToString(computedHmacBytes).Replace("-", string.Empty).ToLower();

            if (computedHmacHex != hmac)
                throw new Exception("Computed HMAC does not match stored HMAC.");
        }
    }
}