namespace SimpleTelegramGame.Networking.Encryption
{
    public interface IEncryptor
    {
        public long Obfuscate(long playerId, long score);
    }
}
