using Juce.Core.Repositories;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.ViewStack.Context;
using Juce.CoreUnity.ViewStack.Sequences;
using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.ViewStack
{
    public class UiViewStack : IUiViewStack
    {
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository = new SimpleKeyValueRepository<Type, IViewStackEntry>();
        private readonly ISingleRepository<IViewContext> currentContextRepository = new SimpleSingleRepository<IViewContext>();
        private readonly Queue<Type> viewStackQueue = new Queue<Type>();
        private readonly Sequencer sequencer = new Sequencer();

        private readonly IUiFrame frame;

        public UiViewStack(IUiFrame frame)
        {
            this.frame = frame;
        }

        public void Register(IViewStackEntry entry)
        {
            bool idAlreadyAdded = entriesRepository.TryGet(entry.Id, out _);

            if(idAlreadyAdded)
            {
                UnityEngine.Debug.LogError($"{nameof(IViewStackEntry)} with id {entry.Id} already registered");
                return;
            }

            entriesRepository.Add(entry.Id, entry);

            frame.Register(entry.Transform);
        }

        public void Unregister(IViewStackEntry entry)
        {
            entriesRepository.Remove(entry.Id);
        }

        public IViewStackSequenceBuilder New()
        {
            throw new NotImplementedException();
        }

        //public void Register<T>(T uiInteractor, UIView uiView) where T : IUIInteractor
        //{
        //    registeredInteractorsRepository.Add(uiView, uiInteractor);
        //    interactorsRepository.Add(typeof(T), uiInteractor);
        //    registeredViewsRepository.Add(uiView);

        //    UIFrame.Instance.Register(uiView);
        //}

        //public void Unregister(UIView uiView)
        //{
        //    registeredInteractorsRepository.Remove(uiView);
        //    registeredViewsRepository.Remove(uiView);

        //    if(uiView.IsPopup)
        //    {
        //        bool found = viewContexRepository.TryGetPopup(uiView, out ViewContex viewContext);

        //        if(found)
        //        {
        //            viewContext.PopupUIViews.Remove(uiView);
        //        }
        //    }
        //    else
        //    {
        //        bool found = viewContexRepository.TryGet(uiView, out ViewContex viewContext);

        //        if (found)
        //        {
        //            viewContexRepository.Remove(viewContext);
        //        }
        //    }
        //}

        //public ViewStackSequence New()
        //{
        //    return new ViewStackSequence(
        //        registeredViewsRepository, 
        //        viewContexRepository,
        //        viewQueueRepository,
        //        registeredInteractorsRepository,
        //        sequencer
        //        );
        //}
    }
}
