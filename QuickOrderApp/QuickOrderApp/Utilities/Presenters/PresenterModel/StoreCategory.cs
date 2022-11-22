namespace QuickOrderApp.Utilities.Presenters.PresenterModel
{
    public class StoreCategory
    {
        public StoreCategory (string category, string icon)
        {
            Category = category;

            CategoryIcon = icon;
        }

        public string Category { get; set; }

        public string CategoryIcon { get; set; }
    }
}