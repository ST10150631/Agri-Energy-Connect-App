using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            List<PostModel> posts = homeModel.GetPosts().Result;
            return View(posts);
        }

        public IActionResult AddPost()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OnFilter()
        {
            string Topic;
            var Post = new PostModel();
            return View(Post);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
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

        [HttpGet]
        public IActionResult CreatePostClicked()
        {
            var Post = new PostModel();
            return View(Post);
        }
        /// <summary>
        /// Checks if there is an uploaded image and adds the post to the database
        /// and redirects to the home page
        /// </summary>
        /// <param name="post"></param>
        /// <param name="PostImage"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePostClicked(PostModel post, IFormFile PostImage)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
