using Juce.Core.Service;
using Juce.Core.UI;
using Juce.Utils.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juce.Core.Services
{
    public class UIControlService : IService
    {
        private ViewModel viewModel;
        private readonly List<ViewModel> subViewModels = new List<ViewModel>();

        public ViewModel ViewModel => viewModel;
        public IReadOnlyList<ViewModel> SubViewModels => subViewModels;

        public void Init()
        {
        }

        public void CleanUp()
        {
        }

        public async Task PushViewModel(ViewModel viewModel)
        {
            Contract.IsNull(this.viewModel);
            Contract.IsNotNull(viewModel);

            this.viewModel = viewModel;

            await viewModel.Show();
        }

        public async Task PopViewModel(bool popAllSubViewModels = true)
        {
            Contract.IsNotNull(viewModel);

            ViewModel toHide = viewModel;
            viewModel = null;

            if (popAllSubViewModels)
            {
                await PopAllSubViewModels();
            }

            await toHide.Hide();
        }

        public void ClearViewModel(bool clearAllSubViewModels = true)
        {
            Contract.IsNotNull(viewModel);

            viewModel = null;

            if (clearAllSubViewModels)
            {
                ClearAllSubViewModels();
            }
        }

        public async Task PushSubViewModel(ViewModel viewModel)
        {
            Contract.IsNotNull(viewModel);

            bool alreadyContains = subViewModels.Contains(viewModel);

            Contract.IsFalse(alreadyContains, "SubViewModel was already added");

            subViewModels.Add(viewModel);

            await viewModel.Show();
        }

        public async Task PopSubViewModel(ViewModel viewModel)
        {
            Contract.IsNotNull(viewModel);

            bool removed = subViewModels.Remove(viewModel);

            Contract.IsTrue(removed, "Tried to remove a SubViewModel but it was not added");

            await viewModel.Hide();
        }

        public void ClearSubViewModel(ViewModel viewModel)
        {
            Contract.IsNotNull(viewModel);

            bool removed = subViewModels.Remove(viewModel);

            Contract.IsTrue(removed, "Tried to remove a SubViewModel but it was not added");
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