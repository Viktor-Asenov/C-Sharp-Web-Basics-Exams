namespace SMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.05", "1000")]
        public decimal Price { get; set; }

        public string CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
