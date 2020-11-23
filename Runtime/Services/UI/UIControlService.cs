using Juce.CoreUnity.Service;
using Juce.CoreUnity.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Services
{
    public class UIControlService : IService
    {
        private readonly List<ViewModel> subViewModels = new List<ViewModel>();

        public ViewModel MainViewModel { get; private set; }
        public IReadOnlyList<ViewModel> SubViewModels => subViewModels;

        public void Init()
        {
        }

        public void CleanUp()
        {
        }

        public Task PushViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new System.ArgumentNullException($"Tried to push {nameof(ViewModel)} but it was null at {nameof(UIControlService)}");
            }

            MainViewModel = viewModel;

            return viewModel.Show();
        }

        public async Task PopViewModel(bool popAllSubViewModels = true)
        {
            if (MainViewModel == null)
            {
                throw new System.Exception($"Tried to pop current{nameof(ViewModel)} but it was null at {nameof(UIControlService)}");
            }

            ViewModel toHide = MainViewModel;
            MainViewModel = null;

            if (popAllSubViewModels)
            {
                await PopAllSubViewModels();
            }

            await toHide.Hide();
        }

        public void ClearViewModel(bool clearAllSubViewModels = true)
        {
            if (MainViewModel == null)
            {
                throw new System.Exception($"Tried to clear {nameof(ViewModel)} but it was null at {nameof(UIControlService)}");
            }

            MainViewModel = null;

            if (clearAllSubViewModels)
            {
                ClearAllSubViewModels();
            }
        }

        public async Task PushSubViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new System.ArgumentNullException($"Tried to push sub {nameof(ViewModel)} but it was null at {nameof(UIControlService)}");
            }

            bool alreadyContains = subViewModels.Contains(viewModel);

            if (alreadyContains)
            {
                throw new System.Exception($"Tried to push sub {nameof(ViewModel)} but it was already pushed {nameof(UIControlService)}");
            }

            subViewModels.Add(viewModel);

            await viewModel.Show();
        }

        public async Task PopSubViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new System.ArgumentNullException($"Tried to pop sub {nameof(ViewModel)} but it was null at {nameof(UIControlService)}");
            }

            bool removed = subViewModels.Remove(viewModel);

            if (!removed)
            {
                throw new System.Exception($"Tried to pop sub {nameof(ViewModel)} but it was not contained at {nameof(UIControlService)}");
            }

            await viewModel.Hide();
        }

        public void ClearSubViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new System.ArgumentNullException($"Tried to clear sub {nameof(ViewModel)} but it was null at {nameof(UIControlService)}");
            }

            bool removed = subViewModels.Remove(viewModel);

            if (!removed)
            {
                throw new System.Exception($"Tried to clear sub {nameof(ViewModel)} but it was not contained at {nameof(UIControlService)}");
            }
        }

        public async Task PopAllSubViewModels()
        {
            List<Task> toWait = new List<Task>();

            List<ViewModel> subViewModelsCopy = new List<ViewModel>(subViewModels);

            for (int i = 0; i < subViewModelsCopy.Count; ++i)
            {
                Task task = PopSubViewModel(subViewModelsCopy[i]);

                toWait.Add(task);
            }

            await Task.WhenAll(toWait.ToArray());
        }

        public void ClearAllSubViewModels()
        {
            List<ViewModel> subViewModelsCopy = new List<ViewModel>(subViewModels);

            for (int i = 0; i < subViewModelsCopy.Count; ++i)
            {
                ClearSubViewModel(subViewModelsCopy[i]);
            }
        }
    }
}