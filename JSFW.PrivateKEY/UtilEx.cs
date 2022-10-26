using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace JSFW.PrivateKEY
{
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion
    }

    static class UtilEx
    {
        /// <summary>
        /// 컨트롤 비동기 호출! 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <param name="ctrl"></param>
        /// <param name="action"></param>
        public static void DoAsync<TControl>(this TControl ctrl, Action<TControl> action) where TControl : Control
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action, ctrl);
            }
            else
            {
                action(ctrl);
            }
        }

        /// <summary>
        /// Object To XML
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="value">object Instance</param>
        /// <returns></returns>
        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;
            string xml = "";
            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (var stringWriter = new System.IO.StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                    {
                        xmlSerializer.Serialize(xmlWriter, value);
                        xml = stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // 변환 중 Error!
                System.Diagnostics.Debugger.Log(0, typeof(T).GetType().Name + " Serialize", ex.Message);
            }
            return xml;
        }

        /// <summary>
        /// Xml String !
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(this string xml) where T : class, new()
        {
            T obj = default(T);
            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (var stringReader = new System.IO.StringReader(xml))
                {
                    using (var reader = XmlReader.Create(stringReader, new XmlReaderSettings()))
                    {
                        obj = xmlSerializer.Deserialize(reader) as T;
                    }
                }
            }
            catch (Exception ex)
            {
                // 변환 중 Error!
                System.Diagnostics.Debugger.Log(0, typeof(T).GetType().Name + " Serialize", ex.Message);
            }
            return obj;
        }

        /// <summary>
        /// Object To XML
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="value">object Instance</param>
        /// <returns></returns>
        public static string JsonSerialize<T>(this T value)
        {
            if (value == null) return string.Empty;
            string json = "";
            try
            {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(value);

            }
            catch (Exception ex)
            {
                // 변환 중 Error!
                System.Diagnostics.Debugger.Log(0, typeof(T).GetType().Name + " Serialize", ex.Message);
            }
            return json;
        }

        /// <summary>
        /// Xml String !
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T JsonDeSerialize<T>(this string xml) where T : class, new()
        {
            T obj = default(T);
            try
            {
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject(xml, typeof(T)) as T;
            }
            catch (Exception ex)
            {
                // 변환 중 Error!
                System.Diagnostics.Debugger.Log(0, typeof(T).GetType().Name + " Serialize", ex.Message);
            }
            return obj;
        }

        public static string Enc(this string data, string pwd)
        {
            //https://h5bak.tistory.com/148
            //https://lazydeveloper.net/1703266
            using (RijndaelManaged RijndaelCipher = new RijndaelManaged())
            {
                // 입력받은 문자열을 바이트 배열로 변환 -- UniCode 설정!! 주의 Dec도 UniCode로 설정!! (2022-03-29)
                byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(data);

                // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서 
                // Salt를 사용한다.
                byte[] Salt = Encoding.ASCII.GetBytes(pwd.Length.ToString());

                using (PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(pwd, Salt))
                {
                    // Create a encryptor from the existing SecretKey bytes.
                    // encryptor 객체를 SecretKey로부터 만든다.
                    // Secret Key에는 32바이트
                    // Initialization Vector로 16바이트를 사용
                    ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(PlainText, 0, PlainText.Length);
                            cryptoStream.FlushFinalBlock();

                            byte[] CipherBytes = memoryStream.ToArray();

                            string EncryptedData = Convert.ToBase64String(CipherBytes);

                            return EncryptedData;
                        }
                    }
                }
            }
        }

        public static string Dec(this string data, string pwd)
        {
            //https://h5bak.tistory.com/148
            //https://lazydeveloper.net/1703266
            using (RijndaelManaged RijndaelCipher = new RijndaelManaged())
            {
                byte[] EncryptedData = Convert.FromBase64String(data);
                byte[] Salt = Encoding.ASCII.GetBytes(pwd.Length.ToString());

                using (PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(pwd, Salt))
                {
                    // Decryptor 객체를 만든다.
                    ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                    using (MemoryStream memoryStream = new MemoryStream(EncryptedData))
                    {
                        // 데이터 읽기 용도의 cryptoStream객체
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
                        {

                            // 복호화된 데이터를 담을 바이트 배열을 선언한다.
                            byte[] PlainText = new byte[EncryptedData.Length];
                                                       
                            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                            //주의!! Enc에서 UniCode로 설정되어 있음!!

                            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
                            return DecryptedData.Replace("\0", "");
                        }
                    }
                }
            }
        }

        public static void Alert(this object msg)
        {
            MessageBox.Show("" + msg);
        }

        public static bool Confirm(this string msg)
        {
            return MessageBox.Show("" + msg, "확인?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
         
        public static string FirstMasking(this string txt, int right = 16)
        {
            if (right < 0) right = 0;

            int mask = (int)Math.Round((txt.Length * 0.3), 0);
            string maskString = new string('*', mask);
            return (maskString + txt.Substring(mask)).PadRight(right, '*');
        }

        public static string LastMasking(this string txt, int right = 16)
        {
            if (right < 0) right = 0;

            int mask = (int)Math.Round((txt.Length * 0.3), 0);
            string maskString = new string('*', mask);
            return (txt.Substring(0, txt.Length - mask) + maskString).PadRight(right, '*');
        }

        public  static string GetCommandLineOption()
        {
            string[] cmd = Environment.GetCommandLineArgs();
            if (1 < cmd.Length) return cmd[1];
            return "";
        }
         
        public static string GetDeviceSerialNumber(string driveName)
        {
            // ref : https://stackoverflow.com/questions/31129989/obtaining-hdd-serial-number-via-drive-letter-using-wmi-query-in-c-sharp
            try
            {
                using (var partitions = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + driveName.TrimEnd( Path.DirectorySeparatorChar ) +
                                                    "'} WHERE ResultClass=Win32_DiskPartition"))
                {
                    foreach (var partition in partitions.Get())
                    {
                        using (var drives = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" +
                                                                partition["DeviceID"] +
                                                                "'} WHERE ResultClass=Win32_DiskDrive"))
                        {
                            foreach (var drive in drives.Get())
                            {
                                return (string)drive["SerialNumber"];
                            }
                        }
                    }
                }
            }
            catch
            {
                return "<unknown>";
            }

            // Not Found
            return "<unknown>";
        }

    }

    //# 사용예 ) 
    //    string str = "abc"; 
    //    var bt = GZIP.Compress(Encoding.UTF8.GetBytes(str)); 
    //    var tt = GZIP.Decompress(bt);
    //    var ot = Encoding.UTF8.GetString(tt); 
    //# 클래스 )

    internal class GZIP
    {
        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static byte[] Decompress(byte[] gzip)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gunzip = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress, true))
                {
                    const int size = 1024;
                    byte[] buffer = new byte[size];
                    int count = 0;
                    do
                    {
                        count = gunzip.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    } while (count > 0);
                }
                return memory.ToArray();
            }

        }
    }

    internal class Encrypt
    {
        static void ENC()
        {
            string ivAsBase64;
            string encryptedTextAsBase64;
            string keyAsBase64;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                // Store the IV (they can be stored if you don't re-use a key)
                aes.GenerateIV();
                byte[] iv = aes.IV;
                ivAsBase64 = Convert.ToBase64String(iv);
                Console.WriteLine("IV base64: {0}", ivAsBase64);

                // See how long the default key length is
                aes.GenerateKey();
                Console.WriteLine("Algorithm key length should be: {0}", aes.Key.Length);

                // Set a key
                string key = "a very long sentence for a key that should exceed the max length of a key for AES therefore we're going to need to substring it based on the GenerateKey length we're given";
                Console.WriteLine("Key length: {0}", key.Length);

                byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, aes.Key.Length));
                aes.Key = keyBytes;
                Console.WriteLine("Key length is now: {0}", aes.Key.Length);

                // Base64 the key for storage
                keyAsBase64 = Convert.ToBase64String(aes.Key);
                Console.WriteLine("Key base64: {0}", keyAsBase64);

                // Encrypt the text
                byte[] textBytes = Encoding.UTF8.GetBytes("chris áááéééééé óóóó 💩");
                var cryptor = aes.CreateEncryptor();
                byte[] encryptedBytes = cryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                encryptedTextAsBase64 = Convert.ToBase64String(encryptedBytes);
                Console.WriteLine("Encrypted (base64'd): {0}", encryptedTextAsBase64);
            }

            Console.WriteLine("==================================================");

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                // Decrypt the text
                byte[] iv = Convert.FromBase64String(ivAsBase64);
                byte[] keyBytes = Convert.FromBase64String(keyAsBase64);
                byte[] fromBase64ToBytes = Convert.FromBase64String(encryptedTextAsBase64);
                var decryptor = aes.CreateDecryptor(keyBytes, iv);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(fromBase64ToBytes, 0, fromBase64ToBytes.Length);

                Console.WriteLine("Decrypted: {0}", Encoding.UTF8.GetString(decryptedBytes));
            }
        }

        // using System.Security.Cryptography;

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    textBox3.Text = AESEncrypt256(textBox2.Text);
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    textBox4.Text = AESDecrypt256(textBox3.Text);
        //}

        public static String AESEncrypt256(String InputText )
        {
            string Password = "PWD";

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            // 입력받은 문자열을 바이트 배열로 변환
            byte[] PlainText = System.Text.Encoding.UTF8.GetBytes(InputText);

            // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서 
            // Salt를 사용한다.
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Create a encryptor from the existing SecretKey bytes.
            // encryptor 객체를 SecretKey로부터 만든다.
            // Secret Key에는 32바이트
            // Initialization Vector로 16바이트를 사용
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();

            // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }

        //AES_256 복호화
        public static String AESDecrypt256(String InputText)
        {
            string Password = "PWD";

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Decryptor 객체를 만든다.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            // 데이터 읽기 용도의 cryptoStream객체
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            // 복호화된 데이터를 담을 바이트 배열을 선언한다.
            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.UTF8.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }


        public static string EncSHA1(string key, string msg)
        {
            byte[] result;

            byte[] msg_buffer = new ASCIIEncoding().GetBytes(msg);
            byte[] key_buffer = new ASCIIEncoding().GetBytes(key);

            HMACSHA1 h = new HMACSHA1(key_buffer);

            result = h.ComputeHash(msg_buffer);

            return Convert.ToBase64String(result);
        }
    }
}
