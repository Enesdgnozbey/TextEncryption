using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace TextEncryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string hash = "";
        string Encrypt(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);
            using (MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        string Descrypt(string text)
        {
            byte[] data =Convert.FromBase64String(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.Default.GetString(results);
                        
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            txtCozulen.Text = Descrypt(txtSifrelenen.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSifrelenen.Text = Encrypt(txtMetin.Text);
        }

        private void txtMetin_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
