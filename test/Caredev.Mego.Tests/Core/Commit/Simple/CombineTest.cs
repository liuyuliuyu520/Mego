﻿using System.Linq;
using Caredev.Mego.Tests.Models.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Transactions;
using System;

namespace Caredev.Mego.Tests.Core.Commit.Simple
{
    [TestClass, TestCategory(Constants.TestCategoryRootName + ".Commit.Combine")]
    public class CombineTest : ISimpleTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Utility.CommitTest(CreateContext(), db =>
            {
                var customers = Enumerable.Range(1000, 5000).Select(i => new Customer()
                {
                    Id = i,
                    Name = "Customer " + i.ToString(),
                    Code = "C" + i.ToString(),
                    Address1 = "A",
                    Address2 = "B",
                    Zip = "Z"
                });
                Stopwatch w = Stopwatch.StartNew();
                //var all = db.Customers.ToList();
                db.Customers.AddRange(customers);
                db.Executor.Execute();
                w.Stop();
                var m = w.ElapsedMilliseconds;
                var t = w.ElapsedTicks;

                var list = db.Customers.ToList();
            });
        }

        [TestMethod]
        public void TestMethod2()
        {
            Utility.CommitTest(CreateContext(), db =>
            {
                var query = from a in db.Customers
                            where a.Id >= 10 && a.Id < 200
                            select a;
                db.Database.Connection.Open();
                var list = query.ToList();
                var obj = query.First();

                var aaa = query.First();
                var query2 = from b in db.Customers
                             where b.Id >= 200
                             select b;
                
                var str = query.ToString();
                var customer = new Customer()
                {
                    Id = 10000,
                    Name = "Customer 10000",
                    Code = "C10000",
                    Address1 = "A",
                    Address2 = "B",
                    Zip = "Z"
                };
                var order = new Order() { Id = 10000, ModifyDate = DateTime.Now };
                var produts = db.Products.Take(5).ToArray();
                db.Customers.Add(customer);
                db.Orders.Add(order);
                var operate = db.Orders.AddRelation(order, o => o.Customer, customer);
                for (int i = 0; i < 5; i++)
                {
                    var detail = new OrderDetail()
                    {
                        Quantity = 2,
                        Price = 12,
                    };
                    db.OrderDetails.Add(detail);
                    db.OrderDetails.AddRelation(detail, d => d.Product, produts[i]);
                    db.Orders.AddRelation(order, o => o.Details, detail);
                }
                var special = db.OrderDetails.Where(a => a.Id == 11).First();
                db.OrderDetails.AddRelation(special, d => d.Product, produts[0]);

                Stopwatch w = Stopwatch.StartNew();
                db.Executor.Execute();
                w.Stop();
                var m = w.ElapsedMilliseconds;
                var t = w.ElapsedTicks;
                var query1 = from a in db.Orders.Include(a => a.Details.Include(b => b.Product)).Include(a => a.Customer)
                             where a.Id > 9999
                             select a;
                var temp = query1.ToArray();
                temp.ToString();
            });
        }

        public OrderManageEntities CreateContext()
        {
            return new OrderManageEntities(Constants.ConnectionNameSimple);
        }
    }
}
