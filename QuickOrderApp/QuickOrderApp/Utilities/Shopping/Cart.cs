using Library.Models;
using Library.Services.Interface;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Utilities.Shopping
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class Cart
    {
        private static double total;

        public static IOrderDataStore orderDataStore => DependencyService.Get<IOrderDataStore>();

        public static IOrderProductDataStore orderProductDataStore => DependencyService.Get<IOrderProductDataStore>();

        public static ObservableCollection<OrderProduct> OrderProducts { get; set; } = new ObservableCollection<OrderProduct>();

        public static double Total
        {
            get { return total; }
            set
            {
                total = value;
            }
        }

        public static async Task<bool> OrderManger (ProductPresenter product)
        {
            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();

                return false;
            }
            else
            {
                var ordersOfuser = orderDataStore.GetUserOrdersOfStore(App.LogUser.UserId, App.CurrentStore.StoreId);

                Order orderOfuser = ordersOfuser.Where(o => o.OrderStatus == Status.NotSubmited && o.IsDisisble == false).FirstOrDefault();

                //Crear por primera vez la orden
                if( orderOfuser == null )
                {
                    //Create a new order
                    Order order = new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        BuyerId = App.LogUser.UserId,
                        StoreId = App.CurrentStore.StoreId,
                        OrderType = Library.Models.Type.None,
                        OrderDate = DateTime.Now,
                        OrderStatus = Status.NotSubmited,
                    };

                    //Add order to DB
                    var orderadded = await orderDataStore.AddItemAsync(order);

                    //Create a new order product to the order that was created
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        OrderProductId = Guid.NewGuid(),
                        Price = product.ProductPrice,
                        ProductName = product.ProductName,
                        Quantity = (int) product.Quantity,
                        BuyerId = App.LogUser.UserId,
                        StoreId = App.CurrentStore.StoreId,
                        OrderId = order.OrderId,
                        ProductImage = product.ProductImg,
                        ProductIdReference = product.ProductId
                    };

                    //Check id orderproduct exist in the order
                    var _productoExisteEnOrden = orderProductDataStore.OrderProductOfUserExistInOrder(App.LogUser.UserId, orderProduct.ProductIdReference, order.OrderId);

                    if( !_productoExisteEnOrden )
                    {
                        //Add orderproduct to order
                        var orderProductAdded = await orderProductDataStore.AddItemAsync(orderProduct);

                        if( orderProductAdded )
                        {
                            Cart.OrderProducts.Add(orderProduct);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var orderProducttoUpdate = orderProductDataStore.OrderProductOfUserExistOnOrder(orderProduct.ProductIdReference, order.OrderId);

                        if( orderProducttoUpdate.Quantity != orderProduct.Quantity )
                        {
                            orderProducttoUpdate.Quantity = orderProduct.Quantity;

                            var uptedResult = await orderProductDataStore.UpdateItemAsync(orderProducttoUpdate);

                            return uptedResult;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        OrderProductId = Guid.NewGuid(),
                        Price = product.ProductPrice,
                        ProductName = product.ProductName,
                        Quantity = (int) product.Quantity,
                        BuyerId = App.LogUser.UserId,
                        StoreId = App.CurrentStore.StoreId,
                        OrderId = orderOfuser.OrderId,
                        ProductImage = product.ProductImg,
                        ProductIdReference = product.ProductId
                    };

                    var _productoExisteEnOrden = orderProductDataStore.OrderProductOfUserExistInOrder(App.LogUser.UserId, orderProduct.ProductIdReference, orderOfuser.OrderId);

                    if( !_productoExisteEnOrden )
                    {
                        var orderProductAdded = await orderProductDataStore.AddItemAsync(orderProduct);

                        if( orderProductAdded )
                        {
                            Cart.OrderProducts.Add(orderProduct);

                            Cart.Total += Cart.OrderProducts.Sum(o => o.Price * o.Quantity);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var orderProducttoUpdate = orderProductDataStore.OrderProductOfUserExistOnOrder(orderProduct.ProductIdReference, orderOfuser.OrderId);

                        if( orderProducttoUpdate.Quantity != orderProduct.Quantity )
                        {
                            if( orderOfuser.OrderStatus == Status.NotSubmited )
                            {
                                orderProducttoUpdate.Quantity = orderProduct.Quantity;

                                var updateResult = await orderProductDataStore.UpdateItemAsync(orderProducttoUpdate);

                                if( updateResult )
                                {
                                    MessagingCenter.Send<OrderProduct>(orderProducttoUpdate, "OrderProductUpdate");
                                }
                                return updateResult;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }
}