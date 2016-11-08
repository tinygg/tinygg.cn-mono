using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBUtility;
using mono_blog.Models;
using System.Data;

namespace mono_blog.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public static string site_name = DbHelperMySQL.GetSingle("select dict_value from dict where dict_key = 'site_name'").ToString();
        public static string site_moto = DbHelperMySQL.GetSingle("select dict_value from dict where dict_key = 'site_moto'").ToString();
        public static string site_domain = DbHelperMySQL.GetSingle("select dict_value from dict where dict_key = 'site_domain'").ToString();
        public static int site_page_size = int.Parse(DbHelperMySQL.GetSingle("select dict_value from dict where dict_key = 'site_page_size'").ToString());


        public ActionResult Index()
        {
            int page = Request.Params["page"] == null ? 0 : int.Parse(Request.Params["page"].ToString());
            int max_count = int.Parse(DbHelperMySQL.GetSingle("select count(*) from post where 1=1").ToString());
            int max_page = max_count / site_page_size + (max_count % site_page_size > 0 ? 1 : 0);
            DataSet index_post = DbHelperMySQL.Query(string.Format("select * from post where 1=1 order by stamp desc limit {0},{1}", site_page_size * page, site_page_size));
            List<Post> posts = Models.Post.Transform(index_post);
            ViewData["title"] = string.Format("首页-{0} {1}", site_moto, site_name);
            ViewData["canonical_url"] = string.Format("http://{0}{1}", site_domain, "");
            ViewData["posts"] = posts;

            ViewData["last_page_url"] = (page > 1 ? string.Format("/?page={0}", page - 1) : "/");
            ViewData["last_page_text"] = (page >= 1 ? "上一页" : "首页");

            ViewData["next_page_url"] = (page >= max_page - 1 ? string.Format("/?page={0}", max_page - 1) : string.Format("/?page={0}", page + 1));
            ViewData["next_page_text"] = (page >= max_page - 1 ? "末页" : "下一页");
            return View();
        }

        public ActionResult Post()
        {
            string[] path_arr = Request.FilePath.Split('/');
            //2016-01-01
            bool mode_canonical = false;
            string stamp = string.Empty;

            string year = string.Empty;
            string month = string.Empty;
            string day = string.Empty;
            string title_en = string.Empty;
            if (path_arr.Length == 3 && path_arr[1].Length == (4 + 1 + 2 + 1 + 2))
            {
                string[] time_arr = path_arr[1].Split('-');
                year = time_arr[0];
                month = time_arr[1];
                day = time_arr[2];

                title_en = path_arr[2].Trim();
            }
            else if (path_arr[1].Length == 13)
            {
                mode_canonical = true;
                stamp = path_arr[1];
            }
            DataSet index_post = DbHelperMySQL.Query(
                mode_canonical ?
                string.Format("select * from post where stamp={0} limit 1", stamp) :
                string.Format("select * from post where year={0} and month = {1} and day = {2} and title_en = '{3}' limit 1", year, month, day, title_en)
                );
            List<Post> posts = Models.Post.Transform(index_post);

            if (posts.Count == 1)
            {
                ViewData["title"] = string.Format("文章-{0} {1}", posts[0].title_cn, site_name);
                ViewData["canonical_url"] = string.Format("http://{0}{1}", site_domain, posts[0].canonical_url);
                ViewData["post"] = posts[0];

                DataSet last_post = DbHelperMySQL.Query(string.Format("select * from post where stamp>{0} order by stamp asc limit 1", posts[0].stamp));
                List<Post> last_post_list = Models.Post.Transform(last_post);

                ViewData["last_post_url"] = (last_post_list.Count == 1 ? last_post_list[0].url : "/");
                ViewData["last_post_text"] = (last_post_list.Count == 1 ? "上一篇" : "返回首页");//时间更大


                DataSet next_post = DbHelperMySQL.Query(string.Format("select * from post where stamp<{0} order by stamp desc limit 1", posts[0].stamp));
                List<Post> next_post_list = Models.Post.Transform(next_post);
                ViewData["next_post_url"] = (next_post_list.Count == 1 ? next_post_list[0].url : "/");
                ViewData["next_post_text"] = (next_post_list.Count == 1 ? "下一篇" : "返回首页");//时间更小
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
