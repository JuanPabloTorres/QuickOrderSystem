//using Library.Models;
//using QuickOrderApp.Utilities.Keys;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Essentials;
//using Xamarin.Forms;

//namespace QuickOrderApp.Manager
//{
//    public class AppManager
//    {

//        #region Fields

//        private User currentUser;
//        //private Store? currentStore;
//        private Guid currentStoreId;


//        #endregion Fields

//        #region Constructors

//        public AppManager()
//        {
//        }

//        #endregion Constructors

//        #region Properties

//        //public User CurrentUser
//        //{
//        //    get => currentUser;
//        //    set
//        //    {
//        //        if (currentUser != value)
//        //        {
//        //            currentUser = value;

//        //            if (currentUser is User
//        //                && currentUser.MyStores is object
//        //                && currentUser.HasStore())
//        //            {

//        //                IShellViewModel shellViewModel = Startup.ServiceProvider.GetService<IShellViewModel>();

//        //                if (CurrenstStoreGuid == Guid.Empty || CurrenstStoreGuid == null)
//        //                {
//        //                    Guid storeGuid;

//        //                    if (Guid.TryParse(
//        //                        Preferences.Get(nameof(CurrenstStoreGuid), string.Empty),
//        //                        out storeGuid) && storeGuid != Guid.Empty)
//        //                    {
//        //                        CurrenstStoreGuid = storeGuid;
//        //                    }
//        //                    else
//        //                    {
//        //                        CurrenstStoreGuid = currentUser.MyStores.FirstOrDefault().Id;
//        //                    }
//        //                }

//        //                shellViewModel.MyStores =
//        //                        new ObservableCollection<Store>(currentUser.MyStores);

//        //                if (shellViewModel.HeaderContent is null)
//        //                {
//        //                    shellViewModel.CreateHeader();
//        //                }
//        //            }
//        //        }
//        //    }
//        //}

//        //public Store? CurrentStore
//        //{
//        //    get => currentStore;
//        //    private set
//        //    {
//        //        if (currentStore != value)
//        //        {
//        //            currentStore = value;
//        //        }
//        //    }
//        //}

//        //public Guid CurrenstStoreGuid
//        //{
//        //    get => currentStoreId;
//        //    set
//        //    {
//        //        Store? existingStore;

//        //        if (CurrentUser is User
//        //            && CurrentUser.HasStore())
//        //        {
//        //            existingStore = CurrentUser.MyStores.FirstOrDefault(_ => _.Id == value);

//        //            if (existingStore is object)
//        //            {
//        //                CurrentStore = existingStore;
//        //                currentStoreId = value;
//        //                Preferences.Set(nameof(CurrenstStoreGuid), currentStoreId.ToString());
//        //            }
//        //            else
//        //            {
//        //                currentStoreId = Guid.Empty;
//        //                Preferences.Set(nameof(CurrenstStoreGuid), Guid.Empty.ToString());
//        //                existingStore = CurrentUser.MyStores.FirstOrDefault();
//        //                currentStoreId = existingStore.Id;
//        //            }
//        //        }
//        //    }
//        //}

//        #endregion Properties

//        #region Methods

//        /// <summary>
//        /// Checks if storage has credentials. If does, sets CurrentUserEmail Id for CurrentUser fetching
//        /// </summary>
//        /// <returns></returns>
//        public async Task<bool> HasStorageCredentials()
//        {
//            bool hasCred = false;
//            string email = string.Empty;
//            string pass = string.Empty;

//            try
//            {
//                email = await SecureStorage.GetAsync(LocalStorage.SecureStoreageEmailKey);
//                //pass = await SecureStorage.GetAsync(LocalStorage.SecureStoreagePassKey);

//                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pass)) { hasCred = true; }
//            }
//            catch (Exception)
//            {
//                // Possible that device doesn't support secure storage on device.
//            }

//            return hasCred;
//        }

//        /// <summary>
//        /// Sets data to SecureStorage
//        /// </summary>
//        /// <param name="email">User email/key</param>
//        /// <param name="pass">User password</param>
//        /// <param name="discriminator">User type</param>
//        /// <returns></returns>
//        public async Task SetCredentialsInStorage(string email = null, string pass = null)
//        {
//            if (string.IsNullOrEmpty(email))
//            {
//                await SecureStorage.SetAsync(LocalStorage.SecureStoreageEmailKey, email);
//            }
//            else if (string.IsNullOrEmpty(pass))
//            {
//                //await SecureStorage.SetAsync(LocalStorage.SecureStoreagePassKey, pass);
//            }
//        }

//        /// <summary>
//        /// Sets credentials in storage using user data
//        /// </summary>
//        /// <param name="user"></param>
//        /// <returns></returns>
//        public async Task SetCredentialsInStorage(User user)
//        {
//            if (user != null)
//            {
//                if (!string.IsNullOrEmpty(user.Email))
//                {
//                    await SecureStorage.SetAsync(LocalStorage.SecureStoreageEmailKey, user.Email);
//                }
//            }
//        }

//        /// <summary>
//        /// Sets CurrentUserData using Secure Storage
//        /// Calls FetchCurrentUser() to set CurrentUserData.CurrentUser and completes CurrentUserData setting
//        /// </summary>
//        /// <returns></returns>
//        public async Task SetCurrentUserDataFromSecureStorage()
//        {
//            IUserDataStore userDataStore = Startup.ServiceProvider.GetService<IUserDataStore>();
//            string email = string.Empty;
//            User user = null;

//            email = await SecureStorage.GetAsync(LocalStorage.SecureStoreageEmailKey);
//            user = await userDataStore.GetUserByEmail(email: email);

//            if (user != null)
//            {
//                CurrentUser = user;
//            }
//            else
//            {
//                CurrentUser = null;
//                SecureStorage.RemoveAll();
//            }
//        }

//        public bool IsLogged(bool logoutProcedure = false)
//        {
//            bool isLogged = false;

//            if (CurrentUser is object)
//            {
//                isLogged = true;
//            }
//            else if (logoutProcedure)
//            {
//                isLogged = false;
//                Task.Run(() => LogOff());
//            }

//            return isLogged;
//        }

//        public void LogOff()
//        {
//			Device.BeginInvokeOnMainThread(async () => {
//                await Shell.Current.GoToAsync(LoginPage.Route);
//            });
//            SecureStorage.RemoveAll();
//            Preferences.Clear();
//        }

//        /// <summary>
//        /// Fetches the User by the discriminator
//        /// </summary>
//        //private async Task<User?> FetchCurrentUser(string email)
//        //{
//        //    IUserDataStore userDataStore = Startup.ServiceProvider.GetService<IUserDataStore>();
//        //    User? user = await userDataStore.GetUserByEmail(email: email).ConfigureAwait(false);
//        //    return user;
//        //}

//        #endregion Methods
//    }
//}
