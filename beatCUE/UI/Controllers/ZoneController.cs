using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace beatCUE.UI.Controllers
{
    [ViewDefinition("beatCUE.UI.Views.zoneControls.bsml")]
    [HotReload("./../Views/zoneControls.bsml")]
    class ZoneController : BSMLAutomaticViewController
    {

    }
}
