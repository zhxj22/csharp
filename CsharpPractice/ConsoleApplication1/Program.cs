using System;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace simpleDemo
{
    class Program
    {

        static string GetDriveData(long data)//将磁盘大小的单位由byte转化为G
        {
            return (data / Convert.ToDouble(1024) / Convert.ToDouble(1024) / Convert.ToDouble(1024)).ToString("0.00");
        }


        class HardDiskInfo//自定义类
        {
            public string Name { get; set; }
            public string FreeSpace { get; set; }
            public string TotalSpace { get; set; }

        }

        enum T
        {
            hello, 
            no
        }
        /// <summary>
        /// 下载文件保留字
        /// </summary>
        public static string PERSIST_EXP = ".temp";

        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string key = @"iqiyivrt";

        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        /// <summary>
        /// get uuid
        /// </summary>
        /// <param name="sInputFilename"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string GetUUID(string keys)
        {
            string encryped = keys;
            string decryped = DecryptDES(encryped, key);
            //QiyiLog.Log("UUID is: " + decryped);
            return decryped;
        }



        /// <summary> 
        public static void Main(string[] args)
        {
            string keys = "PgQfjrq7RojqXUQQphL+awLKjNL67giHe5LsHemaHgjrtwtONGkaeA==";//default uuid
            string keysios = "PgQfjrq7RojqXUQQphL+azpwYDT2w6F7moaiIujagNOcl/uHc4dCqw==";//ios uuid
            string result = GetUUID(keysios);
            string ax = T.hello.ToString();
            string va = "323.51";

            float zz = (float)55 / 100;
            
            uint a = Convert.ToUInt32(va.Split('.')[0]);
            double z = Math.Round(double.Parse(va), 1);
            float zf = (float)z;


            string ok = "xxx||ljlkljl||xxx";
            int start = ok.IndexOf("||");
            int end = ok.LastIndexOf("||");
            DateTime t = DateTime.Now;
            long sec1 = t.Ticks;
            string url = "http://cdn.data.video.iqiyi.com/cdn/vr/20160826/apk/vr_389_102_10011472188768359.apk";
            string path1 = "D:\\a.apk";
            string name = url.Substring(url.LastIndexOf('/') + 1);
            download(url, path1);

            DateTime t2 = DateTime.Now;
            long sec2 = t2.Ticks;//20267973
            print(sec2 - sec1);
        }
        public static void download(string url, string path)
        {
                path = path + PERSIST_EXP;
                HttpDownload(url, path);//开始下载
        }
        /// <summary>
        /// 下载网络资源(支持断点续传)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public static void HttpDownload(string url, string path)
        {
            HttpWebRequest request = httpClient.getWebRequest(url, 0);
            WebResponse response = null;
            FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            long lStartPos = writer.Length; ;//当前文件大小
            long currentLength = 0;
            long totalLength = 0;//总大小 
            if (File.Exists(path))//断点续传
            {
                response = request.GetResponse();
                long sTotal = response.ContentLength;
                if (sTotal == lStartPos)
                {
                    close(writer);
                    File.Move(path, path.Replace(PERSIST_EXP, ""));
                    print("下载完成!");
                    return;
                }
                request = httpClient.getWebRequest(url, (int)lStartPos);
                //设置Range值
                writer.Seek(lStartPos, SeekOrigin.Begin);//指针跳转
                response = request.GetResponse();
                totalLength = response.ContentLength + lStartPos; //总长度
                currentLength = lStartPos; //当前长度
            }
            else
            {
                response = request.GetResponse();
                totalLength = response.ContentLength;
            }
            Stream reader = response.GetResponseStream();
            byte[] buff = new byte[1024];
            int c = 0; //实际读取的字节数
            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
            {
                currentLength += c;
                writer.Write(buff, 0, c);
                progressBar(currentLength, totalLength);//进度条
                writer.Flush();
            }
            close(writer);
            if (currentLength == totalLength)
            {
                File.Move(path, path.Replace(PERSIST_EXP, ""));
                print("下载完成!");
            }

            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
                response.Close();
            }
        }
        private static void close(FileStream writer)
        {
            if (writer != null)
            {
                writer.Close();
                writer.Dispose();
            }
        }
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="currentLength">当前长度</param>
        /// <param name="totalLength">总长度</param>
        public static void progressBar(Object currentLength, Object totalLength)
        {
            double aaa = System.Convert.ToDouble(currentLength);
            double bbb = System.Convert.ToDouble(totalLength);
            print(currentLength + "/" + totalLength + "__" + (aaa / bbb).ToString("0.00 %"));
        }
        /// <summary>
        /// 系统输出
        /// </summary>
        /// <param name="obj"></param>
        public static void print(Object obj)
        {
            Console.WriteLine(obj);
        }
        public static void printStr(string[] str)
        {
            foreach (string d in str)
            {
                print(d);
            }
        }
    }
}
