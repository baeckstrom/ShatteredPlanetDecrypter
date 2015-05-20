using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShatteredPlanetDecrypter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            int decryptedDataSize = -1;
            string decryptedData = null;

            DialogResult openFileDialogResult = openFileDialog.ShowDialog();
            if (openFileDialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    decryptedData = LoadFromFile(fileName, true);
                    decryptedDataSize = decryptedData.Length;
                }
                catch (System.NullReferenceException)
                {
                    return;
                }
            }

            if (decryptedDataSize > 0)
            {
                DialogResult saveFileDialogResult = saveFileDialog.ShowDialog();
                if (saveFileDialogResult == DialogResult.OK)
                {
                    string saveFileName = saveFileDialog.FileName;
                    try
                    {
                        SaveToFile(saveFileName, decryptedData, false);
                    }
                    catch (System.NullReferenceException)
                    {
                        return;
                    }
                }
            }
        }


        public static string LoadFromFile(string path, bool encrypted)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            StreamReader reader = new StreamReader(path);
            string cipherText = reader.ReadToEnd();
            reader.Close();
            if (encrypted)
            {
                return StringEncryptionClass.Decrypt(cipherText);
            }
            return cipherText;
        }

        public void SaveToFile(string path, string data, bool encrypted)
        {

            StreamWriter writer = new StreamWriter(path);
            if (encrypted)
            {
                writer.Write(StringEncryptionClass.Encrypt(data));
            }
            else
            {
                writer.Write(data);
            }
            writer.Flush();
            writer.Close();
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            int encryptedDataSize = -1;
            string encryptedData = null;

            openFileDialog.Filter = "";
            DialogResult openFileDialogResult = openFileDialog.ShowDialog();
            if (openFileDialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    encryptedData = LoadFromFile(fileName, false);
                    encryptedDataSize = encryptedData.Length;
                }
                catch (System.NullReferenceException)
                {
                    return;
                }
            }

            if (encryptedDataSize > 0)
            {
                DialogResult saveFileDialogResult = saveFileDialog.ShowDialog();
                if (saveFileDialogResult == DialogResult.OK)
                {
                    string saveFileName = saveFileDialog.FileName;
                    try
                    {
                        SaveToFile(saveFileName, encryptedData, true);
                    }
                    catch (System.NullReferenceException)
                    {
                        return;
                    }
                }
            }
        }

        private void pictureBoxB_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.baeckstrom.com");
        }


    }
}
