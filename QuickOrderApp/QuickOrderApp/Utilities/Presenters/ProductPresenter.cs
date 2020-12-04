using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Shopping;
using QuickOrderApp.ViewModels;
using QuickOrderApp.Views.Store.StoreManger;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class ProductPresenter : BaseViewModel
    {

        private Guid productid;

        public Guid ProductId
        {
            get { return productid; }
            set
            {
                productid = value;
                OnPropertyChanged();
            }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged();
            }
        }

        private double productprice;

        public double ProductPrice
        {
            get { return productprice; }
            set
            {
                productprice = value;
                OnPropertyChanged();
            }
        }

        private byte[] img;

        public byte[] ProductImg
        {
            get { return img; }
            set
            {
                img = value;
                OnPropertyChanged();
            }
        }

        private double quantity;

        public double Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        private int itemleft;

        public int ItemLeft
        {
            get { return itemleft; }
            set
            {
                itemleft = value;
                OnPropertyChanged();
            }
        }

        private string productDescription;

        public string ProductDescription
        {
            get { return productDescription; }
            set
            {
                productDescription = value;
                OnPropertyChanged();
            }
        }

        private string producttype;

        public string ProductType
        {
            get { return producttype; }
            set
            {
                producttype = value;
                OnPropertyChanged();
            }
        }

        private bool areVisible;

        public bool AreVisible
        {
            get { return areVisible; }
            set
            {
                areVisible = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddToCartCommand { get; set; }
        public ICommand RemoveFromCartCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ProductPresenter(Product product)
        {
            ProductName = product.ProductName;
            ProductImg = product.ProductImage;
            ProductId = product.ProductId;
            ProductPrice = product.Price;
            ItemLeft = product.InventoryQuantity;
            ProductType = product.Type.ToString();
            ProductDescription = product.ProductDescription;

            Quantity = 0;

            AddToCartCommand = new Command(async () =>
            {

                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                    if (ItemLeft > 0)
                    {

                        if (Quantity > 0)
                        {

                            int left = ItemLeft - (int)Quantity;

                            if (Quantity <= ItemLeft)
                            {

                                Cart cartmanager = new Cart();

                                await cartmanager.OrderManger(this);
                                //await Cart.OrderManger(this);
                            }
                            else
                            {
                                await Shell.Current.DisplayAlert("Notification", "Item dont have to many in stock selecte a less quantity.", "OK");
                            }



                            #region OrderManger
                            //               var userhaveorder =  orderDataStore.HaveOrder(App.LogUser.UserId, App.CurrentStore.StoreId);

                            //if (userhaveorder == null)
                            //{
                            //	Order order = new Order()
                            //	{
                            //		OrderId = Guid.NewGuid(),
                            //		BuyerId = App.LogUser.UserId,
                            //		StoreId = App.CurrentStore.StoreId,
                            //		OrderType = Library.Models.Type.None,
                            //		OrderDate = DateTime.Today,
                            //		OrderStatus = Status.NotSubmited,


                            //	};

                            //	var orderadded = await orderDataStore.AddItemAsync(order);

                            //	OrderProduct orderProduct = new OrderProduct()
                            //	{
                            //		OrderProductId = Guid.NewGuid(),
                            //		Price = ProductPrice,
                            //		ProductName = ProductName,
                            //		Quantity = (int)Quantity,
                            //		BuyerId = App.LogUser.UserId,
                            //		StoreId = App.CurrentStore.StoreId,
                            //		OrderId = order.OrderId,
                            //		ProductImage = ProductImg
                            //	};

                            //	var result = orderProductDataStore.OrderProductOfUserExist(App.LogUser.UserId, orderProduct.ProductName);
                            //	if (result == false)
                            //	{

                            //		var orderProductAdded = await orderProductDataStore.AddItemAsync(orderProduct);

                            //		if (orderProductAdded)
                            //		{
                            //			Cart.OrderProducts.Add(orderProduct);
                            //		}
                            //	}
                            //	else
                            //	{

                            //		var toupdate = orderProductDataStore.OrderProductOfUserExistWith(ProductName);

                            //		if (toupdate.Quantity != orderProduct.Quantity)
                            //		{

                            //			toupdate.Quantity = orderProduct.Quantity;
                            //			var uptedResult = await orderProductDataStore.UpdateItemAsync(toupdate);
                            //		}
                            //	}

                            //}
                            //else
                            //{

                            //	OrderProduct orderProduct = new OrderProduct()
                            //	{
                            //		OrderProductId = Guid.NewGuid(),
                            //		Price = ProductPrice,
                            //		ProductName = ProductName,
                            //		Quantity = (int)Quantity,
                            //		BuyerId = App.LogUser.UserId,
                            //		StoreId = App.CurrentStore.StoreId,
                            //		 OrderId = userhaveorder.OrderId,
                            //		ProductImage = ProductImg
                            //	};

                            //	var result = orderProductDataStore.OrderProductOfUserExist(App.LogUser.UserId, orderProduct.ProductName);
                            //	if (result == false)
                            //	{

                            //		var orderProductAdded = await orderProductDataStore.AddItemAsync(orderProduct);

                            //		if (orderProductAdded)
                            //		{
                            //			Cart.OrderProducts.Add(orderProduct);
                            //			Cart.Total += Cart.OrderProducts.Sum(o => o.Price * o.Quantity);
                            //		}
                            //	}
                            //	else
                            //	{

                            //		var toupdate = orderProductDataStore.OrderProductOfUserExistWith(ProductName);

                            //		if (toupdate.Quantity != orderProduct.Quantity)
                            //		{

                            //			toupdate.Quantity = orderProduct.Quantity;
                            //			var uptedResult = await orderProductDataStore.UpdateItemAsync(toupdate);
                            //		}
                            //	}
                            //}
                            #endregion


                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Notification", "Quantiy selected is 0.", "OK");
                        }

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Notification", "There are no products.", "OK");
                    }
                }




            });



            DeleteCommand = new Command(async () =>
            {


                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                    var answer = await Shell.Current.DisplayAlert("Notification", "Are you sure that you want to delete this item?", "Yes", "No");

                    if (answer)
                    {
                        var result = await productDataStore.DeleteItemAsync(this.ProductId.ToString());

                        if (result)
                        {
                            MessagingCenter.Send<ProductPresenter>(this, "DeleteProductInventory");
                        }

                    }
                }




            });

            EditCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                    await Shell.Current.GoToAsync($"{EditProductPage.Route}?pId={ProductId.ToString()}", animate: true);
                }


            });

        }

        public ProductPresenter(OrderProduct product)
        {
            ProductName = product.ProductName;
            ProductImg = product.ProductImage;
            ProductId = product.OrderProductId;
            ProductPrice = product.Price;
            Quantity = product.Quantity;

            //ProductDescription = product.ProductDescription;


            RemoveFromCartCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if (tokenExpManger.IsExpired())
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {

                    var orderProductRemovedResult = await orderProductDataStore.DeleteItemAsync(ProductId.ToString());

                    if (orderProductRemovedResult)
                    {
                        await Shell.Current.DisplayAlert("Notification", "Product Removed succefully.", "OK");

                        MessagingCenter.Send<ProductPresenter>(this, "RemoveOrderProduct");
                    }

                }

            });

        }




    }
}
