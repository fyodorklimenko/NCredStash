namespace NCredStash.Crypto
{
    public interface IHmacVerifier
    {
        void Verify(byte[] hmacKey, byte[] contentsBytes, string hmacActual);
    }
}