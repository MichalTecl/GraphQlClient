using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;

namespace MTecl.GraphQlClient.IntegrationTests
{
    public class QueryMappingWithDynamicArgumentsTests
    {
        [Fact]
        public void ArgumentPassedAsNameValue()
        {
            var query = QueryMapper.MapQuery<OrdersQuery, IEnumerable<Order>>(o => o.Orders.Argument("orderNr", "1234").Argument("country", "cz")
                                
                .With(o => o.Customer.Argument("e-mail", "*@gmail.com"), o => o.Products)
                );


            var ordersNode = query.FindChild("Orders");
            ordersNode.Should().NotBeNull();

            ordersNode.Arguments.Should().HaveCount(2);
            ordersNode.Arguments[0].Key.Should().Be("orderNr");
            ordersNode.Arguments[0].Value.ToString().Should().Be("1234");

            ordersNode.Arguments[1].Key.Should().Be("country");
            ordersNode.Arguments[1].Value.ToString().Should().Be("cz");

            var customerNode = query.FindChild("Customer");
            customerNode.Should().NotBeNull();

            customerNode.Arguments.Should().HaveCount(1);
            customerNode.Arguments[0].Key.Should().Be("e-mail");
            customerNode.Arguments[0].Value.Should().Be("*@gmail.com");

            var rendered = query.ToString();

            rendered.Should().Contain("orders(orderNr: \"1234\", country: \"cz\")");
            rendered.Should().Contain("customer(e-mail: \"*@gmail.com\")");
        }

        [Fact]
        public void ArgumentPassedAsVariablePass()
        {
            var query = QueryMapper.MapQuery<OrdersQuery, IEnumerable<Order>>(o => o.Orders.Argument("orderNr", QueryVariable.Pass<string>("$v1")).With(o => o.Customer, o => o.Products));

            var ordersNode = query.FindChild("Orders");
            ordersNode.Should().NotBeNull();

            ordersNode.Arguments.Should().HaveCount(1);
            ordersNode.Arguments[0].Key.Should().Be("orderNr");
            ordersNode.Arguments[0].Value.ToString().Should().Be("$v1");

            var rendered = query.ToString();
            rendered.Should().Contain("orders(orderNr: $v1)");
        }

        [Fact]
        public void ArgumentsPassedAsObject()
        {
            var query = QueryMapper.MapQuery<OrdersQuery, IEnumerable<Order>>(o => o.Orders.Argument(new { orderNr = "1234", country = "cz"})

                .With(o => o.Customer.Argument(new { email = QueryVariable.GetVariableNameRenderer("$v1") }), o => o.Products)
                );


            var ordersNode = query.FindChild("Orders");
            ordersNode.Should().NotBeNull();

            ordersNode.Arguments.Should().HaveCount(2);
            ordersNode.Arguments[0].Key.Should().Be("orderNr");
            ordersNode.Arguments[0].Value.ToString().Should().Be("1234");

            ordersNode.Arguments[1].Key.Should().Be("country");
            ordersNode.Arguments[1].Value.ToString().Should().Be("cz");

            var customerNode = query.FindChild("Customer");
            customerNode.Should().NotBeNull();

            customerNode.Arguments.Should().HaveCount(1);
            customerNode.Arguments[0].Key.Should().Be("email");

            var rendered = query.ToString();

            rendered.Should().Contain("orders(orderNr: \"1234\", country: \"cz\")");
            rendered.Should().Contain("customer(email: $v1)");
        }

        [Fact]
        public void ArgumentsPassedAsDictionary()
        {
            var query = QueryMapper.MapQuery<OrdersQuery, IEnumerable<Order>>(o => o.Orders.Argument(new Dictionary<string, object> { { "orderNr", "1234" }, { "country", "cz" } })

                .With(o => o.Customer.Argument(new Dictionary<string, object> { { "email", QueryVariable.GetVariableNameRenderer("$v1") } }), o => o.Products)
                );


            var ordersNode = query.FindChild("Orders");
            ordersNode.Should().NotBeNull();

            ordersNode.Arguments.Should().HaveCount(2);
            ordersNode.Arguments[0].Key.Should().Be("orderNr");
            ordersNode.Arguments[0].Value.ToString().Should().Be("1234");

            ordersNode.Arguments[1].Key.Should().Be("country");
            ordersNode.Arguments[1].Value.ToString().Should().Be("cz");

            var customerNode = query.FindChild("Customer");
            customerNode.Should().NotBeNull();

            customerNode.Arguments.Should().HaveCount(1);
            customerNode.Arguments[0].Key.Should().Be("email");

            var rendered = query.ToString();

            rendered.Should().Contain("orders(orderNr: \"1234\", country: \"cz\")");
            rendered.Should().Contain("customer(email: $v1)");
        }

        class OrdersQuery
        {
            public IEnumerable<Order> Orders { get; set; }
        }

        class Customer
        {
            public string Name { get; set; }
            public string EMail { get; set; }
        }

        class Product
        {
            string Name { get; set; }
        }

        class Order
        {
            public Customer Customer { get; set; }
            public string OrderNumber { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}

