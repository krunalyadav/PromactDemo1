using PromactDemo.Enum;

namespace PromactDemo.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cuisine Cuisine { get; set; }
    }
}
