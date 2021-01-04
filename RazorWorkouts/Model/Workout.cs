using System;
using System.ComponentModel.DataAnnotations;

namespace RazorWorkouts.Model
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public WorkoutSets WorkoutSets { get; set; }
        
    }
}
