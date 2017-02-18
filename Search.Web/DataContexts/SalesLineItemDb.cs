using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Search.Entities;

namespace Search.Web.DataContexts
{
    public class SalesLineItemDb : DbContext
    {
        public SalesLineItemDb() :base("DefaultConnection")
        {

        }
        public DbSet<SalesLineItem> SalesOrderItems { get; set; }
}

    
}