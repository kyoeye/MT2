using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.CS.Pixiv_api
{
    class tuijian
    {
        //pixiv推荐列表
        // https://app-api.pixiv.net/v1/illust/recommended-nologin
        // https://app-api.pixiv.net/v1/illust/recommended-nologin?include_ranking_illusts=false&offset=30
        public class ImageUrls
        {
            public string square_medium { get; set; }
            public string medium { get; set; }
            public string large { get; set; }
        }

        public class ProfileImageUrls
        {
            public string medium { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string account { get; set; }
            public ProfileImageUrls profile_image_urls { get; set; }
            public bool is_followed { get; set; }
        }

        public class Tag
        {
            public string name { get; set; }
        }

        public class MetaSinglePage
        {
            //原图
            public string original_image_url { get; set; }
        }

        public class Illust
        {
            public int id { get; set; }
            public string title { get; set; }
            public string type { get; set; }
            public ImageUrls image_urls { get; set; }
            public string caption { get; set; }
            public int restrict { get; set; }
            public User user { get; set; }
            public List<Tag> tags { get; set; }
            public List<object> tools { get; set; }
            public string create_date { get; set; }
            public int page_count { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int sanity_level { get; set; }
            public MetaSinglePage meta_single_page { get; set; }
            public List<object> meta_pages { get; set; }
            public int total_view { get; set; }
            public int total_bookmarks { get; set; }
            public bool is_bookmarked { get; set; }
            public bool visible { get; set; }
            public bool is_muted { get; set; }
        }

        public class RootObject
        {
            public List<Illust> illusts { get; set; }
            public List<object> ranking_illusts { get; set; }
            public string next_url { get; set; }
        }

    }
}
