using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT2.Model
{

    public class Yande_post_json
    {
        public int id { get; set; }
        public string tags { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public string   creator_id { get ; set ; }
        public object approver_id { get; set; }
        public string author { get; set; }
        public int change { get; set; }
        public string source { get; set; }
        public int score { get; set; }
        public string md5 { get; set; }
        public int file_size { get; set; }
        public string file_ext { get; set; }
        public string file_url { get; set; }
        public bool is_shown_in_index { get; set; }
        public string preview_url { get; set; }//预览的url
        public int preview_width { get; set; }
        public int preview_Height { get; set; }
        public int actual_preview_width { get; set; }
        public int actual_preview_Height { get; set; }
        public string sample_url { get; set; }//样品的url
        public int sample_width { get; set; }
        public int sample_Height { get; set; }
        public int sample_file_size { get; set; }
        public string jpeg_url { get; set; } //原图的url
        public int jpeg_width { get; set; }
        public int jpeg_Height { get; set; }
        public int jpeg_file_size { get; set; }
        public string rating { get; set; }
        public bool is_rating_locked { get; set; }
        public bool has_children { get; set; }
        public object parent_id { get; set; }
        public string status { get; set; }
        public bool is_pending { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool is_Held { get; set; }
        public string frames_pending_string { get; set; }
        public List<object> frames_pending { get; set; }
        public string frames_string { get; set; }
        public List<object> frames { get; set; }
        public bool is_note_locked { get; set; }
        public int last_noted_at { get; set; }
        public int last_commented_at { get; set; }
    }
    public class Konachan_post_json
    {
        public int id { get; set; }
        public string tags { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public string creator_id { get; set; }
        public object approver_id { get; set; }
        public string author { get; set; }
        public int change { get; set; }
        public string source { get; set; }
        public int score { get; set; }
        public string md5 { get; set; }
        public int file_size { get; set; }
        public string file_ext { get; set; }
        private string _f_url;
        public string file_url
        {
            get { return _f_url; }
            set { _f_url = "https:" + value; }
        }
        public bool is_shown_in_index { get; set; }
        private string _p_url;
        public string preview_url
        {
            get { return _p_url ; }
            set { _p_url = "https:" + value; }
        }//预览的url
        public int preview_width { get; set; }
        public int preview_Height { get; set; }
        public int actual_preview_width { get; set; }
        public int actual_preview_Height { get; set; }
        private string _s_url;
        public string sample_url
        {
            get { return _s_url; }
            set { _s_url = "https:" + value; }
        }//样品的url
        public int sample_width { get; set; }
        public int sample_Height { get; set; }
        public int sample_file_size { get; set; }
        public string jpeg_url { get; set; } //原图的url
        public int jpeg_width { get; set; }
        public int jpeg_Height { get; set; }
        public int jpeg_file_size { get; set; }
        public string rating { get; set; }
        public bool is_rating_locked { get; set; }
        public bool has_children { get; set; }
        public object parent_id { get; set; }
        public string status { get; set; }
        public bool is_pending { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool is_Held { get; set; }
        public string frames_pending_string { get; set; }
        public List<object> frames_pending { get; set; }
        public string frames_string { get; set; }
        public List<object> frames { get; set; }
        public bool is_note_locked { get; set; }
        public int last_noted_at { get; set; }
        public int last_commented_at { get; set; }
    }

}
