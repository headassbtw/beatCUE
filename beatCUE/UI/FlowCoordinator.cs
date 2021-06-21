using System;
using HMUI;
using beatCUE.UI.Controllers;
using BeatSaberMarkupLanguage;

namespace beatCUE.UI
{
    class beatCUEFlowCoordinator : FlowCoordinator
    {
        private static DeviceController _deviceController;
        public void Awake()
        {
            if (!_deviceController)
                _deviceController = BeatSaberUI.CreateViewController<DeviceController>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try
            {
                if (firstActivation)
                {
                    SetTitle("beatCUE settings");
                    showBackButton = true;
                    ProvideInitialViewControllers(_deviceController);
                }
            }
            catch (Exception e)
            {
                Plugin.Log.Error(e);
            }
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
