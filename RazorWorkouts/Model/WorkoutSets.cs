using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorWorkouts.Model
{
    public class WorkoutSets
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        // maybe include userid

        // frequency

        public ICollection<Workout> Workout { get; set; }

    }
}
