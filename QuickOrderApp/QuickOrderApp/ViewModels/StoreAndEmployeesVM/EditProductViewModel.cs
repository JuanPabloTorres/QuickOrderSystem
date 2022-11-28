using Library.Helpers;
using Library.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using QuickOrderApp.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("ProductId", "pId")]
    public class EditProductViewModel : BaseViewModel
    {
        private string description;

        private ObservableCollection<MediaFile> files = new ObservableCollection<MediaFile>();

        private byte[] ImgArray;

        private string productdescription;

        private string productId;

        private ImageSource productimg;

        private string productname;

        private string productPrice;

        private int productQuantity;

        private string selectedtype;

        public EditProductViewModel ()
        {
            var types = Enum.GetValues(typeof(ProductType));

            ProductTypes = new List<string>();

            foreach( var item in types )
            {
                ProductTypes.Add(item.ToString());
            }

            //Verificar bug al no seleccionar imagen por segunda vez
            IPickPhotoCommand = new Command(async () =>
            {
                try
                {
                    await CrossMedia.Current.Initialize();

                    files.Clear();
                    if( !CrossMedia.Current.IsPickPhotoSupported )
                    {
                        await Shell.Current.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");

                        return;
                    }

                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Full,
                    });

                    if( file == null )
                    {
                        ProductImg = ImageSource.FromFile("imgPlaceholder.jpg");

                        return;
                    }
                    else
                    {
                        ImgArray = ConvertToByteArray(file.GetStream());

                        ProductImg = ImageSource.FromStream(() => file.GetStream());
                    }

                    //files.Add(file);
                }
                catch( Exception e )
                {
                    Console.WriteLine(e);
                }
            });
            UpdateCommand = new Command(async () =>
            {
                List<string> values = new List<string>();

                values.Add(ProductName);

                values.Add(ProductDescription);

                values.Add(ProductPrice);

                values.Add(ProductQuantity.ToString());

                values.Add(SelectedType);

                if( GlobalValidator.CheckNullOrEmptyPropertiesOfListValues(values) )
                {
                    ProductType productType = (ProductType) Enum.Parse(typeof(ProductType), SelectedType);

                    var toupdate = new Product()
                    {
                        InventoryQuantity = ProductQuantity,
                        Price = Convert.ToDouble(ProductPrice),
                        ProductDescription = ProductDescription,
                        ID = ToEditProduct.ID,
                        ProductImage = ImgArray,
                        ProductName = ProductName,
                        StoreID = ToEditProduct.ID,
                        Type = productType
                    };

                    var itemUpdateResult = await productDataStore.UpdateItemAsync(toupdate);

                    if( itemUpdateResult )
                    {
                        await Shell.Current.DisplayAlert("Notification", "Item Update.", "OK");

                        MessagingCenter.Send<Product>(toupdate, "InventoryItemUpdated");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Some values are empty", "OK");
                }
            });
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;

                OnPropertyChanged();
            }
        }

        public ICommand IPickPhotoCommand { get; set; }

        public string ProductDescription
        {
            get { return productdescription; }
            set
            {
                productdescription = value;

                OnPropertyChanged();
            }
        }

        public string ProductId
        {
            get { return productId; }
            set
            {
                productId = value;

                OnPropertyChanged();

                GetItemToEdit(ProductId);
            }
        }

        public ImageSource ProductImg
        {
            get { return productimg; }
            set
            {
                productimg = value;

                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get { return productname; }
            set
            {
                productname = value;

                OnPropertyChanged();
            }
        }

        public string ProductPrice
        {
            get { return productPrice; }
            set
            {
                productPrice = value;

                OnPropertyChanged();
            }
        }

        public int ProductQuantity
        {
            get { return productQuantity; }
            set
            {
                productQuantity = value;

                OnPropertyChanged();
            }
        }

        public List<string> ProductTypes { get; set; }

        public string SelectedType
        {
            get { return selectedtype; }
            set
            {
                selectedtype = value;

                OnPropertyChanged();
            }
        }

        public Product ToEditProduct { get; set; }

        public ICommand UpdateCommand { get; set; }

        private byte[] ConvertToByteArray (Stream value)
        {
            byte[] imageArray = null;

            using( MemoryStream memory = new MemoryStream() )
            {
                Stream stream = value;

                stream.CopyTo(memory);

                imageArray = memory.ToArray();
            }

            return imageArray;
        }

        private async Task GetItemToEdit (string id)
        {
            ToEditProduct = await productDataStore.GetItemAsync(id);

            ProductName = ToEditProduct.ProductName;

            ProductDescription = ToEditProduct.ProductDescription;

            ProductPrice = ToEditProduct.Price.ToString();

            ProductQuantity = ToEditProduct.InventoryQuantity;

            SelectedType = ToEditProduct.Type.ToString();

            ImgArray = ToEditProduct.ProductImage;

            var stream = new MemoryStream(ToEditProduct.ProductImage);

            ProductImg = ImageSource.FromStream(() => stream);
        }
    }
}