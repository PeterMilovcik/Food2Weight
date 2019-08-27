using System;
using Autofac;
using Food2Weight.Services.Dialog;
using Food2Weight.Services.Navigation;
using Food2Weight.Services.Preference;
using Food2Weight.Services.Repository;
using Food2Weight.ViewModels;

namespace Food2Weight.Bootstrap
{
    public class AppContainer
    {
        private static IContainer container;

        public AppContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<RepositoryService>().As<IRepositoryService>().SingleInstance();
            builder.RegisterType<PreferenceService>().As<IPreferenceService>().SingleInstance();

            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<AddWeightViewModel>().SingleInstance();
            builder.RegisterType<AddFoodViewModel>().SingleInstance();
            builder.RegisterType<FoodHistoryViewModel>().SingleInstance();
            builder.RegisterType<WeightHistoryViewModel>().SingleInstance();
            builder.RegisterType<SetGoalWeightViewModel>().SingleInstance();
            builder.RegisterType<EditWeightHistoryViewModel>().SingleInstance();
            builder.RegisterType<EditWeightRecordViewModel>().SingleInstance();
            builder.RegisterType<FoodListViewModel>().SingleInstance();

            container = builder.Build();
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);
    }

}