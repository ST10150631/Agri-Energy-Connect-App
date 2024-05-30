using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Controllers
{
    public class EducationHubController : Controller
    {
        private EducationHubModel model = new EducationHubModel();

        public IActionResult Hub()
        {
            List<CourseModel> courses = model.GetAllCourses().Result;
            return View(courses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public IActionResult AddEducationResource()
        {
            var course = new CourseModel();
            return View(course);
        }
        //======================================================= End of Method ===================================================


        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <param name="image"></param>
        /// <param name="video"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddResource(CourseModel course, IFormFile image, IFormFile video)
        {
            if(image == null || video == null)
            {
                return View(course);
            }
            else
            {
                using (var imageMemoryStream = new MemoryStream())
                using (var videoMemoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(imageMemoryStream);
                    course.CourseImage = imageMemoryStream.ToArray();

                    await video.CopyToAsync(videoMemoryStream);
                    course.CourseVideo = videoMemoryStream.ToArray();
                }
                await model.AddCourseDB(course.Name,course.Topic,course.Content,course.CourseImage,course.CourseVideo);
                return RedirectToAction("Hub");

            }
            
        }
        //======================================================= End of Method ===================================================

    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>