using Microsoft.VisualBasic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace LVCore.LVAppService.Utilities
{
    public class CCrypto
    {
        // Token: 0x06000008 RID: 8 RVA: 0x000020F4 File Offset: 0x000002F4
        public CCrypto()
        {
            this.m_Key = new byte[]
            {
                141,
                149,
                37,
                251,
                92,
                146,
                52,
                14,
                21,
                212,
                48,
                56,
                137,
                43,
                250,
                227,
                242,
                109,
                1,
                60,
                88,
                73,
                254,
                184,
                219,
                69,
                56,
                158,
                150,
                232,
                113,
                239
            };
            this.m_IV = new byte[]
            {
                176,
                0,
                174,
                43,
                39,
                3,
                18,
                20,
                149,
                136,
                225,
                78,
                193,
                183,
                204,
                223
            };
            this.Base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            this.Base64Alpha = this.Base64Chars.ToCharArray();
        }

        // Token: 0x06000009 RID: 9 RVA: 0x00002154 File Offset: 0x00000354
        public void Generate()
        {
            SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
            symmetricAlgorithm.GenerateIV();
            symmetricAlgorithm.GenerateKey();
            checked
            {
                int num = symmetricAlgorithm.Key.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    bool flag = i != symmetricAlgorithm.Key.Length - 1;
                    if (flag)
                    {
                    }
                }
                int num2 = symmetricAlgorithm.IV.Length - 1;
                for (int i = 0; i <= num2; i++)
                {
                    bool flag2 = i != symmetricAlgorithm.IV.Length - 1;
                    if (flag2)
                    {
                    }
                }
            }
        }
        // Token: 0x0600000A RID: 10 RVA: 0x000021D4 File Offset: 0x000003D4
        public string EncryptString(string sStringToCrypt, bool utf8)
        {
            checked
            {
                string result;
                try
                {
                    MemoryStream memoryStream = new MemoryStream();
                    SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(this.m_Key, this.m_IV), CryptoStreamMode.Write);
                    cryptoStream.Write(this.ConvertStringToByte(sStringToCrypt, utf8), 0, sStringToCrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    memoryStream.Position = 0L;
                    byte[] array = new byte[(int)(memoryStream.Length - 1L) + 1];
                    memoryStream.Read(array, 0, (int)memoryStream.Length);
                    cryptoStream.Close();
                    if (utf8)
                    {
                        result = this.ConvertToBase64String(array);
                    }
                    else
                    {
                        result = this.ConvertByteToString(array, utf8);
                    }
                }
                catch (Exception)
                {

                    result = string.Empty;
                }
                return result;
            }
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000022A0 File Offset: 0x000004A0
        private byte[] ConvertStringToByte(string sText, bool utf8)
        {
            checked
            {
                byte[] result;
                if (utf8)
                {
                    result = Encoding.UTF8.GetBytes(sText);
                }
                else
                {
                    byte[] array = new byte[sText.Length - 1 + 1];
                    int num = sText.Length - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        array[i] = (byte)Strings.Asc(sText[i]);
                    }
                    result = array;
                }
                return result;
            }
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002300 File Offset: 0x00000500
        private string ConvertByteToString(byte[] sByte, bool utf8)
        {
            string text = string.Empty;
            checked
            {
                string result;
                if (utf8)
                {
                    result = Encoding.UTF8.GetString(sByte);
                }
                else
                {
                    int num = sByte.Length - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        text += Convert.ToString(Strings.Chr((int)sByte[i]));
                    }
                    result = text;
                }
                return result;
            }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002358 File Offset: 0x00000558
        private string ConvertToBase64String(byte[] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            checked
            {
                for (; ; )
                {
                    bool flag = num + 2 >= input.Length;
                    if (flag)
                    {
                        break;
                    }
                    int num2 = (int)input[num];
                    int num3 = (int)input[num + 1];
                    int num4 = (int)input[num + 2];
                    byte b = (byte)CCrypto.ShiftRight(num2 & 252, 2);
                    byte b2 = (byte)(CCrypto.ShiftLeft(num2 & 3, 4) | CCrypto.ShiftRight(num3 & 240, 4));
                    byte b3 = (byte)(CCrypto.ShiftLeft(num3 & 15, 2) | CCrypto.ShiftRight(num4 & 192, 6));
                    byte b4 = (byte)(num4 & 63);
                    stringBuilder.Append(this.Base64Alpha[(int)b]);
                    stringBuilder.Append(this.Base64Alpha[(int)b2]);
                    stringBuilder.Append(this.Base64Alpha[(int)b3]);
                    stringBuilder.Append(this.Base64Alpha[(int)b4]);
                    num += 3;
                }
                bool flag2 = num < input.Length;
                if (flag2)
                {
                    int num5 = input.Length - num;
                    int num2 = (int)input[num];
                    bool flag3 = num5 == 2;
                    int num3;
                    byte b3;
                    if (flag3)
                    {
                        num3 = (int)input[num + 1];
                        b3 = (byte)CCrypto.ShiftLeft(num3 & 15, 2);
                    }
                    else
                    {
                        num3 = 0;
                        b3 = 64;
                    }
                    byte b = (byte)CCrypto.ShiftRight(num2 & 252, 2);
                    byte b2 = (byte)(CCrypto.ShiftRight(num2 & 3, 4) | CCrypto.ShiftRight(num3 & 240, 4));
                    byte b4 = 64;
                    stringBuilder.Append(this.Base64Alpha[(int)b]);
                    stringBuilder.Append(this.Base64Alpha[(int)b2]);
                    stringBuilder.Append(this.Base64Alpha[(int)b3]);
                    stringBuilder.Append(this.Base64Alpha[(int)b4]);
                }
                return stringBuilder.ToString();
            }
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002500 File Offset: 0x00000700
        private byte[] ConvertFromBase64String(string input)
        {
            char[] array = input.ToCharArray();
            int num = input.IndexOf("=");
            bool flag = num == -1;
            checked
            {
                byte[] array2;
                if (flag)
                {
                    num = input.Length;
                    array2 = new byte[(int)Math.Round(unchecked((double)num / 4.0 * 3.0 - 1.0)) + 1];
                }
                else
                {
                    bool flag2 = array[input.Length - 2] == '=';
                    if (flag2)
                    {
                        array2 = new byte[(int)Math.Round(unchecked((double)input.Length / 4.0 * 3.0 - 2.0 - 1.0)) + 1];
                        num = input.Length - 2;
                    }
                    else
                    {
                        array2 = new byte[(int)Math.Round(unchecked((double)input.Length / 4.0 * 3.0 - 1.0 - 1.0)) + 1];
                        num = input.Length - 1;
                    }
                }
                int num2 = 0;
                int num3 = 0;
                while (num2 + 3 < num)
                {
                    byte @byte = this.GetByte(array[num2]);
                    byte byte2 = this.GetByte(array[num2 + 1]);
                    byte byte3 = this.GetByte(array[num2 + 2]);
                    byte byte4 = this.GetByte(array[num2 + 3]);
                    int num4 = CCrypto.ShiftLeft((int)@byte, 2) | CCrypto.ShiftRight((int)byte2, 4);
                    int num5 = CCrypto.ShiftLeft((int)(byte2 & 15), 4) | CCrypto.ShiftRight((int)byte3, 2);
                    int num6 = CCrypto.ShiftLeft((int)(byte3 & 3), 6) | (int)byte4;
                    array2[num3] = (byte)num4;
                    array2[num3 + 1] = (byte)num5;
                    array2[num3 + 2] = (byte)num6;
                    num2 += 4;
                    num3 += 3;
                }
                bool flag3 = num2 < num;
                if (flag3)
                {
                    int num7 = num - num2;
                    byte @byte = this.GetByte(array[num2]);
                    byte byte2 = this.GetByte(array[num2 + 1]);
                    int num4 = CCrypto.ShiftLeft((int)@byte, 2) | CCrypto.ShiftRight((int)byte2, 4);
                    array2[num3] = (byte)num4;
                    bool flag4 = num7 == 3;
                    if (flag4)
                    {
                        byte byte3 = this.GetByte(array[num2 + 2]);
                        int num5 = CCrypto.ShiftLeft((int)(byte2 & 15), 4) | CCrypto.ShiftRight((int)byte3, 2);
                        array2[num3 + 1] = (byte)num5;
                    }
                }
                return array2;
            }
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002758 File Offset: 0x00000958
        public static int ShiftLeft(int i, int places)
        {
            return checked((int)((long)Math.Round(unchecked((double)i * Math.Pow(2.0, (double)places))) & 255L));
        }

        // Token: 0x06000010 RID: 16 RVA: 0x0000278C File Offset: 0x0000098C
        public static int ShiftRight(int i, int places)
        {
            return checked((int)(unchecked((long)i) / (long)Math.Round(Math.Pow(2.0, (double)places)) & 255L));
        }

        // Token: 0x06000011 RID: 17 RVA: 0x000027C0 File Offset: 0x000009C0
        private byte GetByte(char aChar)
        {
            int num = this.Base64Chars.IndexOf(aChar);
            bool flag = num == -1;
            if (flag)
            {
                throw new Exception("Base64 Error");
            }
            return checked((byte)num);
        }

        // Token: 0x04000005 RID: 5
        private byte[] m_Key;

        // Token: 0x04000006 RID: 6
        private byte[] m_IV;

        // Token: 0x04000007 RID: 7
        private string Base64Chars;

        // Token: 0x04000008 RID: 8
        private char[] Base64Alpha;
    }
}
