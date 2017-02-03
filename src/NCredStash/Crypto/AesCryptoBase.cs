namespace NCredStash.Crypto
{
    public abstract class AesCryptoBase
    {
        protected readonly byte[] InitializationVector = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };

        public abstract byte[] Decrypt(byte[] key, byte[] contents);
    }
}