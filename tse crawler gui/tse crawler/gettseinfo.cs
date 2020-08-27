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
        public stock[] search(string name)
        {
            string[] lines = downloadString("http://www.tsetmc.com/tsev2/data/search.aspx?skey=" + WebUtility.UrlEncode(name)).Split(';');
            List<stock> stocks = new List<stock>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                if (data.Length > 3)
                    stocks.Add(new stock(data[0], data[2], data[1]));
            }
            return stocks.ToArray();
        }
        public string get_history_url(string id)
        {
            return "http://www.tsetmc.com/Loader.aspx?ParTree=151311&i=" + id;
        }
        public string get_table_url(string id)
        {
            return "http://www.tsetmc.com/tsev2/data/instinfodata.aspx?i=" + id + "&c=25+";
        }

        public history[] get_history(string history_url)
        {
            string source = downloadString(history_url);

            //start of the history
            int sind = source.IndexOf("=[['") + 4;
            //end of the history
            int eind = source.IndexOf("']];", sind);
            source = source.Substring(sind, eind - sind);
            string[] lines = source.Split(new string[] { "'],['" }, StringSplitOptions.None);
            List<history> histories = new List<history>();
            for (int j = 0; j < lines.Length; j++)
            {
                string[] data = lines[j].Split(new string[] { "','" }, StringSplitOptions.None);
                histories.Add(new history(data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
            }
            return histories.ToArray();
        }
        public table get_table(string table_url)
        {
            string[] rows = downloadString(table_url).Split(';')[2].Split(',');
            table t = new table("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] row = rows[i].Split('@');
                    if (i == 0)
                    {
                        t.row1buycount     = row[0];
                        t.row1buyvolume    = row[1];
                        t.row1buycost      = row[2];
                        t.row1sellcost     = row[3];
                        t.row1sellvolume   = row[4];
                        t.row1sellcount    = row[5];
                    }
                    else if (i == 1)
                    {
                        t.row2buycount = row[0];
                        t.row2buyvolume    = row[1];
                        t.row2buycost      = row[2];
                        t.row2sellcost     = row[3];
                        t.row2sellvolume   = row[4];
                        t.row2sellcount    = row[5];
                    }
                    else if (i == 2)
                    {
                        t.row3buycount     = row[0];
                        t.row3buyvolume    = row[1];
                        t.row3buycost      = row[2];
                        t.row3sellcost     = row[3];
                        t.row3sellvolume   = row[4];
                        t.row3sellcount = row[5];
                    }
                }
            }
            catch { }
            return t;
        }
        public string decompress(byte[] content)
        {
            var from = new MemoryStream(content);
            var to = new MemoryStream();
            var compress = new GZipStream(from, CompressionMode.Decompress);
            compress.CopyTo(to);
            return Encoding.UTF8.GetString(to.ToArray());
        }
    }
    public class history
    {
        public history(string date,string final,string min,string max,string count,string volume,string cost)
        {
            this.date = date;
            this.final = final;
            this.min = min;
            this.max = max;
            this.count = count;
            this.volume = volume;
            this.cost = cost;
        }
        public string date;
        public string final;
        public string min;
        public string max;
        public string count;
        public string volume;
        public string cost;
    }
    public class table
    {
        public table(string r1bn, string r1bv, string r1bc, string r1sn, string r1sv, string r1sc, string r2bn, string r2bv, string r2bc, string r2sn, string r2sv, string r2sc, string r3bn, string r3bv, string r3bc, string r3sn, string r3sv, string r3sc)
        {
            row1buycount = r1bn;
            row1buyvolume = r1bv;
            row1buycost = r1bc;
            row1sellcount = r1sn;
            row1sellvolume = r1sv;
            row1sellcost = r1sc;

            row2buycount = r2bn;
            row2buyvolume = r2bv;
            row2buycost = r2bc;
            row2sellcount = r2sn;
            row2sellvolume = r2sv;
            row2sellcost = r2sc;

            row3buycount = r3bn;
            row3buyvolume = r3bv;
            row3buycost = r3bc;
            row3sellcount = r3sn;
            row3sellvolume = r3sv;
            row3sellcost = r3sc;


        }
        //row 1
        //buy
        public string row1buycount;
        public string row1buyvolume;
        public string row1buycost;
        //sell
        public string row1sellcount;
        public string row1sellvolume;
        public string row1sellcost;
        //row 2
        //buy
        public string row2buycount;
        public string row2buyvolume;
        public string row2buycost;
        //sell
        public string row2sellcount;
        public string row2sellvolume;
        public string row2sellcost;
        //row 3
        //buy
        public string row3buycount;
        public string row3buyvolume;
        public string row3buycost;
        //sell
        public string row3sellcount;
        public string row3sellvolume;
        public string row3sellcost;
    }
    public class stock
    {
        public stock(string name,string id,string desc)
        {
            this.name = name;
            this.id = id;
            this.description = desc;
        }
        public string name;
        public string id;
        public string description;
    }
}