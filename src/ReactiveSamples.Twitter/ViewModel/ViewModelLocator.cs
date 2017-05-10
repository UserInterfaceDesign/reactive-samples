using ReactiveSamples.Twitter.Common;
using ReactiveSamples.Twitter.Services;

namespace ReactiveSamples.Twitter.ViewModel
{
    /// <summary>
    /// Simplified locator for viewmodels to enable view-first MVVM with dependency injection and a good design time experience.
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            // Services
            SimpleIoC.Register<IEmotionService, EmotionService>();

            // ViewModels
            SimpleIoC.Register<IShellViewModel, ShellViewModel>();
            SimpleIoC.Register<IMainViewModel, MainViewModel>();
            SimpleIoC.Register<IDetailsViewModel, DetailsViewModel>();
        }

        public IShellViewModel ShellViewModel
        {
            get { return SimpleIoC.Resolve<IShellViewModel>(); }
        }

        public IMainViewModel MainViewModel
        {
            get { return SimpleIoC.Resolve<IMainViewModel>(); }
        }

        public IDetailsViewModel DetailsViewModel
        {
            get { return SimpleIoC.Resolve<IDetailsViewModel>(); }
        }
    }
}
