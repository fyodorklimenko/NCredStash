using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace NCredStash.Crypto
{
    public sealed class AesCrypto : AesCryptoBase
    {
        public override byte[] Decrypt(byte[] key, byte[] contents)
        {
            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), InitializationVector));
            var resultBytes = cipher.DoFinal(contents);

            return resultBytes;
        }
    }
}