using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorWorkouts.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RazorWorkouts.Controllers
{
    [Route("api/Workout")]
    [ApiController]
    public class WorkoutController : Controller
    {

        private readonly ApplicationDbContext _db;

        public WorkoutController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Workout.ToListAsync() });
        }


        [HttpPost]
        public async Task<IActionResult> CreateRecord(int id)
        {

            var newWorkout = new Workout
            {
                Name = "",
                Reps = 0,
                Sets = 0,
                WorkoutSets = _db.WorkoutSets.Where(x => x.Id == id).First()

            };
            if (newWorkout.WorkoutSets != null)
            {
                _db.Workout.Add(newWorkout);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Add successful" });
            }


            return Json(new { success = false, message = "Error while Adding" });

        }



        //To delete workout sets, how do we delete individual workouts? TODO
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var bookFromDb = await _db.WorkoutSets.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            // we need to delete each indiviual workouts too first
            _db.WorkoutSets.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });

        }

        /*
         * [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data =await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        */
    }
}
