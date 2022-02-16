namespace eCommerce.Models.CategoryModels
{
    public class CreateCategoryModel
    {
        private string name;

        public string Name { get { return name; } set { name = value.Trim(); } }
    }
}
