using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace MT2.Control
{
    class VersionHelper
    {
        public static Boolean Windows10Build10240 => ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 1, 0);
        public static Boolean Windows10Build10586 => ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 2, 0);
        public static Boolean Windows10Build14393 => ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3, 0);
        public static Boolean Windows10Build15063 => ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4, 0);
    }
}
