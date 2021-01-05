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

    public class WorkoutSetsDisplay
    {
        public int WorkoutSetID { get; set; }
        public string WorkoutSetName { get; set; }
        public int Count { get; set; }

        public WorkoutSetsDisplay (int _Id, string _Name ,int _Count)
        {
            WorkoutSetID = _Id;
            WorkoutSetName = _Name;
            Count = _Count;
        }
    }


    public class IndexModel : PageModel
    {


        private readonly ApplicationDbContext _db;


       


        public IEnumerable<WorkoutSets> WorkoutSets { get; set; }
        public IEnumerable<Workout> Workouts { get; set; }
        public List<WorkoutSetsDisplay> WorkoutSetsDisplay { get; set; }



        public IndexModel (ApplicationDbContext db)
        {
            _db = db;

            if (!_db.Database.EnsureCreated())
            {

                Console.WriteLine("Failed to connect");

            }
        }



        // retrieve workout sets
        public async Task OnGet()
        {



            WorkoutSets = await _db.WorkoutSets.ToListAsync();
            Workouts = await _db.Workout.ToListAsync();
            WorkoutSetsDisplay = new List<WorkoutSetsDisplay>();
            //Workouts = await _db.Workout.ToListAsync();
            // we need to also get the page count for each
            // pull into a new data holder that can handle that?

            int count = 0;

            foreach (var wrk in WorkoutSets)
            {

                count = Workouts.Where(x => x.WorkoutSets.Id == wrk.Id).Count();

                if (count > 0)
                {
                    WorkoutSetsDisplay.Add(new WorkoutSetsDisplay(wrk.Id, wrk.Name, count));
                }
                else
                {
                    WorkoutSetsDisplay.Add(new WorkoutSetsDisplay(wrk.Id, wrk.Name,0));
                }

                
            }


        }

       

    }
}
