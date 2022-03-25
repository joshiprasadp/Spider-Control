using System.ComponentModel.DataAnnotations;
using RoboticSpider.Web.Validators;

namespace RoboticSpider.Web.Models
{
    public class RobotMovesModel
    {
        [Required]
        [Display(Name = "Wall size")]
        [ValidWallSize]
        public string Grid { get; set; }

        [Required]
        [Display(Name = "Current position")]
        [ValidPosition]
        public string Position { get; set; }

        [Display(Name = "Move commands")]
        [Required]
        public string Moves { get; set; }
    }
}