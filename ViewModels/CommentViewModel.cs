using System.Collections.Generic;
using DbBasicApp.Models;

namespace DbBasicApp.ViewModels
{
    public class CommentViewModel
    {
        public bool IsStaff { get; set; }

        public double AverageRating { get; set; }

        public IEnumerable<RatingRecord> ReceiveComments { get; set; }

        public IEnumerable<RatingRecord> Comments { get; set; }
    }
}