using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tse_crawler
{
    public class gettseinfo
    {
        public string downloadString(string url)
        {
            WebClient w = new WebClient();
            return decompress(w.DownloadData(url));
        }
        public string search(string name)
        {
            return downloadString("http://www.tsetmc.com/tsev2/data/search.aspx?skey=" + WebUtility.UrlEncode(name));
        }
        public string get_history_url(string id)
        {
            return "http://www.tsetmc.com/Loader.aspx?ParTree=151311&i=" + id;
        }
        public string get_table_url(string id)
        {
            return "http://www.tsetmc.com/tsev2/data/instinfodata.aspx?i=" + id + "&c=25+";
        }
        //the downloaded data is in gzip format so should decompress it
        public string decompress(byte[] content)
        {
            var from = new MemoryStream(content);
            var to = new MemoryStream();
            var compress = new GZipStream(from, CompressionMode.Decompress);
            compress.CopyTo(to);
            return Encoding.UTF8.GetString(to.ToArray());
        }
    }
}