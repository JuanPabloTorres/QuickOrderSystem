namespace QuickOrderApp.Utilities.Presenters.PresenterModel
{
    public class StoreCategory
    {

        public string Category { get; set; }
        public string CategoryIcon { get; set; }

        public StoreCategory(string category, string icon)
        {
            Category = category;
            CategoryIcon = icon;
        }


    }
}
