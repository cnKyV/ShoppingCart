using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Global.Utility.Settings
{
    public class FeatureFlags
    {
        public bool ShouldIncludeVasItemsForPromotionCalculation { get; set; }
        public bool ShouldVasProductBeAppliedToEachProductWithPluralQuantity { get; set; }
    }
}
