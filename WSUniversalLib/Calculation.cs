using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WSUniversalLib
{
    public class Calculation
    {
        public int GetQuantityForProduct(int productType, int materialType, int count, float width, float height)
        {
            int quantity = 0;



            return quantity;
        }

        public int GetDiscountForAgent( PleasantRustleEntities db, Agent agent )
        {
            decimal amountRealization = 0;
            foreach(var item in db.Sales.Where(x=>x.agentId == agent.id))
            {
                amountRealization += (decimal) item.count * Convert.ToDecimal(db.Product.FirstOrDefault(x => x.id == item.productId).minPriceForAgent);
            }
            if (amountRealization < 10000) { return 0; }
            if (amountRealization >= 10000 && amountRealization < 50000) { return 5; }
            if (amountRealization >= 50000 && amountRealization < 150000) { return 10; }
            if (amountRealization >= 150000 && amountRealization < 500000) { return 20; }
            return 25;

        }
    }
}
