using NSec.Cryptography;
using System;
using System.Text;

namespace HealthyGamerPortal.Common.Cryptography
{
    public static class Rfc7905
    {
        private static readonly AeadAlgorithm _algorithm = AeadAlgorithm.ChaCha20Poly1305;
        private static readonly byte[] _associatedData = new byte[] { 0x60, 0x41, 0xe2, 0xbf, 0x3c };

        private static readonly Nonce _sendIV = new Nonce(fixedField: RandomGenerator.Default.GenerateBytes(12), counterFieldSize: 0);
        private static readonly Key _sendKey = Key.Import(_algorithm, System.IO.File.ReadAllBytes("clientkey.nsec"), KeyBlobFormat.NSecSymmetricKey);

        private static Nonce _sendSequenceNumber = new Nonce(fixedFieldSize: 4, counterFieldSize: 8);
        private static Nonce _receiveSequenceNumber = new Nonce(fixedFieldSize: 4, counterFieldSize: 8);

        private static void EncryptBeforeSend(ReadOnlySpan<byte> associatedData, Nonce sendNonce, ReadOnlySpan<byte> plaintext, Span<byte> ciphertext)
        {
            _algorithm.Encrypt(_sendKey, sendNonce, associatedData, plaintext, ciphertext);

            if (!Nonce.TryIncrement(ref _sendSequenceNumber))
            {
                _sendKey.Dispose();
            }
        }

        private static bool DecryptAfterReceive(Nonce receiveNonce, ReadOnlySpan<byte> ciphertext, Span<byte> plaintext)
        {
            if (!_algorithm.Decrypt(_sendKey, receiveNonce, _associatedData, ciphertext, plaintext))
            {
                _sendKey.Dispose();
                return false;
            }

            if (!Nonce.TryIncrement(ref _receiveSequenceNumber))
            {
                _sendKey.Dispose();
                return false;
            }
            return true;
        }

        public static string DecryptText(int length, string encryptedText)
        {
            var cipherText = Convert.FromBase64String(encryptedText);
            var receiveNonce = new Nonce(fixedField: cipherText.AsSpan(0, 12), counterFieldSize: 0);
            var actual = new byte[length];

            DecryptAfterReceive(receiveNonce, cipherText.AsSpan(12), actual);
            return Encoding.UTF8.GetString(actual);
        }

        public static string EncryptText(string text)
        {
            var convertedString = Encoding.UTF8.GetBytes(text);
            var ciphertext = new byte[convertedString.Length + _algorithm.TagSize];
            var nonce = _sendSequenceNumber ^ _sendIV;

            EncryptBeforeSend(_associatedData, nonce, convertedString, ciphertext);
            return Convert.ToBase64String(nonce.ToArray()) + Convert.ToBase64String(ciphertext);
        }

        /*
        private static void ExportThatShit()
        {
            var creationParameters = new KeyCreationParameters
            {
                ExportPolicy = KeyExportPolicies.AllowPlaintextExport
            };

            using (var clientKey = new Key(_algorithm, creationParameters))
            using (var serverKey = new Key(_algorithm, creationParameters))
            {
                var clientBlob = clientKey.Export(KeyBlobFormat.NSecSymmetricKey);
                var serverBlob = serverKey.Export(KeyBlobFormat.NSecSymmetricKey);
                File.WriteAllBytes("clientkey.nsec", clientBlob);
                File.WriteAllBytes("serverkey.nsec", serverBlob);
            }
        }
        */
    }
}