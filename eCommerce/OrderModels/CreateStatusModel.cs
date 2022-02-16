namespace eCommerce.OrderModels
{
    public class CreateStatusModel
    {
        public CreateStatusModel(string statusName)
        {
            StatusName = statusName;
        }

        public int Id { get; set; }
        public string StatusName { get; set; }
    }
}
