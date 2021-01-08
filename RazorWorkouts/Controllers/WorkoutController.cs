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
    //[Route("api/Workout")]
    [ApiController]
    public class WorkoutController : Controller
    {

        private readonly ApplicationDbContext _db;

        public WorkoutController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("api/Workout/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Workout.ToListAsync() });
        }

        [Route("api/Workout")]
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

        // Add an api call to remove each individual workouts


        [Route("api/Workout")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var WorkoutSetFromDb = await _db.WorkoutSets.FirstOrDefaultAsync(u => u.Id == id);
            var WorkoutsFromDb = await _db.Workout.Where(x => x.WorkoutSets.Id == WorkoutSetFromDb.Id).ToListAsync();

            if (WorkoutSetFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            // we need to delete each indiviual workouts too first

            if (WorkoutSetFromDb.Workout.Count() > 0)
            {
                foreach (var work in WorkoutsFromDb)
                {
                    _db.Workout.Remove(work);
                }
            }

            _db.WorkoutSets.Remove(WorkoutSetFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });

        }
        [Route("api/Workout/DeleteWorkout")]
        [HttpDelete]
        public async Task<IActionResult> DeleteWorkout(int id)
        {

            var WorkoutFromDb = await _db.Workout.FirstOrDefaultAsync(u => u.Id == id);
            if (WorkoutFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _db.Workout.Remove(WorkoutFromDb);
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
