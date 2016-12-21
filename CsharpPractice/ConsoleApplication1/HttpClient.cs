using System;
using System.Net;
namespace simpleDemo
{
    /// <summary>
    /// 公用 Http 请求类
    /// </summary>
    class httpClient
    {
        /// <summary>
        /// 获取基础WebRequest
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="lStartPos">请求的开始位置</param>
        /// <returns></returns>
        public static HttpWebRequest getWebRequest(string url, int starPos)
        {
            HttpWebRequest request = null;
            try
            {
                request = (System.Net.HttpWebRequest)HttpWebRequest.Create(url);
                request.AddRange(starPos);
            }
            catch (Exception ex)
            {
                Program.print(ex.Message);
            }
            return request;
        }
    }
}