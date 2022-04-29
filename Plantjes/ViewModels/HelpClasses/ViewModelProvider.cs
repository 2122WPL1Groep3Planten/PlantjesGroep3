﻿using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels;

namespace Plantjes.ViewModels.HelpClasses
{  /*written by kenny*/
    /// <summary>
    /// Provider van viewmodels
    /// De views worden in de SimpleIoc IoC (Inversion of Control) container geregistreerd
    /// </summary>
    public class ViewModelProvider
    {
        public ViewModelProvider()
        {
            this.RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            //basisstructuur kenny, mede gebruikt door Robin
            // gebruik de default instantie (singleton van de SimpleIoc class)
            var iocc = SimpleIoc.Default;

            // haal singletons (elke keer dezelfde instantie) van de services om de viewmodels te voorzien van de nodige services(service locator)
            var loginService = iocc.GetInstance<ILoginUserService>();
            var searchService = iocc.GetInstance<ISearchService>();
            var detailService = iocc.GetInstance<IDetailService>();


            // registreer de viewmodels in de IoC Container
            // factory pattern om een instantie te maken van de viewmodels
            // Dependency Injection: constructor injection: injecteer  de services in the constructors van de viewmodels;

            iocc.Register<ViewModelLogin>(() => new ViewModelLogin(loginService));
            iocc.Register<ViewModelRegister>(() => new ViewModelRegister(loginService));
            iocc.Register<ViewModelAdd>(() => new ViewModelAdd(searchService));

            //SimpleIoc.Default.Unregister<ViewModelMain>();
            iocc.Register<ViewModelBase>(() => new ViewModelBase());
            iocc.Register<ViewModelMain>(() => new ViewModelMain(loginService, searchService));
            iocc.Register<ViewModelRepo>(() => new ViewModelRepo());
        }
    }
}