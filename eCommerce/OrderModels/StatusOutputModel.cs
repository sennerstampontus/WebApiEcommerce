namespace eCommerce.Models.ViewModels
{
    public class StatusOutputModel
    {
        public StatusOutputModel()
        {

        }
        public StatusOutputModel(int statusId = 1, string statusName = "processing")
        {
            StatusId = statusId;
            StatusName = statusName;
        }

        public int StatusId{ get; set; }
        public string StatusName{ get; set; }
    }
}
