using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       private PostModel model = new PostModel();
        private HomeModel homeModel = new HomeModel();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        ///  Posts are displayed with the latest post first
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult Index()
        {
            List<PostModel> posts = homeModel.GetPosts().Result;
            return View(posts);
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// displays the add post page
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult AddPost()
        {
            return View();
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// displays the privacy policy
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public IActionResult Privacy()
        {
            return View();
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// http get for on filtered posts
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public IActionResult OnFilter()
        {
            string Topic;
            var Post = new PostModel();
            return View(Post);
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        ///  Filters the posts by topic
        /// </summary>
        /// <param name="post"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnFilter(PostModel post,string Topic)
        {
            if(Topic.Equals("All"))
            {
                List<PostModel> posts = homeModel.GetPosts().Result;
                return RedirectToAction("Index", posts);
            }
            List<PostModel> filteredPosts = homeModel.GetFilteredPosts(Topic).Result;
            return View("Index", filteredPosts);
        }
        //======================================================= End of Method ===================================================


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public IActionResult CreatePostClicked()
        {
            var Post = new PostModel();
            return View(Post);
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Checks if there is an uploaded image and adds the post to the database
        /// and redirects to the home page
        /// </summary>
        /// <param name="post"></param>
        /// <param name="PostImage"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePostClicked(PostModel post, IFormFile PostImage)
        {
            if(post.Content.IsNullOrEmpty() )
            {
                TempData["RequiredFieldsMissing"] = "Please fill in the Post content. This field is required";
                return View("AddPost",post);
            }
            else
            { 
                if(PostImage != null)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        PostImage.CopyTo(ms);
                        post.PostImage = ms.ToArray();
                    }
                }
                homeModel.AddPost(post.Content, post.Topic, post.PostImage);
                return RedirectToAction("Index","Home");
            }
           
        }
        //======================================================= End of Method ===================================================

    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>