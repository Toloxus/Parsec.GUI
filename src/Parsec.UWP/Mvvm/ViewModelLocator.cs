using Parsec.Core.Main;
using System;
using System.Globalization;
using System.Reflection;
using Windows.UI.Xaml;

namespace Parsec.UWP.Mvvm
{
    /// <summary>
    /// Idea borrowed from: https://www.andrewhoefling.com/Blog/Post/view-model-locator-for-mvvm-applications-in-uno-platform
    /// View model locator automatically sets DataContext of Page to the right ViewModel.
    /// E.g. MainPage => MainViewModel
    ///      AboutPage => AboutViewModel
    /// </summary>
    public static class ViewModelLocator
    {
        public static DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool),
        typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        public static bool GetAutoWireViewModel(UIElement element)
        {
            return (bool)element.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(UIElement element, bool value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                Bind(d);
        }

        private static void Bind(DependencyObject view)
        {
            if (view is FrameworkElement frameworkElement)
            {
                var viewType = frameworkElement.GetType();
                var viewModelType = FindViewModel(viewType);
                var vm = (Application.Current as App).Services.GetService(viewModelType);
                if (vm is null)
                    throw new Exception($"No view model configurated for {viewType}");

                frameworkElement.DataContext = vm;
            }
        }

        private static Type FindViewModel(Type viewType)
        {
            string viewName = string.Empty;

            if (viewType.FullName.EndsWith("Page"))
            {
                viewName = viewType.FullName
                    .Replace("Page", string.Empty)
                    .Replace("UWP", "Core");
            }

            var viewAssemblyName = Assembly.GetAssembly(typeof(MainViewModel)).FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            return Type.GetType(viewModelName);
        }
    }
}
