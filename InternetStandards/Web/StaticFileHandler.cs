using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

namespace InternetStandards.Web
{
    class StaticFileHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        private static void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            if (((context.Request.Headers["Range"] != null) || context.Request.RequestType.Equals("(GETSOURCE)")) || context.Request.RequestType.Equals("(HEADSOURCE)"))
                validationStatus = HttpValidationStatus.IgnoreThisRequest;
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            FileInfo fileInfo = new FileInfo(context.Request.PhysicalPath);
            if (fileInfo.Exists)
            {
                if (request.RequestType == "GET" && request.RequestType == "HEAD")
                {
                    bool unmodifed = false;
                    if (request.Headers["If-Unmodified-Since"] != null)
                        try
                        {
                            unmodifed = fileInfo.LastWriteTimeUtc <= DateTime.Parse(request.Headers["If-Unmodified-Since"]);
                        }
                        catch
                        {
                        }

                    bool modified = true;
                    if (request.Headers["If-Modified-Since"] != null)
                        modified = fileInfo.LastWriteTimeUtc > DateTime.Parse(request.Headers["If-Modified-Since"]);

                    bool send = !unmodifed & modified;

                    if (request.Headers["Range"] != null)
                    {
                        List<long[]> offestLengthPairs = new List<long[]>();
                        long contentLength = 0;
                        foreach (string range in request.Headers.GetValues("Range"))
                        {
                            string[] rangeParts = range.Split('-');
                            long[] offesetLengthPair = new long[2];
                            if (rangeParts[0] == null)
                            {
                                offesetLengthPair[1] = long.Parse(rangeParts[0]);
                                offesetLengthPair[0] = fileInfo.Length - offesetLengthPair[1];
                            }
                            else
                            {
                                offesetLengthPair[0] = long.Parse(rangeParts[0]);
                                
                                long lastByte = fileInfo.Length - 1;
                                if(rangeParts[0] != null)
                                    lastByte = long.Parse(rangeParts[1]);
                                
                                offesetLengthPair[1] = lastByte - offesetLengthPair[0] + 1;
                            }

                            contentLength += offesetLengthPair[1];
                        }
                    }
                }
                else
                {
                    response.StatusCode = 405;
                    response.AppendHeader("Allow", "GET");
                }
            }
        }

        private static void BuildFileItemResponse(HttpContext context, string fileName, long fileSize, DateTime lastModifiedTime, string strETag)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            bool readIntoMemory = false;
            int num = 0x40000;
            bool flag2 = false;
            string str = request.Headers["Range"];
            if ((str != null) && str.StartsWith("bytes", StringComparison.InvariantCultureIgnoreCase))
            {
                flag2 = true;
            }
            if (flag2)
            {
                SendEntireEntity(context, strETag, lastModifiedTime);
            }
            if (((fileSize <= num) && !request.RequestType.Equals("(GETSOURCE)")) && !request.RequestType.Equals("(HEADSOURCE)"))
            {
                readIntoMemory = true;
            }
            if (readIntoMemory)
            {
                response.WriteFile(fileName, readIntoMemory);
            }
            else
            {
                response.TransmitFile(fileName);
            }
            
            //response.ContentType = MimeMapping.GetMimeMapping(fileName);
            response.AppendHeader("Accept-Ranges", "bytes");
            if (readIntoMemory)
            {
                response.Cache.AddValidationCallback(new HttpCacheValidateHandler(StaticFileHandler.CacheValidateHandler), null);
                response.AddFileDependency(fileName);
                response.Cache.SetExpires(DateTime.Now.AddDays(1));
            }
        }

        public static void SendEntity(HttpContext context, FileInfo fileInfo)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            response.AppendHeader("Content-Length", fileInfo.Length.ToString());
        }

        public static void SendEntity(HttpContext context, FileInfo fileInfo, long firstByte, long lastByte)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            long contentLength = lastByte - firstByte + 1;

            response.AppendHeader("Content-Range", "bytes " + firstByte.ToString() + '-' + lastByte.ToString() + '/' + fileInfo.Length);
            response.AppendHeader("Content-Length", contentLength.ToString());

            response.WriteFile(fileInfo.FullName, firstByte, contentLength);
        }

        public static void SendEntityHeaders(HttpContext context, string path)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            response.AppendHeader("Last-Modifed", File.GetLastWriteTimeUtc(path).ToString("r"));
            response.AppendHeader("Content-MD5", Convert.ToBase64String(MD5.Create().ComputeHash(File.ReadAllBytes(path))));
        }

        internal static string GenerateETag(HttpContext context, DateTime lastModTime)
        {
            StringBuilder builder = new StringBuilder();
            long num = DateTime.Now.ToFileTime();
            long num2 = lastModTime.ToFileTime();
            builder.Append("\"");
            builder.Append(num2.ToString("X8", CultureInfo.InvariantCulture));
            builder.Append(":");
            builder.Append(num.ToString("X8", CultureInfo.InvariantCulture));
            builder.Append("\"");
            if ((DateTime.Now.ToFileTime() - num2) <= 0x1c9c380L)
                return ("W/" + builder.ToString());
            return builder.ToString();
        }

        private static bool CompareETags(string strETag1, string strETag2)
        {
            if (strETag1.Equals("*") || strETag2.Equals("*"))
                return true;
            if (strETag1.StartsWith("W/", StringComparison.InvariantCultureIgnoreCase))
                strETag1 = strETag1.Substring(2);
            if (strETag2.StartsWith("W/", StringComparison.InvariantCultureIgnoreCase))
                strETag2 = strETag2.Substring(2);
            return strETag2.Equals(strETag1);
        }

        internal static bool SendEntireEntity(HttpContext context, string strETag, DateTime lastModifiedTime)
        {
            bool flag = false;
            string str = context.Request.Headers["If-Range"];
            if (str == null)
                return false;
            if (str[0] == '"')
            {
                if (!CompareETags(str, strETag))
                    flag = true;
                return flag;
            }
            try
            {
                DateTime time = DateTime.Parse(str, CultureInfo.InvariantCulture);
                if (DateTime.Compare(lastModifiedTime, time) == 1)
                    flag = true;
            }
            catch
            {
                flag = true;
            }
            return flag;
        }
    }
}
