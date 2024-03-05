using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class TrainingDTO
    {
        public int TrainingID { get; set; }

        public string Comments { get; set; }

        public DateTime DateTraining { get; set; }


        public string Objective { get; set; }

        public string Issues { get; set; }

        public int ChangeID { get; set; }
        public int UserID { get; set; }
        public int TypeID { get; set; }
    }
}
