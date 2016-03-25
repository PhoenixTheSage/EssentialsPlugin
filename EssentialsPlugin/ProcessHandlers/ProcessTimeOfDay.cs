using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssentialsPlugin.ProcessHandlers
{
    class ProcessTimeOfDay : ProcessHandlerBase
    {
        public override int GetUpdateResolution()
        {
            return 1000;
        }

        public override void Handle()
        {
            if (!PluginSettings.Instance.TimedCommandsEnabled)
                return;
        }
}
