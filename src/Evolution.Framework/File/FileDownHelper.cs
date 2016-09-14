/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.FileProviders;

namespace Evolution.Framework
{
    public class FileDownHelper
    {
        HttpContext _context;
        public FileDownHelper(HttpContext context)
        {
            _context = context;
        }
        public bool FileExists(string FileName)
        {
            string destFileName = FileName;
            if (File.Exists(destFileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DownLoadold(string FileName, string name)
        {
            string destFileName = FileName;
            if (File.Exists(destFileName))
            {
                FileInfo fi = new FileInfo(destFileName);
                _context.Response.Clear();
                _context.Response.Headers.Clear();
                _context.Response.Headers.Append("Content-Disposition", "attachment;filename=" + WebUtility.UrlEncode(name));
                _context.Response.Headers.Append("Content-Length", fi.Length.ToString());
                _context.Response.ContentType = "application/octet-stream";
                _context.Response.SendFileAsync(destFileName);
                //_context.Response.Flush();
                //_context.Response.End();

            }
        }
        public void DownLoad(string FileName,string filePath)
        {
            ////string filePath = MapPathFile(FileName);
            //long chunkSize = 204800;             //指定块大小 
            //byte[] buffer = new byte[chunkSize]; //建立一个200K的缓冲区 
            //long dataToRead = 0;                 //已读的字节数   
            //FileStream stream = null;
            //try
            //{
            //    //打开文件   
            //    stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //    dataToRead = stream.Length;

            //    //添加Http头   
            //    _context.Response.ContentType = "application/octet-stream";
            //    _context.Response.Headers.Add("Content-Disposition", "attachement;filename=" + WebUtility.UrlEncode(Path.GetFileName(filePath)));
            //    _context.Response.Headers.Add("Content-Length", dataToRead.ToString());

            //    while (dataToRead > 0)
            //    {
            //        if (_context.Response.IsClientConnected)
            //        {
            //            int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
            //            _context.Response.OutputStream.Write(buffer, 0, length);
            //            _context.Response.Flush();
            //            _context.Response.Clear();
            //            dataToRead -= length;
            //        }
            //        else
            //        {
            //            dataToRead = -1; //防止client失去连接 
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //_context.Response.Write("Error:" + ex.Message);
            //}
            //finally
            //{
            //    //if (stream != null) stream.Close();
            //    //_context.Response.Close();
            //}
        }
        public bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            //try
            //{
            //    FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //    BinaryReader br = new BinaryReader(myFile);
            //    try
            //    {
            //        _Response.Headers.Add("Accept-Ranges", "bytes");

            //        long fileLength = myFile.Length;
            //        long startBytes = 0;
            //        int pack = 10240;  //10K bytes
            //        int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;

            //        if (_Request.Headers["Range"].ToString() != null)
            //        {
            //            _Response.StatusCode = 206;
            //            string[] range = _Request.Headers["Range"].ToString().Split(new char[] { '=', '-' });
            //            startBytes = Convert.ToInt64(range[1]);
            //        }
            //        _Response.Headers.Add("Content-Length", (fileLength - startBytes).ToString());
            //        if (startBytes != 0)
            //        {
            //            _Response.Headers.Add("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
            //        }

            //        _Response.Headers.Add("Connection", "Keep-Alive");
            //        _Response.ContentType = "application/octet-stream";
            //        _Response.Headers.Add("Content-Disposition", "attachment;filename=" + WebHelper.UrlEncode(_fileName));

            //        br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
            //        int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

            //        for (int i = 0; i < maxCount; i++)
            //        {
            //            if (_Response.IsClientConnected)
            //            {
            //                _Response.BinaryWrite(br.ReadBytes(pack));
            //                Thread.Sleep(sleep);
            //            }
            //            else
            //            {
            //                i = maxCount;
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        return false;
            //    }
            //    finally
            //    {
            //        //br.Close();
            //        //myFile.Close();
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
        }
    }
}
