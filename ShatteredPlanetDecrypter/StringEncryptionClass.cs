using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShatteredPlanetDecrypter
{
    public class StringEncryptionClass
    {
        /// <summary>
        /// Decryption function
        /// </summary>
        /// <param name="encryptedText">Encrypted and Base64 encoded data to be decrypted.</param>
        /// <returns>Decrypted data as UTF8 encoded String.</returns>
        public static string Decrypt(string encryptedText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes("12348638601234567890123456789012");
            byte[] inputBuffer = null;
            byte[] outputBuffer = null;
            try
            {
                inputBuffer = Convert.FromBase64String(encryptedText);
            }
            catch (System.ArgumentNullException e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (System.FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = keyBytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            try
            {
                outputBuffer = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            catch (System.NullReferenceException e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            
            return Encoding.UTF8.GetString(outputBuffer);
        }

        /// <summary>
        /// Encryption function
        /// </summary>
        /// <param name="plainText">Plain text to encrypt and Base64 encode.</param>
        /// <returns>Encrypted and Base64 encoded String.</returns>
        public static string Encrypt(string plainText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes("12348638601234567890123456789012");
            byte[] inputBuffer = Encoding.UTF8.GetBytes(plainText);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = keyBytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] outputBuffer = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(outputBuffer, 0, outputBuffer.Length);
        }
    }
}
