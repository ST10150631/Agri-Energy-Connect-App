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
        /// Validateas the user input and uploads the image and video to the database with the course details
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
                TempData["UploadError"] = "Eductaional Resources require both an Image and a video. Please upload an image and a video";
                return RedirectToAction("AddEducationResource",course);
            }
            else if(course.Name == null || course.Topic == null || course.Content == null)
            {
                TempData["UploadError"] = "Please fill in all fields";
                return RedirectToAction("AddEducationResource", course);
            }
            else
            {
                //Converts both the image and video to byte arrays in order to be stored in the database
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