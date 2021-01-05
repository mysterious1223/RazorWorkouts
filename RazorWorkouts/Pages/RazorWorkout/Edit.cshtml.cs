using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWorkouts.Model;

namespace RazorWorkouts.Pages.RazorWorkout
{


    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public WorkoutSets WorkoutSets { get; set; }

        [BindProperty]
        public List<Workout> Workouts { get; set; }


        public async Task OnGet(int id)
        {
            WorkoutSets = await _db.WorkoutSets.FindAsync(id);
            Workouts = await _db.Workout.Where(x => x.WorkoutSets.Id == WorkoutSets.Id).ToListAsync();
      
        }
       
        public async Task<IActionResult> OnPost()
        {
            // TODO ability to add workouts to workout sets
            // Create an API to handle this call

            if (ModelState.IsValid)
            {

                

                var workoutsFromDb = await _db.Workout.Where (x => x.WorkoutSets.Id == WorkoutSets.Id).ToListAsync();
                // ad
                var workoutSetFromDb =  await _db.WorkoutSets.FindAsync(WorkoutSets.Id);

                workoutSetFromDb.Name = WorkoutSets.Name;

                foreach (var workout in workoutsFromDb)
                {
                    // how do we get all model information from db?
                    //Console.WriteLine(Workouts.Count());
                    workout.Name = Workouts.Where(x => x.Id == workout.Id).First().Name;
                    workout.Reps = Workouts.Where(x => x.Id == workout.Id).First().Reps;
                    workout.Sets = Workouts.Where(x => x.Id == workout.Id).First().Sets;
                }

                //BookFromDb.Name = Book.Name;
                //BookFromDb.ISBN = Book.ISBN;
                //BookFromDb.Author = Book.Author;

                await _db.SaveChangesAsync();

                return Page();
            }
            return RedirectToPage();
        }

    }
}
