namespace BlogProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Contracts.Models;

    public class Tag : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}