namespace Library.Helpers
{
    public enum Answer
    {
        None,
        Accept,
        Decline
    }

    public enum EmployeeType
    {
        New,
        OrderPreperarer,
        Delivery,
        PickUpRecepcionist,
        SubAdministrator
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum ProductType
    {
        Carnes,
        Bebidas,
        FrutasYVerduras,
        Limpieza,
        Herramientas,
        Escolar,
        Alimentos,
        Animales,
        Juguetes,
        Otro
    }

    public enum RequestType
    {
        JobRequest
    }

    public enum ResponseStatus
    {
        Error,
        Success,
        InvalidUser,
        DataEntryError,
        RequestFail,
        User_Found,
        Not_Found
    }

    public enum Status
    {
        InProccess,
        Pending,
        Completed,
        OnTheWay,
        NotSubmited,
        Submited,
    }

    public enum StoreType
    {
        None,
        Store,
        Grocery,
        BarberShop,
        Farming,
        Restaurant,
        Service,
        AutorParts,
        ClothingStore,
    }

    public enum Type
    {
        PickUp,
        Delivery,
        None
    }
    public enum UserType
    {
        RegularUser,
        EmployeeUser,
        StoreOwnerUser,
        PremiunUser
    }
}