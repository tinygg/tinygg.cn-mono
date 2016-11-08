using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace mono_blog.Models
{
    public class Post
    {
        public Int64 stamp;
        public int year;
        public int month;
        public int day;
        public string title_en;
        public string title_cn;
        public string content;
        public string short_content;
        public string url;
        public string canonical_url;

        public static List<Post> Transform(DataSet ds)
        {
            List<Post> rlt = new List<Post>();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Post post = new Post();
                    DataRow row = dt.Rows[i];
                    if (dt.Columns.Contains("stamp") && row["stamp"] != null)
                    {
                        post.stamp = Int64.Parse(row["stamp"].ToString());
                    }

                    if (dt.Columns.Contains("year") && row["year"] != null)
                    {
                        post.year = int.Parse(row["year"].ToString());
                    }

                    if (dt.Columns.Contains("month") && row["month"] != null)
                    {
                        post.month = int.Parse(row["month"].ToString());
                    }

                    if (dt.Columns.Contains("day") && row["day"] != null)
                    {
                        post.day = int.Parse(row["day"].ToString());
                    }

                    if (dt.Columns.Contains("title_en") && row["title_en"] != null)
                    {
                        post.title_en = row["title_en"].ToString();
                    }

                    if (dt.Columns.Contains("title_cn") && row["title_cn"] != null)
                    {
                        post.title_cn = row["title_cn"].ToString();
                    }

                    if (dt.Columns.Contains("content") && row["content"] != null)
                    {
                        post.content = row["content"].ToString();
                        if (post.content.Length > 250)
                        {
                            post.short_content = post.content.Substring(0, 250) + "...";
                        }
                        else
                        {
                            post.short_content = post.content;
                        }
                    }

                    post.url = MakeUrl(post);
                    post.canonical_url = MakeCanonicalUrl(post);

                    rlt.Add(post);
                }
            }
            return rlt;
        }


        public static string MakeUrl(Post p)
        {
            return string.Format("/{0:D4}-{1:D2}-{2:D2}/{3}", p.year, p.month, p.day, p.title_en);
        }

        public static string MakeCanonicalUrl(Post p)
        {
            return string.Format("/post/?id={0:D13}", p.stamp);
        }
    }
}