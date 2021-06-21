using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace beatCUE.UI
{
    class UICreator
    {
        public static beatCUEFlowCoordinator beatCUEFlowCoordinator;
        public static bool Created;

        public static void CreateMenu()
        {
            if (!Created)
            {
                MenuButton menuButton = new MenuButton("beatCUE", "Manage Corsair lighting integration", ShowFlow);
                MenuButtons.instance.RegisterButton(menuButton);
                Created = true;
            }
        }


        public static void ShowFlow()
        {
            if (beatCUEFlowCoordinator == null)
                beatCUEFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<beatCUEFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(beatCUEFlowCoordinator);
        }
    }
}
