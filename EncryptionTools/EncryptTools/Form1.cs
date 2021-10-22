using EncryptionTools;
using System;
using System.Windows.Forms;

namespace EncryptTools
{
    public partial class Form1 : Form
    {
        Encoder encode = new Encoder();

        public Form1()
        {
            InitializeComponent();

            string[] comboItems = { 
                                    //可逆編碼(對稱金鑰)
                                    "AES",
                                    "DES",
                                    "RC2",
                                    "TripleDES",

                                    //可逆編碼(非對稱金鑰)
                                    "RSA",

                                    //不可逆編碼(雜湊值)
                                    "MD5",
                                    "SHA1",
                                    "SHA256",
                                    "SHA384",
                                    "SHA512"
                                  };

            this.comboBox1.Items.Clear();
            foreach (string item in comboItems)
            {
                this.comboBox1.Items.Add(item);
            }
            this.comboBox1.SelectedIndex = 0;
        }
               

        EncoderType GetEncoderType()
        {
            string str = this.comboBox1.Items[this.comboBox1.SelectedIndex].ToString();
            switch (str)
            {
                //可逆編碼(對稱金鑰)
                case "AES": return EncoderType.AES;
                case "DES": return EncoderType.DES;
                case "RC2": return EncoderType.RC2;
                case "TripleDES": return EncoderType.TripleDES;

                //可逆編碼(非對稱金鑰)
                case "RSA": return EncoderType.RSA;

                //不可逆編碼(雜湊值)
                case "MD5": return EncoderType.MD5;
                case "SHA1": return EncoderType.SHA1;
                case "SHA256": return EncoderType.SHA256;
                case "SHA384": return EncoderType.SHA384;
                case "SHA512": return EncoderType.SHA512;
                default:
                    {
                        this.comboBox1.SelectedIndex = 0;
                        return EncoderType.AES;
                    }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Encrypt.Text))
            {
                MessageBox.Show("加密字串不可為空");
            }
            else
            {
                if (string.IsNullOrEmpty(this.Key.Text) || string.IsNullOrEmpty(this.Iv.Text))
                {
                    MessageBox.Show("請先產生key Iv");
                }
                else
                {
                    EncoderType type = GetEncoderType();
                    encode.Key = this.Key.Text;
                    encode.IV = this.Iv.Text;
                    this.Decrypt.Text = encode.Encrypt(type, this.Encrypt.Text);
                }
            }           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Decrypt.Text))
            {
                MessageBox.Show("解密字串不可為空");
            }
            else
            {
                if (string.IsNullOrEmpty(this.Key.Text) || string.IsNullOrEmpty(this.Iv.Text))
                {
                    MessageBox.Show("請先產生key Iv");
                }
                else
                {
                    EncoderType type = GetEncoderType();
                    encode.Key = this.Key.Text;
                    encode.IV = this.Iv.Text;
                    this.Encrypt.Text = encode.Decrypt(type, this.Decrypt.Text);
                }
            }                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EncoderType type = GetEncoderType();
            encode.GenerateKey(type);
            this.Key.Text = encode.Key;
            this.Iv.Text = encode.IV;
        }
    }
}
